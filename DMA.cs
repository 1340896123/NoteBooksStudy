#region 程序集 vmmsharp, Version=5.14.3.193, Culture=neutral, PublicKeyToken=null
// E:\Desktop\秀牛C#虚拟机穿透读写基础框架\TestMain\packages\Vmmsharp.5.14.3\lib\net48\vmmsharp.dll
// Decompiled with ICSharpCode.Decompiler 8.2.0.7535
#endregion

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Vmmsharp.Internal;

namespace Vmmsharp;

public class VmmProcess
{
    public struct ProcessInfo
    {
        public bool fValid;

        public uint tpMemoryModel;

        public uint tpSystem;

        public bool fUserOnly;

        public uint dwPID;

        public uint dwPPID;

        public uint dwState;

        public string sName;

        public string sNameLong;

        public ulong paDTB;

        public ulong paDTB_UserOpt;

        public ulong vaEPROCESS;

        public ulong vaPEB;

        public bool fWow64;

        public uint vaPEB32;

        public uint dwSessionId;

        public ulong qwLUID;

        public string sSID;

        public uint IntegrityLevel;
    }

    public struct PteEntry
    {
        public ulong vaBase;

        public ulong vaEnd;

        public ulong cbSize;

        public ulong cPages;

        public ulong fPage;

        public bool fWoW64;

        public string sText;

        public uint cSoftware;

        public bool fS;

        public bool fR;

        public bool fW;

        public bool fX;
    }

    public struct VadEntry
    {
        public ulong vaStart;

        public ulong vaEnd;

        public ulong vaVad;

        public ulong cbSize;

        public uint VadType;

        public uint Protection;

        public bool fImage;

        public bool fFile;

        public bool fPageFile;

        public bool fPrivateMemory;

        public bool fTeb;

        public bool fStack;

        public uint fSpare;

        public uint HeapNum;

        public bool fHeap;

        public uint cwszDescription;

        public uint CommitCharge;

        public bool MemCommit;

        public uint u2;

        public uint cbPrototypePte;

        public ulong vaPrototypePte;

        public ulong vaSubsection;

        public string sText;

        public ulong vaFileObject;

        public uint cVadExPages;

        public uint cVadExPagesBase;
    }

    public struct VadExEntryPrototype
    {
        public uint tp;

        public ulong pa;

        public ulong pte;
    }

    public struct VadExEntry
    {
        public uint tp;

        public uint iPML;

        public ulong va;

        public ulong pa;

        public ulong pte;

        public uint pteFlags;

        public VadExEntryPrototype proto;

        public ulong vaVadBase;
    }

    public struct ModuleEntryDebugInfo
    {
        public bool fValid;

        public uint dwAge;

        public string sGuid;

        public string sPdbFilename;
    }

    public struct ModuleEntryVersionInfo
    {
        public bool fValid;

        public string sCompanyName;

        public string sFileDescription;

        public string sFileVersion;

        public string sInternalName;

        public string sLegalCopyright;

        public string sFileOriginalFilename;

        public string sProductName;

        public string sProductVersion;
    }

    public struct ModuleEntry
    {
        public bool fValid;

        public ulong vaBase;

        public ulong vaEntry;

        public uint cbImageSize;

        public bool fWow64;

        public string sText;

        public string sFullName;

        public uint tp;

        public uint cbFileSizeRaw;

        public uint cSection;

        public uint cEAT;

        public uint cIAT;

        public ModuleEntryDebugInfo DebugInfo;

        public ModuleEntryVersionInfo VersionInfo;
    }

    public struct UnloadedModuleEntry
    {
        public ulong vaBase;

        public uint cbImageSize;

        public bool fWow64;

        public string wText;

        public uint dwCheckSum;

        public uint dwTimeDateStamp;

        public ulong ftUnload;
    }

    public struct EATInfo
    {
        public bool fValid;

        public ulong vaModuleBase;

        public ulong vaAddressOfFunctions;

        public ulong vaAddressOfNames;

        public uint cNumberOfFunctions;

        public uint cNumberOfForwardedFunctions;

        public uint cNumberOfNames;

        public uint dwOrdinalBase;
    }

    public struct EATEntry
    {
        public ulong vaFunction;

        public uint dwOrdinal;

        public uint oFunctionsArray;

        public uint oNamesArray;

        public string sFunction;

        public string sForwardedFunction;
    }

    public struct IATEntry
    {
        public ulong vaFunction;

        public ulong vaModule;

        public string sFunction;

        public string sModule;

        public bool f32;

        public ushort wHint;

        public uint rvaFirstThunk;

        public uint rvaOriginalFirstThunk;

        public uint rvaNameModule;

        public uint rvaNameFunction;
    }

    public struct HeapEntry
    {
        public ulong va;

        public uint tpHeap;

        public bool f32;

        public uint iHeapNum;
    }

    public struct HeapSegmentEntry
    {
        public ulong va;

        public uint cb;

        public uint tpHeapSegment;

        public uint iHeapNum;
    }

    public struct HeapMap
    {
        public HeapEntry[] heaps;

        public HeapSegmentEntry[] segments;
    }

    public struct HeapAllocEntry
    {
        public ulong va;

        public uint cb;

        public uint tp;
    }

    public struct ThreadEntry
    {
        public uint dwTID;

        public uint dwPID;

        public uint dwExitStatus;

        public byte bState;

        public byte bRunning;

        public byte bPriority;

        public byte bBasePriority;

        public ulong vaETHREAD;

        public ulong vaTeb;

        public ulong ftCreateTime;

        public ulong ftExitTime;

        public ulong vaStartAddress;

        public ulong vaWin32StartAddress;

        public ulong vaStackBaseUser;

        public ulong vaStackLimitUser;

        public ulong vaStackBaseKernel;

        public ulong vaStackLimitKernel;

        public ulong vaTrapFrame;

        public ulong vaImpersonationToken;

        public ulong vaRIP;

        public ulong vaRSP;

        public ulong qwAffinity;

        public uint dwUserTime;

        public uint dwKernelTime;

        public byte bSuspendCount;

        public byte bWaitReason;
    }

    public struct ThreadCallstackEntry
    {
        public uint dwPID;

        public uint dwTID;

        public uint i;

        public bool fRegPresent;

        public ulong vaRetAddr;

        public ulong vaRSP;

        public ulong vaBaseSP;

        public int cbDisplacement;

        public string sModule;

        public string sFunction;
    }

    public struct HandleEntry
    {
        public ulong vaObject;

        public uint dwHandle;

        public uint dwGrantedAccess;

        public uint iType;

        public ulong qwHandleCount;

        public ulong qwPointerCount;

        public ulong vaObjectCreateInfo;

        public ulong vaSecurityDescriptor;

        public string sText;

        public uint dwPID;

        public uint dwPoolTag;

        public string sType;
    }

    public struct IMAGE_SECTION_HEADER
    {
        public string Name;

        public uint MiscPhysicalAddressOrVirtualSize;

        public uint VirtualAddress;

        public uint SizeOfRawData;

        public uint PointerToRawData;

        public uint PointerToRelocations;

        public uint PointerToLinenumbers;

        public ushort NumberOfRelocations;

        public ushort NumberOfLinenumbers;

        public uint Characteristics;
    }

    public struct IMAGE_DATA_DIRECTORY
    {
        public string name;

        public uint VirtualAddress;

        public uint Size;
    }

    protected readonly Vmm _hVmm;

    private ProcessInfo? _info;

    public const uint MAP_MODULEENTRY_TP_NORMAL = 0u;

    public const uint VMMDLL_MODULE_TP_DATA = 1u;

    public const uint VMMDLL_MODULE_TP_NOTLINKED = 2u;

