using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace www_4u2v_com
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new backgroundworder());
        }
    }
}