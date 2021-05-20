using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace project1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            for (;;)
            {
                if (File.Exists($@"{System.Environment.CurrentDirectory}\A306.txt"))
                {
                    Application.Run(new Form1());
                    break;
                }
                else
                {
                    DialogResult result = MessageBox.Show("在您的程序目录下未找到数据文件，请将数据文件移动到程序所在目录。", "未找到数据文件", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                    if (result == DialogResult.Retry) continue;
                    else break;
                }
            }
        }
    }
}
