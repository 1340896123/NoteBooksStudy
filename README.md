# NoteBooksStudy
自己学习的


# 读取VMWare,读写内存

Vmm:VMMSharp包
```CSharp
   string vmPID = GetprocessID("vmware-vmx").ToString();
   Vmm vmm = new Vmm("-device", $"vmware://ro=1,id={vmPID}");
   PID = vmm.Process(ProcessName);

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
```
