using System;
using System.Windows.Forms;
using System.Reflection;

namespace project2
{
    public partial class Startup : Form
    {
        public Startup()
        {
            InitializeComponent();
        }

        private void load_Click(object sender, EventArgs e)
        {
            LoadData loadData = new LoadData();
            loadData.Show();
            this.Hide();
        }

        private void Startup_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void creat_Click(object sender, EventArgs e)
        {
            CreateData createData = new CreateData();
            createData.Show();
            this.Hide();
        }

        public void SetButtonsFocus(Button[] buttons)
        {
            foreach (Button button in buttons)
                SetButton(button);
        }

        public void SetButton(Button button)
        {
            MethodInfo methodinfo = 
                button.GetType().GetMethod("SetStyle", 
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod);
            methodinfo.Invoke(button, 
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, 
                null, new object[] { ControlStyles.Selectable, false }, 
                Application.CurrentCulture);
        }

        private void Startup_Load(object sender, EventArgs e)
        {
            Button[] buttons = { load, create };
            SetButtonsFocus(buttons);
        }
    }
}
