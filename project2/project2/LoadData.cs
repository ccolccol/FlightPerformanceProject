using System;
using System.Windows.Forms;
using System.Reflection;
using DataIO;
using System.Text.RegularExpressions;

namespace project2
{
    public partial class LoadData : Form
    {
        private bool[] clickModifyFlag = new bool[4] { false, false, false, false };
        private Label[] keysAll = new Label[108];
        private TextBox[] basesAll = new TextBox[108];
        private TextBox[] powersAll = new TextBox[108];
        private string[] keysAllString = new string[108];
        private Label[] keysOPF = new Label[55];
        private TextBox[] basesOPF = new TextBox[55];
        private TextBox[] powersOPF = new TextBox[55];
        private Label[] keysAPF = new Label[9];
        private TextBox[] basesAPF = new TextBox[9];
        private TextBox[] powersAPF = new TextBox[9];
        private Label[] keysGPF = new Label[44];
        private TextBox[] basesGPF = new TextBox[44];
        private TextBox[] powersGPF = new TextBox[44];
        private Button[] modifysAll = new Button[4];
        private DataContainer dataContainer = new DataContainer();

        public LoadData()
        {
            InitializeComponent();
        }

        private void loadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDataFile = new OpenFileDialog();
            openDataFile.Title = "Select BADA data file";
            openDataFile.Filter = "txt file(.txt)|*.txt";
            openDataFile.InitialDirectory = label216.Text;
            openDataFile.CheckPathExists = true;
            if (openDataFile.ShowDialog() == DialogResult.OK)
            {

                ReadData(openDataFile.FileName);
                label216.Text = Regex.Match(openDataFile.FileName, @".+\\").Value;
            }

        }


        public void ReadData(string fileName)
        {

            for (int i = 0; i < basesAll.Length; i++)
            {
                basesAll[i].ReadOnly = true;
                basesAll[i].Text = "";
                powersAll[i].ReadOnly = true;
                powersAll[i].Text = "";
            }
            textBox217.ReadOnly = true;
            textBox217.Text = "";

            DataContainer dataContainerNew = new DataContainer();
            dataContainerNew.DataReader(fileName);
            dataContainer = dataContainerNew;

            if (dataContainer.ACcode != null)
                textBox217.Text = dataContainer.ACcode;
            
            string[] values;
            if (dataContainer.Contain(keysAll[0].Text))
            {
                values = dataContainer.Import(keysAll[0].Text);
                if (values[0] != null) basesAll[0].Text = values[0];
            }
            for (int i = 1; i < keysAll.Length; i++)
                if (dataContainer.Contain(keysAll[i].Text))
                {
                    values = dataContainer.Import(keysAll[i].Text);
                    if(values[0] != null) basesAll[i].Text = values[0];
                    if (values[1] != null) powersAll[i].Text = values[1];
                }
            for (int i = 0; i < modifysAll.Length; i++)
            {
                modifysAll[i].Text = "Modify";
                modifysAll[i].Enabled = true;
                clickModifyFlag[i] = false;
            }

        }


        public string checkACcode(string acCode)
        {
            string errorInformation = null;
            if (!Regex.IsMatch(acCode, @"^[A-Za-z0-9]{1,4}$"))
                errorInformation = "\"A/C CODE\" input may be wrong.";
            return errorInformation;
        }

