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
        [STAThread]
        static void Main()
        {
            string aircraftType = "A306";

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            for (;;)
            {
                if (File.Exists($@"{System.Environment.CurrentDirectory}\{aircraftType}.txt"))
                {
                    Application.Run(new Platform());
                    break;
                }
                else
                {
                    DialogResult result = MessageBox.Show("在程序目录下未找到数据文件，请将数据文件移动到程序所在目录。", "未找到数据文件", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                    if (result == DialogResult.Retry) continue;
                    else break;
                }
            }
        }
    }
}
