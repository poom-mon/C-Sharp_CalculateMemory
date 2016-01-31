using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace c_Shap_calculateMemory
{
    public  class PerformanceInfo
    {
        [DllImport("psapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPerformanceInfo([Out] out PerformanceInformation PerformanceInformation, [In] int Size);
        [StructLayout(LayoutKind.Sequential)]
        public struct PerformanceInformation
        {
            public int Size;
            public IntPtr CommitTotal;
            public IntPtr CommitLimit;
            public IntPtr CommitPeak;
            public IntPtr PhysicalTotal;
            public IntPtr PhysicalAvailable;
            public IntPtr SystemCache;
            public IntPtr KernelTotal;
            public IntPtr KernelPaged;
            public IntPtr KernelNonPaged;
            public IntPtr PageSize;
            public int HandlesCount;
            public int ProcessCount;
            public int ThreadCount;
        } 
        public static Int64 GetPhysicalAvailableMemoryInMb()
        {
            PerformanceInformation pi = new PerformanceInformation();
            if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
            {
                return Convert.ToInt64((pi.PhysicalAvailable.ToInt64() * pi.PageSize.ToInt64() / 1048576));
            }
            else
            {
                return -1;
            }

        }
        public static Int64 GetTotalMemoryInMb()
        {
            PerformanceInformation pi = new PerformanceInformation();
            if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
            {
                return Convert.ToInt64((pi.PhysicalTotal.ToInt64() * pi.PageSize.ToInt64() / 1048576));
            }
            else
            {
                return -1;
            } 
        }

        public Int64 _physicalAvalMemory = 0;
        public Int64 _totalMemory = 0;
        public decimal _percentFreeMemory = 0;
        public decimal _percentOccupied = 0;

        public  PerformanceInfo() {
            _physicalAvalMemory = GetPhysicalAvailableMemoryInMb();
            _totalMemory = PerformanceInfo.GetTotalMemoryInMb();
            _percentFreeMemory = ((decimal)_physicalAvalMemory / (decimal)_totalMemory) * 100;
            _percentOccupied = 100 - _percentFreeMemory;
        }
        ~PerformanceInfo()
        {
            _physicalAvalMemory   = 0 ;
            _totalMemory = 0;
            _percentFreeMemory = 0; 
            _percentOccupied = 0 ; 
        }
        public  void Display() {  
            Console.WriteLine("Available Physical Memory (Mb) " + _physicalAvalMemory.ToString());
            Console.WriteLine("Total Memory (Mb) " + _totalMemory.ToString());
            Console.WriteLine("Free (%) " + _percentFreeMemory.ToString());
            Console.WriteLine("Occupied (%) " + _percentOccupied.ToString()); 
            Console.ReadLine();
        }



    }

}