    public const uint VMMDLL_MODULE_TP_INJECTED = 3u;

    public const uint VMMDLL_PROCESS_INFORMATION_OPT_STRING_PATH_KERNEL = 1u;

    public const uint VMMDLL_PROCESS_INFORMATION_OPT_STRING_PATH_USER_IMAGE = 2u;

    public const uint VMMDLL_PROCESS_INFORMATION_OPT_STRING_CMDLINE = 3u;

    public uint PID { get; }

    public ProcessInfo? Info
    {
        get
        {
            ProcessInfo? info = _info;
            if (info.HasValue)
            {
                return info.GetValueOrDefault();
            }

            bool result;
            ProcessInfo info2 = GetInfo(out result);
            if (result)
            {
                return _info = info2;
            }

            return null;
        }
    }

    public bool IsValid => Info?.fValid ?? false;

    public string Name => Info?.sName;

    private VmmProcess()
    {
    }

    internal VmmProcess(Vmm hVmm, string name)
    {
        if (hVmm == null)
        {
            throw new ArgumentNullException("hVmm");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException("name");
        }

        if (!Vmmi.VMMDLL_PidGetFromName(hVmm, name, out var pdwPID))
        {
            throw new VmmException("Failed to get PID from process name: " + name);
        }

        PID = pdwPID;
        _hVmm = hVmm;
    }

    internal VmmProcess(Vmm hVmm, uint pid)
    {
        if (hVmm == null)
        {
            throw new ArgumentNullException("hVmm");
        }

        PID = pid;
        _hVmm = hVmm;
    }