        public bool checkVstall(string[] keys, string[] bases, string[] powers)
        {
            double v_stall_CR = 0;
            double v_stall_IC = 0;
            double v_stall_TO = 0;
            double v_stall_AP = 0;
            double v_stall_LD = 0;
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i] == "Vstallcr")
                    v_stall_CR = double.Parse(bases[i])
                        * Math.Pow(10, double.Parse(powers[i]));
                else if (keys[i] == "Vstallic")
                    v_stall_IC = double.Parse(bases[i])
                        * Math.Pow(10, double.Parse(powers[i]));
                else if (keys[i] == "Vstallto")
                    v_stall_TO = double.Parse(bases[i])
                        * Math.Pow(10, double.Parse(powers[i]));
                else if (keys[i] == "Vstallap")
                    v_stall_AP = double.Parse(bases[i])
                        * Math.Pow(10, double.Parse(powers[i]));
                else if (keys[i] == "Vstallld")
                    v_stall_LD = double.Parse(bases[i])
                        * Math.Pow(10, double.Parse(powers[i]));
            }
            return v_stall_CR >= v_stall_IC && v_stall_IC >= v_stall_TO &&
                v_stall_TO >= v_stall_AP && v_stall_AP >= v_stall_LD;
        }

        public string checkh_desAndh_max_AP()
        {
            string errorInformation = null;

            int h_desIndex = 0;
            for (int i = 0; i < keysOPF.Length; i++)
                if (keysOPF[i].Text == "Desclevel")
                {
                    h_desIndex = i;
                    break;
                }
            int h_max_APIndex = 0;
            for (int i = 0; i < keysGPF.Length; i++)
                if (keysGPF[i].Text == "H_max_app")
                {
                    h_max_APIndex = i;
                    break;
                }

            double h_des = 0;
            if (basesOPF[h_desIndex].Text != "" && powersOPF[h_desIndex].Text != "")
                h_des = double.Parse(basesOPF[h_desIndex].Text) *
                    Math.Pow(10, double.Parse(powersOPF[h_desIndex].Text));
            double h_max_AP = 0;
            if (basesGPF[h_max_APIndex].Text != "" && powersGPF[h_max_APIndex].Text != "")
                h_max_AP = double.Parse(basesGPF[h_max_APIndex].Text) * Math.Pow(10, double.Parse(powersGPF[h_max_APIndex].Text));

            if (h_des != 0 && h_max_AP != 0)
                if (h_des < h_max_AP)
                    errorInformation = "\"Desclevel\" data input on OPF FILE or \"H_max_app\" on GPF FILE data input may be wrong. The former must no less than later one.";

            return errorInformation;
        }

        public string checkFormat(string[] keys, string[] bases, string[] powers)
        {
            bool checkOK = true;
            string errorInformation = null;
            for (int i = 0; i < keys.Length; i++)
            {
                if(keys[i] == "indexengine")
                {
                    checkOK = Regex.IsMatch(bases[i], @"[123]");
                    if (!checkOK)
                    {
                        errorInformation = "\"indexengine\" can only be 1, 2 or 3.";
                        break;
                    }
                } 
                else if(keys[i] == "tempgrad")
                {
                    checkOK = Regex.IsMatch(bases[i], @"^[-]0?\.\d{1,5}$") && 
                        Regex.IsMatch(powers[i], @"[+-][01]\d");
                    if (!checkOK)
                    {
                        errorInformation = "\"tempgrad\" data input may be wrong and it must less than 0.";
                        break;
                    }
                }
                else if(bases[i] == "" || powers[i] == "")
                {
                    errorInformation = $"\"{keys[i]}\" data input is missing.";
                    break;
                }
                else
                {
                    if(keys[i] == "ctc3" || keys[i] == "cf4")
                        checkOK = Regex.IsMatch(bases[i], @"^[-]?0?\.\d{1,5}$") &&
                        Regex.IsMatch(powers[i], @"[+-][01]\d");
                    else
                        checkOK = Regex.IsMatch(bases[i], @"^0?\.\d{1,5}$") &&
                            Regex.IsMatch(powers[i], @"[+-][01]\d");
                    if (!checkOK)
                    {
                        errorInformation = $"\"{keys[i]}\" data input may be wrong.";
                        break;
                    }
                    else if (keys[i] == "Vstallld")
                    {
                        int[] indexs = new int[5];
                        indexs[0] = i;
                        bool notFindAllvstall = true;
                        int k = 1;
                        for(int j = i; notFindAllvstall; j--)
                        {
                            if(keys[j] == "Vstallap" || keys[j] == "Vstallto" || 
                                keys[j] == "Vstallic")
                            {
                                indexs[k] = j;
                                k++;
                            }
                            else if(keys[j] == "Vstallcr")
                            {
                                indexs[k] = j;
                                notFindAllvstall = false;
                            }
                        }
                        string[] v_stall_keys = new string[5];
                        string[] v_stall_bases = new string[5];
                        string[] v_stall_powers = new string[5];
                        for (int j = 0; j < indexs.Length; j++)
                        {
                            v_stall_keys[j] = keys[indexs[j]];
                            v_stall_bases[j] = bases[indexs[j]];
                            v_stall_powers[j] = powers[indexs[j]];
                        }
                        if (!checkVstall(v_stall_keys, v_stall_bases, v_stall_powers))
                        {
                            errorInformation = "\"Vstallcr\", \"Vstallic\", \"Vstallto\", \"Vstallap\" and \"Vstallld\" data input may be wrong. The former v_stall must no less than the later one.";
                            break;
                        }
                    }
                    else if(keys[i] == "H_max_to")
                    {
                        double h_max_TO = double.Parse(bases[i]) 
                            * Math.Pow(10, double.Parse(powers[i]));
                        if(h_max_TO > 400)
                        {
                            errorInformation = "\"H_max_to\" must no more than 400 ft.";
                            break;
                        }
                    }
                    else if (keys[i] == "H_max_ic")
                    {
                        double h_max_IC = double.Parse(bases[i])
                            * Math.Pow(10, double.Parse(powers[i]));
                        if (h_max_IC > 2000)
                        {
                            errorInformation = "\"H_max_ic\" must no more than 2000 ft.";
                            break;
                        }
                    }
                    else if (keys[i] == "H_max_app")
                    {
                        double h_max_APP = double.Parse(bases[i])
                            * Math.Pow(10, double.Parse(powers[i]));
                        if (h_max_APP > 8000)
                        {
                            errorInformation = "\"H_max_app\" must no more than 8000 ft.";
                            break;
                        }
                    }
                    else if (keys[i] == "H_max_ld")
                    {
                        double h_max_LD = double.Parse(bases[i])
                            * Math.Pow(10, double.Parse(powers[i]));
                        if (h_max_LD > 3000)
                        {
                            errorInformation = "\"H_max_ld\" must no more than 3000 ft.";
                            break;
                        }
                    }
                }
            }
            return errorInformation;
        }


        public string FormatFill(string key, string bases)
        {
            switch (key)
            {
                case "indexengine":
                    break;
                case "tempgrad":
                case "ctc3":
                case "cf4":
                    if (bases.StartsWith("-"))
                    {
                        if (bases[1] != '0')
                            bases = bases.Insert(1, "0");
                        if (bases.Length != 8)
                        {
                            string fill0 = "";
                            for (int i = 0; i < 8 - bases.Length; i++)
                                fill0 += "0";
                            bases += fill0;
                        }
                    }
                    break;
                default:
                    if (bases[0] != '0')
                        bases = bases.Insert(0, "0");
                    if(bases.Length != 7)
                    {
                        string fill0 = "";
                        for (int i = 0; i < 7 - bases.Length; i++)
                            fill0 += "0";
                        bases += fill0;
                    }
                    break;
            }
            return bases;
        }



        private void modify1_Click(object sender, EventArgs e)
        {

            if (clickModifyFlag[0])
            {
                string[] keys = new string[keysOPF.Length];
                string[] bases = new string[keysOPF.Length];
                string[] powers = new string[keysOPF.Length];

                for(int i = 0; i < keysOPF.Length; i++)
                {
                    keys[i] = keysOPF[i].Text;
                    bases[i] = basesOPF[i].Text;
                    powers[i] = powersOPF[i].Text;
                }

                string errorInformation = checkFormat(keys, bases, powers);
                if (errorInformation == null && 
                    (errorInformation = checkh_desAndh_max_AP()) == null)
                {
                    for(int i = 0; i < keysOPF.Length; i++)
                    {
                        bases[i] = FormatFill(keys[i], bases[i]);
                        basesOPF[i].Text = bases[i];
                    }

                    if (dataContainer.Modify_Save(keys, bases, powers, keysAllString))
                    {
                        for (int i = 0; i < basesOPF.Length; i++)
                        {
                            basesOPF[i].ReadOnly = true;
                            powersOPF[i].ReadOnly = true;
                        }
                        clickModifyFlag[0] = false;
                        modify1.Text = "Modify";
                        MessageBox.Show("Data file has been saved.", 
                            "Successfully Saved", MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);
                    }
                }
                else
                    MessageBox.Show(errorInformation, "Input Error", MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
            }

            else
            {
                for (int i = 0; i < basesOPF.Length; i++)
                {
                    basesOPF[i].ReadOnly = false;
                    powersOPF[i].ReadOnly = false;
                }
                clickModifyFlag[0] = true;
                modify1.Text = "Save";
            }
        }

        private void modify2_Click(object sender, EventArgs e)
        {
            if (clickModifyFlag[1])
            {
                string[] keys = new string[keysAPF.Length];
                string[] bases = new string[keysAPF.Length];
                string[] powers = new string[keysAPF.Length];

                for (int i = 0; i < keysAPF.Length; i++)
                {
                    keys[i] = keysAPF[i].Text;
                    bases[i] = basesAPF[i].Text;
                    powers[i] = powersAPF[i].Text;
                }

                string errorInformation = checkFormat(keys, bases, powers);
                if (errorInformation == null)
                {
                    for (int i = 0; i < keysAPF.Length; i++)
                    {
                        bases[i] = FormatFill(keys[i], bases[i]);
                        basesAPF[i].Text = bases[i];
                    }

                    if (dataContainer.Modify_Save(keys, bases, powers, keysAllString))
                    {
                        for (int i = 0; i < basesAPF.Length; i++)
                        {
                            basesAPF[i].ReadOnly = true;
                            powersAPF[i].ReadOnly = true;
                        }
                        clickModifyFlag[1] = false;
                        modify2.Text = "Modify";
                        MessageBox.Show("Data file has been saved.", 
                            "Successfully Saved", MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);
                    }
                }
                else
                    MessageBox.Show(errorInformation,  "Input Error", MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
            }

            else
            {
                for (int i = 0; i < basesAPF.Length; i++)
                {
                    basesAPF[i].ReadOnly = false;
                    powersAPF[i].ReadOnly = false;
                }
                clickModifyFlag[1] = true;
                modify2.Text = "Save";
            }
        }

        private void modify3_Click(object sender, EventArgs e)
        {
            if (clickModifyFlag[2])
            {
                string[] keys = new string[keysGPF.Length];
                string[] bases = new string[keysGPF.Length];
                string[] powers = new string[keysGPF.Length];

                for (int i = 0; i < keysGPF.Length; i++)
                {
                    keys[i] = keysGPF[i].Text;
                    bases[i] = basesGPF[i].Text;
                    powers[i] = powersGPF[i].Text;
                }

                string errorInformation = checkFormat(keys, bases, powers);
                if (errorInformation == null && 
                    (errorInformation = checkh_desAndh_max_AP()) == null)
                {
                    for (int i = 0; i < keysGPF.Length; i++)
                    {
                        bases[i] = FormatFill(keys[i], bases[i]);
                        basesGPF[i].Text = bases[i];
                    }

                    if (dataContainer.Modify_Save(keys, bases, powers, keysAllString))
                    {
                        for (int i = 0; i < basesGPF.Length; i++)
                        {
                            basesGPF[i].ReadOnly = true;
                            powersGPF[i].ReadOnly = true;
                        }
                        clickModifyFlag[2] = false;
                        modify3.Text = "Modify";
                        MessageBox.Show("Data file has been saved.", 
                            "Successfully Saved", MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);
                    }
                }
                else
                    MessageBox.Show(errorInformation, "Input Error", MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
            }

            else
            {
                for (int i = 0; i < basesGPF.Length; i++)
                {
                    basesGPF[i].ReadOnly = false;
                    powersGPF[i].ReadOnly = false;
                }
                clickModifyFlag[2] = true;
                modify3.Text = "Save";
            }
        }


        private void modify4_Click(object sender, EventArgs e)
        {
            if (clickModifyFlag[3])
            {
                string errorInformation = checkACcode(textBox217.Text);
                if (errorInformation == null)
                {
                    dataContainer.SetACcode(textBox217.Text);
                    if (dataContainer.DataWriter(keysAllString))
                    {
                        textBox217.ReadOnly = true;
                        clickModifyFlag[3] = false;
                        modify4.Text = "Modify";
                        MessageBox.Show("A/C CODE has been saved.", 
                            "Successfully Saved", MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);
                    }
                }
                else
                    MessageBox.Show(errorInformation, "Input Error", MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
            }
            else
            {
                textBox217.ReadOnly = false;
                clickModifyFlag[3] = true;
                modify4.Text = "Save";
            }
        }

        private void LoadData_FormClosing (object sender, FormClosingEventArgs e)
        {
            DialogResult result = DialogResult.OK;
            for(int i = 0; i < clickModifyFlag.Length; i++)
                if (clickModifyFlag[i])
                {
                    result = MessageBox.Show("You have not saved the data file, are you sure leaving?", 
                        "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    break;
                }
            if (result == DialogResult.OK)
            {
                Startup startup = new Startup();
                startup.Show();
            }
            else
                e.Cancel = true;
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

        private void LoadData_Load(object sender, EventArgs e)
        {
            
            Label[] keys = {label1, label4, label8, label6, label16, label14, label12,
                label10, label32, label30, label28, label26, label24, label22, label20,
                label18, label38, label36, label34, label76, label74, label72, label70,
                label68, label66, label64, label62, label60, label58, label56, label54,
                label52, label50, label48, label46, label44, label42, label40, label114,
                label112, label110, label108, label106, label104, label102, label100,
                label98, label96, label94, label92, label90, label88, label86, label84,
                label82, label127, label125, label123, label121, label119, label117,
                label115, label79, label77, label165, label163, label161, label159,
                label157, label155, label153, label151, label149, label147, label145,
                label143, label141, label139, label137, label135, label133, label131,
                label129, label203, label201, label199, label197, label195, label193,
                label191, label189, label187, label185, label183, label181, label179,
                label177, label175, label173, label171, label169, label167, label215,
                label213, label211, label209, label207, label205 };

            TextBox[] bases = {textBox1, textBox4, textBox8, textBox6, textBox16, textBox14,
                textBox12, textBox10, textBox32, textBox30, textBox28, textBox26, textBox24,
                textBox22, textBox20, textBox18, textBox38, textBox36, textBox34, textBox76,
                textBox74, textBox72, textBox70, textBox68, textBox66, textBox64, textBox62,
                textBox60, textBox58, textBox56, textBox54, textBox52, textBox50, textBox48,
                textBox46, textBox44, textBox42, textBox40, textBox114, textBox112,
                textBox110, textBox108, textBox106, textBox104, textBox102,
                textBox100, textBox98, textBox96, textBox94, textBox92,
                textBox90, textBox88, textBox86, textBox84, textBox82,
                textBox127, textBox125, textBox123, textBox121, textBox119,
                textBox117, textBox115, textBox79, textBox77, textBox165,
                textBox163, textBox161, textBox159, textBox157, textBox155,
                textBox153, textBox151, textBox149, textBox147, textBox145,
                textBox143, textBox141, textBox139, textBox137, textBox135,
                textBox133, textBox131, textBox129, textBox203, textBox201,
                textBox199, textBox197, textBox195, textBox193, textBox191,
                textBox189, textBox187, textBox185, textBox183, textBox181,
                textBox179, textBox177, textBox175, textBox173, textBox171,
                textBox169, textBox167, textBox215, textBox213, textBox211,
                textBox209, textBox207, textBox205 };

            TextBox[] powers = {textBox216, textBox3, textBox7, textBox5, textBox15,
                textBox13, textBox11, textBox9, textBox31, textBox29, textBox27,
                textBox25, textBox23, textBox21, textBox19, textBox17, textBox37,
                textBox35, textBox33, textBox75, textBox73, textBox71, textBox69,
                textBox67, textBox65, textBox63, textBox61, textBox59, textBox57,
                textBox55, textBox53, textBox51, textBox49, textBox47, textBox45,
                textBox43, textBox41, textBox39, textBox113, textBox111, textBox109,
                textBox107, textBox105, textBox103, textBox101, textBox99, textBox97,
                textBox95, textBox93, textBox91, textBox89, textBox87, textBox85,
                textBox83, textBox81, textBox126, textBox124, textBox122, textBox120,
                textBox118, textBox116, textBox80, textBox78, textBox2, textBox164,
                textBox162, textBox160, textBox158, textBox156, textBox154,
                textBox152, textBox150, textBox148, textBox146, textBox144,
                textBox142, textBox140, textBox138, textBox136, textBox134,
                textBox132, textBox130, textBox128, textBox202, textBox200,
                textBox198, textBox196, textBox194, textBox192, textBox190,
                textBox188, textBox186, textBox184, textBox182, textBox180,
                textBox178, textBox176, textBox174, textBox172, textBox170,
                textBox168, textBox166, textBox214, textBox212, textBox210,
                textBox206, textBox208, textBox204 };

            for(int i = 0; i < keys.Length; i++)
            {
                keysAll[i] = keys[i];
                keysAllString[i] = keys[i].Text;
                basesAll[i] = bases[i];
                powersAll[i] = powers[i];
            }
            for (int i = 0; i < keysOPF.Length; i++)
            {
                keysOPF[i] = keysAll[i];
                basesOPF[i] = basesAll[i];
                powersOPF[i] = powersAll[i];
            }
            for(int i = keysOPF.Length, j = 0; i < keysOPF.Length + keysAPF.Length; i++, j++)
            {
                keysAPF[j] = keysAll[i];
                basesAPF[j] = basesAll[i];
                powersAPF[j] = powersAll[i];
            }
            for(int i = keysOPF.Length + keysAPF.Length, j = 0; i < keysAll.Length; i++, j++)
            {
                keysGPF[j] = keysAll[i];
                basesGPF[j] = basesAll[i];
                powersGPF[j] = powersAll[i];
            }

            Button[] modifys = { modify1, modify2, modify3, modify4 };
            for (int i = 0; i < modifys.Length; i++)
                modifysAll[i] = modifys[i];

            label216.Text = System.Environment.CurrentDirectory;

            Button[] buttonsAll = { modify1, modify2, modify3, modify4, loadFile };
            SetButtonsFocus(buttonsAll);
        }
    }
}
