using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AircraftModel;
using AtmosphereModel;

namespace project1_mod1_outwindow
{
    public partial class Form1 : Form
    {
        TabControl[] tabs;
        List<TabControl> movedTabs = new List<TabControl>();
        int currentFrontTabIndex;
        int frontTabX;
        int frontTabY;

        System.Drawing.Font axisFont;
        System.Drawing.Font labelFont;
        System.Drawing.Font legendFont;

        string path;
        string fileName;
        Aircraft A306;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabs = new TabControl[] { tabControl1, tabControl2, tabControl3, tabControl4, tabControl5, tabControl6, tabControl7 };
            for (int i = 0; i < tabs.Count(); i++)
                movedTabs.Add(tabs[i]);
            currentFrontTabIndex = 6;
            frontTabX = tabs[currentFrontTabIndex].Location.X;
            frontTabY = tabs[currentFrontTabIndex].Location.Y;


            path = System.Environment.CurrentDirectory;
            fileName = $@"{path}\A306.txt";
            A306 = new Aircraft(fileName);

            axisFont = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelFont = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legendFont = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
        
        private void moveTabs(int newFrontTabIndex)
        {
            int oldFrontTabIndex = currentFrontTabIndex;
            currentFrontTabIndex = newFrontTabIndex;
            Point oldFrontTabPoint = new Point(tabs[oldFrontTabIndex].Location.X, tabs[oldFrontTabIndex].Location.Y);
            Point currentFrontTabPoint = new Point(tabs[currentFrontTabIndex].Location.X, tabs[currentFrontTabIndex].Location.Y);
            tabs[oldFrontTabIndex].Location = currentFrontTabPoint;
            tabs[currentFrontTabIndex].Location = oldFrontTabPoint;

            int movedTabsOldFrontTabIndex = movedTabs.IndexOf(tabs[oldFrontTabIndex]);
            int movedTabsCurrentFrontTabIndex = movedTabs.IndexOf(tabs[currentFrontTabIndex]);
            var temp = movedTabs[movedTabsOldFrontTabIndex];
            movedTabs[movedTabsOldFrontTabIndex] = movedTabs[movedTabsCurrentFrontTabIndex];
            movedTabs[movedTabsCurrentFrontTabIndex] = temp;
            for (int i = 0; i < movedTabs.Count; i++)
                movedTabs[i].BringToFront();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            moveTabs(6);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            moveTabs(5);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            moveTabs(4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            moveTabs(3);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            moveTabs(2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            moveTabs(1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            moveTabs(0);
        }

        // 极曲线
        private void button9_Click(object sender, EventArgs e)
        {
            List<double> c_L = new List<double>();
            List<double> c_D = new List<double>();
            List<double> k = new List<double>();

            for (double C_L = 0; C_L < 2; C_L += 0.01)
            {
                c_L.Add(C_L);
                double C_D = A306.C_D0_CR + A306.C_D2_CR * C_L * C_L;
                c_D.Add(C_D);
                double K = C_L / C_D;
                k.Add(K);
            }

            int index = k.IndexOf(k.Max());
            List<double> c_D_k = new List<double>() { 0, c_D[index], c_D[index] * 2 };
            List<double> c_L_k = new List<double>() { 0, c_L[index], c_L[index] * 2 };
            

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 0.02;
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart1.ChartAreas[0].AxisX.Title = "Drag Coefficient";
            chart1.ChartAreas[0].AxisX.TitleFont = labelFont;
            chart1.ChartAreas[0].AxisY.LabelStyle.Interval = 0.2;
            chart1.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart1.ChartAreas[0].AxisY.Title = "Lift Coefficient";
            chart1.ChartAreas[0].AxisY.TitleFont = labelFont;
            chart1.Series[0].Points.DataBindXY(c_D, c_L);

            chart1.Series[4].Points.DataBindXY(c_D_k, c_L_k);
        }

        // 阻力图，可以100ft往上加
        private void button10_Click(object sender, EventArgs e)
        {
            List<double> tas = new List<double>();
            List<double> d = new List<double>();
            List<double> d0 = new List<double>();
            List<double> di = new List<double>();

            double h = 18000;
            double vmin = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);

            for (double TAS
                = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(vmin * 0.9));
                TAS < Units.kt2mps(520); TAS++)
            {
                tas.Add(TAS);
                double CAS = AtmosphereEnviroment.Get_CAS(h, TAS);
                double D = A306.Get_D(h, CAS, Aircraft.FlightPhase.Cruise) / 10000;
                double D0 = 0.5 * A306.C_D0_CR * AtmosphereEnviroment.Get_rho(h) * A306.S * TAS * TAS / 10000;
                double Di = 2 * A306.C_D2_CR * (A306.m_ref * AtmosphereEnviroment.g) * (A306.m_ref * AtmosphereEnviroment.g) / (AtmosphereEnviroment.Get_rho(h) * A306.S) / (TAS * TAS) / 10000;
                d.Add(D);

                d0.Add(D0);
                di.Add(Di);
            }


            for (int i = 0; i < tas.Count; i++)
                tas[i] = Units.mps2kt(tas[i]);

            chart2.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
            chart2.ChartAreas[0].AxisX.LabelStyle.Interval = 50;
            chart2.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart2.ChartAreas[0].AxisX.Title = "TAS kt";
            chart2.ChartAreas[0].AxisX.TitleFont = labelFont;
            chart2.ChartAreas[0].AxisY.LabelStyle.Interval = 2;
            chart2.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart2.ChartAreas[0].AxisY.Title = "Drag 10000 N";
            chart2.ChartAreas[0].AxisY.TitleFont = labelFont;


            chart2.Series[0].Points.DataBindXY(tas, d);

            chart2.Series[1].Points.DataBindXY(tas, d0);
            chart2.Series[2].Points.DataBindXY(tas, di);

            chart2.Legends[0].Enabled = true;
            chart2.Legends[0].Font = legendFont;
            chart2.Legends[0].Position = new System.Windows.Forms.DataVisualization.Charting.ElementPosition(65, 0, 20, 18);
            foreach (var series in chart2.Series)
                series.IsVisibleInLegend = false;
            chart2.Series[0].IsVisibleInLegend = true;
            chart2.Series[0].LegendText = "Total drag";
            chart2.Series[1].IsVisibleInLegend = true;
            chart2.Series[1].LegendText = "Parasite drag";
            chart2.Series[2].IsVisibleInLegend = true;
            chart2.Series[2].LegendText = "Induced drag";
        }
        
        // 所需推力随重量，可以100ft往上加
        private void button11_Click_1(object sender, EventArgs e)
        {
            List<double> tas = new List<double>();
            List<double> t = new List<double>();

            List<double> t1 = new List<double>();
            List<double> t2 = new List<double>();


            double h = 18000;

            double vmin = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);


            for (double TAS
                = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(vmin));
                TAS < Units.kt2mps(1000); TAS++)
            {
                tas.Add(TAS);
                double CAS = AtmosphereEnviroment.Get_CAS(h, TAS);
                double T = A306.Get_T(h, CAS, Aircraft.FlightPhase.Cruise) / 1000;

                t.Add(T);

                double T1 = A306.Get_T(h, CAS, Aircraft.FlightPhase.Cruise, m:A306.m_ref + A306.m_ref * 0.4) / 1000;
                double T2 = A306.Get_T(h, CAS, Aircraft.FlightPhase.Cruise, m: A306.m_ref + A306.m_ref * 0.8) / 1000;
                t1.Add(T1);
                t2.Add(T2);
            }


            for (int i = 0; i < tas.Count; i++)
                tas[i] = Units.mps2kt(tas[i]);

            
            chart3.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
            chart3.ChartAreas[0].AxisX.LabelStyle.Interval = 50;
            chart3.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart3.ChartAreas[0].AxisX.Title = "TAS kt";
            chart3.ChartAreas[0].AxisX.TitleFont = labelFont;
            chart3.ChartAreas[0].AxisY.LabelStyle.Interval = 50;
            chart3.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart3.ChartAreas[0].AxisY.Title = "Required Thrust 10000 N";
            chart3.ChartAreas[0].AxisY.TitleFont = labelFont;

            chart3.Series[0].Points.DataBindXY(tas, t);
            chart3.Series[1].Points.DataBindXY(tas, t1);
            chart3.Series[2].Points.DataBindXY(tas, t2);

            chart3.Legends[0].Enabled = true;
            chart3.Legends[0].Font = legendFont;
            chart3.Legends[0].Position = new System.Windows.Forms.DataVisualization.Charting.ElementPosition(60, 5, 20, 18);
            foreach (var series in chart3.Series)
                series.IsVisibleInLegend = false;
            chart3.Series[0].IsVisibleInLegend = true;
            chart3.Series[0].LegendText = "m = m_ref";
            chart3.Series[1].IsVisibleInLegend = true;
            chart3.Series[1].LegendText = "m = 1.4 m_ref";
            chart3.Series[2].IsVisibleInLegend = true;
            chart3.Series[2].LegendText = "m = 1.8 m_ref";
        }
        
        // 所需推力随高度，可以设定h1往上加
        private void button12_Click(object sender, EventArgs e)
        {
            List<double> tas = new List<double>();
            List<double> t = new List<double>();
            List<double> tas1 = new List<double>();
            List<double> t1 = new List<double>();
            List<double> tas2 = new List<double>();
            List<double> t2 = new List<double>();



            double h = 15000;
            double h1 = 24000;
            double h2 = 33000;

            double vmin = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);

            double vmin1 = A306.Get_v_stall(h1, Aircraft.FlightPhase.Cruise);
            double vmin2 = A306.Get_v_stall(h2, Aircraft.FlightPhase.Cruise);