    public override string ToString()
    {
        return "VmmProcess:" + PID;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LeechCore.MemScatter[] MemReadScatter(uint flags, params ulong[] va)
    {
        return Vmmi.MemReadScatter(_hVmm, PID, flags, va);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VmmScatterMemory Scatter_Initialize(uint flags = 0u)
    {
        return Vmmi.Scatter_Initialize(_hVmm, PID, flags);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte[] MemRead(ulong va, uint cb, uint flags = 0u)
    {
        return Vmmi.MemReadArray<byte>(_hVmm, PID, va, cb, flags);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe bool MemRead(ulong va, IntPtr pb, uint cb, out uint cbRead, uint flags = 0u)
    {
        return Vmmi.MemRead(_hVmm, PID, va, pb.ToPointer(), cb, out cbRead, flags);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe bool MemRead(ulong va, void* pb, uint cb, out uint cbRead, uint flags = 0u)
    {
        return Vmmi.MemRead(_hVmm, PID, va, pb, cb, out cbRead, flags);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T? MemReadAs<T>(ulong va, uint flags = 0u) where T : unmanaged
    {
        return Vmmi.MemReadAs<T>(_hVmm, PID, va, flags);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] MemReadArray<T>(ulong va, uint count, uint flags = 0u) where T : unmanaged
    {
        return Vmmi.MemReadArray<T>(_hVmm, PID, va, count, flags);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string MemReadString(Encoding encoding, ulong va, uint cb, uint flags = 0u, bool terminateOnNullChar = true)
    {
        return Vmmi.MemReadString(_hVmm, encoding, PID, va, cb, flags, terminateOnNullChar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MemPrefetchPages(ulong[] va)
    {
        return Vmmi.MemPrefetchPages(_hVmm, PID, va);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MemWrite(ulong va, byte[] data)
    {
        return Vmmi.MemWriteArray(_hVmm, PID, va, data);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe bool MemWrite(ulong va, IntPtr pb, uint cb)
    {
        return Vmmi.MemWrite(_hVmm, PID, va, pb.ToPointer(), cb);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe bool MemWrite(ulong va, void* pb, uint cb)
    {
        return Vmmi.MemWrite(_hVmm, PID, va, pb, cb);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MemWriteStruct<T>(ulong va, T value) where T : unmanaged
    {
        return Vmmi.MemWriteStruct(_hVmm, PID, va, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MemWriteArray<T>(ulong va, T[] data) where T : unmanaged
    {
        return Vmmi.MemWriteArray(_hVmm, PID, va, data);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong MemVirt2Phys(ulong va)
    {
        ulong pqwPA = 0uL;
        Vmmi.MemVirt2Phys(_hVmm, PID, va, out pqwPA);
        return pqwPA;
    }

    public unsafe PteEntry[] MapPTE(bool fIdentifyModules = true)
    {
        int num = Marshal.SizeOf<Vmmi.VMMDLL_MAP_PTE>();
        int num2 = Marshal.SizeOf<Vmmi.VMMDLL_MAP_PTEENTRY>();
        IntPtr ppPteMap = IntPtr.Zero;
        PteEntry[] array = new PteEntry[0];
        if (Vmmi.VMMDLL_Map_GetPte(_hVmm, PID, fIdentifyModules, out ppPteMap))
        {
            Vmmi.VMMDLL_MAP_PTE vMMDLL_MAP_PTE = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_PTE>(ppPteMap);
            if (vMMDLL_MAP_PTE.dwVersion == 2)
            {
                array = new PteEntry[vMMDLL_MAP_PTE.cMap];
                PteEntry pteEntry = default(PteEntry);
                for (int i = 0; i < vMMDLL_MAP_PTE.cMap; i++)
                {
                    Vmmi.VMMDLL_MAP_PTEENTRY vMMDLL_MAP_PTEENTRY = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_PTEENTRY>((IntPtr)(ppPteMap.ToInt64() + num + i * num2));
                    pteEntry.vaBase = vMMDLL_MAP_PTEENTRY.vaBase;
                    pteEntry.vaEnd = vMMDLL_MAP_PTEENTRY.vaBase + (vMMDLL_MAP_PTEENTRY.cPages << 12) - 1;
                    pteEntry.cbSize = vMMDLL_MAP_PTEENTRY.cPages << 12;
                    pteEntry.cPages = vMMDLL_MAP_PTEENTRY.cPages;
                    pteEntry.fPage = vMMDLL_MAP_PTEENTRY.fPage;
                    pteEntry.fWoW64 = vMMDLL_MAP_PTEENTRY.fWoW64;
                    pteEntry.sText = vMMDLL_MAP_PTEENTRY.uszText;
                    pteEntry.cSoftware = vMMDLL_MAP_PTEENTRY.cSoftware;
                    pteEntry.fR = true;
                    pteEntry.fW = (pteEntry.fPage & 2) != 0;
                    pteEntry.fS = (pteEntry.fPage & 4) == 0;
                    pteEntry.fX = (pteEntry.fPage & 0x8000000000000000uL) == 0;
                    array[i] = pteEntry;
                }
            }
        }

        Vmmi.VMMDLL_MemFree((byte*)ppPteMap.ToPointer());
        return array;
    }

    public unsafe VadEntry[] MapVAD(bool fIdentifyModules = true)
    {
        int num = Marshal.SizeOf<Vmmi.VMMDLL_MAP_VAD>();
        int num2 = Marshal.SizeOf<Vmmi.VMMDLL_MAP_VADENTRY>();
        IntPtr ppVadMap = IntPtr.Zero;
        VadEntry[] array = new VadEntry[0];
        if (Vmmi.VMMDLL_Map_GetVad(_hVmm, PID, fIdentifyModules, out ppVadMap))
        {
            Vmmi.VMMDLL_MAP_VAD vMMDLL_MAP_VAD = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_VAD>(ppVadMap);
            if (vMMDLL_MAP_VAD.dwVersion == 6)
            {
                array = new VadEntry[vMMDLL_MAP_VAD.cMap];
                VadEntry vadEntry = default(VadEntry);
                for (int i = 0; i < vMMDLL_MAP_VAD.cMap; i++)
                {
                    Vmmi.VMMDLL_MAP_VADENTRY vMMDLL_MAP_VADENTRY = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_VADENTRY>((IntPtr)(ppVadMap.ToInt64() + num + i * num2));
                    vadEntry.vaStart = vMMDLL_MAP_VADENTRY.vaStart;
                    vadEntry.vaEnd = vMMDLL_MAP_VADENTRY.vaEnd;
                    vadEntry.cbSize = vMMDLL_MAP_VADENTRY.vaEnd + 1 - vMMDLL_MAP_VADENTRY.vaStart;
                    vadEntry.vaVad = vMMDLL_MAP_VADENTRY.vaVad;
                    vadEntry.VadType = vMMDLL_MAP_VADENTRY.dw0 & 7u;
                    vadEntry.Protection = (vMMDLL_MAP_VADENTRY.dw0 >> 3) & 0x1Fu;
                    vadEntry.fImage = ((vMMDLL_MAP_VADENTRY.dw0 >> 8) & 1) == 1;
                    vadEntry.fFile = ((vMMDLL_MAP_VADENTRY.dw0 >> 9) & 1) == 1;
                    vadEntry.fPageFile = ((vMMDLL_MAP_VADENTRY.dw0 >> 10) & 1) == 1;
                    vadEntry.fPrivateMemory = ((vMMDLL_MAP_VADENTRY.dw0 >> 11) & 1) == 1;
                    vadEntry.fTeb = ((vMMDLL_MAP_VADENTRY.dw0 >> 12) & 1) == 1;
                    vadEntry.fStack = ((vMMDLL_MAP_VADENTRY.dw0 >> 13) & 1) == 1;
                    vadEntry.fSpare = (vMMDLL_MAP_VADENTRY.dw0 >> 14) & 3u;
                    vadEntry.HeapNum = (vMMDLL_MAP_VADENTRY.dw0 >> 16) & 0x1Fu;
                    vadEntry.fHeap = ((vMMDLL_MAP_VADENTRY.dw0 >> 23) & 1) == 1;
                    vadEntry.cwszDescription = (vMMDLL_MAP_VADENTRY.dw0 >> 24) & 0xFFu;
                    vadEntry.CommitCharge = vMMDLL_MAP_VADENTRY.dw1 & 0x7FFFFFFFu;
                    vadEntry.MemCommit = ((vMMDLL_MAP_VADENTRY.dw1 >> 31) & 1) == 1;
                    vadEntry.u2 = vMMDLL_MAP_VADENTRY.u2;
                    vadEntry.cbPrototypePte = vMMDLL_MAP_VADENTRY.cbPrototypePte;
                    vadEntry.vaPrototypePte = vMMDLL_MAP_VADENTRY.vaPrototypePte;
                    vadEntry.vaSubsection = vMMDLL_MAP_VADENTRY.vaSubsection;
                    vadEntry.sText = vMMDLL_MAP_VADENTRY.uszText;
                    vadEntry.vaFileObject = vMMDLL_MAP_VADENTRY.vaFileObject;
                    vadEntry.cVadExPages = vMMDLL_MAP_VADENTRY.cVadExPages;
                    vadEntry.cVadExPagesBase = vMMDLL_MAP_VADENTRY.cVadExPagesBase;
                    array[i] = vadEntry;
                }
            }
        }

        Vmmi.VMMDLL_MemFree((byte*)ppVadMap.ToPointer());
        return array;
    }

    public unsafe VadExEntry[] MapVADEx(uint oPages, uint cPages)
    {
        int num = Marshal.SizeOf<Vmmi.VMMDLL_MAP_VADEX>();
        int num2 = Marshal.SizeOf<Vmmi.VMMDLL_MAP_VADEXENTRY>();
        IntPtr ppVadExMap = IntPtr.Zero;
        VadExEntry[] array = new VadExEntry[0];
        if (Vmmi.VMMDLL_Map_GetVadEx(_hVmm, PID, oPages, cPages, out ppVadExMap))
        {
            Vmmi.VMMDLL_MAP_VADEX vMMDLL_MAP_VADEX = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_VADEX>(ppVadExMap);
            if (vMMDLL_MAP_VADEX.dwVersion == 4)
            {
                array = new VadExEntry[vMMDLL_MAP_VADEX.cMap];
                VadExEntry vadExEntry = default(VadExEntry);
                for (int i = 0; i < vMMDLL_MAP_VADEX.cMap; i++)
                {
                    Vmmi.VMMDLL_MAP_VADEXENTRY vMMDLL_MAP_VADEXENTRY = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_VADEXENTRY>((IntPtr)(ppVadExMap.ToInt64() + num + i * num2));
                    vadExEntry.tp = vMMDLL_MAP_VADEXENTRY.tp;
                    vadExEntry.iPML = vMMDLL_MAP_VADEXENTRY.iPML;
                    vadExEntry.pteFlags = vMMDLL_MAP_VADEXENTRY.pteFlags;
                    vadExEntry.va = vMMDLL_MAP_VADEXENTRY.va;
                    vadExEntry.pa = vMMDLL_MAP_VADEXENTRY.pa;
                    vadExEntry.pte = vMMDLL_MAP_VADEXENTRY.pte;
                    vadExEntry.proto.tp = vMMDLL_MAP_VADEXENTRY.proto_tp;
                    vadExEntry.proto.pa = vMMDLL_MAP_VADEXENTRY.proto_pa;
                    vadExEntry.proto.pte = vMMDLL_MAP_VADEXENTRY.proto_pte;
                    vadExEntry.vaVadBase = vMMDLL_MAP_VADEXENTRY.vaVadBase;
                    array[i] = vadExEntry;
                }
            }
        }

        Vmmi.VMMDLL_MemFree((byte*)ppVadExMap.ToPointer());
        return array;
    }

    public unsafe ModuleEntry[] MapModule(bool fExtendedInfo = false)
    {
        int num = Marshal.SizeOf<Vmmi.VMMDLL_MAP_MODULE>();
        int num2 = Marshal.SizeOf<Vmmi.VMMDLL_MAP_MODULEENTRY>();
        IntPtr ppModuleMap = IntPtr.Zero;
        ModuleEntry[] array = new ModuleEntry[0];
        uint flags = (fExtendedInfo ? 255u : 0u);
        if (Vmmi.VMMDLL_Map_GetModule(_hVmm, PID, out ppModuleMap, flags))
        {
            Vmmi.VMMDLL_MAP_MODULE vMMDLL_MAP_MODULE = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_MODULE>(ppModuleMap);
            if (vMMDLL_MAP_MODULE.dwVersion == 6)
            {
                array = new ModuleEntry[vMMDLL_MAP_MODULE.cMap];
                ModuleEntry moduleEntry = default(ModuleEntry);
                ModuleEntryDebugInfo debugInfo = default(ModuleEntryDebugInfo);
                ModuleEntryVersionInfo versionInfo = default(ModuleEntryVersionInfo);
                for (int i = 0; i < vMMDLL_MAP_MODULE.cMap; i++)
                {
                    Vmmi.VMMDLL_MAP_MODULEENTRY vMMDLL_MAP_MODULEENTRY = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_MODULEENTRY>((IntPtr)(ppModuleMap.ToInt64() + num + i * num2));
                    moduleEntry.fValid = true;
                    moduleEntry.vaBase = vMMDLL_MAP_MODULEENTRY.vaBase;
                    moduleEntry.vaEntry = vMMDLL_MAP_MODULEENTRY.vaEntry;
                    moduleEntry.cbImageSize = vMMDLL_MAP_MODULEENTRY.cbImageSize;
                    moduleEntry.fWow64 = vMMDLL_MAP_MODULEENTRY.fWow64;
                    moduleEntry.sText = vMMDLL_MAP_MODULEENTRY.uszText;
                    moduleEntry.sFullName = vMMDLL_MAP_MODULEENTRY.uszFullName;
                    moduleEntry.tp = vMMDLL_MAP_MODULEENTRY.tp;
                    moduleEntry.cbFileSizeRaw = vMMDLL_MAP_MODULEENTRY.cbFileSizeRaw;
                    moduleEntry.cSection = vMMDLL_MAP_MODULEENTRY.cSection;
                    moduleEntry.cEAT = vMMDLL_MAP_MODULEENTRY.cEAT;
                    moduleEntry.cIAT = vMMDLL_MAP_MODULEENTRY.cIAT;
                    if (vMMDLL_MAP_MODULEENTRY.pExDebugInfo.ToInt64() == 0L)
                    {
                        debugInfo.fValid = false;
                        debugInfo.dwAge = 0u;
                        debugInfo.sGuid = "";
                        debugInfo.sPdbFilename = "";
                    }
                    else
                    {
                        Vmmi.VMMDLL_MAP_MODULEENTRY_DEBUGINFO vMMDLL_MAP_MODULEENTRY_DEBUGINFO = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_MODULEENTRY_DEBUGINFO>(vMMDLL_MAP_MODULEENTRY.pExDebugInfo);
                        debugInfo.fValid = true;
                        debugInfo.dwAge = vMMDLL_MAP_MODULEENTRY_DEBUGINFO.dwAge;
                        debugInfo.sGuid = vMMDLL_MAP_MODULEENTRY_DEBUGINFO.uszGuid;
                        debugInfo.sPdbFilename = vMMDLL_MAP_MODULEENTRY_DEBUGINFO.uszPdbFilename;
                    }

                    moduleEntry.DebugInfo = debugInfo;
                    if (vMMDLL_MAP_MODULEENTRY.pExDebugInfo.ToInt64() == 0L)
                    {
                        versionInfo.fValid = false;
                        versionInfo.sCompanyName = "";
                        versionInfo.sFileDescription = "";
                        versionInfo.sFileVersion = "";
                        versionInfo.sInternalName = "";
                        versionInfo.sLegalCopyright = "";
                        versionInfo.sFileOriginalFilename = "";
                        versionInfo.sProductName = "";
                        versionInfo.sProductVersion = "";
                    }
                    else
                    {
                        Vmmi.VMMDLL_MAP_MODULEENTRY_VERSIONINFO vMMDLL_MAP_MODULEENTRY_VERSIONINFO = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_MODULEENTRY_VERSIONINFO>(vMMDLL_MAP_MODULEENTRY.pExVersionInfo);
                        versionInfo.fValid = true;
                        versionInfo.sCompanyName = vMMDLL_MAP_MODULEENTRY_VERSIONINFO.uszCompanyName;
                        versionInfo.sFileDescription = vMMDLL_MAP_MODULEENTRY_VERSIONINFO.uszFileDescription;
                        versionInfo.sFileVersion = vMMDLL_MAP_MODULEENTRY_VERSIONINFO.uszFileVersion;
                        versionInfo.sInternalName = vMMDLL_MAP_MODULEENTRY_VERSIONINFO.uszInternalName;
                        versionInfo.sLegalCopyright = vMMDLL_MAP_MODULEENTRY_VERSIONINFO.uszLegalCopyright;
                        versionInfo.sFileOriginalFilename = vMMDLL_MAP_MODULEENTRY_VERSIONINFO.uszFileOriginalFilename;
                        versionInfo.sProductName = vMMDLL_MAP_MODULEENTRY_VERSIONINFO.uszProductName;
                        versionInfo.sProductVersion = vMMDLL_MAP_MODULEENTRY_VERSIONINFO.uszProductVersion;
                    }

                    moduleEntry.VersionInfo = versionInfo;
                    array[i] = moduleEntry;
                }
            }
        }

        Vmmi.VMMDLL_MemFree((byte*)ppModuleMap.ToPointer());
        return array;
    }

    public unsafe ModuleEntry MapModuleFromName(string module)
    {
        IntPtr ppModuleMapEntry = IntPtr.Zero;
        ModuleEntry result = default(ModuleEntry);
        if (Vmmi.VMMDLL_Map_GetModuleFromName(_hVmm, PID, module, out ppModuleMapEntry, 0u))
        {
            Vmmi.VMMDLL_MAP_MODULEENTRY vMMDLL_MAP_MODULEENTRY = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_MODULEENTRY>(ppModuleMapEntry);
            result.fValid = true;
            result.vaBase = vMMDLL_MAP_MODULEENTRY.vaBase;
            result.vaEntry = vMMDLL_MAP_MODULEENTRY.vaEntry;
            result.cbImageSize = vMMDLL_MAP_MODULEENTRY.cbImageSize;
            result.fWow64 = vMMDLL_MAP_MODULEENTRY.fWow64;
            result.sText = module;
            result.sFullName = vMMDLL_MAP_MODULEENTRY.uszFullName;
            result.tp = vMMDLL_MAP_MODULEENTRY.tp;
            result.cbFileSizeRaw = vMMDLL_MAP_MODULEENTRY.cbFileSizeRaw;
            result.cSection = vMMDLL_MAP_MODULEENTRY.cSection;
            result.cEAT = vMMDLL_MAP_MODULEENTRY.cEAT;
            result.cIAT = vMMDLL_MAP_MODULEENTRY.cIAT;
        }

        Vmmi.VMMDLL_MemFree((byte*)ppModuleMapEntry.ToPointer());
        return result;
    }

    public unsafe UnloadedModuleEntry[] MapUnloadedModule()
    {
        int num = Marshal.SizeOf<Vmmi.VMMDLL_MAP_UNLOADEDMODULE>();
        int num2 = Marshal.SizeOf<Vmmi.VMMDLL_MAP_UNLOADEDMODULEENTRY>();
        IntPtr ppModuleMap = IntPtr.Zero;
        UnloadedModuleEntry[] array = new UnloadedModuleEntry[0];
        if (Vmmi.VMMDLL_Map_GetUnloadedModule(_hVmm, PID, out ppModuleMap))
        {
            Vmmi.VMMDLL_MAP_UNLOADEDMODULE vMMDLL_MAP_UNLOADEDMODULE = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_UNLOADEDMODULE>(ppModuleMap);
            if (vMMDLL_MAP_UNLOADEDMODULE.dwVersion == 2)
            {
                array = new UnloadedModuleEntry[vMMDLL_MAP_UNLOADEDMODULE.cMap];
                UnloadedModuleEntry unloadedModuleEntry = default(UnloadedModuleEntry);
                for (int i = 0; i < vMMDLL_MAP_UNLOADEDMODULE.cMap; i++)
                {
                    Vmmi.VMMDLL_MAP_UNLOADEDMODULEENTRY vMMDLL_MAP_UNLOADEDMODULEENTRY = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_UNLOADEDMODULEENTRY>((IntPtr)(ppModuleMap.ToInt64() + num + i * num2));
                    unloadedModuleEntry.vaBase = vMMDLL_MAP_UNLOADEDMODULEENTRY.vaBase;
                    unloadedModuleEntry.cbImageSize = vMMDLL_MAP_UNLOADEDMODULEENTRY.cbImageSize;
                    unloadedModuleEntry.fWow64 = vMMDLL_MAP_UNLOADEDMODULEENTRY.fWow64;
                    unloadedModuleEntry.wText = vMMDLL_MAP_UNLOADEDMODULEENTRY.uszText;
                    unloadedModuleEntry.dwCheckSum = vMMDLL_MAP_UNLOADEDMODULEENTRY.dwCheckSum;
                    unloadedModuleEntry.dwTimeDateStamp = vMMDLL_MAP_UNLOADEDMODULEENTRY.dwTimeDateStamp;
                    unloadedModuleEntry.ftUnload = vMMDLL_MAP_UNLOADEDMODULEENTRY.ftUnload;
                    array[i] = unloadedModuleEntry;
                }
            }
        }

        Vmmi.VMMDLL_MemFree((byte*)ppModuleMap.ToPointer());
        return array;
    }

    public EATEntry[] MapModuleEAT(string module)
    {
        EATInfo info;
        return MapModuleEAT(module, out info);
    }

    public unsafe EATEntry[] MapModuleEAT(string module, out EATInfo info)
    {
        info = default(EATInfo);
        int num = Marshal.SizeOf<Vmmi.VMMDLL_MAP_EAT>();
        int num2 = Marshal.SizeOf<Vmmi.VMMDLL_MAP_EATENTRY>();
        IntPtr ppEatMap = IntPtr.Zero;
        EATEntry[] array = new EATEntry[0];
        if (Vmmi.VMMDLL_Map_GetEAT(_hVmm, PID, module, out ppEatMap))
        {
            Vmmi.VMMDLL_MAP_EAT vMMDLL_MAP_EAT = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_EAT>(ppEatMap);
            if (vMMDLL_MAP_EAT.dwVersion == 3)
            {
                array = new EATEntry[vMMDLL_MAP_EAT.cMap];
                EATEntry eATEntry = default(EATEntry);
                for (int i = 0; i < vMMDLL_MAP_EAT.cMap; i++)
                {
                    Vmmi.VMMDLL_MAP_EATENTRY vMMDLL_MAP_EATENTRY = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_EATENTRY>((IntPtr)(ppEatMap.ToInt64() + num + i * num2));
                    eATEntry.vaFunction = vMMDLL_MAP_EATENTRY.vaFunction;
                    eATEntry.dwOrdinal = vMMDLL_MAP_EATENTRY.dwOrdinal;
                    eATEntry.oFunctionsArray = vMMDLL_MAP_EATENTRY.oFunctionsArray;
                    eATEntry.oNamesArray = vMMDLL_MAP_EATENTRY.oNamesArray;
                    eATEntry.sFunction = vMMDLL_MAP_EATENTRY.uszFunction;
                    eATEntry.sForwardedFunction = vMMDLL_MAP_EATENTRY.uszForwardedFunction;
                    array[i] = eATEntry;
                }

                info.fValid = true;
                info.vaModuleBase = vMMDLL_MAP_EAT.vaModuleBase;
                info.vaAddressOfFunctions = vMMDLL_MAP_EAT.vaAddressOfFunctions;
                info.vaAddressOfNames = vMMDLL_MAP_EAT.vaAddressOfNames;
                info.cNumberOfFunctions = vMMDLL_MAP_EAT.cNumberOfFunctions;
                info.cNumberOfForwardedFunctions = vMMDLL_MAP_EAT.cNumberOfForwardedFunctions;
                info.cNumberOfNames = vMMDLL_MAP_EAT.cNumberOfNames;
                info.dwOrdinalBase = vMMDLL_MAP_EAT.dwOrdinalBase;
            }
        }

        Vmmi.VMMDLL_MemFree((byte*)ppEatMap.ToPointer());
        return array;
    }

    public unsafe IATEntry[] MapModuleIAT(string module)
    {
        int num = Marshal.SizeOf<Vmmi.VMMDLL_MAP_IAT>();
        int num2 = Marshal.SizeOf<Vmmi.VMMDLL_MAP_IATENTRY>();
        IntPtr ppIatMap = IntPtr.Zero;
        IATEntry[] array = new IATEntry[0];
        if (Vmmi.VMMDLL_Map_GetIAT(_hVmm, PID, module, out ppIatMap))
        {
            Vmmi.VMMDLL_MAP_IAT vMMDLL_MAP_IAT = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_IAT>(ppIatMap);
            if (vMMDLL_MAP_IAT.dwVersion == 2)
            {
                array = new IATEntry[vMMDLL_MAP_IAT.cMap];
                IATEntry iATEntry = default(IATEntry);
                for (int i = 0; i < vMMDLL_MAP_IAT.cMap; i++)
                {
                    Vmmi.VMMDLL_MAP_IATENTRY vMMDLL_MAP_IATENTRY = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_IATENTRY>((IntPtr)(ppIatMap.ToInt64() + num + i * num2));
                    iATEntry.vaFunction = vMMDLL_MAP_IATENTRY.vaFunction;
                    iATEntry.sFunction = vMMDLL_MAP_IATENTRY.uszFunction;
                    iATEntry.sModule = vMMDLL_MAP_IATENTRY.uszModule;
                    iATEntry.f32 = vMMDLL_MAP_IATENTRY.f32;
                    iATEntry.wHint = vMMDLL_MAP_IATENTRY.wHint;
                    iATEntry.rvaFirstThunk = vMMDLL_MAP_IATENTRY.rvaFirstThunk;
                    iATEntry.rvaOriginalFirstThunk = vMMDLL_MAP_IATENTRY.rvaOriginalFirstThunk;
                    iATEntry.rvaNameModule = vMMDLL_MAP_IATENTRY.rvaNameModule;
                    iATEntry.rvaNameFunction = vMMDLL_MAP_IATENTRY.rvaNameFunction;
                    iATEntry.vaModule = vMMDLL_MAP_IAT.vaModuleBase;
                    array[i] = iATEntry;
                }
            }
        }

        Vmmi.VMMDLL_MemFree((byte*)ppIatMap.ToPointer());
        return array;
    }

    public unsafe HeapMap MapHeap()
    {
        IntPtr ppHeapMap = IntPtr.Zero;
        int num = Marshal.SizeOf<Vmmi.VMMDLL_MAP_HEAP>();
        int num2 = Marshal.SizeOf<Vmmi.VMMDLL_MAP_HEAPENTRY>();
        int num3 = Marshal.SizeOf<Vmmi.VMMDLL_MAP_HEAPSEGMENTENTRY>();
        HeapMap result = default(HeapMap);
        result.heaps = new HeapEntry[0];
        result.segments = new HeapSegmentEntry[0];
        if (Vmmi.VMMDLL_Map_GetHeap(_hVmm, PID, out ppHeapMap))
        {
            Vmmi.VMMDLL_MAP_HEAP vMMDLL_MAP_HEAP = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_HEAP>(ppHeapMap);
            if (vMMDLL_MAP_HEAP.dwVersion == 4)
            {
                result.heaps = new HeapEntry[vMMDLL_MAP_HEAP.cMap];
                for (int i = 0; i < vMMDLL_MAP_HEAP.cMap; i++)
                {
                    Vmmi.VMMDLL_MAP_HEAPENTRY vMMDLL_MAP_HEAPENTRY = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_HEAPENTRY>((IntPtr)(ppHeapMap.ToInt64() + num + i * num2));
                    result.heaps[i].va = vMMDLL_MAP_HEAPENTRY.va;
                    result.heaps[i].f32 = vMMDLL_MAP_HEAPENTRY.f32;
                    result.heaps[i].tpHeap = vMMDLL_MAP_HEAPENTRY.tp;
                    result.heaps[i].iHeapNum = vMMDLL_MAP_HEAPENTRY.dwHeapNum;
                }

                result.segments = new HeapSegmentEntry[vMMDLL_MAP_HEAP.cSegments];
                for (int j = 0; j < vMMDLL_MAP_HEAP.cMap; j++)
                {
                    Vmmi.VMMDLL_MAP_HEAPSEGMENTENTRY vMMDLL_MAP_HEAPSEGMENTENTRY = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_HEAPSEGMENTENTRY>((IntPtr)(vMMDLL_MAP_HEAP.pSegments.ToInt64() + j * num3));
                    result.segments[j].va = vMMDLL_MAP_HEAPSEGMENTENTRY.va;
                    result.segments[j].cb = vMMDLL_MAP_HEAPSEGMENTENTRY.cb;
                    result.segments[j].tpHeapSegment = vMMDLL_MAP_HEAPSEGMENTENTRY.tp;
                    result.segments[j].iHeapNum = vMMDLL_MAP_HEAPSEGMENTENTRY.iHeap;
                }
            }
        }

        Vmmi.VMMDLL_MemFree((byte*)ppHeapMap.ToPointer());
        return result;
    }

    public unsafe HeapAllocEntry[] MapHeapAlloc(ulong vaHeapOrHeapNum)
    {
        IntPtr ppHeapAllocMap = IntPtr.Zero;
        int num = Marshal.SizeOf<Vmmi.VMMDLL_MAP_HEAPALLOC>();
        int num2 = Marshal.SizeOf<Vmmi.VMMDLL_MAP_HEAPALLOCENTRY>();
        if (!Vmmi.VMMDLL_Map_GetHeapAlloc(_hVmm, PID, vaHeapOrHeapNum, out ppHeapAllocMap))
        {
            return new HeapAllocEntry[0];
        }

        Vmmi.VMMDLL_MAP_HEAPALLOC vMMDLL_MAP_HEAPALLOC = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_HEAPALLOC>(ppHeapAllocMap);
        if (vMMDLL_MAP_HEAPALLOC.dwVersion != 1)
        {
            Vmmi.VMMDLL_MemFree((byte*)ppHeapAllocMap.ToPointer());
            return new HeapAllocEntry[0];
        }

        HeapAllocEntry[] array = new HeapAllocEntry[vMMDLL_MAP_HEAPALLOC.cMap];
        for (int i = 0; i < vMMDLL_MAP_HEAPALLOC.cMap; i++)
        {
            Vmmi.VMMDLL_MAP_HEAPALLOCENTRY vMMDLL_MAP_HEAPALLOCENTRY = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_HEAPALLOCENTRY>((IntPtr)(ppHeapAllocMap.ToInt64() + num + i * num2));
            array[i].va = vMMDLL_MAP_HEAPALLOCENTRY.va;
            array[i].cb = vMMDLL_MAP_HEAPALLOCENTRY.cb;
            array[i].tp = vMMDLL_MAP_HEAPALLOCENTRY.tp;
        }

        Vmmi.VMMDLL_MemFree((byte*)ppHeapAllocMap.ToPointer());
        return array;
    }

    public unsafe ThreadEntry[] MapThread()
    {
        int num = Marshal.SizeOf<Vmmi.VMMDLL_MAP_THREAD>();
        int num2 = Marshal.SizeOf<Vmmi.VMMDLL_MAP_THREADENTRY>();
        IntPtr ppThreadMap = IntPtr.Zero;
        ThreadEntry[] array = new ThreadEntry[0];
        if (Vmmi.VMMDLL_Map_GetThread(_hVmm, PID, out ppThreadMap))
        {
            Vmmi.VMMDLL_MAP_THREAD vMMDLL_MAP_THREAD = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_THREAD>(ppThreadMap);
            if (vMMDLL_MAP_THREAD.dwVersion == 4)
            {
                array = new ThreadEntry[vMMDLL_MAP_THREAD.cMap];
                ThreadEntry threadEntry = default(ThreadEntry);
                for (int i = 0; i < vMMDLL_MAP_THREAD.cMap; i++)
                {
                    Vmmi.VMMDLL_MAP_THREADENTRY vMMDLL_MAP_THREADENTRY = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_THREADENTRY>((IntPtr)(ppThreadMap.ToInt64() + num + i * num2));
                    threadEntry.dwTID = vMMDLL_MAP_THREADENTRY.dwTID;
                    threadEntry.dwPID = vMMDLL_MAP_THREADENTRY.dwPID;
                    threadEntry.dwExitStatus = vMMDLL_MAP_THREADENTRY.dwExitStatus;
                    threadEntry.bState = vMMDLL_MAP_THREADENTRY.bState;
                    threadEntry.bRunning = vMMDLL_MAP_THREADENTRY.bRunning;
                    threadEntry.bPriority = vMMDLL_MAP_THREADENTRY.bPriority;
                    threadEntry.bBasePriority = vMMDLL_MAP_THREADENTRY.bBasePriority;
                    threadEntry.vaETHREAD = vMMDLL_MAP_THREADENTRY.vaETHREAD;
                    threadEntry.vaTeb = vMMDLL_MAP_THREADENTRY.vaTeb;
                    threadEntry.ftCreateTime = vMMDLL_MAP_THREADENTRY.ftCreateTime;
                    threadEntry.ftExitTime = vMMDLL_MAP_THREADENTRY.ftExitTime;
                    threadEntry.vaStartAddress = vMMDLL_MAP_THREADENTRY.vaStartAddress;
                    threadEntry.vaWin32StartAddress = vMMDLL_MAP_THREADENTRY.vaWin32StartAddress;
                    threadEntry.vaStackBaseUser = vMMDLL_MAP_THREADENTRY.vaStackBaseUser;
                    threadEntry.vaStackLimitUser = vMMDLL_MAP_THREADENTRY.vaStackLimitUser;
                    threadEntry.vaStackBaseKernel = vMMDLL_MAP_THREADENTRY.vaStackBaseKernel;
                    threadEntry.vaStackLimitKernel = vMMDLL_MAP_THREADENTRY.vaStackLimitKernel;
                    threadEntry.vaImpersonationToken = vMMDLL_MAP_THREADENTRY.vaImpersonationToken;
                    threadEntry.vaTrapFrame = vMMDLL_MAP_THREADENTRY.vaTrapFrame;
                    threadEntry.vaRIP = vMMDLL_MAP_THREADENTRY.vaRIP;
                    threadEntry.vaRSP = vMMDLL_MAP_THREADENTRY.vaRSP;
                    threadEntry.qwAffinity = vMMDLL_MAP_THREADENTRY.qwAffinity;
                    threadEntry.dwUserTime = vMMDLL_MAP_THREADENTRY.dwUserTime;
                    threadEntry.dwKernelTime = vMMDLL_MAP_THREADENTRY.dwKernelTime;
                    threadEntry.bSuspendCount = vMMDLL_MAP_THREADENTRY.bSuspendCount;
                    threadEntry.bWaitReason = vMMDLL_MAP_THREADENTRY.bWaitReason;
                    array[i] = threadEntry;
                }
            }
        }

        Vmmi.VMMDLL_MemFree((byte*)ppThreadMap.ToPointer());
        return array;
    }

    public unsafe ThreadCallstackEntry[] MapThreadCallstack(uint tid, uint flags = 0u)
    {
        int num = Marshal.SizeOf<Vmmi.VMMDLL_MAP_THREAD_CALLSTACK>();
        int num2 = Marshal.SizeOf<Vmmi.VMMDLL_MAP_THREAD_CALLSTACKENTRY>();
        IntPtr ppThreadCallstack = IntPtr.Zero;
        ThreadCallstackEntry[] array = new ThreadCallstackEntry[0];
        if (Vmmi.VMMDLL_Map_GetThread_Callstack(_hVmm, PID, tid, flags, out ppThreadCallstack))
        {
            Vmmi.VMMDLL_MAP_THREAD_CALLSTACK vMMDLL_MAP_THREAD_CALLSTACK = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_THREAD_CALLSTACK>(ppThreadCallstack);
            if (vMMDLL_MAP_THREAD_CALLSTACK.dwVersion == 1)
            {
                array = new ThreadCallstackEntry[vMMDLL_MAP_THREAD_CALLSTACK.cMap];
                ThreadCallstackEntry threadCallstackEntry = default(ThreadCallstackEntry);
                for (int i = 0; i < vMMDLL_MAP_THREAD_CALLSTACK.cMap; i++)
                {
                    Vmmi.VMMDLL_MAP_THREAD_CALLSTACKENTRY vMMDLL_MAP_THREAD_CALLSTACKENTRY = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_THREAD_CALLSTACKENTRY>((IntPtr)(ppThreadCallstack.ToInt64() + num + i * num2));
                    threadCallstackEntry.dwPID = PID;
                    threadCallstackEntry.dwTID = tid;
                    threadCallstackEntry.i = vMMDLL_MAP_THREAD_CALLSTACKENTRY.i;
                    threadCallstackEntry.fRegPresent = vMMDLL_MAP_THREAD_CALLSTACKENTRY.fRegPresent;
                    threadCallstackEntry.vaRetAddr = vMMDLL_MAP_THREAD_CALLSTACKENTRY.vaRetAddr;
                    threadCallstackEntry.vaRSP = vMMDLL_MAP_THREAD_CALLSTACKENTRY.vaRSP;
                    threadCallstackEntry.vaBaseSP = vMMDLL_MAP_THREAD_CALLSTACKENTRY.vaBaseSP;
                    threadCallstackEntry.cbDisplacement = (int)vMMDLL_MAP_THREAD_CALLSTACKENTRY.cbDisplacement;
                    threadCallstackEntry.sModule = vMMDLL_MAP_THREAD_CALLSTACKENTRY.uszModule;
                    threadCallstackEntry.sFunction = vMMDLL_MAP_THREAD_CALLSTACKENTRY.uszFunction;
                    array[i] = threadCallstackEntry;
                }
            }
        }

        Vmmi.VMMDLL_MemFree((byte*)ppThreadCallstack.ToPointer());
        return array;
    }

    public unsafe HandleEntry[] MapHandle()
    {
        int num = Marshal.SizeOf<Vmmi.VMMDLL_MAP_HANDLE>();
        int num2 = Marshal.SizeOf<Vmmi.VMMDLL_MAP_HANDLEENTRY>();
        IntPtr ppHandleMap = IntPtr.Zero;
        HandleEntry[] array = new HandleEntry[0];
        if (Vmmi.VMMDLL_Map_GetHandle(_hVmm, PID, out ppHandleMap))
        {
            Vmmi.VMMDLL_MAP_HANDLE vMMDLL_MAP_HANDLE = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_HANDLE>(ppHandleMap);
            if (vMMDLL_MAP_HANDLE.dwVersion == 3)
            {
                array = new HandleEntry[vMMDLL_MAP_HANDLE.cMap];
                HandleEntry handleEntry = default(HandleEntry);
                for (int i = 0; i < vMMDLL_MAP_HANDLE.cMap; i++)
                {
                    Vmmi.VMMDLL_MAP_HANDLEENTRY vMMDLL_MAP_HANDLEENTRY = Marshal.PtrToStructure<Vmmi.VMMDLL_MAP_HANDLEENTRY>((IntPtr)(ppHandleMap.ToInt64() + num + i * num2));
                    handleEntry.vaObject = vMMDLL_MAP_HANDLEENTRY.vaObject;
                    handleEntry.dwHandle = vMMDLL_MAP_HANDLEENTRY.dwHandle;
                    handleEntry.dwGrantedAccess = vMMDLL_MAP_HANDLEENTRY.dwGrantedAccess_iType & 0xFFFFFFu;
                    handleEntry.iType = vMMDLL_MAP_HANDLEENTRY.dwGrantedAccess_iType >> 24;
                    handleEntry.qwHandleCount = vMMDLL_MAP_HANDLEENTRY.qwHandleCount;
                    handleEntry.qwPointerCount = vMMDLL_MAP_HANDLEENTRY.qwPointerCount;
                    handleEntry.vaObjectCreateInfo = vMMDLL_MAP_HANDLEENTRY.vaObjectCreateInfo;
                    handleEntry.vaSecurityDescriptor = vMMDLL_MAP_HANDLEENTRY.vaSecurityDescriptor;
                    handleEntry.sText = vMMDLL_MAP_HANDLEENTRY.uszText;
                    handleEntry.dwPID = vMMDLL_MAP_HANDLEENTRY.dwPID;
                    handleEntry.dwPoolTag = vMMDLL_MAP_HANDLEENTRY.dwPoolTag;
                    handleEntry.sType = vMMDLL_MAP_HANDLEENTRY.uszType;
                    array[i] = handleEntry;
                }
            }
        }

        Vmmi.VMMDLL_MemFree((byte*)ppHandleMap.ToPointer());
        return array;
    }

    public string GetPathUser()
    {
        return GetInformationString(2u);
    }

    public string GetPathKernel()
    {
        return GetInformationString(1u);
    }

    public string GetCmdline()
    {
        return GetInformationString(3u);
    }

    public unsafe string GetInformationString(uint fOptionString)
    {
        byte* ptr = Vmmi.VMMDLL_ProcessGetInformationString(_hVmm, PID, fOptionString);
        if (ptr == null)
        {
            return "";
        }

        string result = Marshal.PtrToStringAnsi((IntPtr)ptr);
        Vmmi.VMMDLL_MemFree(ptr);
        return result;
    }

    public unsafe IMAGE_DATA_DIRECTORY[] MapModuleDataDirectory(string sModule)
    {
        string[] array = new string[16]
        {
            "EXPORT", "IMPORT", "RESOURCE", "EXCEPTION", "SECURITY", "BASERELOC", "DEBUG", "ARCHITECTURE", "GLOBALPTR", "TLS",
            "LOAD_CONFIG", "BOUND_IMPORT", "IAT", "DELAY_IMPORT", "COM_DESCRIPTOR", "RESERVED"
        };
        uint num = (uint)Marshal.SizeOf<Vmmi.VMMDLL_IMAGE_DATA_DIRECTORY>();
        fixed (byte* ptr = new byte[16 * num])
        {
            if (!Vmmi.VMMDLL_ProcessGetDirectories(_hVmm, PID, sModule, ptr))
            {
                return new IMAGE_DATA_DIRECTORY[0];
            }

            IMAGE_DATA_DIRECTORY[] array2 = new IMAGE_DATA_DIRECTORY[16];
            IMAGE_DATA_DIRECTORY iMAGE_DATA_DIRECTORY = default(IMAGE_DATA_DIRECTORY);
            for (int i = 0; i < 16; i++)
            {
                Vmmi.VMMDLL_IMAGE_DATA_DIRECTORY vMMDLL_IMAGE_DATA_DIRECTORY = Marshal.PtrToStructure<Vmmi.VMMDLL_IMAGE_DATA_DIRECTORY>((IntPtr)(ptr + i * num));
                iMAGE_DATA_DIRECTORY.name = array[i];
                iMAGE_DATA_DIRECTORY.VirtualAddress = vMMDLL_IMAGE_DATA_DIRECTORY.VirtualAddress;
                iMAGE_DATA_DIRECTORY.Size = vMMDLL_IMAGE_DATA_DIRECTORY.Size;
                array2[i] = iMAGE_DATA_DIRECTORY;
            }

            return array2;
        }
    }

    public unsafe IMAGE_SECTION_HEADER[] MapModuleSection(string sModule)
    {
        uint num = (uint)Marshal.SizeOf<Vmmi.VMMDLL_IMAGE_SECTION_HEADER>();
        if (!Vmmi.VMMDLL_ProcessGetSections(_hVmm, PID, sModule, null, 0u, out var pcData) || pcData == 0)
        {
            return new IMAGE_SECTION_HEADER[0];
        }

        fixed (byte* ptr = new byte[pcData * num])
        {
            if (!Vmmi.VMMDLL_ProcessGetSections(_hVmm, PID, sModule, ptr, pcData, out pcData) || pcData == 0)
            {
                return new IMAGE_SECTION_HEADER[0];
            }

            IMAGE_SECTION_HEADER[] array = new IMAGE_SECTION_HEADER[pcData];
            IMAGE_SECTION_HEADER iMAGE_SECTION_HEADER = default(IMAGE_SECTION_HEADER);
            for (int i = 0; i < pcData; i++)
            {
                Vmmi.VMMDLL_IMAGE_SECTION_HEADER vMMDLL_IMAGE_SECTION_HEADER = Marshal.PtrToStructure<Vmmi.VMMDLL_IMAGE_SECTION_HEADER>((IntPtr)(ptr + i * num));
                iMAGE_SECTION_HEADER.Name = vMMDLL_IMAGE_SECTION_HEADER.Name;
                iMAGE_SECTION_HEADER.MiscPhysicalAddressOrVirtualSize = vMMDLL_IMAGE_SECTION_HEADER.MiscPhysicalAddressOrVirtualSize;
                iMAGE_SECTION_HEADER.VirtualAddress = vMMDLL_IMAGE_SECTION_HEADER.VirtualAddress;
                iMAGE_SECTION_HEADER.SizeOfRawData = vMMDLL_IMAGE_SECTION_HEADER.SizeOfRawData;
                iMAGE_SECTION_HEADER.PointerToRawData = vMMDLL_IMAGE_SECTION_HEADER.PointerToRawData;
                iMAGE_SECTION_HEADER.PointerToRelocations = vMMDLL_IMAGE_SECTION_HEADER.PointerToRelocations;
                iMAGE_SECTION_HEADER.PointerToLinenumbers = vMMDLL_IMAGE_SECTION_HEADER.PointerToLinenumbers;
                iMAGE_SECTION_HEADER.NumberOfRelocations = vMMDLL_IMAGE_SECTION_HEADER.NumberOfRelocations;
                iMAGE_SECTION_HEADER.NumberOfLinenumbers = vMMDLL_IMAGE_SECTION_HEADER.NumberOfLinenumbers;
                iMAGE_SECTION_HEADER.Characteristics = vMMDLL_IMAGE_SECTION_HEADER.Characteristics;
                array[i] = iMAGE_SECTION_HEADER;
            }

            return array;
        }
    }

    public ulong GetProcAddress(string wszModuleName, string szFunctionName)
    {
        return Vmmi.VMMDLL_ProcessGetProcAddress(_hVmm, PID, wszModuleName, szFunctionName);
    }

    public ulong GetModuleBase(string wszModuleName)
    {
        return Vmmi.VMMDLL_ProcessGetModuleBase(_hVmm, PID, wszModuleName);
    }

    public ProcessInfo GetInfo()
    {
        bool result;
        return GetInfo(out result);
    }

    public unsafe ProcessInfo GetInfo(out bool result)
    {
        ulong pcbProcessInformation = (ulong)Marshal.SizeOf<Vmmi.VMMDLL_PROCESS_INFORMATION>();
        fixed (byte* ptr = new byte[pcbProcessInformation])
        {
            Marshal.WriteInt64(new IntPtr(ptr), -4539647776472354786L);
            Marshal.WriteInt16(new IntPtr(ptr + 8), 7);
            result = Vmmi.VMMDLL_ProcessGetInformation(_hVmm, PID, ptr, ref pcbProcessInformation);
            if (!result)
            {
                return default(ProcessInfo);
            }

            Vmmi.VMMDLL_PROCESS_INFORMATION vMMDLL_PROCESS_INFORMATION = Marshal.PtrToStructure<Vmmi.VMMDLL_PROCESS_INFORMATION>((IntPtr)ptr);
            if (vMMDLL_PROCESS_INFORMATION.wVersion != 7)
            {
                return default(ProcessInfo);
            }

            ProcessInfo result2 = default(ProcessInfo);
            result2.fValid = true;
            result2.tpMemoryModel = vMMDLL_PROCESS_INFORMATION.tpMemoryModel;
            result2.tpSystem = vMMDLL_PROCESS_INFORMATION.tpSystem;
            result2.fUserOnly = vMMDLL_PROCESS_INFORMATION.fUserOnly;
            result2.dwPID = vMMDLL_PROCESS_INFORMATION.dwPID;
            result2.dwPPID = vMMDLL_PROCESS_INFORMATION.dwPPID;
            result2.dwState = vMMDLL_PROCESS_INFORMATION.dwState;
            result2.sName = vMMDLL_PROCESS_INFORMATION.szName;
            result2.sNameLong = vMMDLL_PROCESS_INFORMATION.szNameLong;
            result2.paDTB = vMMDLL_PROCESS_INFORMATION.paDTB;
            result2.paDTB_UserOpt = vMMDLL_PROCESS_INFORMATION.paDTB_UserOpt;
            result2.vaEPROCESS = vMMDLL_PROCESS_INFORMATION.vaEPROCESS;
            result2.vaPEB = vMMDLL_PROCESS_INFORMATION.vaPEB;
            result2.fWow64 = vMMDLL_PROCESS_INFORMATION.fWow64;
            result2.vaPEB32 = vMMDLL_PROCESS_INFORMATION.vaPEB32;
            result2.dwSessionId = vMMDLL_PROCESS_INFORMATION.dwSessionId;
            result2.qwLUID = vMMDLL_PROCESS_INFORMATION.qwLUID;
            result2.sSID = vMMDLL_PROCESS_INFORMATION.szSID;
            result2.IntegrityLevel = vMMDLL_PROCESS_INFORMATION.IntegrityLevel;
            return result2;
        }
    }

    public VmmPdb Pdb(ulong vaModuleBase)
    {
        return new VmmPdb(_hVmm, PID, vaModuleBase);
    }

    public VmmPdb Pdb(string sModule)
    {
        ModuleEntry moduleEntry = MapModuleFromName(sModule);
        if (!moduleEntry.fValid)
        {
            throw new VmmException("Module not found.");
        }

        return Pdb(moduleEntry.vaBase);
    }

    public VmmSearch Search(ulong addr_min = 0uL, ulong addr_max = ulong.MaxValue, uint cMaxResult = 0u, uint readFlags = 0u)
    {
        return new VmmSearch(_hVmm, PID, addr_min, addr_max, cMaxResult, readFlags);
    }

    public VmmYara SearchYara(string[] yara_rules, ulong addr_min = 0uL, ulong addr_max = ulong.MaxValue, uint cMaxResult = 0u, uint readFlags = 0u)
    {
        return new VmmYara(_hVmm, PID, yara_rules, addr_min, addr_max, cMaxResult, readFlags);
    }

    public VmmYara SearchYara(string yara_rule, ulong addr_min = 0uL, ulong addr_max = ulong.MaxValue, uint cMaxResult = 0u, uint readFlags = 0u)
    {
        string[] yara_rules = new string[1] { yara_rule };
        return new VmmYara(_hVmm, PID, yara_rules, addr_min, addr_max, cMaxResult, readFlags);
    }
}
#if false // 反编译日志
缓存中的 11 项
------------------
解析: "mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
找到单个程序集: "mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
从以下位置加载: "C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\mscorlib.dll"
------------------
解析: "System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
找到单个程序集: "System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
从以下位置加载: "C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Core.dll"
#endif
