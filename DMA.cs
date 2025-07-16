using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vmmsharp;

namespace TestMain
{
     class DMA
    {
        public static VmmProcess PID;

        /// <summary>
        /// 初始化DMA
        /// </summary>
        /// <param name="ProcessName"></param>
        public static void DMAInit(string ProcessName)
        {
            try
            {
                string vmPID = GetprocessID("vmware-vmx").ToString();
                Vmm vmm = new Vmm("-device", $"vmware://ro=1,id={vmPID}");
                PID = vmm.Process(ProcessName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"初始化失败!\n{ex.Message}");
            }
        }

        /// <summary>
        /// 读字节集
        /// </summary>
        /// <param name="MemoryAddress"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static byte[] ReadBytes(Int64 MemoryAddress, int size)
        {
            return PID.MemRead((UInt64)MemoryAddress, (uint)size);
        }

        /// <summary>
        /// 写字节集
        /// </summary>
        /// <param name="MemoryAddress"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool WriteBytes(Int64 MemoryAddress, byte[] code)
        {
            return PID.MemWrite((UInt64)MemoryAddress, code);
        }

        /// <summary>
        /// 读整数
        /// </summary>
        /// <param name="MemoryAddress"></param>
        /// <returns></returns>
        public static int ReadInt(Int64 MemoryAddress)
        {
            return BitConverter.ToInt32(ReadBytes(MemoryAddress, 4), 0);
        }

        /// <summary>
        /// 写整数
        /// </summary>
        /// <param name="MemoryAddress"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool WriteInt(Int64 MemoryAddress, int value)
        {
            return WriteBytes(MemoryAddress, BitConverter.GetBytes(value));
        }

        /// <summary>
        /// 读短整数
        /// </summary>
        /// <param name="MemoryAddress"></param>
        /// <returns></returns>
        public static Int16 ReadInt16(Int64 MemoryAddress)
        {
            return BitConverter.ToInt16(ReadBytes(MemoryAddress, 2), 0);
        }

        /// <summary>
        /// 写短整数
        /// </summary>
        /// <param name="MemoryAddress"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool WriteInt16(Int64 MemoryAddress, Int16 value)
        {
            return WriteBytes(MemoryAddress, BitConverter.GetBytes(value));
        }

        /// <summary>
        /// 读长整数
        /// </summary>
        /// <param name="MemoryAddress"></param>
        /// <returns></returns>
        public static Int64 ReadInt64(Int64 MemoryAddress)
        {
            return BitConverter.ToInt64(ReadBytes(MemoryAddress, 8), 0);
        }

        /// <summary>
        /// 写长整数
        /// </summary>
        /// <param name="MemoryAddress"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool WriteInt64(Int64 MemoryAddress, Int64 value)
        {
            return WriteBytes(MemoryAddress, BitConverter.GetBytes(value));
        }

        /// <summary>
        /// 读字节
        /// </summary>
        /// <param name="MemoryAddress"></param>
        /// <returns></returns>
        public static byte ReadFByte(Int64 MemoryAddress)
        {
            return ReadBytes(MemoryAddress, 1)[0];
        }

        /// <summary>
        /// 写字节
        /// </summary>
        /// <param name="MemoryAddress"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool WriteFByte(Int64 MemoryAddress, byte value)
        {
            return WriteBytes(MemoryAddress, new byte[] { value });
        }

        /// <summary>
        /// 读小数Float
        /// </summary>
        /// <param name="MemoryAddress"></param>
        /// <returns></returns>
        public static float ReadFloat(Int64 MemoryAddress)
        {
            return BitConverter.ToSingle(ReadBytes(MemoryAddress, 4), 0);
        }

        /// <summary>
        /// 写小数Float
        /// </summary>
        /// <param name="MemoryAddress"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool WriteFloat(Int64 MemoryAddress, float value)
        {
            return WriteBytes(MemoryAddress, BitConverter.GetBytes(value));
        }

        /// <summary>
        /// 读双浮点
        /// </summary>
        /// <param name="MemoryAddress"></param>
        /// <returns></returns>
        public static double ReadDouble(Int64 MemoryAddress)
        {
            return BitConverter.ToDouble(ReadBytes(MemoryAddress, 8), 0);
        }

        /// <summary>
        /// 写双精度浮点
        /// </summary>
        /// <param name="MemoryAddress"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool WriteDouble(Int64 MemoryAddress, double value)
        {
            return WriteBytes(MemoryAddress, BitConverter.GetBytes(value));
        }

        /// <summary>
        /// 查找指定进程名PID
        /// </summary>
        /// <param name="processName">进程名</param>
        /// <returns>返回最新启动的同名进程</returns>
        public static int GetprocessID(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0)
            {
                var sortedProcesses = processes.OrderByDescending(p => p.StartTime).ToArray();
                return sortedProcesses.First().Id;
            }
            return -1;
        }

        /// <summary>
        /// Unicode转ANSI
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string UnicodeToAnsi(byte[] arr)
        {
            return UnicodeEncoding.Unicode.GetString(arr).Split('\0')[0];
        }

        /// <summary>
        /// Unicode转ANSI
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string UniCodeToAnsi(byte[] data)
        {
            if (data == null || data.Length < 1)
                return string.Empty;

            string AnsiStringText = UnicodeEncoding.Unicode.GetString(data);

            return AnsiStringText.Split(new string[] { "\0" }, StringSplitOptions.None)[0];
        }

        /// <summary>
        /// 添加字节集
        /// </summary>
        /// <param name="code"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static byte[] AddBytes(byte[] code, params byte[][] array)
        {
            List<byte> result = code.ToList();

            for (int i = 0; i < array.Length; i++)
            {
                byte[] data = array[i];

                for (int j = 0; j < data.Length; j++)
                {
                    result.Add(data[j]);
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// 添加字节集
        /// </summary>
        /// <param name="code"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static long[] AddBytes(long[] code, params long[][] array)
        {
            List<long> result = code.ToList();

            for (int i = 0; i < array.Length; i++)
            {
                long[] data = array[i];

                for (int j = 0; j < data.Length; j++)
                {
                    result.Add(data[j]);
                }
            }
            return result.ToArray();
        }
    }
}