            for (double TAS
                = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(vmin));
                TAS < Units.kt2mps(1000); TAS++)
            {
                tas.Add(TAS);
                double CAS = AtmosphereEnviroment.Get_CAS(h, TAS);
                double T = A306.Get_T(h, CAS, Aircraft.FlightPhase.Cruise) / 10000;
                t.Add(T);
            }

            for (double TAS
                = AtmosphereEnviroment.Get_TAS(h1, Units.kt2mps(vmin1));
                TAS < Units.kt2mps(1000); TAS++)
            {
                tas1.Add(TAS);
                double CAS = AtmosphereEnviroment.Get_CAS(h1, TAS);
                double T = A306.Get_T(h1, CAS, Aircraft.FlightPhase.Cruise) / 10000;
                t1.Add(T);
            }

            for (double TAS
                = AtmosphereEnviroment.Get_TAS(h2, Units.kt2mps(vmin2));
                TAS < Units.kt2mps(1000); TAS++)
            {
                tas2.Add(TAS);
                double CAS = AtmosphereEnviroment.Get_CAS(h2, TAS);
                double T = A306.Get_T(h2, CAS, Aircraft.FlightPhase.Cruise) / 10000;
                t2.Add(T);
            }


            for (int i = 0; i < tas.Count; i++)
            {
                tas[i] = Units.mps2kt(tas[i]);
                if (i < tas1.Count)
                    tas1[i] = Units.mps2kt(tas1[i]);
                if (i < tas2.Count)
                    tas2[i] = Units.mps2kt(tas2[i]);
            }


            chart4.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.1);
            chart4.ChartAreas[0].AxisX.LabelStyle.Interval = 50;
            chart4.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart4.ChartAreas[0].AxisX.Title = "TAS kt";
            chart4.ChartAreas[0].AxisX.TitleFont = labelFont;
            chart4.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart4.ChartAreas[0].AxisY.Title = "Required Thrust 10000 N";
            chart4.ChartAreas[0].AxisY.TitleFont = labelFont;

            chart4.Series[0].Points.DataBindXY(tas, t);
            chart4.Series[1].Points.DataBindXY(tas1, t1);
            chart4.Series[2].Points.DataBindXY(tas2, t2);

            chart4.Legends[0].Enabled = true;
            chart4.Legends[0].Font = legendFont;
            chart4.Legends[0].Position = new System.Windows.Forms.DataVisualization.Charting.ElementPosition(60, 5, 20, 18);
            foreach (var series in chart4.Series)
                series.IsVisibleInLegend = false;
            chart4.Series[0].IsVisibleInLegend = true;
            chart4.Series[0].LegendText = "h = 15000 ft";
            chart4.Series[1].IsVisibleInLegend = true;
            chart4.Series[1].LegendText = "h = 24000 ft";
            chart4.Series[2].IsVisibleInLegend = true;
            chart4.Series[2].LegendText = "h = 33000 ft";
        }

        // 可用推力随高度，可以设置h1以100ft增加
        private void button13_Click(object sender, EventArgs e)
        {
            List<double> tas = new List<double>();
            List<double> tmax = new List<double>();
            List<double> tas1 = new List<double>();
            List<double> tmax1 = new List<double>();
            List<double> tas2 = new List<double>();
            List<double> tmax2 = new List<double>();



            double h = 15000;
            double h1 = 24000;
            double h2 = 33000;

            double vmin = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);

            double vmin1 = A306.Get_v_stall(h1, Aircraft.FlightPhase.Cruise);
            double vmin2 = A306.Get_v_stall(h2, Aircraft.FlightPhase.Cruise);

            for (double TAS
                = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(vmin));
                TAS < Units.kt2mps(1000); TAS++)
            {
                tas.Add(TAS);
                double CAS = AtmosphereEnviroment.Get_CAS(h, TAS);
                double Tmax = A306.Get_T_max_cruise(h, CAS) / 10000;
                tmax.Add(Tmax);
            }

            for (double TAS
                = AtmosphereEnviroment.Get_TAS(h1, Units.kt2mps(vmin1));
                TAS < Units.kt2mps(1000); TAS++)
            {
                tas1.Add(TAS);
                double CAS = AtmosphereEnviroment.Get_CAS(h1, TAS);
                double Tmax = A306.Get_T_max_cruise(h1, CAS) / 10000;
                tmax1.Add(Tmax);
            }

            for (double TAS
                = AtmosphereEnviroment.Get_TAS(h2, Units.kt2mps(vmin2));
                TAS < Units.kt2mps(1000); TAS++)
            {
                tas2.Add(TAS);
                double CAS = AtmosphereEnviroment.Get_CAS(h2, TAS);
                double Tmax = A306.Get_T_max_cruise(h2, CAS) / 10000;
                tmax2.Add(Tmax);
            }


            for (int i = 0; i < tas.Count; i++)
            {
                tas[i] = Units.mps2kt(tas[i]);
                if (i < tas1.Count)
                    tas1[i] = Units.mps2kt(tas1[i]);
                if (i < tas2.Count)
                    tas2[i] = Units.mps2kt(tas2[i]);
            }

            chart5.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
            chart5.ChartAreas[0].AxisX.LabelStyle.Interval = 50;
            chart5.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart5.ChartAreas[0].AxisX.Title = "TAS kt";
            chart5.ChartAreas[0].AxisX.TitleFont = labelFont;
            chart5.ChartAreas[0].AxisY.LabelStyle.Interval = 2;
            chart5.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart5.ChartAreas[0].AxisY.Title = "Available Thrust 10000 N";
            chart5.ChartAreas[0].AxisY.TitleFont = labelFont;

            chart5.Series[0].Points.DataBindXY(tas, tmax);
            chart5.Series[1].Points.DataBindXY(tas1, tmax1);
            chart5.Series[2].Points.DataBindXY(tas2, tmax2);

            chart5.Legends[0].Enabled = true;
            chart5.Legends[0].Font = legendFont;
            chart5.Legends[0].Position = new System.Windows.Forms.DataVisualization.Charting.ElementPosition(75, 60, 20, 18);
            foreach (var series in chart5.Series)
                series.IsVisibleInLegend = false;
            chart5.Series[0].IsVisibleInLegend = true;
            chart5.Series[0].LegendText = "h = 15000 ft";
            chart5.Series[1].IsVisibleInLegend = true;
            chart5.Series[1].LegendText = "h = 24000 ft";
            chart5.Series[2].IsVisibleInLegend = true;
            chart5.Series[2].LegendText = "h = 33000 ft";
        }

        // 推力图：久航与远航速度
        private void button14_Click(object sender, EventArgs e)
        {
            List<double> tas = new List<double>();
            List<double> t = new List<double>();
            List<double> slope = new List<double>();

            double h = 18000;
            double vmin = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);


            for (double TAS
                = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(vmin));
                TAS < Units.kt2mps(1000); TAS++)
            {
                double TAS1 = Units.mps2kt(TAS);
                tas.Add(TAS1);
                double CAS = AtmosphereEnviroment.Get_CAS(h, TAS);
                double T = A306.Get_T(h, CAS, Aircraft.FlightPhase.Cruise) / 10000;
                t.Add(T);

                double Slope = T / TAS;
                slope.Add(Slope);
            }

            int indexOfV_e = t.IndexOf(t.Min());
            int indexOfV_MRC = slope.IndexOf(slope.Min());

            List<double> V_e_tas = new List<double>() { 0, tas[indexOfV_e], tas[indexOfV_e] * 1.5 };
            List<double> V_e_t = new List<double>() { t[indexOfV_e], t[indexOfV_e], t[indexOfV_e] };

            List<double> V_MRC_tas = new List<double>() { 0, tas[indexOfV_MRC], tas[indexOfV_MRC] * 1.5 };
            List<double> V_MRC_t = new List<double>() { 0, t[indexOfV_MRC], t[indexOfV_MRC] * 1.5 };


            chart6.ChartAreas[0].AxisX.Minimum = 0;
            chart6.ChartAreas[0].AxisX.LabelStyle.Interval = 100;
            chart6.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart6.ChartAreas[0].AxisX.Title = "TAS kt";
            chart6.ChartAreas[0].AxisX.TitleFont = labelFont;
            chart6.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart6.ChartAreas[0].AxisY.Title = "Thrust 10000 N";
            chart6.ChartAreas[0].AxisY.TitleFont = labelFont;

            chart6.Series[0].Points.DataBindXY(tas, t);

            chart6.Series[5].Points.DataBindXY(V_e_tas, V_e_t);
            chart6.Series[6].Points.DataBindXY(V_MRC_tas, V_MRC_t);
        }

        // 剩余推力图：陡升速度
        private void button15_Click(object sender, EventArgs e)
        {
            List<double> tas = new List<double>();
            List<double> t = new List<double>();
            List<double> tmax = new List<double>();
            List<double> redundant_t = new List<double>();

            double h = 28000;
            double vmin = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);


            for (double TAS
                = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(vmin));
                TAS < Units.kt2mps(1000); TAS++)
            {
                tas.Add(TAS);
                double CAS = AtmosphereEnviroment.Get_CAS(h, TAS);
                double T = A306.Get_T(h, CAS, Aircraft.FlightPhase.Cruise);
                double Tmax = A306.Get_T_max_cruise(h, CAS);
                double redundantT = (Tmax - T) / 10000;

                t.Add(T);
                tmax.Add(Tmax);
                redundant_t.Add(redundantT);
            }


            for (int i = 0; i < tas.Count; i++)
                if (redundant_t[i] < 0)
                {
                    tas.Remove(tas[i]);
                    redundant_t.Remove(redundant_t[i]);
                    i--;
                }


            // Note, to display yAxis value, if redundant thrust <= 50000 but > 10000, 
            // let redundant_t element * 10, thus unit in yAxis is 1000N; 
            // if redundant thrust <= 10000, let redundant_t element * 100, 
            // thus unit in yAxis is 100N

            if (redundant_t.Max() <= 1)
                for (int i = 0; i < redundant_t.Count; i++)
                    redundant_t[i] *= 100;
            else if (redundant_t.Max() <= 5)
                for (int i = 0; i < redundant_t.Count; i++)
                    redundant_t[i] *= 10;

            for (int i = 0; i < tas.Count; i++)
                tas[i] = Units.mps2kt(tas[i]);

            int indexOfV_e = redundant_t.IndexOf(redundant_t.Max());
            List<double> V_e_tas = new List<double>() { 0, tas[indexOfV_e] };
            List<double> V_e_redundantT = new List<double>() { redundant_t[indexOfV_e], redundant_t[indexOfV_e] };



            chart7.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.2);
            chart7.ChartAreas[0].AxisX.LabelStyle.Interval = 50;
            chart7.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart7.ChartAreas[0].AxisX.Title = "TAS kt";
            chart7.ChartAreas[0].AxisX.TitleFont = labelFont;
            chart7.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart7.ChartAreas[0].AxisY.Title = "Redundant Thrust 1000 N";
            chart7.ChartAreas[0].AxisY.TitleFont = labelFont;

            chart7.Series[0].Points.DataBindXY(tas, redundant_t);
            chart7.Series[4].Points.DataBindXY(V_e_tas, V_e_redundantT);
        }

        // 所需功率与可用功率图
        private void button16_Click(object sender, EventArgs e)
        {
            List<double> tas = new List<double>();
            List<double> w = new List<double>();
            List<double> wmax = new List<double>();


            double h = 18000;
            double vmin = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);


            for (double TAS
                = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(vmin));
                TAS < Units.kt2mps(1000); TAS++)
            {
                tas.Add(TAS);
                double CAS = AtmosphereEnviroment.Get_CAS(h, TAS);
                double T = A306.Get_T(h, CAS, Aircraft.FlightPhase.Cruise) / 10000;
                double W = T * TAS / 1000;
                double Tmax = A306.Get_T_max_cruise(h, CAS) / 10000;
                double Wmax = Tmax * TAS / 1000;

                w.Add(W);
                wmax.Add(Wmax);
            }


            for (int i = 0; i < tas.Count; i++)
                tas[i] = Units.mps2kt(tas[i]);

            chart8.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
            chart8.ChartAreas[0].AxisX.LabelStyle.Interval = 50;
            chart8.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart8.ChartAreas[0].AxisX.Title = "TAS kt";
            chart8.ChartAreas[0].AxisX.TitleFont = labelFont;
            chart8.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart8.ChartAreas[0].AxisY.Title = "Power 1000 W";
            chart8.ChartAreas[0].AxisY.TitleFont = labelFont;

            chart8.Series[0].Points.DataBindXY(tas, w);
            chart8.Series[1].Points.DataBindXY(tas, wmax);

            chart8.Legends[0].Enabled = true;
            chart8.Legends[0].Font = legendFont;
            chart8.Legends[0].Position = new System.Windows.Forms.DataVisualization.Charting.ElementPosition(65, 0, 20, 12);
            foreach (var series in chart8.Series)
                series.IsVisibleInLegend = false;
            chart8.Series[0].IsVisibleInLegend = true;
            chart8.Series[0].LegendText = "Required power";
            chart8.Series[1].IsVisibleInLegend = true;
            chart8.Series[1].LegendText = "Available power";
        }

        // 所需推力与所需功率中有利速度
        private void button17_Click(object sender, EventArgs e)
        {
            List<double> tas = new List<double>();
            List<double> t = new List<double>();
            List<double> w = new List<double>();
            List<double> slope = new List<double>();

            double h = 32000;
            double vmin = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);


            for (double TAS
                = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(vmin));
                TAS < Units.kt2mps(700); TAS++)
            {
                double TAS1 = Units.mps2kt(TAS);
                tas.Add(TAS1);
                double CAS = AtmosphereEnviroment.Get_CAS(h, TAS);
                double T = A306.Get_T(h, CAS, Aircraft.FlightPhase.Cruise) / 10000;
                double W = T * TAS / 100;
                double Slope = W / TAS;

                t.Add(T);
                w.Add(W);
                slope.Add(Slope);
            }

            int indexOfV_eFromThrust = t.IndexOf(t.Min());
            int indexOfV_eFromPower = slope.IndexOf(slope.Min());

            List<double> V_e_FromThrust_tas = new List<double>() { 0, tas[indexOfV_eFromThrust], tas[indexOfV_eFromThrust] * 1.5 };
            List<double> V_e_FromThrust_t = new List<double>() { t[indexOfV_eFromThrust], t[indexOfV_eFromThrust], t[indexOfV_eFromThrust] };

            List<double> V_e_FromPower_tas = new List<double>() { 0, tas[indexOfV_eFromPower], tas[indexOfV_eFromPower] * 1.5 };
            List<double> V_e_FromPower_power = new List<double>() { 0, w[indexOfV_eFromPower], w[indexOfV_eFromPower] * 1.5 };

            List<double> V_e_tas = new List<double>() { tas[indexOfV_eFromThrust], tas[indexOfV_eFromPower], tas[indexOfV_eFromPower] };
            List<double> V_e_ThrustPower = new List<double>() { 0, t[indexOfV_eFromThrust], w[indexOfV_eFromPower] };


            chart9.ChartAreas[0].AxisX.Minimum = 0;
            chart9.ChartAreas[0].AxisX.LabelStyle.Interval = 50;
            chart9.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart9.ChartAreas[0].AxisX.Title = "TAS kt";
            chart9.ChartAreas[0].AxisX.TitleFont = labelFont;
            chart9.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart9.ChartAreas[0].AxisY.Title = "Required Thrust 10000 N, Required Power 100 W";
            chart9.ChartAreas[0].AxisY.TitleFont = labelFont;
            

            chart9.Series[0].Points.DataBindXY(tas, t);
            chart9.Series[2].Points.DataBindXY(tas, w);
            chart9.Series[4].Points.DataBindXY(V_e_FromThrust_tas, V_e_FromThrust_t);
            chart9.Series[5].Points.DataBindXY(V_e_FromPower_tas, V_e_FromPower_power);
            chart9.Series[6].Points.DataBindXY(V_e_tas, V_e_ThrustPower);

            chart9.Legends[0].Enabled = true;
            chart9.Legends[0].Font = legendFont;
            chart9.Legends[0].Position = new System.Windows.Forms.DataVisualization.Charting.ElementPosition(65, 0, 20, 12);
            foreach (var series in chart9.Series)
                series.IsVisibleInLegend = false;
            chart9.Series[0].IsVisibleInLegend = true;
            chart9.Series[0].LegendText = "Required thrust";
            chart9.Series[2].IsVisibleInLegend = true;
            chart9.Series[2].LegendText = "Required power";
        }

        // 剩余功率图：快升与陡升速度
        private void button18_Click(object sender, EventArgs e)
        {
            List<double> tas = new List<double>();
            List<double> w = new List<double>();
            List<double> wmax = new List<double>();
            List<double> redundant_w = new List<double>();
            List<double> slope = new List<double>();

            double h = 18000;
            double vmin = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);


            for (double TAS
                = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(vmin));
                TAS < Units.kt2mps(1000); TAS++)
            {
                double TAS1 = Units.mps2kt(TAS);
                tas.Add(TAS1);
                double CAS = AtmosphereEnviroment.Get_CAS(h, TAS);
                double T = A306.Get_T(h, CAS, Aircraft.FlightPhase.Cruise) / 10000;
                double W = T * TAS;
                double Tmax = A306.Get_T_max_cruise(h, CAS) / 10000;
                double Wmax = Tmax * TAS;
                double redundantW = (Wmax - W) / 1000;

                w.Add(W);
                wmax.Add(Wmax);
                redundant_w.Add(redundantW);
            }


            for (int i = 0; i < tas.Count; i++)
                if (redundant_w[i] < 0)
                {
                    tas.Remove(tas[i]);
                    redundant_w.Remove(redundant_w[i]);
                    i--;
                }


            if (redundant_w.Max() <= 1)
                for (int i = 0; i < redundant_w.Count; i++)
                    redundant_w[i] *= 100;
            else if (redundant_w.Max() <= 5)
                for (int i = 0; i < redundant_w.Count; i++)
                    redundant_w[i] *= 10;


            for (int i = 0; i < redundant_w.Count; i++)
            {
                double Slope = redundant_w[i] / tas[i];
                slope.Add(Slope);
            }


            int indexOfV_e = slope.IndexOf(slope.Max());
            int indexOfV_fast_climb = redundant_w.IndexOf(redundant_w.Max());

            List<double> v_e_tas = new List<double>() { 0, tas[indexOfV_e], tas[indexOfV_e] * 1.1 };
            List<double> v_e_redundantW = new List<double>() { 0, redundant_w[indexOfV_e], redundant_w[indexOfV_e] * 1.1 };

            List<double> v_fast_climb_tas = new List<double>() { 0, tas[indexOfV_fast_climb] };
            List<double> v_fast_climb_redundantW = new List<double>() { redundant_w[indexOfV_fast_climb], redundant_w[indexOfV_fast_climb] };




            chart10.ChartAreas[0].AxisX.Minimum = 0;
            chart10.ChartAreas[0].AxisX.LabelStyle.Interval = 50;
            chart10.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart10.ChartAreas[0].AxisX.Title = "TAS kt";
            chart10.ChartAreas[0].AxisX.TitleFont = labelFont;
            chart10.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart10.ChartAreas[0].AxisY.Title = "Redundant Power 100 W";
            chart10.ChartAreas[0].AxisY.TitleFont = labelFont;

            chart10.Series[0].Points.DataBindXY(tas, redundant_w);
            chart10.Series[4].Points.DataBindXY(v_e_tas, v_e_redundantW);
            chart10.Series[5].Points.DataBindXY(v_fast_climb_tas, v_fast_climb_redundantW);
        }

        // 推力线左右交点
        private void button19_Click(object sender, EventArgs e)
        {
            List<double> tas = new List<double>();
            List<double> t = new List<double>();
            List<double> tmax = new List<double>();
            List<double> tas1 = new List<double>();
            List<double> t1 = new List<double>();
            List<double> tmax1 = new List<double>();



            double h = 15000;
            double h1 = 32000;

            double vmin = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);

            double vmin1 = A306.Get_v_stall(h1, Aircraft.FlightPhase.Cruise);

            for (double TAS
                = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(vmin));
                TAS < Units.kt2mps(1000); TAS++)
            {
                tas.Add(TAS);
                double CAS = AtmosphereEnviroment.Get_CAS(h, TAS);
                double T = A306.Get_T(h, CAS, Aircraft.FlightPhase.Cruise) / 10000;
                double Tmax = A306.Get_T_max_cruise(h, CAS) / 10000;
                t.Add(T);
                tmax.Add(Tmax);
            }

            for (double TAS
                = AtmosphereEnviroment.Get_TAS(h1, Units.kt2mps(vmin1));
                TAS < Units.kt2mps(1000); TAS++)
            {
                tas1.Add(TAS);
                double CAS = AtmosphereEnviroment.Get_CAS(h1, TAS);
                double T = A306.Get_T(h1, CAS, Aircraft.FlightPhase.Cruise) / 10000;
                double Tmax = A306.Get_T_max_cruise(h1, CAS) / 10000;
                t1.Add(T);
                tmax1.Add(Tmax);
            }


            for (int i = 0; i < tas.Count; i++)
            {
                tas[i] = Units.mps2kt(tas[i]);
                if (i < tas1.Count)
                    tas1[i] = Units.mps2kt(tas1[i]);
            }
                

            chart11.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
            chart11.ChartAreas[0].AxisX.LabelStyle.Interval = 50;
            chart11.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart11.ChartAreas[0].AxisX.Title = "TAS kt";
            chart11.ChartAreas[0].AxisX.TitleFont = labelFont;
            chart11.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart11.ChartAreas[0].AxisY.Title = "Required Thrust 10000 N";
            chart11.ChartAreas[0].AxisY.TitleFont = labelFont;



            chart11.Series[0].Points.DataBindXY(tas, t);
            chart11.Series[1].Points.DataBindXY(tas, tmax);
            chart11.Series[4].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chart11.Series[4].BorderWidth = 1;
            chart11.Series[6].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chart11.Series[6].BorderWidth = 1;
            chart11.Series[4].Points.DataBindXY(tas1, t1);
            chart11.Series[6].Points.DataBindXY(tas1, tmax1);


            chart11.Legends[0].Enabled = true;
            chart11.Legends[0].Font = legendFont;
            chart11.Legends[0].Position = new System.Windows.Forms.DataVisualization.Charting.ElementPosition(60, 0, 26, 24);
            foreach (var series in chart11.Series)
                series.IsVisibleInLegend = false;
            chart11.Series[0].IsVisibleInLegend = true;
            chart11.Series[0].LegendText = "h = 15000 ft required thrust";
            chart11.Series[1].IsVisibleInLegend = true;
            chart11.Series[1].LegendText = "h = 15000 ft available thrust";
            chart11.Series[4].IsVisibleInLegend = true;
            chart11.Series[4].LegendText = "h = 32000 ft required thrust";
            chart11.Series[6].IsVisibleInLegend = true;
            chart11.Series[6].LegendText = "h = 32000 ft available thrust";
        }

        // 抖振边界成因
        private void button20_Click(object sender, EventArgs e)
        {
            List<double> m1 = new List<double>();
            List<double> c_l1 = new List<double>();
            List<double> m2 = new List<double>();
            List<double> c_l2 = new List<double>();

            List<double> UBO_Data_M = new List<double>() { 0.2, 0.28, 0.36, 0.42, 0.46, 0.5, 0.54, 0.58,
                0.61, 0.63, 0.65, 0.67, 0.69, 0.71, 0.73, 0.75, 0.77, 0.79, 0.81, 0.82 };
            List<double> UBO_Data_C_L_max = new List<double>() { 1.3540, 1.2769, 1.1999, 1.1416, 1.1031,
                1.0646, 1.0261, 0.9876, 0.9606, 0.9450, 0.9325, 0.9221, 0.9127, 0.9013, 0.8877,
                0.8669, 0.8367, 0.7899, 0.7233, 0.6796 };
            while (UBO_Data_M[0] > 0.01)
            {
                double insertM = UBO_Data_M[0] - 0.01;
                UBO_Data_M.Insert(0, insertM);
                double C_L_max = UBO_Data_C_L_max[0] + 0.01 * (UBO_Data_C_L_max[0] - UBO_Data_C_L_max[1]) /
                    (UBO_Data_M[1] - UBO_Data_M[0]);
                UBO_Data_C_L_max.Insert(0, C_L_max);
            }
            while (UBO_Data_M[UBO_Data_M.Count - 1] < 1.3)
            {
                double insertM = UBO_Data_M[UBO_Data_M.Count - 1] + 0.01;
                double C_L_max = UBO_Data_C_L_max[UBO_Data_C_L_max.Count - 1] + 0.01 *
                    (UBO_Data_C_L_max[UBO_Data_C_L_max.Count - 1] - UBO_Data_C_L_max[UBO_Data_C_L_max.Count - 2]) /
                    (UBO_Data_M[UBO_Data_M.Count - 1] - UBO_Data_M[UBO_Data_M.Count - 2]);
                if (C_L_max <= 0) break;
                UBO_Data_M.Add(insertM);
                UBO_Data_C_L_max.Add(C_L_max);
            }

            double factor = 0.98;
            for (int i = 0; i < UBO_Data_C_L_max.Count; i++)
                UBO_Data_C_L_max[i] *= factor;

            double h1 = 15000;
            double h2 = 30000;

            double CAS_min = A306.Get_v_stall(h1, Aircraft.FlightPhase.Climb);
            double TAS_min = AtmosphereEnviroment.Get_TAS(h1, Units.kt2mps(CAS_min));
            double initialM = TAS_min / AtmosphereEnviroment.Get_a(h1);
            int C_L_InitialMIndex = 0;
            foreach (double M in UBO_Data_M)
                if (M > initialM)
                {
                    C_L_InitialMIndex = UBO_Data_M.IndexOf(M) - 1;
                    break;
                }
            int removeCount = 0;
            while (removeCount < C_L_InitialMIndex)
            {
                UBO_Data_M.Remove(UBO_Data_M[0]);
                UBO_Data_C_L_max.Remove(UBO_Data_C_L_max[0]);
                removeCount++;
            }

            foreach (double M in UBO_Data_M)
            {
                m1.Add(M);

                double TAS = M * AtmosphereEnviroment.Get_a(h1);
                double CAS = AtmosphereEnviroment.Get_CAS(h1, TAS);
                double C_L = A306.Get_C_L(h1, CAS);
                c_l1.Add(C_L);

                TAS = M * AtmosphereEnviroment.Get_a(h2);
                CAS = AtmosphereEnviroment.Get_CAS(h2, TAS);
                C_L = A306.Get_C_L(h2, CAS);
                if (C_L <= c_l1.Max())
                {
                    m2.Add(M);
                    c_l2.Add(C_L);
                }
            }

            chart12.ChartAreas[0].AxisX.Minimum = (int)((UBO_Data_M[0] - UBO_Data_M[0] * 0.1) * 10) * 0.1;
            chart12.ChartAreas[0].AxisX.Maximum = (int)((UBO_Data_M[UBO_Data_M.Count - 1] + UBO_Data_M[UBO_Data_M.Count - 1] * 0.1) * 10) * 0.1;
            chart12.ChartAreas[0].AxisX.LabelStyle.Interval = 0.1;
            chart12.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart12.ChartAreas[0].AxisX.Title = "M";
            chart12.ChartAreas[0].AxisX.TitleFont = labelFont;

            double axisYMax = Math.Max(UBO_Data_C_L_max.Max(), c_l1.Max());
            chart12.ChartAreas[0].AxisY.Maximum = (int)((axisYMax + axisYMax * 0.1) * 10 / 2) * 0.2;
            chart12.ChartAreas[0].AxisY.LabelStyle.Interval = 0.2;
            chart12.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart12.ChartAreas[0].AxisY.Title = "CL";
            chart12.ChartAreas[0].AxisY.TitleFont = labelFont;

            chart12.Series[0].Points.DataBindXY(UBO_Data_M, UBO_Data_C_L_max);
            chart12.Series[1].Points.DataBindXY(m1, c_l1);
            chart12.Series[2].Points.DataBindXY(m2, c_l2);

            chart12.Legends[0].Enabled = true;
            chart12.Legends[0].Font = legendFont;
            chart12.Legends[0].Position = new System.Windows.Forms.DataVisualization.Charting.ElementPosition(65, 0, 26, 18);
            foreach (var series in chart12.Series)
                series.IsVisibleInLegend = false;
            chart12.Series[0].IsVisibleInLegend = true;
            chart12.Series[0].LegendText = "High speed buffet limit";
            chart12.Series[1].IsVisibleInLegend = true;
            chart12.Series[1].LegendText = "h = 15000 ft CL - M";
            chart12.Series[2].IsVisibleInLegend = true;
            chart12.Series[2].LegendText = "h = 30000 ft CL - M";
        }


        public double Get_High_Buffet_M(string fileName, List<double> UBO_Data_M,
            List<double> UBO_Data_C_L_max, double h, double m)
        {
            Aircraft A306 = new Aircraft(fileName);
            double EPS = 0.001;

            double upperM = UBO_Data_M[UBO_Data_M.Count - 1], lowerM = upperM;
            for (int i = UBO_Data_M.Count - 1; i >= 0; i--)
            {
                double TAS = UBO_Data_M[i] * AtmosphereEnviroment.Get_a(h);
                double CAS = AtmosphereEnviroment.Get_CAS(h, TAS);
                double C_L = A306.Get_C_L(h, CAS, m: m);
                if (UBO_Data_C_L_max[i] >= C_L && i + 1 < UBO_Data_M.Count)
                {
                    lowerM = UBO_Data_M[i];
                    upperM = UBO_Data_M[i + 1];
                    break;
                }
            }
            int lowerMIndex = UBO_Data_M.IndexOf(lowerM);
            int upperMIndex = UBO_Data_M.IndexOf(upperM);
            double UBO_M;
            for (UBO_M = lowerM; UBO_M <= upperM; UBO_M += 0.0001)
            {
                double C_L_max = UBO_Data_C_L_max[lowerMIndex] + (UBO_Data_C_L_max[upperMIndex] -
                    UBO_Data_C_L_max[lowerMIndex]) / (upperM - lowerM) * (UBO_M - lowerM);

                double TAS = UBO_M * AtmosphereEnviroment.Get_a(h);
                double CAS = AtmosphereEnviroment.Get_CAS(h, TAS);
                double C_L = A306.Get_C_L(h, CAS, m: m);
                if (Math.Abs(C_L_max - C_L) < EPS) break;
            }
            return UBO_M;
        }


        // 高度抖振包线
        private void button21_Click(object sender, EventArgs e)
        {
            List<double> alt1 = new List<double>();
            List<double> lbo_m1 = new List<double>();
            List<double> ubo_m1 = new List<double>();
            List<double> alt2 = new List<double>();
            List<double> lbo_m2 = new List<double>();
            List<double> ubo_m2 = new List<double>();
            List<double> alt3 = new List<double>();
            List<double> lbo_m3 = new List<double>();
            List<double> ubo_m3 = new List<double>();

            double m_low = A306.m_ref - (A306.m_ref - A306.m_min) * 0.2;
            double m_middle = A306.m_ref + (A306.m_max - A306.m_ref) * 0.1;
            double m_high = A306.m_ref + (A306.m_max - A306.m_ref) * 0.5;


            List<double> UBO_Data_M = new List<double>() { 0.2, 0.28, 0.36, 0.42, 0.46, 0.5, 0.54,
                0.58, 0.61, 0.63, 0.65, 0.67, 0.69, 0.71, 0.73, 0.75, 0.77, 0.79, 0.81, 0.82 };
            List<double> UBO_Data_C_L_max = new List<double>() { 1.3540, 1.2769, 1.1999, 1.1416,
                1.1031, 1.0646, 1.0261, 0.9876, 0.9606, 0.9450, 0.9325, 0.9221, 0.9127, 0.9013,
                0.8877, 0.8669, 0.8367, 0.7899, 0.7233, 0.6796 };
            while (UBO_Data_M[0] > 0.01)
            {
                double insertM = UBO_Data_M[0] - 0.01;
                UBO_Data_M.Insert(0, insertM);
                double C_L_max = UBO_Data_C_L_max[0] + 0.01 * (UBO_Data_C_L_max[0] -
                    UBO_Data_C_L_max[1]) / (UBO_Data_M[1] - UBO_Data_M[0]);
                UBO_Data_C_L_max.Insert(0, C_L_max);
            }
            while (UBO_Data_M[UBO_Data_M.Count - 1] < 1.3)
            {
                double insertM = UBO_Data_M[UBO_Data_M.Count - 1] + 0.01;
                double C_L_max = UBO_Data_C_L_max[UBO_Data_C_L_max.Count - 1] + 0.01 *
                    (UBO_Data_C_L_max[UBO_Data_C_L_max.Count - 1] -
                    UBO_Data_C_L_max[UBO_Data_C_L_max.Count - 2]) / (UBO_Data_M[UBO_Data_M.Count -
                    1] - UBO_Data_M[UBO_Data_M.Count - 2]);
                if (C_L_max <= 0) break;
                UBO_Data_M.Add(insertM);
                UBO_Data_C_L_max.Add(C_L_max);
            }

            double factor = 0.98;

            for (int i = 0; i < UBO_Data_C_L_max.Count; i++)
                UBO_Data_C_L_max[i] *= factor;

            double h_ceiling = 0;
            for (double h = 38000; ; h++)
            {
                int comparePosition;
                for (comparePosition = 0; comparePosition < UBO_Data_M.Count; comparePosition++)
                {
                    double TAS = UBO_Data_M[comparePosition] * AtmosphereEnviroment.Get_a(h);
                    double CAS = AtmosphereEnviroment.Get_CAS(h, TAS);
                    double C_L = A306.Get_C_L(h, CAS, m: m_low);
                    if (C_L < UBO_Data_C_L_max[comparePosition]) break;
                }
                if (comparePosition == UBO_Data_M.Count)
                {
                    h_ceiling = --h;
                    break;
                }
            }
            double h_ceiling_boundary = (int)(h_ceiling / 1000) * 1000;

            for (double h = 15000; h <= h_ceiling_boundary; h += 1000)
            {
                alt1.Add(h);
                lbo_m1.Add(A306.Get_Low_Buffet_M(h, m: m_low, factor: factor));
                ubo_m1.Add(Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max, h, m_low));
            }

            double h_ceiling_middle = alt1[alt1.Count - 1] + (h_ceiling - alt1[alt1.Count - 1]) / 1.3;
            alt1.Add(h_ceiling_middle);
            lbo_m1.Add(A306.Get_Low_Buffet_M(h_ceiling_middle, m: m_low, factor: factor));
            ubo_m1.Add(Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max, h_ceiling_middle, m_low));

            alt1.Add(h_ceiling);
            double buffetM_h_ceiling = A306.Get_Low_Buffet_M(h_ceiling, m: m_low, factor: factor);
            lbo_m1.Add(buffetM_h_ceiling);
            ubo_m1.Add(buffetM_h_ceiling);



            for (double h = 38000; ; h++)
            {
                int comparePosition;
                for (comparePosition = 0; comparePosition < UBO_Data_M.Count; comparePosition++)
                {
                    double TAS = UBO_Data_M[comparePosition] * AtmosphereEnviroment.Get_a(h);
                    double CAS = AtmosphereEnviroment.Get_CAS(h, TAS);
                    double C_L = A306.Get_C_L(h, CAS, m: m_middle);
                    if (C_L < UBO_Data_C_L_max[comparePosition]) break;
                }
                if (comparePosition == UBO_Data_M.Count)
                {
                    h_ceiling = --h;
                    break;
                }
            }
            h_ceiling_boundary = (int)(h_ceiling / 1000) * 1000;

            for (double h = 15000; h <= h_ceiling_boundary; h += 1000)
            {
                alt2.Add(h);
                lbo_m2.Add(A306.Get_Low_Buffet_M(h, m: m_middle, factor: factor));
                ubo_m2.Add(Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max, h, m_middle));
            }

            h_ceiling_middle = alt2[alt2.Count - 1] + (h_ceiling - alt2[alt2.Count - 1]) / 1.3;
            alt2.Add(h_ceiling_middle);
            lbo_m2.Add(A306.Get_Low_Buffet_M(h_ceiling_middle, m: m_middle, factor: factor));
            ubo_m2.Add(Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max, h_ceiling_middle, m_middle));

            alt2.Add(h_ceiling);
            buffetM_h_ceiling = A306.Get_Low_Buffet_M(h_ceiling, m: m_middle, factor: factor);
            lbo_m2.Add(buffetM_h_ceiling);
            ubo_m2.Add(buffetM_h_ceiling);



            for (double h = 38000; ; h++)
            {
                int comparePosition;
                for (comparePosition = 0; comparePosition < UBO_Data_M.Count; comparePosition++)
                {
                    double TAS = UBO_Data_M[comparePosition] * AtmosphereEnviroment.Get_a(h);
                    double CAS = AtmosphereEnviroment.Get_CAS(h, TAS);
                    double C_L = A306.Get_C_L(h, CAS, m: m_high);
                    if (C_L < UBO_Data_C_L_max[comparePosition]) break;
                }
                if (comparePosition == UBO_Data_M.Count)
                {
                    h_ceiling = --h;
                    break;
                }
            }
            h_ceiling_boundary = (int)(h_ceiling / 1000) * 1000;

            for (double h = 15000; h <= h_ceiling_boundary; h += 1000)
            {
                alt3.Add(h);
                lbo_m3.Add(A306.Get_Low_Buffet_M(h, m: m_high, factor: factor));
                ubo_m3.Add(Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max, h, m_high));
            }

            h_ceiling_middle = alt3[alt3.Count - 1] + (h_ceiling - alt3[alt3.Count - 1]) / 1.3;
            alt3.Add(h_ceiling_middle);
            lbo_m3.Add(A306.Get_Low_Buffet_M(h_ceiling_middle, m: m_high, factor: factor));
            ubo_m3.Add(Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max, h_ceiling_middle, m_high));

            alt3.Add(h_ceiling);
            buffetM_h_ceiling = A306.Get_Low_Buffet_M(h_ceiling, m: m_high, factor: factor);
            lbo_m3.Add(buffetM_h_ceiling);
            ubo_m3.Add(buffetM_h_ceiling);

            for (int i = 0; i < alt1.Count; i++)
            {
                alt1[i] /= 100;
                if (i < alt2.Count)
                    alt2[i] /= 100;
                if (i < alt3.Count)
                    alt3[i] /= 100;
            }

            chart13.ChartAreas[0].AxisX.Minimum = (int)((lbo_m1[0] - lbo_m1[0] * 0.05) * 20) * 0.05;
            chart13.ChartAreas[0].AxisX.Maximum = (int)((ubo_m1[0] + ubo_m1[0] * 0.1) * 10) * 0.1;
            chart13.ChartAreas[0].AxisX.LabelStyle.Interval = 0.05;
            chart13.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart13.ChartAreas[0].AxisX.Title = "M";
            chart13.ChartAreas[0].AxisX.TitleFont = labelFont;

            chart13.ChartAreas[0].AxisY.Minimum = alt1[0];
            chart13.ChartAreas[0].AxisY.Maximum = (int)((alt1[alt1.Count - 1] + alt1[alt1.Count - 1] * 0.05) / 10) * 10;
            chart13.ChartAreas[0].AxisY.LabelStyle.Interval = 20;
            chart13.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart13.ChartAreas[0].AxisY.Title = "Altitude 100 ft";
            chart13.ChartAreas[0].AxisY.TitleFont = labelFont;

            chart13.Series[0].Points.DataBindXY(lbo_m1, alt1);
            chart13.Series[1].Points.DataBindXY(ubo_m1, alt1);
            chart13.Series[2].Points.DataBindXY(lbo_m2, alt2);
            chart13.Series[3].Points.DataBindXY(ubo_m2, alt2);
            chart13.Series[7].Points.DataBindXY(lbo_m3, alt3);
            chart13.Series[8].Points.DataBindXY(ubo_m3, alt3);

            chart13.Legends[0].Enabled = true;
            chart13.Legends[0].Font = legendFont;
            chart13.Legends[0].Position = new System.Windows.Forms.DataVisualization.Charting.ElementPosition(13, 0, 30, 18);
            foreach (var series in chart13.Series)
                series.IsVisibleInLegend = false;
            chart13.Series[0].IsVisibleInLegend = true;
            chart13.Series[0].LegendText = "m_ref - 0.2 (m_ref - m_min)";
            chart13.Series[1].IsVisibleInLegend = true;
            chart13.Series[1].LegendText = "m_ref + 0.1 (m_max - m_ref)";
            chart13.Series[2].IsVisibleInLegend = true;
            chart13.Series[2].LegendText = "m_ref + 0.5 (m_max - m_ref)";
        }

        // 重量抖振包线（未剪切）
        private void button23_Click(object sender, EventArgs e)
        {
            foreach (var series in chart14.Series)
                series.Points.Clear();


            List<double> w1 = new List<double>();
            List<double> lbo_m1 = new List<double>();
            List<double> ubo_m1 = new List<double>();
            List<double> w2 = new List<double>();
            List<double> lbo_m2 = new List<double>();
            List<double> ubo_m2 = new List<double>();
            List<double> w3 = new List<double>();
            List<double> lbo_m3 = new List<double>();
            List<double> ubo_m3 = new List<double>();

            double h_low = 33000;
            double h_middle = 35000;
            double h_high = 37000;


            List<double> UBO_Data_M = new List<double>() { 0.2, 0.28, 0.36, 0.42, 0.46, 0.5, 0.54,
                0.58, 0.61, 0.63, 0.65, 0.67, 0.69, 0.71, 0.73, 0.75, 0.77, 0.79, 0.81, 0.82 };
            List<double> UBO_Data_C_L_max = new List<double>() { 1.3540, 1.2769, 1.1999, 1.1416,
                1.1031, 1.0646, 1.0261, 0.9876, 0.9606, 0.9450, 0.9325, 0.9221, 0.9127, 0.9013,
                0.8877, 0.8669, 0.8367, 0.7899, 0.7233, 0.6796 };
            while (UBO_Data_M[0] > 0.01)
            {
                double insertM = UBO_Data_M[0] - 0.01;
                UBO_Data_M.Insert(0, insertM);
                double C_L_max = UBO_Data_C_L_max[0] + 0.01 * (UBO_Data_C_L_max[0] -
                    UBO_Data_C_L_max[1]) / (UBO_Data_M[1] - UBO_Data_M[0]);
                UBO_Data_C_L_max.Insert(0, C_L_max);
            }
            while (UBO_Data_M[UBO_Data_M.Count - 1] < 1.3)
            {
                double insertM = UBO_Data_M[UBO_Data_M.Count - 1] + 0.01;
                double C_L_max = UBO_Data_C_L_max[UBO_Data_C_L_max.Count - 1] + 0.01 *
                    (UBO_Data_C_L_max[UBO_Data_C_L_max.Count - 1] -
                    UBO_Data_C_L_max[UBO_Data_C_L_max.Count - 2]) / (UBO_Data_M[UBO_Data_M.Count -
                    1] - UBO_Data_M[UBO_Data_M.Count - 2]);
                if (C_L_max <= 0) break;
                UBO_Data_M.Add(insertM);
                UBO_Data_C_L_max.Add(C_L_max);
            }

            double factor = 0.98;

            for (int i = 0; i < UBO_Data_C_L_max.Count; i++)
                UBO_Data_C_L_max[i] *= factor;

            double W_ceiling = 0;
            for (double W = A306.m_max; ; W += 1000)
            {
                int comparePosition;
                for (comparePosition = 0; comparePosition < UBO_Data_M.Count; comparePosition++)
                {
                    double TAS = UBO_Data_M[comparePosition] * AtmosphereEnviroment.Get_a(h_low);
                    double CAS = AtmosphereEnviroment.Get_CAS(h_low, TAS);
                    double C_L = A306.Get_C_L(h_low, CAS, m: W);
                    if (C_L < UBO_Data_C_L_max[comparePosition]) break;
                }
                if (comparePosition == UBO_Data_M.Count)
                {
                    W_ceiling = W - 200;
                    break;
                }
            }
            double W_ceiling_boundary = (int)(W_ceiling / 10000) * 10000;

            for (double W = A306.m_min; W <= W_ceiling_boundary; W += 10000)
            {
                w1.Add(W);
                lbo_m1.Add(A306.Get_Low_Buffet_M(h_low, m: W, factor: factor));
                ubo_m1.Add(Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max, h_low, W));
            }

            double W_ceiling_middle = w1[w1.Count - 1] + (W_ceiling - w1[w1.Count - 1]) / 1.3;
            w1.Add(W_ceiling_middle);
            lbo_m1.Add(A306.Get_Low_Buffet_M(h_low, m: W_ceiling_middle, factor: factor));
            ubo_m1.Add(Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max, h_low,
                W_ceiling_middle));

            w1.Add(W_ceiling);
            double buffetM_W_ceiling = A306.Get_Low_Buffet_M(h_low, m: W_ceiling, factor: factor);
            lbo_m1.Add(buffetM_W_ceiling);
            ubo_m1.Add(buffetM_W_ceiling);



            for (double W = A306.m_max; ; W += 1000)
            {
                int comparePosition;
                for (comparePosition = 0; comparePosition < UBO_Data_M.Count; comparePosition++)
                {
                    double TAS = UBO_Data_M[comparePosition] * AtmosphereEnviroment.Get_a(h_middle);
                    double CAS = AtmosphereEnviroment.Get_CAS(h_middle, TAS);
                    double C_L = A306.Get_C_L(h_middle, CAS, m: W);
                    if (C_L < UBO_Data_C_L_max[comparePosition]) break;
                }
                if (comparePosition == UBO_Data_M.Count)
                {
                    W_ceiling = W - 500;
                    break;
                }
            }
            W_ceiling_boundary = (int)(W_ceiling / 10000) * 10000;

            for (double W = A306.m_min; W <= W_ceiling_boundary; W += 10000)
            {
                w2.Add(W);
                lbo_m2.Add(A306.Get_Low_Buffet_M(h_middle, m: W, factor: factor));
                ubo_m2.Add(Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max, h_middle, W));
            }

            W_ceiling_middle = w2[w2.Count - 1] + (W_ceiling - w2[w2.Count - 1]) / 1.3;
            w2.Add(W_ceiling_middle);
            lbo_m2.Add(A306.Get_Low_Buffet_M(h_middle, m: W_ceiling_middle, factor: factor));
            ubo_m2.Add(Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max, h_middle,
                W_ceiling_middle));

            w2.Add(W_ceiling);
            buffetM_W_ceiling = A306.Get_Low_Buffet_M(h_middle, m: W_ceiling, factor: factor);
            lbo_m2.Add(buffetM_W_ceiling);
            ubo_m2.Add(buffetM_W_ceiling);



            for (double W = A306.m_max; ; W += 1000)
            {
                int comparePosition;
                for (comparePosition = 0; comparePosition < UBO_Data_M.Count; comparePosition++)
                {
                    double TAS = UBO_Data_M[comparePosition] * AtmosphereEnviroment.Get_a(h_high);
                    double CAS = AtmosphereEnviroment.Get_CAS(h_high, TAS);
                    double C_L = A306.Get_C_L(h_high, CAS, m: W);
                    if (C_L < UBO_Data_C_L_max[comparePosition]) break;
                }
                if (comparePosition == UBO_Data_M.Count)
                {
                    W_ceiling = W - 200;
                    break;
                }
            }
            W_ceiling_boundary = (int)(W_ceiling / 10000) * 10000;

            for (double W = A306.m_min; W <= W_ceiling_boundary; W += 10000)
            {
                w3.Add(W);
                lbo_m3.Add(A306.Get_Low_Buffet_M(h_high, m: W, factor: factor));
                ubo_m3.Add(Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max, h_high, W));
            }

            W_ceiling_middle = w3[w3.Count - 1] + (W_ceiling - w3[w3.Count - 1]) / 1.3;
            w3.Add(W_ceiling_middle);
            lbo_m3.Add(A306.Get_Low_Buffet_M(h_high, m: W_ceiling_middle, factor: factor));
            ubo_m3.Add(Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max, h_high,
                W_ceiling_middle));

            w3.Add(W_ceiling);
            buffetM_W_ceiling = A306.Get_Low_Buffet_M(h_high, m: W_ceiling, factor: factor);
            lbo_m3.Add(buffetM_W_ceiling);
            ubo_m3.Add(buffetM_W_ceiling);


            for (int i = 0; i < w1.Count; i++)
                w1[i] /= 1000;
            for (int i = 0; i < w2.Count; i++)
                w2[i] /= 1000;
            for (int i = 0; i < w3.Count; i++)
                w3[i] /= 1000;


            List<double> M_MO_m = new List<double>() { A306.M_MO, A306.M_MO };
            List<double> M_MO_w = new List<double>() { 0, w1[w1.Count - 1] + w1[w1.Count - 1] * 0.01 };


            chart14.ChartAreas[0].AxisX.Minimum = (int)((lbo_m1[0] - lbo_m1[0] * 0.05) * 20) * 0.05;
            chart14.ChartAreas[0].AxisX.Maximum = (int)((ubo_m1[0] + ubo_m1[0] * 0.1) * 10) * 0.1;
            chart14.ChartAreas[0].AxisX.LabelStyle.Interval = 0.05;
            chart14.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart14.ChartAreas[0].AxisX.Title = "M";
            chart14.ChartAreas[0].AxisX.TitleFont = labelFont;

            chart14.ChartAreas[0].AxisY.Minimum = w1[0];
            chart14.ChartAreas[0].AxisY.Maximum = (int)((w1[w1.Count - 1] + w1[w1.Count - 1] * 0.1) / 10) * 10;
            chart14.ChartAreas[0].AxisY.LabelStyle.Interval = 20;
            chart14.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart14.ChartAreas[0].AxisY.Title = "Mass ton";
            chart14.ChartAreas[0].AxisY.TitleFont = labelFont;

            chart14.Series[0].Points.DataBindXY(lbo_m1, w1);
            chart14.Series[1].Points.DataBindXY(ubo_m1, w1);
            chart14.Series[2].Points.DataBindXY(lbo_m2, w2);
            chart14.Series[3].Points.DataBindXY(ubo_m2, w2);
            chart14.Series[7].Points.DataBindXY(lbo_m3, w3);
            chart14.Series[8].Points.DataBindXY(ubo_m3, w3);
            chart14.Series[9].Points.DataBindXY(M_MO_m, M_MO_w);

            chart14.Legends[0].Enabled = true;
            chart14.Legends[0].Font = legendFont;
            chart14.Legends[0].Position = new System.Windows.Forms.DataVisualization.Charting.ElementPosition(78, 0, 14, 18);
            foreach (var series in chart14.Series)
                series.IsVisibleInLegend = false;
            chart14.Series[0].IsVisibleInLegend = true;
            chart14.Series[0].LegendText = "FL330";
            chart14.Series[1].IsVisibleInLegend = true;
            chart14.Series[1].LegendText = "FL350";
            chart14.Series[2].IsVisibleInLegend = true;
            chart14.Series[2].LegendText = "FL370";
        }

        // 重量抖振包线（剪切）
        private void button22_Click(object sender, EventArgs e)
        {
            foreach (var series in chart14.Series)
                series.Points.Clear();


            List<double> w1 = new List<double>();
            List<double> lbo_m1 = new List<double>();
            List<double> ubo_m1 = new List<double>();
            List<double> w2 = new List<double>();
            List<double> lbo_m2 = new List<double>();
            List<double> ubo_m2 = new List<double>();
            List<double> w3 = new List<double>();
            List<double> lbo_m3 = new List<double>();
            List<double> ubo_m3 = new List<double>();

            double h_low = 33000;
            double h_middle = 35000;
            double h_high = 37000;


            List<double> UBO_Data_M = new List<double>() { 0.2, 0.28, 0.36, 0.42, 0.46, 0.5, 0.54,
                0.58, 0.61, 0.63, 0.65, 0.67, 0.69, 0.71, 0.73, 0.75, 0.77, 0.79, 0.81, 0.82 };
            List<double> UBO_Data_C_L_max = new List<double>() { 1.3540, 1.2769, 1.1999, 1.1416,
                1.1031, 1.0646, 1.0261, 0.9876, 0.9606, 0.9450, 0.9325, 0.9221, 0.9127, 0.9013,
                0.8877, 0.8669, 0.8367, 0.7899, 0.7233, 0.6796 };
            while (UBO_Data_M[0] > 0.01)
            {
                double insertM = UBO_Data_M[0] - 0.01;
                UBO_Data_M.Insert(0, insertM);
                double C_L_max = UBO_Data_C_L_max[0] + 0.01 * (UBO_Data_C_L_max[0] -
                    UBO_Data_C_L_max[1]) / (UBO_Data_M[1] - UBO_Data_M[0]);
                UBO_Data_C_L_max.Insert(0, C_L_max);
            }
            while (UBO_Data_M[UBO_Data_M.Count - 1] < 1.3)
            {
                double insertM = UBO_Data_M[UBO_Data_M.Count - 1] + 0.01;
                double C_L_max = UBO_Data_C_L_max[UBO_Data_C_L_max.Count - 1] + 0.01 *
                    (UBO_Data_C_L_max[UBO_Data_C_L_max.Count - 1] -
                    UBO_Data_C_L_max[UBO_Data_C_L_max.Count - 2]) / (UBO_Data_M[UBO_Data_M.Count -
                    1] - UBO_Data_M[UBO_Data_M.Count - 2]);
                if (C_L_max <= 0) break;
                UBO_Data_M.Add(insertM);
                UBO_Data_C_L_max.Add(C_L_max);
            }

            double factor = 0.98;

            for (int i = 0; i < UBO_Data_C_L_max.Count; i++)
                UBO_Data_C_L_max[i] *= factor;

            double W_ceiling = 0;
            for (double W = A306.m_max; ; W += 1000)
            {
                int comparePosition;
                for (comparePosition = 0; comparePosition < UBO_Data_M.Count; comparePosition++)
                {
                    double TAS = UBO_Data_M[comparePosition] * AtmosphereEnviroment.Get_a(h_low);
                    double CAS = AtmosphereEnviroment.Get_CAS(h_low, TAS);
                    double C_L = A306.Get_C_L(h_low, CAS, m: W);
                    if (C_L < UBO_Data_C_L_max[comparePosition]) break;
                }
                if (comparePosition == UBO_Data_M.Count)
                {
                    W_ceiling = W - 200;
                    break;
                }
            }
            double W_ceiling_boundary = (int)(W_ceiling / 10000) * 10000;

            for (double W = A306.m_min; W <= W_ceiling_boundary; W += 10000)
            {
                w1.Add(W);
                lbo_m1.Add(A306.Get_Low_Buffet_M(h_low, m: W, factor: factor));
                ubo_m1.Add(Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max, h_low, W));
            }

            double W_ceiling_middle = w1[w1.Count - 1] + (W_ceiling - w1[w1.Count - 1]) / 1.3;
            w1.Add(W_ceiling_middle);
            lbo_m1.Add(A306.Get_Low_Buffet_M(h_low, m: W_ceiling_middle, factor: factor));
            ubo_m1.Add(Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max, h_low,
                W_ceiling_middle));

            w1.Add(W_ceiling);
            double buffetM_W_ceiling = A306.Get_Low_Buffet_M(h_low, m: W_ceiling, factor: factor);
            lbo_m1.Add(buffetM_W_ceiling);
            ubo_m1.Add(buffetM_W_ceiling);



            for (double W = A306.m_max; ; W += 1000)
            {
                int comparePosition;
                for (comparePosition = 0; comparePosition < UBO_Data_M.Count; comparePosition++)
                {
                    double TAS = UBO_Data_M[comparePosition] * AtmosphereEnviroment.Get_a(h_middle);
                    double CAS = AtmosphereEnviroment.Get_CAS(h_middle, TAS);
                    double C_L = A306.Get_C_L(h_middle, CAS, m: W);
                    if (C_L < UBO_Data_C_L_max[comparePosition]) break;
                }
                if (comparePosition == UBO_Data_M.Count)
                {
                    W_ceiling = W - 500;
                    break;
                }
            }
            W_ceiling_boundary = (int)(W_ceiling / 10000) * 10000;

            for (double W = A306.m_min; W <= W_ceiling_boundary; W += 10000)
            {
                w2.Add(W);
                lbo_m2.Add(A306.Get_Low_Buffet_M(h_middle, m: W, factor: factor));
                ubo_m2.Add(Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max, h_middle, W));
            }

            W_ceiling_middle = w2[w2.Count - 1] + (W_ceiling - w2[w2.Count - 1]) / 1.3;
            w2.Add(W_ceiling_middle);
            lbo_m2.Add(A306.Get_Low_Buffet_M(h_middle, m: W_ceiling_middle, factor: factor));
            ubo_m2.Add(Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max, h_middle,
                W_ceiling_middle));

            w2.Add(W_ceiling);
            buffetM_W_ceiling = A306.Get_Low_Buffet_M(h_middle, m: W_ceiling, factor: factor);
            lbo_m2.Add(buffetM_W_ceiling);
            ubo_m2.Add(buffetM_W_ceiling);



            for (double W = A306.m_max; ; W += 1000)
            {
                int comparePosition;
                for (comparePosition = 0; comparePosition < UBO_Data_M.Count; comparePosition++)
                {
                    double TAS = UBO_Data_M[comparePosition] * AtmosphereEnviroment.Get_a(h_high);
                    double CAS = AtmosphereEnviroment.Get_CAS(h_high, TAS);
                    double C_L = A306.Get_C_L(h_high, CAS, m: W);
                    if (C_L < UBO_Data_C_L_max[comparePosition]) break;
                }
                if (comparePosition == UBO_Data_M.Count)
                {
                    W_ceiling = W - 200;
                    break;
                }
            }
            W_ceiling_boundary = (int)(W_ceiling / 10000) * 10000;

            for (double W = A306.m_min; W <= W_ceiling_boundary; W += 10000)
            {
                w3.Add(W);
                lbo_m3.Add(A306.Get_Low_Buffet_M(h_high, m: W, factor: factor));
                ubo_m3.Add(Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max, h_high, W));
            }

            W_ceiling_middle = w3[w3.Count - 1] + (W_ceiling - w3[w3.Count - 1]) / 1.3;
            w3.Add(W_ceiling_middle);
            lbo_m3.Add(A306.Get_Low_Buffet_M(h_high, m: W_ceiling_middle, factor: factor));
            ubo_m3.Add(Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max, h_high,
                W_ceiling_middle));

            w3.Add(W_ceiling);
            buffetM_W_ceiling = A306.Get_Low_Buffet_M(h_high, m: W_ceiling, factor: factor);
            lbo_m3.Add(buffetM_W_ceiling);
            ubo_m3.Add(buffetM_W_ceiling);


            for (int i = 0; i < w1.Count; i++)
                w1[i] /= 1000;
            for (int i = 0; i < w2.Count; i++)
                w2[i] /= 1000;
            for (int i = 0; i < w3.Count; i++)
                w3[i] /= 1000;


            List<double> M_MO_m = new List<double>() { A306.M_MO };
            List<double> M_MO_w = new List<double>() { 0 };


            chart14.ChartAreas[0].AxisX.Minimum = (int)((lbo_m1[0] - lbo_m1[0] * 0.05) * 20) * 0.05;
            chart14.ChartAreas[0].AxisX.Maximum = (int)((ubo_m1[0] + ubo_m1[0] * 0.1) * 10) * 0.1;
            chart14.ChartAreas[0].AxisX.LabelStyle.Interval = 0.05;
            chart14.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart14.ChartAreas[0].AxisX.Title = "M";
            chart14.ChartAreas[0].AxisX.TitleFont = labelFont;

            chart14.ChartAreas[0].AxisY.Minimum = w1[0];
            chart14.ChartAreas[0].AxisY.Maximum = (int)((w1[w1.Count - 1] + w1[w1.Count - 1] * 0.1) / 10) * 10;
            chart14.ChartAreas[0].AxisY.LabelStyle.Interval = 20;
            chart14.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart14.ChartAreas[0].AxisY.Title = "Mass ton";
            chart14.ChartAreas[0].AxisY.TitleFont = labelFont;

            chart14.Series[0].Points.DataBindXY(lbo_m1, w1);
            chart14.Series[2].Points.DataBindXY(lbo_m2, w2);
            chart14.Series[7].Points.DataBindXY(lbo_m3, w3);


            while (ubo_m1[0] > M_MO_m[0])
            {
                ubo_m1.Remove(ubo_m1[0]);
                w1.Remove(w1[0]);
            }
            while (ubo_m2[0] > M_MO_m[0])
            {
                ubo_m2.Remove(ubo_m2[0]);
                w2.Remove(w2[0]);
            } while (ubo_m3[0] > M_MO_m[0])
            {
                ubo_m3.Remove(ubo_m3[0]);
                w3.Remove(w3[0]);
            }

            double EPS = 0.001;

            ubo_m1.Insert(0, M_MO_m[0]);
            double M_MO_W;
            for (M_MO_W = w1[w1.Count - 1] * 1000; ; M_MO_W -= 1000)
            {
                double buffet_high = Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max,
                    h_low, M_MO_W);
                if (Math.Abs(buffet_high - M_MO_m[0]) < EPS) break;
            }
            w1.Insert(0, M_MO_W / 1000);

            ubo_m2.Insert(0, M_MO_m[0]);
            for (M_MO_W = w2[w2.Count - 1] * 1000; ; M_MO_W -= 1000)
            {
                double buffet_high = Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max,
                    h_middle, M_MO_W);
                if (Math.Abs(buffet_high - M_MO_m[0]) < EPS) break;
            }
            w2.Insert(0, M_MO_W / 1000);

            ubo_m3.Insert(0, M_MO_m[0]);
            for (M_MO_W = w3[w3.Count - 1] * 1000; ; M_MO_W -= 1000)
            {
                double buffet_high = Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max,
                    h_high, M_MO_W);
                if (Math.Abs(buffet_high - M_MO_m[0]) < EPS) break;
            }
            w3.Insert(0, M_MO_W / 1000);

            M_MO_m.Add(M_MO_m[0]);
            M_MO_w.Add(w1[0]);

            chart14.Series[1].Points.DataBindXY(ubo_m1, w1);
            chart14.Series[3].Points.DataBindXY(ubo_m2, w2);
            chart14.Series[8].Points.DataBindXY(ubo_m3, w3);
            chart14.Series[9].Points.DataBindXY(M_MO_m, M_MO_w);

            chart14.Legends[0].Enabled = true;
            chart14.Legends[0].Font = legendFont;
            chart14.Legends[0].Position = new System.Windows.Forms.DataVisualization.Charting.ElementPosition(78, 0, 14, 18);
            foreach (var series in chart14.Series)
                series.IsVisibleInLegend = false;
            chart14.Series[0].IsVisibleInLegend = true;
            chart14.Series[0].LegendText = "FL330";
            chart14.Series[1].IsVisibleInLegend = true;
            chart14.Series[1].LegendText = "FL350";
            chart14.Series[2].IsVisibleInLegend = true;
            chart14.Series[2].LegendText = "FL370";
        }


        public delegate double Handler(string fileName, double h, double CAS);

        public List<double> BisectionRootsCalculation(double lowerBoundary,
            double upperBoundary, double step, double EPS, Handler handler, string fileName,
            double functionParameter, int maxNumberOfRoots = 99)
        {
            List<double> roots = new List<double>();
            double xToCalculate = lowerBoundary;
            double functionValue = handler(fileName, functionParameter, xToCalculate);

            while (xToCalculate <= upperBoundary + step / 2)
            {
                if (Math.Abs(functionValue) < EPS)
                {
                    roots.Add(xToCalculate);
                    xToCalculate = xToCalculate + step / 2;
                    functionValue = handler(fileName, functionParameter, xToCalculate);
                }
                else
                {
                    double nextxToCalculate = xToCalculate + step;
                    double nextfunctionValue = handler(fileName, functionParameter,
                        nextxToCalculate);
                    if (Math.Abs(nextfunctionValue) < EPS)
                    {
                        roots.Add(nextxToCalculate);
                        xToCalculate = nextxToCalculate + step / 2;
                        functionValue = handler(fileName, functionParameter, xToCalculate);
                    }
                    else if (functionValue * nextfunctionValue > 0)
                    {
                        xToCalculate = nextxToCalculate;
                        functionValue = nextfunctionValue;
                    }
                    else
                    {
                        bool find = false;
                        while (!find)
                            if (Math.Abs(nextxToCalculate - xToCalculate) < EPS)
                            {
                                roots.Add((xToCalculate + nextxToCalculate) / 2);
                                xToCalculate = nextxToCalculate + step / 2;
                                functionValue = handler(fileName, functionParameter,
                                    xToCalculate);
                                find = true;
                            }
                            else
                            {
                                double xMiddle = (xToCalculate + nextxToCalculate) / 2;
                                double functionValueMiddle = handler(fileName, functionParameter,
                                    xMiddle);
                                if (Math.Abs(functionValueMiddle) < EPS)
                                {
                                    roots.Add(xMiddle);
                                    find = true;
                                    xToCalculate = xMiddle + step / 2;
                                    functionValue = handler(fileName, functionParameter,
                                        xToCalculate);
                                }
                                else if (functionValue * functionValueMiddle < 0)
                                {
                                    nextxToCalculate = xMiddle;
                                    nextfunctionValue = functionValueMiddle;
                                }
                                else
                                {
                                    xToCalculate = xMiddle;
                                    functionValue = functionValueMiddle;
                                }
                            }
                    }
                }
                if (roots.Count >= maxNumberOfRoots)
                    break;
            }
            return roots;
        }

        public double GetRedundantT(string fileName, double h, double CAS)
        {
            Aircraft A306 = new Aircraft(fileName);
            return A306.Get_T_max_cruise(h, Units.kt2mps(CAS)) - A306.Get_T(h,
                Units.kt2mps(CAS), Aircraft.FlightPhase.Cruise);
        }
        
        // 飞行包线（未剪切）
        private void button24_Click(object sender, EventArgs e)
        {
            foreach (var series in chart15.Series)
                series.Points.Clear();

            List<double> altitude_stall = new List<double>();
            List<double> vStall = new List<double>();
            List<double> altitude_v_min_t = new List<double>();
            List<double> v_min_t = new List<double>();
            List<double> altitude_v_lbo = new List<double>();
            List<double> v_buffet = new List<double>();

            List<double> altitude = new List<double>();
            List<double> v_max_t = new List<double>();


            double initialh = 0;
            for (double h = 15000; ; h++)
            {
                double v_stall = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);
                double t = A306.Get_T(h, Units.kt2mps(v_stall), Aircraft.FlightPhase.Cruise);
                double t_max = A306.Get_T_max_cruise(h, Units.kt2mps(v_stall));
                if (t_max < t)
                {
                    initialh = h;
                    break;
                }
            }



            double h_ceiling = 0;
            double v_ceiling_CAS = 0;

            List<double> thrust = new List<double>();
            List<double> v_CAS = new List<double>();

            for (double h = 32000; ; h++)
            {
                double v_stall = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);
                double tmax = A306.Get_T_max_cruise(h, Units.kt2mps(v_stall));

                for (double CAS = v_stall; ; CAS++)
                {
                    v_CAS.Add(CAS);

                    double t = A306.Get_T(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Cruise);
                    thrust.Add(t);

                    if (thrust.Count > 0 && t > thrust.Min())
                        break;
                }

                if (thrust.Min() >= tmax)
                {
                    h_ceiling = h;
                    v_ceiling_CAS = v_CAS[thrust.IndexOf(thrust.Min())];
                    break;
                }
            }

            double h_ceiling_boundary = ((int)h_ceiling / 1000) * 1000;


            for (double h = 15000; h < initialh; h += 1000)
            {
                altitude_stall.Add(h);
                vStall.Add(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise))));
            }
            altitude_stall.Add(initialh);
            vStall.Add(AtmosphereEnviroment.Get_TAS(initialh, Units.kt2mps(A306.Get_v_stall(initialh, Aircraft.FlightPhase.Cruise))));


            for (double h = 15000; h <= h_ceiling_boundary; h += 1000)
            {
                if (h == ((int)initialh / 1000) * 1000)
                    h = initialh;

                altitude.Add(h);
                if (h >= initialh)
                    altitude_v_min_t.Add(h);

                double lowerBoundary = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);
                double upperBoundary = 600;
                double step = 1;
                const double EPS = 1;

                if (GetRedundantT(fileName, h, lowerBoundary) > 0 && GetRedundantT(fileName, h, upperBoundary) < 0)
                {
                    double range = upperBoundary - lowerBoundary;
                    range /= 2.0;
                    double vMiddle = upperBoundary - range;
                    while (GetRedundantT(fileName, h, vMiddle) < 0)
                    {
                        range /= 2.0;
                        vMiddle -= range;
                    }
                    List<double> root = BisectionRootsCalculation(vMiddle, upperBoundary, step, EPS, GetRedundantT, fileName, h, 1);
                    if (root.Count >= 1)
                        v_max_t.Add(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(root[0])));
                }
                else if (Math.Abs(GetRedundantT(fileName, h, lowerBoundary)) < EPS && GetRedundantT(fileName, h, upperBoundary) < 0)
                {
                    v_min_t.Add(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(lowerBoundary)));
                    double range = upperBoundary - lowerBoundary;
                    range /= 2.0;
                    double vMiddle = upperBoundary - range;
                    while (GetRedundantT(fileName, h, vMiddle) < 0)
                    {
                        range /= 2.0;
                        vMiddle -= range;
                    }
                    List<double> root = BisectionRootsCalculation(vMiddle, upperBoundary, step, EPS, GetRedundantT, fileName, h, 1);
                    if (root.Count >= 1)
                        v_max_t.Add(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(root[0])));
                }
                else if (GetRedundantT(fileName, h, lowerBoundary) < 0 && GetRedundantT(fileName, h, upperBoundary) < 0)
                {
                    List<double> roots = BisectionRootsCalculation(lowerBoundary, upperBoundary, step, EPS, GetRedundantT, fileName, h, 2);
                    if (roots.Count >= 2)
                    {
                        v_min_t.Add(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(roots.Min())));
                        v_max_t.Add(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(roots.Max())));
                    }
                }
            }


            for (double h = 15000; h <= h_ceiling_boundary; h += 1000)
            {
                altitude_v_lbo.Add(h);
                double M = A306.Get_Low_Buffet_M(h, factor: 0.98);
                v_buffet.Add(M * AtmosphereEnviroment.Get_a(h));
            }



            double h_ceiling_middle = h_ceiling_boundary / 2 + h_ceiling / 2;
            List<double> h_ceiling_middleVRoots = BisectionRootsCalculation(A306.Get_v_stall(h_ceiling_middle, Aircraft.FlightPhase.Cruise), 600, 1, 1, GetRedundantT, fileName, h_ceiling_middle, 2);
            if (h_ceiling_middleVRoots.Count >= 2)
            {
                altitude.Add(h_ceiling_middle);
                v_max_t.Add(AtmosphereEnviroment.Get_TAS(h_ceiling_middle, Units.kt2mps(h_ceiling_middleVRoots.Max())));
            }


            v_min_t.Add(AtmosphereEnviroment.Get_TAS(h_ceiling, Units.kt2mps(v_ceiling_CAS)));
            altitude_v_min_t.Add(h_ceiling);
            v_max_t.Add(AtmosphereEnviroment.Get_TAS(h_ceiling, Units.kt2mps(v_ceiling_CAS)));
            altitude.Add(h_ceiling);




            List<double> altitude_v_MO = new List<double>();
            List<double> v_v_MO = new List<double>();
            List<double> altitude_M_MO = new List<double>();
            List<double> v_M_MO = new List<double>();
            List<double> v_ubo = new List<double>();
            List<double> altitude_v_ubo = new List<double>();


            double h_cross = AtmosphereEnviroment.Get_h_cross(Units.kt2mps(A306.v_MO), A306.M_MO);

            for (double h = 15000; h <= h_ceiling_boundary + 1000; h += 1000)
            {
                if (h == ((int)h_cross / 1000) * 1000)
                {
                    altitude_v_MO.Add(h_cross);
                    v_v_MO.Add(AtmosphereEnviroment.Get_TAS(h_cross, Units.kt2mps(A306.v_MO)));
                    altitude_M_MO.Add(h_cross);
                    v_M_MO.Add(A306.M_MO * AtmosphereEnviroment.Get_a(h_cross));
                    h = h_cross;
                }
                else if (h < h_cross)
                {
                    altitude_v_MO.Add(h);
                    v_v_MO.Add(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(A306.v_MO)));
                }
                else
                {
                    altitude_M_MO.Add(h);
                    v_M_MO.Add(A306.M_MO * AtmosphereEnviroment.Get_a(h));
                }
            }

            List<double> UBO_Data_M = new List<double>() { 0.2, 0.28, 0.36, 0.42, 0.46, 0.5, 0.54,
                0.58, 0.61, 0.63, 0.65, 0.67, 0.69, 0.71, 0.73, 0.75, 0.77, 0.79, 0.81, 0.82 };
            List<double> UBO_Data_C_L_max = new List<double>() { 1.3540, 1.2769, 1.1999, 1.1416,
                1.1031, 1.0646, 1.0261, 0.9876, 0.9606, 0.9450, 0.9325, 0.9221, 0.9127, 0.9013,
                0.8877, 0.8669, 0.8367, 0.7899, 0.7233, 0.6796 };
            while (UBO_Data_M[0] > 0.01)
            {
                double insertM = UBO_Data_M[0] - 0.01;
                UBO_Data_M.Insert(0, insertM);
                double C_L_max = UBO_Data_C_L_max[0] + 0.01 * (UBO_Data_C_L_max[0] -
                    UBO_Data_C_L_max[1]) / (UBO_Data_M[1] - UBO_Data_M[0]);
                UBO_Data_C_L_max.Insert(0, C_L_max);
            }
            while (UBO_Data_M[UBO_Data_M.Count - 1] < 1.3)
            {
                double insertM = UBO_Data_M[UBO_Data_M.Count - 1] + 0.01;
                double C_L_max = UBO_Data_C_L_max[UBO_Data_C_L_max.Count - 1] + 0.01 *
                    (UBO_Data_C_L_max[UBO_Data_C_L_max.Count - 1] -
                    UBO_Data_C_L_max[UBO_Data_C_L_max.Count - 2]) / (UBO_Data_M[UBO_Data_M.Count -
                    1] - UBO_Data_M[UBO_Data_M.Count - 2]);
                if (C_L_max <= 0) break;
                UBO_Data_M.Add(insertM);
                UBO_Data_C_L_max.Add(C_L_max);
            }

            double factor = 0.98;

            for (int i = 0; i < UBO_Data_C_L_max.Count; i++)
                UBO_Data_C_L_max[i] *= factor;

            for (double h = 15000; h <= h_ceiling_boundary + 1000; h += 1000)
            {
                altitude_v_ubo.Add(h);

                double v_UBO = Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max, h,
                    A306.m_ref) * AtmosphereEnviroment.Get_a(h);
                v_ubo.Add(v_UBO);
            }

            for (int i = 0; i < v_min_t.Count; i++)
                v_min_t[i] = Units.mps2kt(v_min_t[i]);
            for (int i = 0; i < vStall.Count; i++)
                vStall[i] = Units.mps2kt(vStall[i]);
            for (int i = 0; i < v_buffet.Count; i++)
                v_buffet[i] = Units.mps2kt(v_buffet[i]);
            for (int i = 0; i < v_max_t.Count; i++)
                v_max_t[i] = Units.mps2kt(v_max_t[i]);
            for (int i = 0; i < v_v_MO.Count; i++)
                v_v_MO[i] = Units.mps2kt(v_v_MO[i]);
            for (int i = 0; i < v_M_MO.Count; i++)
                v_M_MO[i] = Units.mps2kt(v_M_MO[i]);
            for (int i = 0; i < v_ubo.Count; i++)
                v_ubo[i] = Units.mps2kt(v_ubo[i]);

            for (int i = 0; i < altitude_v_min_t.Count; i++)
                altitude_v_min_t[i] /= 100;
            for (int i = 0; i < altitude_stall.Count; i++)
                altitude_stall[i] /= 100;
            for (int i = 0; i < altitude_v_lbo.Count; i++)
                altitude_v_lbo[i] /= 100;
            for (int i = 0; i < altitude.Count; i++)
                altitude[i] /= 100;
            for (int i = 0; i < altitude_v_MO.Count; i++)
                altitude_v_MO[i] /= 100;
            for (int i = 0; i < altitude_M_MO.Count; i++)
                altitude_M_MO[i] /= 100;
            for (int i = 0; i < altitude_v_ubo.Count; i++)
                altitude_v_ubo[i] /= 100;

            chart15.ChartAreas[0].AxisX.Minimum = (int)(vStall[0] - vStall[0] * 0.05);

            List<double> v_XAxis_max = new List<double>();
            v_XAxis_max.Add(v_max_t.Max());
            v_XAxis_max.Add(v_v_MO.Max());
            v_XAxis_max.Add(v_M_MO.Max());

            chart15.ChartAreas[0].AxisX.Maximum = (int)(v_XAxis_max.Max() + v_XAxis_max.Max() * 0.01);
            chart15.ChartAreas[0].AxisX.LabelStyle.Interval = 50;
            chart15.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart15.ChartAreas[0].AxisX.Title = "TAS kt";
            chart15.ChartAreas[0].AxisX.TitleFont = labelFont;


            chart15.ChartAreas[0].AxisY.Minimum = 150;
            double axisYMax = altitude_v_ubo[altitude_v_ubo.Count - 1] >
                altitude_M_MO[altitude_M_MO.Count - 1] &&
                altitude_v_ubo[altitude_v_ubo.Count - 1] >
                altitude_v_lbo[altitude_v_lbo.Count - 1] ?
                altitude_v_ubo[altitude_v_ubo.Count - 1] :
                (altitude_M_MO[altitude_M_MO.Count - 1] >
                altitude_v_lbo[altitude_v_lbo.Count - 1] ?
                altitude_M_MO[altitude_M_MO.Count - 1] :
                altitude_v_lbo[altitude_v_lbo.Count - 1]);
            chart15.ChartAreas[0].AxisY.Maximum = (int)((axisYMax + axisYMax * 0.03) / 10) * 10;
            chart15.ChartAreas[0].AxisY.LabelStyle.Interval = 20;
            chart15.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart15.ChartAreas[0].AxisY.Title = "Altitude 100 ft";
            chart15.ChartAreas[0].AxisY.TitleFont = labelFont;

            chart15.Series[0].Points.DataBindXY(v_min_t, altitude_v_min_t);
            chart15.Series[1].Points.DataBindXY(vStall, altitude_stall);
            chart15.Series[2].Points.DataBindXY(v_buffet, altitude_v_lbo);
            chart15.Series[3].Points.DataBindXY(v_max_t, altitude);
            chart15.Series[7].Points.DataBindXY(v_v_MO, altitude_v_MO);
            chart15.Series[8].Points.DataBindXY(v_M_MO, altitude_M_MO);
            chart15.Series[9].Points.DataBindXY(v_ubo, altitude_v_ubo);

            chart15.Legends[0].Enabled = true;
            chart15.Legends[0].Font = legendFont;
            chart15.Legends[0].Position = new System.Windows.Forms.DataVisualization.Charting.ElementPosition(34, 36, 25, 40);
            foreach (var series in chart15.Series)
                series.IsVisibleInLegend = false;
            chart15.Series[0].IsVisibleInLegend = true;
            chart15.Series[0].LegendText = "Left thrust line cross-point";
            chart15.Series[1].IsVisibleInLegend = true;
            chart15.Series[1].LegendText = "Stall limit";
            chart15.Series[2].IsVisibleInLegend = true;
            chart15.Series[2].LegendText = "Low speed buffet limit";
            chart15.Series[3].IsVisibleInLegend = true;
            chart15.Series[3].LegendText = "Right thrust line cross-point";
            chart15.Series[7].IsVisibleInLegend = true;
            chart15.Series[7].LegendText = "v_MO";
            chart15.Series[8].IsVisibleInLegend = true;
            chart15.Series[8].LegendText = "M_MO";
            chart15.Series[9].IsVisibleInLegend = true;
            chart15.Series[9].LegendText = "High speed buffet limit";
        }

        // 飞行包线（剪切）
        private void button25_Click(object sender, EventArgs e)
        {
            foreach (var series in chart15.Series)
                series.Points.Clear();

            List<double> altitude_stall = new List<double>();
            List<double> vStall = new List<double>();
            List<double> altitude_v_min_t = new List<double>();
            List<double> v_min_t = new List<double>();
            List<double> altitude_v_min = new List<double>();
            List<double> v_buffet = new List<double>();

            List<double> altitude = new List<double>();
            List<double> v_max_t = new List<double>();


            double initialh = 0;
            for (double h = 15000; ; h++)
            {
                double v_stall = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);
                double t = A306.Get_T(h, Units.kt2mps(v_stall), Aircraft.FlightPhase.Cruise);
                double t_max = A306.Get_T_max_cruise(h, Units.kt2mps(v_stall));
                if (t_max < t)
                {
                    initialh = h;
                    break;
                }
            }



            double h_ceiling = 0;
            double v_ceiling_CAS = 0;

            List<double> thrust = new List<double>();
            List<double> v_CAS = new List<double>();

            for (double h = 32000; ; h++)
            {
                double v_stall = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);
                double tmax = A306.Get_T_max_cruise(h, Units.kt2mps(v_stall));

                for (double CAS = v_stall; ; CAS++)
                {
                    v_CAS.Add(CAS);

                    double t = A306.Get_T(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Cruise);
                    thrust.Add(t);

                    if (thrust.Count > 0 && t > thrust.Min())
                        break;
                }

                if (thrust.Min() >= tmax)
                {
                    h_ceiling = h;
                    v_ceiling_CAS = v_CAS[thrust.IndexOf(thrust.Min())];
                    break;
                }
            }

            double h_ceiling_boundary = ((int)h_ceiling / 1000) * 1000;



            for (double h = 15000; h < initialh; h += 1000)
            {
                altitude_stall.Add(h);
                vStall.Add(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise))));
            }
            altitude_stall.Add(initialh);
            vStall.Add(AtmosphereEnviroment.Get_TAS(initialh, Units.kt2mps(A306.Get_v_stall(initialh, Aircraft.FlightPhase.Cruise))));


            for (double h = 15000; h <= h_ceiling_boundary; h += 1000)
            {
                if (h == ((int)initialh / 1000) * 1000 + 1000)
                    h = initialh;
                else if (h == initialh + 1000)
                    h = (int)initialh / 1000 * 1000 + 1000;

                altitude.Add(h);
                if (h >= initialh)
                    altitude_v_min_t.Add(h);

                double lowerBoundary = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);
                double upperBoundary = 600;
                double step = 1;
                const double EPS = 1;

                if (GetRedundantT(fileName, h, lowerBoundary) > 0 && GetRedundantT(fileName, h, upperBoundary) < 0)
                {
                    double range = upperBoundary - lowerBoundary;
                    range /= 2.0;
                    double vMiddle = upperBoundary - range;
                    while (GetRedundantT(fileName, h, vMiddle) < 0)
                    {
                        range /= 2.0;
                        vMiddle -= range;
                    }
                    List<double> root = BisectionRootsCalculation(vMiddle, upperBoundary, step, EPS, GetRedundantT, fileName, h, 1);
                    if (root.Count >= 1)
                        v_max_t.Add(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(root[0])));
                }
                else if (Math.Abs(GetRedundantT(fileName, h, lowerBoundary)) < EPS && GetRedundantT(fileName, h, upperBoundary) < 0)
                {
                    v_min_t.Add(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(lowerBoundary)));
                    double range = upperBoundary - lowerBoundary;
                    range /= 2.0;
                    double vMiddle = upperBoundary - range;
                    while (GetRedundantT(fileName, h, vMiddle) < 0)
                    {
                        range /= 2.0;
                        vMiddle -= range;
                    }
                    List<double> root = BisectionRootsCalculation(vMiddle, upperBoundary, step, EPS, GetRedundantT, fileName, h, 1);
                    if (root.Count >= 1)
                        v_max_t.Add(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(root[0])));
                }
                else if (GetRedundantT(fileName, h, lowerBoundary) < 0 && GetRedundantT(fileName, h, upperBoundary) < 0)
                {
                    List<double> roots = BisectionRootsCalculation(lowerBoundary, upperBoundary, step, EPS, GetRedundantT, fileName, h, 2);
                    if (roots.Count >= 2)
                    {
                        v_min_t.Add(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(roots.Min())));
                        v_max_t.Add(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(roots.Max())));
                    }
                }
            }


            for (double h = 15000; h <= h_ceiling_boundary; h += 1000)
            {
                altitude_v_min.Add(h);
                double M = A306.Get_Low_Buffet_M(h, factor: 0.98);
                v_buffet.Add(M * AtmosphereEnviroment.Get_a(h));
            }



            double h_ceiling_middle = h_ceiling_boundary / 2 + h_ceiling / 2;
            List<double> h_ceiling_middleVRoots = BisectionRootsCalculation(A306.Get_v_stall(h_ceiling_middle, Aircraft.FlightPhase.Cruise), 600, 1, 1, GetRedundantT, fileName, h_ceiling_middle, 2);
            if (h_ceiling_middleVRoots.Count >= 2)
            {
                altitude.Add(h_ceiling_middle);
                v_max_t.Add(AtmosphereEnviroment.Get_TAS(h_ceiling_middle, Units.kt2mps(h_ceiling_middleVRoots.Max())));
            }



            v_min_t.Add(AtmosphereEnviroment.Get_TAS(h_ceiling, Units.kt2mps(v_ceiling_CAS)));
            altitude_v_min_t.Add(h_ceiling);
            v_max_t.Add(AtmosphereEnviroment.Get_TAS(h_ceiling, Units.kt2mps(v_ceiling_CAS)));
            altitude.Add(h_ceiling);




            List<double> altitude_v_MO = new List<double>();
            List<double> v_v_MO = new List<double>();
            List<double> altitude_M_MO = new List<double>();
            List<double> v_M_MO = new List<double>();
            List<double> v_ubo = new List<double>();
            List<double> altitude_v_ubo = new List<double>();

            double h_cross = AtmosphereEnviroment.Get_h_cross(Units.kt2mps(A306.v_MO), A306.M_MO);

            for (double h = 15000; h <= h_ceiling_boundary + 1000; h += 1000)
            {
                if (h == ((int)h_cross / 1000) * 1000)
                {
                    altitude_v_MO.Add(h_cross);
                    v_v_MO.Add(AtmosphereEnviroment.Get_TAS(h_cross, Units.kt2mps(A306.v_MO)));
                    altitude_M_MO.Add(h_cross);
                    v_M_MO.Add(A306.M_MO * AtmosphereEnviroment.Get_a(h_cross));
                    h = h_cross;
                }
                else if (h < h_cross)
                {
                    altitude_v_MO.Add(h);
                    v_v_MO.Add(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(A306.v_MO)));
                }
                else
                {
                    if (h == h_cross + 1000)
                        h = ((int)h_cross / 1000) * 1000 + 1000;
                    else if (h == ((int)initialh / 1000) * 1000 + 1000)
                    {
                        altitude_M_MO.Add(initialh);
                        v_M_MO.Add(A306.M_MO * AtmosphereEnviroment.Get_a(initialh));
                    }
                    altitude_M_MO.Add(h);
                    v_M_MO.Add(A306.M_MO * AtmosphereEnviroment.Get_a(h));
                }
            }


            List<double> UBO_Data_M = new List<double>() { 0.2, 0.28, 0.36, 0.42, 0.46, 0.5, 0.54,
                0.58, 0.61, 0.63, 0.65, 0.67, 0.69, 0.71, 0.73, 0.75, 0.77, 0.79, 0.81, 0.82 };
            List<double> UBO_Data_C_L_max = new List<double>() { 1.3540, 1.2769, 1.1999, 1.1416,
                1.1031, 1.0646, 1.0261, 0.9876, 0.9606, 0.9450, 0.9325, 0.9221, 0.9127, 0.9013,
                0.8877, 0.8669, 0.8367, 0.7899, 0.7233, 0.6796 };
            while (UBO_Data_M[0] > 0.01)
            {
                double insertM = UBO_Data_M[0] - 0.01;
                UBO_Data_M.Insert(0, insertM);
                double C_L_max = UBO_Data_C_L_max[0] + 0.01 * (UBO_Data_C_L_max[0] -
                    UBO_Data_C_L_max[1]) / (UBO_Data_M[1] - UBO_Data_M[0]);
                UBO_Data_C_L_max.Insert(0, C_L_max);
            }
            while (UBO_Data_M[UBO_Data_M.Count - 1] < 1.3)
            {
                double insertM = UBO_Data_M[UBO_Data_M.Count - 1] + 0.01;
                double C_L_max = UBO_Data_C_L_max[UBO_Data_C_L_max.Count - 1] + 0.01 *
                    (UBO_Data_C_L_max[UBO_Data_C_L_max.Count - 1] -
                    UBO_Data_C_L_max[UBO_Data_C_L_max.Count - 2]) / (UBO_Data_M[UBO_Data_M.Count -
                    1] - UBO_Data_M[UBO_Data_M.Count - 2]);
                if (C_L_max <= 0) break;
                UBO_Data_M.Add(insertM);
                UBO_Data_C_L_max.Add(C_L_max);
            }

            double factor = 0.98;

            for (int i = 0; i < UBO_Data_C_L_max.Count; i++)
                UBO_Data_C_L_max[i] *= factor;

            for (double h = 15000; h <= h_ceiling_boundary + 1000; h += 1000)
            {
                altitude_v_ubo.Add(h);

                double v_UBO = Get_High_Buffet_M(fileName, UBO_Data_M, UBO_Data_C_L_max, h,
                    A306.m_ref) * AtmosphereEnviroment.Get_a(h);
                v_ubo.Add(v_UBO);
            }


            bool vBuffetWrapVStallFlag = true;
            if (v_buffet.Count >= vStall.Count)
                for (int i = 0; i < vStall.Count; i++)
                    if (vStall[i] > v_buffet[i])
                    {
                        vBuffetWrapVStallFlag = false;
                        break;
                    }


            bool vBuffetCrossv_min_tFlag = false;
            int vBuffetInitialIndexOfv_min_t = altitude_v_min.IndexOf(altitude_v_min_t[1]);
            int vBuffetCrossv_min_tIndex = -1;
            for (int i = vBuffetInitialIndexOfv_min_t; i < v_buffet.Count; i++)
                if (v_min_t[i - vBuffetInitialIndexOfv_min_t + 1] > v_buffet[i])
                {
                    vBuffetCrossv_min_tFlag = true;
                    vBuffetCrossv_min_tIndex = --i;
                    break;
                }

            if (vBuffetCrossv_min_tFlag)
            {
                for (int i = vBuffetCrossv_min_tIndex + 1; i < v_buffet.Count; i++)
                {
                    v_buffet.Remove(v_buffet[i]);
                    altitude_v_min.Remove(altitude_v_min[i]);
                    i--;
                }
                while (altitude_v_min_t[0] <= altitude_v_min[altitude_v_min.Count - 1])
                {
                    altitude_v_min_t.Remove(altitude_v_min_t[0]);
                    v_min_t.Remove(v_min_t[0]);
                }

                altitude_v_min_t.Insert(0, altitude_v_min[altitude_v_min.Count - 1]);
                v_min_t.Insert(0, v_buffet[v_buffet.Count - 1]);
            }



            bool vMOWitninv_uboFlag = true;
            for (int i = 0; i < v_ubo.Count; i++)
            {
                if (altitude_M_MO.Contains(altitude_v_ubo[i]))
                    if (v_ubo[i] < v_M_MO[altitude_M_MO.IndexOf(altitude_v_ubo[i])])
                        vMOWitninv_uboFlag = false;
            }
            if (vMOWitninv_uboFlag == true)
            {
                v_ubo.Clear();
                altitude_v_ubo.Clear();
            }



            bool vMOWithinv_max_tFlag = true;
            for (int i = 0; i <= altitude.IndexOf(((int)h_cross / 1000) * 1000 + 1000); i++)
                if (altitude_v_MO.Contains(altitude[i]))
                    if (v_v_MO[altitude_v_MO.IndexOf(altitude[i])] > v_max_t[i])
                        vMOWithinv_max_tFlag = false;
            if (vMOWithinv_max_tFlag)
                while (altitude[0] < ((int)h_cross / 1000) * 1000 + 1000)
                {
                    altitude.Remove(altitude[0]);
                    v_max_t.Remove(v_max_t[0]);
                }



            while (altitude_M_MO[altitude_M_MO.Count - 1] >= altitude.Max())
            {
                altitude_M_MO.Remove(altitude_M_MO[altitude_M_MO.Count - 1]);
                v_M_MO.Remove(v_M_MO[v_M_MO.Count - 1]);
            }
            altitude_M_MO.Add(h_ceiling_middle);
            v_M_MO.Add(A306.M_MO * AtmosphereEnviroment.Get_a(h_ceiling_middle));
            while (altitude_M_MO.Contains(altitude[0]) && v_max_t[0] >= v_M_MO[altitude_M_MO.IndexOf(altitude[0])])
            {
                altitude.Remove(altitude[0]);
                v_max_t.Remove(v_max_t[0]);
            }
            altitude.Insert(0, altitude_M_MO[altitude_M_MO.Count - 1]);
            v_max_t.Insert(0, v_M_MO[v_M_MO.Count - 1]);

            for (int i = 0; i < v_min_t.Count; i++)
                v_min_t[i] = Units.mps2kt(v_min_t[i]);
            for (int i = 0; i < vStall.Count; i++)
                vStall[i] = Units.mps2kt(vStall[i]);
            for (int i = 0; i < v_buffet.Count; i++)
                v_buffet[i] = Units.mps2kt(v_buffet[i]);
            for (int i = 0; i < v_max_t.Count; i++)
                v_max_t[i] = Units.mps2kt(v_max_t[i]);
            for (int i = 0; i < v_v_MO.Count; i++)
                v_v_MO[i] = Units.mps2kt(v_v_MO[i]);
            for (int i = 0; i < v_M_MO.Count; i++)
                v_M_MO[i] = Units.mps2kt(v_M_MO[i]);
            for (int i = 0; i < v_ubo.Count; i++)
                v_ubo[i] = Units.mps2kt(v_ubo[i]);

            for (int i = 0; i < altitude_v_min_t.Count; i++)
                altitude_v_min_t[i] /= 100;
            for (int i = 0; i < altitude_stall.Count; i++)
                altitude_stall[i] /= 100;
            for (int i = 0; i < altitude_v_min.Count; i++)
                altitude_v_min[i] /= 100;
            for (int i = 0; i < altitude.Count; i++)
                altitude[i] /= 100;
            for (int i = 0; i < altitude_v_MO.Count; i++)
                altitude_v_MO[i] /= 100;
            for (int i = 0; i < altitude_M_MO.Count; i++)
                altitude_M_MO[i] /= 100;
            for (int i = 0; i < altitude_v_ubo.Count; i++)
                altitude_v_ubo[i] /= 100;

            List<double> v_XAxis_max = new List<double>();
            v_XAxis_max.Add(v_max_t.Max());
            v_XAxis_max.Add(v_v_MO.Max());
            v_XAxis_max.Add(v_M_MO.Max());


            chart15.ChartAreas[0].AxisX.Minimum = (int)(vStall[0] - vStall[0] * 0.005);
            chart15.ChartAreas[0].AxisX.Maximum = (int)(v_XAxis_max.Max() + v_XAxis_max.Max() * 0.1);
            chart15.ChartAreas[0].AxisX.LabelStyle.Interval = 50;
            chart15.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart15.ChartAreas[0].AxisX.Title = "TAS kt";
            chart15.ChartAreas[0].AxisX.TitleFont = labelFont;

            chart15.ChartAreas[0].AxisY.Minimum = 150;
            chart15.ChartAreas[0].AxisY.Maximum = 410;
            chart15.ChartAreas[0].AxisY.LabelStyle.Interval = 20;
            chart15.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart15.ChartAreas[0].AxisY.Title = "Altitude 100 ft";
            chart15.ChartAreas[0].AxisY.TitleFont = labelFont;
            chart15.Series[0].Points.DataBindXY(v_min_t, altitude_v_min_t);

            if (vBuffetWrapVStallFlag == false)
                chart15.Series[1].Points.DataBindXY(vStall, altitude_stall);

            chart15.Series[2].Points.DataBindXY(v_buffet, altitude_v_min);
            chart15.Series[3].Points.DataBindXY(v_max_t, altitude);
            chart15.Series[7].Points.DataBindXY(v_v_MO, altitude_v_MO);
            chart15.Series[8].Points.DataBindXY(v_M_MO, altitude_M_MO);
            chart15.Series[9].Points.DataBindXY(v_ubo, altitude_v_ubo);

            chart15.Legends[0].Enabled = true;
            chart15.Legends[0].Font = legendFont;
            chart15.Legends[0].Position = new System.Windows.Forms.DataVisualization.Charting.ElementPosition(42, 36, 25, 30);
            foreach (var series in chart15.Series)
                series.IsVisibleInLegend = false;
            if(v_min_t.Count > 1)
            {
                chart15.Series[0].IsVisibleInLegend = true;
                chart15.Series[0].LegendText = "Left thrust line cross-point";
            }
            if (vBuffetWrapVStallFlag == false)
            {
                chart15.Series[1].IsVisibleInLegend = true;
                chart15.Series[1].LegendText = "Stall limit";
            }
            if(v_buffet.Count > 1)
            {
                chart15.Series[2].IsVisibleInLegend = true;
                chart15.Series[2].LegendText = "Low speed buffet limit";
            }
            if(v_max_t.Count > 1)
            {
                chart15.Series[3].IsVisibleInLegend = true;
                chart15.Series[3].LegendText = "Right thrust line cross-point";
            }
            if(v_v_MO.Count > 1)
            {
                chart15.Series[7].IsVisibleInLegend = true;
                chart15.Series[7].LegendText = "v_MO";
            }
            if(v_M_MO.Count > 1)
            {
                chart15.Series[8].IsVisibleInLegend = true;
                chart15.Series[8].LegendText = "M_MO";
            }
            if(v_ubo.Count > 1)
            {
                chart15.Series[9].IsVisibleInLegend = true;
                chart15.Series[9].LegendText = "High speed buffet limit";
            }
        }

        public double GetBFLSpread(string fileName, double numberOfEngines, double CAS)
        {
            Aircraft A306 = new Aircraft(fileName);
            return A306.Get_Balanced_Field_Length((int)numberOfEngines, CAS,
                Aircraft.Balanced_Field_LengthType.AccelerateGo) -
                A306.Get_Balanced_Field_Length((int)numberOfEngines, CAS,
                Aircraft.Balanced_Field_LengthType.AccelerateStop);
        }

        // 平衡场长图
        private void button26_Click(object sender, EventArgs e)
        {
            List<double> v1 = new List<double>();
            List<double> bflAccelerateGo = new List<double>();
            List<double> bflAccelerateStop = new List<double>();


            double V1_max = Units.kt2mps(A306.Get_V_1_Maximum());
            for (double V1 = 0.9 * V1_max; V1 <= V1_max; V1 += 0.1)
            {
                v1.Add(V1);
                bflAccelerateGo.Add(A306.Get_Balanced_Field_Length(2, V1, Aircraft.Balanced_Field_LengthType.AccelerateGo));
                bflAccelerateStop.Add(A306.Get_Balanced_Field_Length(2, V1, Aircraft.Balanced_Field_LengthType.AccelerateStop));
            }


            List<double> BFL_V1Roots = BisectionRootsCalculation(0.9 * V1_max, V1_max, 0.01, 1, GetBFLSpread, fileName, 2, 1);
            double BFL_V1 = 0;
            if (BFL_V1Roots.Count >= 1)
                BFL_V1 = BFL_V1Roots[0];
            double BFL = A306.Get_Balanced_Field_Length(2, BFL_V1, Aircraft.Balanced_Field_LengthType.AccelerateGo);


            int v1InsertIndex = 0, bflAccelerateGoInsertIndex = 0, bflAccelerateStopInsertIndex = 0;
            for (int i = 0; i < v1.Count; i++)
            {
                if (BFL_V1 < v1[i])
                    v1InsertIndex = i;
                if (BFL > bflAccelerateGo[i])
                    bflAccelerateGoInsertIndex = i;
                if (BFL < bflAccelerateStop[i])
                    bflAccelerateStopInsertIndex = i;
                if (v1InsertIndex != 0 && bflAccelerateGoInsertIndex != 0 && bflAccelerateStopInsertIndex != 0)
                    break;
            }


            v1.Insert(v1InsertIndex, BFL_V1);
            bflAccelerateGo.Insert(bflAccelerateGoInsertIndex, BFL);
            bflAccelerateStop.Insert(bflAccelerateStopInsertIndex, BFL);


            List<double> bflHorizontalLine_length = new List<double>() { BFL, BFL };
            List<double> bflHorizontalLine_v1 = new List<double>() { 0, BFL_V1 };
            List<double> bflVerticalLine_length = new List<double>() { 0, BFL };
            List<double> bflVerticalLine_v1 = new List<double>() { BFL_V1, BFL_V1 };


            for (int i = 0; i < v1.Count; i++)
                v1[i] = Units.mps2kt(v1[i]);
            for (int i = 0; i < bflHorizontalLine_v1.Count; i++)
                bflHorizontalLine_v1[i] = Units.mps2kt(bflHorizontalLine_v1[i]);
            for (int i = 0; i < bflVerticalLine_v1.Count; i++)
                bflVerticalLine_v1[i] = Units.mps2kt(bflVerticalLine_v1[i]);

            chart16.ChartAreas[0].AxisX.Minimum = (int)(v1[0] - v1[0] * 0.01);
            chart16.ChartAreas[0].AxisX.Maximum = (int)(v1[v1.Count - 1] + v1[v1.Count - 1] * 0.05);
            chart16.ChartAreas[0].AxisX.LabelStyle.Interval = 2;
            chart16.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart16.ChartAreas[0].AxisX.Title = "V1 kt";
            chart16.ChartAreas[0].AxisX.TitleFont = labelFont;

            double axisYMin = Math.Min(bflAccelerateGo[bflAccelerateGo.Count - 1], bflAccelerateStop[0]);
            chart16.ChartAreas[0].AxisY.Minimum = ((int)(axisYMin - axisYMin * 0.03) / 100) * 100;
            double axisYMax = Math.Max(bflAccelerateGo[0], bflAccelerateStop[bflAccelerateStop.Count - 1]);
            chart16.ChartAreas[0].AxisY.Maximum = ((int)(axisYMax + axisYMax * 0.03) / 100) * 100;
            chart16.ChartAreas[0].AxisY.LabelStyle.Interval = 100;
            chart16.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart16.ChartAreas[0].AxisY.Title = "Take-off distance m";
            chart16.ChartAreas[0].AxisY.TitleFont = labelFont;

            chart16.Series[0].Points.DataBindXY(v1, bflAccelerateGo);
            chart16.Series[1].Points.DataBindXY(v1, bflAccelerateStop);
            chart16.Series[4].Points.DataBindXY(bflHorizontalLine_v1, bflHorizontalLine_length);
            chart16.Series[5].Points.DataBindXY(bflVerticalLine_v1, bflVerticalLine_length);

            chart16.Legends[0].Enabled = true;
            chart16.Legends[0].Font = legendFont;
            chart16.Legends[0].Position = new System.Windows.Forms.DataVisualization.Charting.ElementPosition(60, 5, 18, 12);
            foreach (var series in chart16.Series)
                series.IsVisibleInLegend = false;
            chart16.Series[0].IsVisibleInLegend = true;
            chart16.Series[0].LegendText = "Accelerate-go";
            chart16.Series[1].IsVisibleInLegend = true;
            chart16.Series[1].LegendText = "Accelerate-stop";
        }

        // 爬升梯度随速度
        private void button27_Click(object sender, EventArgs e)
        {
            List<double> cg = new List<double>();
            List<double> tas = new List<double>();


            double W = A306.m_ref * AtmosphereEnviroment.g;
            double h = 18000;


            double initialCAS = 0;
            for (double CAS = 0; CAS <= 500; CAS++)
            {
                double T = A306.Get_T(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Climb);
                double D = A306.Get_D(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Climb);
                if (T > D)
                {
                    initialCAS = --CAS;
                    break;
                }
            }





            double endCAS = 0;
            for (double CAS = 600; CAS >= 0; CAS--)
            {
                double T = A306.Get_T(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Climb);
                double D = A306.Get_D(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Climb);
                if (T > D)
                {
                    endCAS = ++CAS;
                    break;
                }
            }




            for (double CAS = initialCAS; CAS <= endCAS; CAS++)
            {
                double TAS = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(CAS));
                tas.Add(Units.mps2kt(TAS));

                double T = A306.Get_T(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Climb);
                double D = A306.Get_D(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Climb);
                double FM = A306.Get_functionM(h, Aircraft.SpeedMode.ConstantCAS, Units.kt2mps(CAS));

                double CG = (T - D) / W / FM;
                cg.Add(CG);
            }


            chart17.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
            chart17.ChartAreas[0].AxisX.LabelStyle.Interval = 50;
            chart17.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart17.ChartAreas[0].AxisX.Title = "TAS kt";
            chart17.ChartAreas[0].AxisX.TitleFont = labelFont;

            chart17.ChartAreas[0].AxisY.Minimum = 0;
            chart17.ChartAreas[0].AxisY.LabelStyle.Interval = 0.01;
            chart17.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart17.ChartAreas[0].AxisY.Title = "Climb Gradient %";
            chart17.ChartAreas[0].AxisY.TitleFont = labelFont;

            chart17.Series[0].Points.DataBindXY(tas, cg);
        }

        // 爬升率随速度
        private void button28_Click(object sender, EventArgs e)
        {
            List<double> rocd = new List<double>();
            List<double> tas = new List<double>();



            double h = 18000;


            double initialCAS = 0;
            for (double CAS = 0; CAS <= 600; CAS++)
            {
                double ROCD = A306.Get_ROCD(h, Aircraft.SpeedMode.ConstantCAS, Units.kt2mps(CAS), Aircraft.FlightPhase.Climb);
                if (ROCD > 0)
                {
                    initialCAS = --CAS;
                    break;
                }
            }


            double endCAS = 0;
            for (double CAS = 600; CAS >= 0; CAS--)
            {
                double ROCD = A306.Get_ROCD(h, Aircraft.SpeedMode.ConstantCAS, Units.kt2mps(CAS), Aircraft.FlightPhase.Climb);
                if (ROCD > 0)
                {
                    endCAS = ++CAS;
                    break;
                }
            }

            for (double CAS = initialCAS; CAS <= endCAS; CAS++)
            {
                double TAS = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(CAS));
                tas.Add(Units.mps2kt(TAS));

                double ROCD = A306.Get_ROCD(h, Aircraft.SpeedMode.ConstantCAS, Units.kt2mps(CAS), Aircraft.FlightPhase.Climb);
                rocd.Add(ROCD);
            }



            chart18.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
            chart18.ChartAreas[0].AxisX.LabelStyle.Interval = 50;
            chart18.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart18.ChartAreas[0].AxisX.Title = "TAS kt";
            chart18.ChartAreas[0].AxisX.TitleFont = labelFont;

            chart18.ChartAreas[0].AxisY.Minimum = 0;
            chart18.ChartAreas[0].AxisY.LabelStyle.Interval = 300;
            chart18.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart18.ChartAreas[0].AxisY.Title = "Rate of Climb ft/min";
            chart18.ChartAreas[0].AxisY.TitleFont = labelFont;

            chart18.Series[0].Points.DataBindXY(tas, rocd);
        }

        // 爬升数值表
        private void button29_Click(object sender, EventArgs e)
        {
            textBox1.Text = "\t\t\t    CLIMB" + System.Environment.NewLine;
            textBox1.Text += "\tCAS/M: 250/300/.79\t\tTemperature: ISA" + System.Environment.NewLine;
            textBox1.Text += "\tlo: 104.4t\tnom: 140t\thi: 171.7t" + System.Environment.NewLine;
            textBox1.Text += "\t================================================" + System.Environment.NewLine;
            textBox1.Text += "\tFL\t TAS\t\tROCD\t\t fuel" + System.Environment.NewLine;
            textBox1.Text += "\t\t[kts]\t\t[fpm]\t\t[kg/min]" + System.Environment.NewLine;
            textBox1.Text += "\t\t\t lo\t nom\t hi\t nom" + System.Environment.NewLine;
            textBox1.Text += "\t================================================" + System.Environment.NewLine;

            double m_low = 104400;
            double m_low_v_climb = A306.m_min;
            double m_low_above_h_cross = 108000;
            double m_nominal = 140000;
            double m_high = 171700;

            double h_cross = AtmosphereEnviroment.Get_h_cross(Units.kt2mps(A306.v_cl_2), A306.M_cl);

            for (double h = 0; h <= 41000;)
            {
                double CAS_low = 0;
                double CAS_nominal = 0;
                double CAS_high = 0;
                double TAS = 0;
                double M_cl = A306.M_cl;

                if (h <= h_cross)
                {
                    CAS_low = A306.Get_v_climb(h, m: m_low_v_climb);
                    CAS_nominal = A306.Get_v_climb(h, m: m_nominal);
                    CAS_high = A306.Get_v_climb(h, m: m_high);
                    TAS = Units.mps2kt(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(CAS_nominal)));
                    TAS = Math.Round(TAS);
                }
                else
                {
                    TAS = M_cl * AtmosphereEnviroment.Get_a(h);
                    TAS = Math.Round(Units.mps2kt(TAS));
                }


                double ROCD_low;
                if (h > h_cross)
                    ROCD_low = A306.Get_ROCD(h, Aircraft.SpeedMode.ConstantMach, M_cl, Aircraft.FlightPhase.Climb, m: m_low_above_h_cross);
                else
                    ROCD_low = A306.Get_ROCD(h, Aircraft.SpeedMode.ConstantCAS, Units.kt2mps(CAS_low), Aircraft.FlightPhase.Climb, m: m_low, reduceFlag: true);
                ROCD_low = Math.Round(ROCD_low);
                if (ROCD_low < 0) ROCD_low = 0;


                double ROCD_nominal;
                if (h > h_cross)
                    ROCD_nominal = A306.Get_ROCD(h, Aircraft.SpeedMode.ConstantMach, M_cl, Aircraft.FlightPhase.Climb, m: m_nominal);
                else
                    ROCD_nominal = A306.Get_ROCD(h, Aircraft.SpeedMode.ConstantCAS, Units.kt2mps(CAS_nominal), Aircraft.FlightPhase.Climb, m: m_nominal, reduceFlag: true);
                ROCD_nominal = Math.Round(ROCD_nominal);
                if (ROCD_nominal < 0) ROCD_nominal = 0;


                double ROCD_high;
                if (h > h_cross)
                    ROCD_high = A306.Get_ROCD(h, Aircraft.SpeedMode.ConstantMach, M_cl, Aircraft.FlightPhase.Climb, m: m_high);
                else
                    ROCD_high = A306.Get_ROCD(h, Aircraft.SpeedMode.ConstantCAS, Units.kt2mps(CAS_high), Aircraft.FlightPhase.Climb, m: m_high, reduceFlag: true);
                ROCD_high = Math.Round(ROCD_high);
                if (ROCD_high < 0) ROCD_high = 0;


                if (h > h_cross)
                    CAS_nominal = A306.Get_v_climb(h); ;
                double ff;
                ff = A306.Get_ff(Aircraft.FlightPhase.Climb, h: h, CAS: Units.kt2mps(CAS_nominal));
                ff = Math.Round(ff, 1);

                if(h == 41000)
                    textBox1.Text += $"\t{h / 100}\t{TAS}\t{ROCD_low}\t{ROCD_nominal}\t{ROCD_high}\t{ff}";
                else if(h == 0)
                    textBox1.Text += $"\t{h}\t{TAS}\t{ROCD_low}\t{ROCD_nominal}\t{ROCD_high}\t{ff}" + System.Environment.NewLine;
                else
                    textBox1.Text += $"\t{h / 100}\t{TAS}\t{ROCD_low}\t{ROCD_nominal}\t{ROCD_high}\t{ff}" + System.Environment.NewLine;

                if (h < 2000)
                    h += 500;
                else if (h < 4000)
                    h += 1000;
                else if (h < 28000)
                    h += 2000;
                else if (h == 28000)
                    h += 1000;
                else
                    h += 2000;
            }
        }

        // 燃油里程随马赫数与巡航方式
        private void button30_Click(object sender, EventArgs e)
        {
            List<double> m = new List<double>();
            List<double> sr = new List<double>();
            List<double> sr1 = new List<double>();
            List<double> sr2 = new List<double>();


            double h = 33000;

            for (double CAS = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise); CAS <= 500; CAS++)
            {
                double TAS = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(CAS));
                double a = AtmosphereEnviroment.Get_a(h);
                double M = TAS / a;

                m.Add(M);

                double ff = A306.Get_ff(Aircraft.FlightPhase.Cruise, m: A306.m_ref - (A306.m_ref - A306.m_min) * 0.5, h: h, CAS: Units.kt2mps(CAS));
                ff /= 60;

                double SR = TAS / ff;
                sr.Add(SR);

                ff = A306.Get_ff(Aircraft.FlightPhase.Cruise, m: A306.m_ref - (A306.m_ref - A306.m_min) * 0.7, h: h, CAS: Units.kt2mps(CAS));
                ff /= 60;

                SR = TAS / ff;
                sr1.Add(SR);

                ff = A306.Get_ff(Aircraft.FlightPhase.Cruise, m: A306.m_ref - (A306.m_ref - A306.m_min) * 0.9, h: h, CAS: Units.kt2mps(CAS));
                ff /= 60;

                SR = TAS / ff;
                sr2.Add(SR);
            }



            int index_sr_max = sr.IndexOf(sr.Max());
            int index_sr1_max = sr1.IndexOf(sr1.Max());
            int index_sr2_max = sr2.IndexOf(sr2.Max());

            List<double> v_sr_max = new List<double>() { m[index_sr_max], m[index_sr1_max], m[index_sr2_max] };
            List<double> sr_max = new List<double>() { sr[index_sr_max], sr1[index_sr1_max], sr2[index_sr2_max] };




            int index_sr_99max = index_sr_max;
            for (int i = index_sr_max; i < sr.Count; i++)
                if (sr[i] <= sr.Max() * 0.99)
                {
                    index_sr_99max = i;
                    break;
                }

            int index_sr1_99max = index_sr1_max;
            for (int i = index_sr1_max; i < sr1.Count; i++)
                if (sr1[i] <= sr1.Max() * 0.99)
                {
                    index_sr1_99max = i;
                    break;
                }


            int index_sr2_99max = index_sr2_max;
            for (int i = index_sr2_max; i < sr2.Count; i++)
                if (sr2[i] <= sr2.Max() * 0.99)
                {
                    index_sr2_99max = i;
                    break;
                }



            List<double> v_sr_99max = new List<double>() { m[index_sr_99max], m[index_sr1_99max], m[index_sr2_99max] };
            List<double> sr_99max = new List<double>() { sr[index_sr_99max], sr1[index_sr1_99max], sr2[index_sr2_99max] };




            List<double> v_constM = new List<double>() { m[index_sr_99max], m[index_sr_99max], m[index_sr_99max], m[index_sr_99max] };
            List<double> sr_constM = new List<double>() { sr[index_sr_99max], sr1[index_sr_99max], sr2[index_sr_99max], sr2[index_sr_99max] * 1.05 };

            chart19.ChartAreas[0].AxisX.Minimum = (int)((m[0] - m[0] * 0.01) * 10) / 10.0;
            chart19.ChartAreas[0].AxisX.LabelStyle.Interval = 0.1;
            chart19.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart19.ChartAreas[0].AxisX.Title = "M";
            chart19.ChartAreas[0].AxisX.TitleFont = labelFont;

            chart19.ChartAreas[0].AxisY.LabelStyle.Interval = 20;
            chart19.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart19.ChartAreas[0].AxisY.Title = "SR m/kg";
            chart19.ChartAreas[0].AxisY.TitleFont = labelFont;

            chart19.Series[0].Points.DataBindXY(m, sr);
            chart19.Series[1].Points.DataBindXY(m, sr1);
            chart19.Series[2].Points.DataBindXY(m, sr2);
            chart19.Series[4].Points.DataBindXY(v_sr_max, sr_max);
            chart19.Series[5].Points.DataBindXY(v_sr_99max, sr_99max);
            chart19.Series[7].Points.DataBindXY(v_constM, sr_constM);

            chart19.Legends[0].Enabled = true;
            chart19.Legends[0].Font = legendFont;
            chart19.Legends[0].Position = new System.Windows.Forms.DataVisualization.Charting.ElementPosition(70, 45, 30, 34);
            foreach (var series in chart19.Series)
                series.IsVisibleInLegend = false;
            chart19.Series[0].IsVisibleInLegend = true;
            chart19.Series[0].LegendText = "m_ref - 0.5 (m_ref - m_min)";
            chart19.Series[1].IsVisibleInLegend = true;
            chart19.Series[1].LegendText = "m_ref - 0.7 (m_ref - m_min)";
            chart19.Series[2].IsVisibleInLegend = true;
            chart19.Series[2].LegendText = "m_ref - 0.9 (m_ref - m_min)";
            chart19.Series[4].IsVisibleInLegend = true;
            chart19.Series[4].LegendText = "MRC";
            chart19.Series[5].IsVisibleInLegend = true;
            chart19.Series[5].LegendText = "LRC";
            chart19.Series[7].IsVisibleInLegend = true;
            chart19.Series[7].LegendText = "Constant M cruise";
        }

        // 燃油里程与高度：最佳巡航高度
        private void button31_Click(object sender, EventArgs e)
        {
            List<double> h = new List<double>();
            List<double> sr = new List<double>();
            List<double> sr1 = new List<double>();
            List<double> sr2 = new List<double>();


            for (double alt = 30000; alt <= 43000; alt += 1000)
            {
                h.Add(alt / 100);

                double TAS = A306.M_MO * AtmosphereEnviroment.Get_a(alt);
                double CAS = AtmosphereEnviroment.Get_CAS(alt, TAS);


                double ff = A306.Get_ff(Aircraft.FlightPhase.Cruise, m: A306.m_max - (A306.m_max - A306.m_ref) * 0.6, h: alt, CAS: CAS);
                ff /= 60;
                double SR = TAS / ff;
                sr.Add(SR);

                ff = A306.Get_ff(Aircraft.FlightPhase.Cruise, m: A306.m_max - (A306.m_max - A306.m_ref) * 0.8, h: alt, CAS: CAS);
                ff /= 60;
                SR = TAS / ff;
                sr1.Add(SR);

                ff = A306.Get_ff(Aircraft.FlightPhase.Cruise, h: alt, CAS: CAS);
                ff /= 60;
                SR = TAS / ff;
                sr2.Add(SR);
            }


            List<double> sr_sr_max = new List<double>() { sr.Max(), sr1.Max(), sr2.Max() };
            List<double> h_sr_max = new List<double>() { h[sr.IndexOf(sr.Max())], h[sr1.IndexOf(sr1.Max())], h[sr2.IndexOf(sr2.Max())] };


            chart20.ChartAreas[0].AxisX.Minimum = (int)(sr[0] - sr[0] * 0.02);
            chart20.ChartAreas[0].AxisX.Maximum = (int)(sr2[sr.Count - 1] + sr2[sr.Count - 1] * 0.05);
            chart20.ChartAreas[0].AxisX.LabelStyle.Interval = 5;
            chart20.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart20.ChartAreas[0].AxisX.Title = "SR m/kg";
            chart20.ChartAreas[0].AxisX.TitleFont = labelFont;

            chart20.ChartAreas[0].AxisY.Minimum = 290;
            chart20.ChartAreas[0].AxisY.Maximum = 440;
            chart20.ChartAreas[0].AxisY.LabelStyle.Interval = 10;
            chart20.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart20.ChartAreas[0].AxisY.Title = "Altitude 100 ft";
            chart20.ChartAreas[0].AxisY.TitleFont = labelFont;

            chart20.Series[0].Points.DataBindXY(sr, h);
            chart20.Series[1].Points.DataBindXY(sr1, h);
            chart20.Series[2].Points.DataBindXY(sr2, h);
            chart20.Series[4].Points.DataBindXY(sr_sr_max, h_sr_max);

            chart20.Legends[0].Enabled = true;
            chart20.Legends[0].Font = legendFont;
            chart20.Legends[0].Position = new System.Windows.Forms.DataVisualization.Charting.ElementPosition(70, 55, 30, 22);
            foreach (var series in chart20.Series)
                series.IsVisibleInLegend = false;
            chart20.Series[0].IsVisibleInLegend = true;
            chart20.Series[0].LegendText = "m_ref + 0.4 (m_max - m_ref)";
            chart20.Series[1].IsVisibleInLegend = true;
            chart20.Series[1].LegendText = "m_ref + 0.2 (m_max - m_ref)";
            chart20.Series[2].IsVisibleInLegend = true;
            chart20.Series[2].LegendText = "m_ref";
            chart20.Series[4].IsVisibleInLegend = true;
            chart20.Series[4].LegendText = "Optimal altitude";
        }

        // 巡航数值表
        private void button32_Click(object sender, EventArgs e)
        {
            textBox2.Text = "\t\t\tCRUISE" + System.Environment.NewLine;
            textBox2.Text += "\tCAS/M: 250/310/.79    Temperature: ISA" + System.Environment.NewLine;
            textBox2.Text += "\tlo: 104.4t     nom: 140t    hi: 171.7t" + System.Environment.NewLine;
            textBox2.Text += "\t======================================" + System.Environment.NewLine;
            textBox2.Text += "\tFL\t TAS\t\tfuel" + System.Environment.NewLine;
            textBox2.Text += "\t\t[kts]\t      [kg/min]" + System.Environment.NewLine;
            textBox2.Text += "\t\t\tlo\tnom\thi" + System.Environment.NewLine;
            textBox2.Text += "\t======================================" + System.Environment.NewLine;

            double m_low = 104400;
            double m_nominal = 140000;
            double m_high = 171700;

            double h_cross = AtmosphereEnviroment.Get_h_cross(Units.kt2mps(A306.v_cr_2), A306.M_cr);

            for (double h = 3000; h <= 41000; h += 2000)
            {
                double CAS = A306.Get_v_cruise(h);
                double TAS = 0;
                double M_cr = A306.M_cr;

                if (h <= h_cross)
                {
                    TAS = Units.mps2kt(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(CAS)));
                    TAS = Math.Round(TAS);
                }
                else
                {
                    TAS = M_cr * AtmosphereEnviroment.Get_a(h);
                    TAS = Math.Round(Units.mps2kt(TAS));
                }


                double ff_low;
                ff_low = A306.Get_ff(Aircraft.FlightPhase.Cruise, m_low, h: h, CAS: Units.kt2mps(CAS));
                ff_low = Math.Round(ff_low, 1);


                double ff_nominal;
                if (h == 41000)
                    ff_nominal = A306.Get_ff(Aircraft.FlightPhase.Cruise, m_nominal, h: h, CAS: Units.kt2mps(CAS), thrustMode: Aircraft.ThrustMode.MaximumCruiseThrust);
                else
                    ff_nominal = A306.Get_ff(Aircraft.FlightPhase.Cruise, m_nominal, h: h, CAS: Units.kt2mps(CAS));
                ff_nominal = Math.Round(ff_nominal, 1);


                double ff_high;
                if (h >= 35000)
                    ff_high = A306.Get_ff(Aircraft.FlightPhase.Cruise, m_high, h: h, CAS: Units.kt2mps(CAS), thrustMode: Aircraft.ThrustMode.MaximumCruiseThrust);
                else
                    ff_high = A306.Get_ff(Aircraft.FlightPhase.Cruise, m_high, h: h, CAS: Units.kt2mps(CAS));
                ff_high = Math.Round(ff_high, 1);


                if(h == 41000)
                    textBox2.Text += $"\t{h / 100}\t{TAS}\t{ff_low}\t{ff_nominal}\t{ff_high}" ;
                else
                    textBox2.Text += $"\t{h / 100}\t{TAS}\t{ff_low}\t{ff_nominal}\t{ff_high}" + System.Environment.NewLine;


                if (h == 3000)
                    h = 2000;
                else if (h == 28000)
                    h = 27000;
            }
        }

        // 下降梯度图及飘降速度
        private void button33_Click(object sender, EventArgs e)
        {
            List<double> tas = new List<double>();
            List<double> dg = new List<double>();

            double W = A306.m_ref * AtmosphereEnviroment.g;
            double h = 18000;

            for (double CAS = A306.Get_v_stall(h, Aircraft.FlightPhase.Descent); CAS <= 600; CAS++)
            {
                double TAS = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(CAS));
                tas.Add(Units.mps2kt(TAS));


                double D = A306.Get_D(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Descent);
                double T = A306.Get_T(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Descent);
                double FM = A306.Get_functionM(h, Aircraft.SpeedMode.ConstantCAS, Units.kt2mps(CAS));

                double DG = (D - T) / W / FM;
                dg.Add(DG);
            }


            int v_e_index = dg.IndexOf(dg.Min());
            List<double> v_e_tas = new List<double>() { 0, tas[v_e_index], tas[v_e_index] * 1.4 };
            List<double> v_e_dg = new List<double>() { dg[v_e_index], dg[v_e_index], dg[v_e_index] };


            chart21.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
            chart21.ChartAreas[0].AxisX.LabelStyle.Interval = 50;
            chart21.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart21.ChartAreas[0].AxisX.Title = "TAS kt";
            chart21.ChartAreas[0].AxisX.TitleFont = labelFont;

            chart21.ChartAreas[0].AxisY.LabelStyle.Interval = 0.02;
            chart21.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart21.ChartAreas[0].AxisY.Title = "Descent Gradient %";
            chart21.ChartAreas[0].AxisY.TitleFont = labelFont;

            chart21.Series[0].Points.DataBindXY(tas, dg);
            chart21.Series[4].Points.DataBindXY(v_e_tas, v_e_dg);
        }

        // 下降率图及飘降速度
        private void button34_Click(object sender, EventArgs e)
        {
            List<double> tas = new List<double>();
            List<double> rocd = new List<double>();
            List<double> slope = new List<double>();

            double h = 18000;

            for (double CAS = A306.Get_v_stall(h, Aircraft.FlightPhase.Descent) * 0.5; CAS <= 600; CAS++)
            {
                double TAS = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(CAS));
                tas.Add(Units.mps2kt(TAS));

                double ROCD = (-1) * A306.Get_ROCD(h, Aircraft.SpeedMode.ConstantCAS, Units.kt2mps(CAS), Aircraft.FlightPhase.Descent);
                rocd.Add(ROCD);

                double Slope = ROCD / TAS;
                slope.Add(Slope);
            }



            int v_e_index = slope.IndexOf(slope.Min());
            List<double> v_e_tas = new List<double>() { 0, tas[v_e_index], tas[v_e_index] * 1.5 };
            List<double> v_e_dg = new List<double>() { 0, rocd[v_e_index], rocd[v_e_index] * 1.5 };


            chart22.ChartAreas[0].AxisX.Minimum = 0;
            chart22.ChartAreas[0].AxisX.LabelStyle.Interval = 50;
            chart22.ChartAreas[0].AxisX.LabelStyle.Font = axisFont;
            chart22.ChartAreas[0].AxisX.Title = "TAS kt";
            chart22.ChartAreas[0].AxisX.TitleFont = labelFont;

            chart22.ChartAreas[0].AxisY.LabelStyle.Interval = 1000;
            chart22.ChartAreas[0].AxisY.LabelStyle.Font = axisFont;
            chart22.ChartAreas[0].AxisY.Title = "Rate of descent ft/min";
            chart22.ChartAreas[0].AxisY.TitleFont = labelFont;

            chart22.Series[0].Points.DataBindXY(tas, rocd);
            chart22.Series[4].Points.DataBindXY(v_e_tas, v_e_dg);
        }

        // 下降数值表
        private void button35_Click(object sender, EventArgs e)
        {
            textBox3.Text = "\t\t\tDESCENT" + System.Environment.NewLine;
            textBox3.Text += "\tCAS/M: 250/280/.79" + System.Environment.NewLine;
            textBox3.Text += "\tnom: 140t     Temperature: ISA" + System.Environment.NewLine;
            textBox3.Text += "\t==============================" + System.Environment.NewLine;
            textBox3.Text += "\tFL\t TAS\tROCD\tfuel" + System.Environment.NewLine;
            textBox3.Text += "\t\t[kts]\t[fpm] [kg/min]" + System.Environment.NewLine;
            textBox3.Text += "\t\t\tnom\tnom" + System.Environment.NewLine;
            textBox3.Text += "\t==============================" + System.Environment.NewLine;

            double h_cross = AtmosphereEnviroment.Get_h_cross(Units.kt2mps(A306.v_des_1), A306.M_des);

            for (double h = 0; h <= 41000;)
            {
                double CAS = 0;
                double TAS = 0;
                double M_des = A306.M_des;

                if (h <= h_cross)
                {
                    CAS = A306.Get_v_descent(h);
                    TAS = Units.mps2kt(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(CAS)));
                    TAS = Math.Round(TAS);
                }
                else
                {
                    TAS = M_des * AtmosphereEnviroment.Get_a(h);
                    CAS = AtmosphereEnviroment.Get_CAS(h, TAS);
                    TAS = Math.Round(Units.mps2kt(TAS));
                }


                double ROCD = 0;
                if (h > h_cross)
                    ROCD = (-1) * A306.Get_ROCD(h, Aircraft.SpeedMode.ConstantMach, M_des, Aircraft.FlightPhase.Descent);
                else if (h > 2000)
                    ROCD = (-1) * A306.Get_ROCD(h, Aircraft.SpeedMode.ConstantCAS, Units.kt2mps(CAS), Aircraft.FlightPhase.Descent);
                else if (h == 2000)
                    ROCD = (-1) * A306.Get_ROCD(h, Aircraft.SpeedMode.ConstantCAS, Units.kt2mps(CAS), Aircraft.FlightPhase.Approach);
                else
                    ROCD = (-1) * A306.Get_ROCD(h, Aircraft.SpeedMode.ConstantCAS, Units.kt2mps(CAS), Aircraft.FlightPhase.Landing);
                ROCD = Math.Round(ROCD);


                double ff;
                if (h > 2000)
                    ff = A306.Get_ff(Aircraft.FlightPhase.Descent, h: h, CAS: Units.kt2mps(CAS), thrustMode: Aircraft.ThrustMode.IdleThrust);
                else if (h == 2000)
                    ff = A306.Get_ff(Aircraft.FlightPhase.Approach, h: h, CAS: Units.kt2mps(CAS), thrustMode: Aircraft.ThrustMode.IdleThrust);
                else
                    ff = A306.Get_ff(Aircraft.FlightPhase.Landing, h: h, CAS: Units.kt2mps(CAS), thrustMode: Aircraft.ThrustMode.IdleThrust);
                ff = Math.Round(ff, 1);


                if(h == 41000)
                    textBox3.Text += $"\t{h / 100}\t{TAS}\t{ROCD}\t{ff}";
                else if (h == 0)
                    textBox3.Text += $"\t{h}\t{TAS}\t{ROCD}\t{ff}" + System.Environment.NewLine;
                else
                    textBox3.Text += $"\t{h / 100}\t{TAS}\t{ROCD}\t{ff}" + System.Environment.NewLine;


                if (h < 2000)
                    h += 500;
                else if (h < 4000)
                    h += 1000;
                else if (h < 28000)
                    h += 2000;
                else if (h == 28000)
                    h += 1000;
                else
                    h += 2000;
            }
        }
    }
}
