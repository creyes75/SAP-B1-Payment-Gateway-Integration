using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.InteropServices;
using ConectorDataFast.Procesos;

namespace ConectorDataFast
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        static void Main(string[] args)
        {
            //Conexion.open();
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
           EnlaceDataFast eD = new EnlaceDataFast();
            Console.ReadKey();

        }

    }
}
