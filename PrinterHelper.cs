using System;
using System.Runtime.InteropServices;
using System.Text;

public static class PrinterHelper
{
    [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true)]
    static extern bool OpenPrinter(string szPrinter, out IntPtr hPrinter, IntPtr pd);

    [DllImport("winspool.Drv", EntryPoint = "ClosePrinter")]
    static extern bool ClosePrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true)]
    static extern bool StartDocPrinter(IntPtr hPrinter, int level, ref DOCINFOA di);

    [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter")]
    static extern bool EndDocPrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter")]
    static extern bool StartPagePrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter")]
    static extern bool EndPagePrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "WritePrinter")]
    static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

    [StructLayout(LayoutKind.Sequential)]
    public struct DOCINFOA
    {
        [MarshalAs(UnmanagedType.LPStr)]
        public string pDocName;
        [MarshalAs(UnmanagedType.LPStr)]
        public string pOutputFile;
        [MarshalAs(UnmanagedType.LPStr)]
        public string pDataType;
    }

    public static bool SendStringToPrinter(string printerName, string zpl)
    {
        IntPtr hPrinter;
        DOCINFOA di = new DOCINFOA { pDocName = "ZPL", pDataType = "RAW" };
        IntPtr pBytes = Marshal.StringToCoTaskMemAnsi(zpl);
        int dwCount = zpl.Length;

        if (!OpenPrinter(printerName.Normalize(), out hPrinter, IntPtr.Zero)) return false;

        if (StartDocPrinter(hPrinter, 1, ref di))
        {
            StartPagePrinter(hPrinter);
            WritePrinter(hPrinter, pBytes, dwCount, out _);
            EndPagePrinter(hPrinter);
            EndDocPrinter(hPrinter);
        }

        ClosePrinter(hPrinter);
        Marshal.FreeCoTaskMem(pBytes);
        return true;
    }
}
