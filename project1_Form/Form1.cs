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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        /* 1------------------------------------------------print Thrust - v fig
        List<double> tas = new List<double>();
            List<double> t = new List<double>();
            List<double> tmax = new List<double>();

            //List<double> t1 = new List<double>();
            //List<double> t2 = new List<double>();

            double h = 18000;
            //double vmin = A306.Get_v_min(h, Aircraft.FlightPhase.Cruise);
            double vmin = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);


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

                //double T1 = A306.Get_T(h, CAS, Aircraft.FlightPhase.Cruise, m:A306.m_ref + A306.m_ref * 0.4) / 10000;
                //double T2 = A306.Get_T(h, CAS, Aircraft.FlightPhase.Cruise, m: A306.m_ref + A306.m_ref * 0.8) / 10000;
                //t1.Add(T1);
                //t2.Add(T2);
            }


            for (int i = 0; i < tas.Count; i++)
                tas[i] = (int)tas[i];

            chart1.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
            chart1.Series[0].Points.DataBindXY(tas, t);
            chart1.Series[1].Points.DataBindXY(tas,tmax);
            
            
            //chart1.Series[2].Points.DataBindXY(tas, t1);
            //chart1.Series[3].Points.DataBindXY(tas, t2);


            */








        /*2----------------------------------------print CD - CL polar with maximum k
        List<double> c_L = new List<double>();
        List<double> c_D = new List<double>();
        List<double> k = new List<double>();

        for(double C_L = 0; C_L < 2; C_L += 0.01)
        {
            c_L.Add(C_L);
            double C_D = A306.C_D0_CR + A306.C_D2_CR * C_L * C_L;
            c_D.Add(C_D);
            double K = C_L / C_D;
            k.Add(K);
        }

        int index = k.IndexOf(k.Max());
        List<double> c_D_k = new List<double>() { 0, c_D[index], c_D[index] * 2};
        List<double> c_L_k = new List<double>() { 0, c_L[index], c_L[index] * 2 };

        chart1.ChartAreas[0].AxisX.Minimum = 0;
        chart1.Series[0].Points.DataBindXY(c_D, c_L);

        chart1.Series[1].Points.DataBindXY(c_D_k, c_L_k);

        */





        /*3------------------------------------------print Total Drag, D0, Di with TAS
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
            tas[i] = (int)tas[i];

        chart1.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
        chart1.Series[0].Points.DataBindXY(tas, d);

        chart1.Series[1].Points.DataBindXY(tas, d0);
        chart1.Series[2].Points.DataBindXY(tas, di);

        */




        /*4----------------------------------print Thrust - v with h variable
        List<double> tas = new List<double>();
        List<double> t = new List<double>();
        List<double> tas1 = new List<double>();
        List<double> t1 = new List<double>();
        List<double> tas2 = new List<double>();
        List<double> t2 = new List<double>();



        double h = 15000;
        double h1 = 24000;
        double h2 = 33000;

        //double vmin = A306.Get_v_min(h, Aircraft.FlightPhase.Cruise);
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
            tas[i] = (int)tas[i];

        chart1.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
        chart1.Series[0].Points.DataBindXY(tas, t);
        chart1.Series[1].Points.DataBindXY(tas1, t1);
        chart1.Series[2].Points.DataBindXY(tas2, t2);

        */





        /*5---------------------------------print maxThrust - v with h variable
                    List<double> tas = new List<double>();
        List<double> tmax = new List<double>();
        List<double> tas1 = new List<double>();
        List<double> tmax1 = new List<double>();
        List<double> tas2 = new List<double>();
        List<double> tmax2 = new List<double>();



        double h = 15000;
        double h1 = 24000;
        double h2 = 33000;

        //double vmin = A306.Get_v_min(h, Aircraft.FlightPhase.Cruise);
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
            tas[i] = (int)tas[i];

        chart1.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
        chart1.Series[0].Points.DataBindXY(tas, tmax);
        chart1.Series[1].Points.DataBindXY(tas1, tmax1);
        chart1.Series[2].Points.DataBindXY(tas2, tmax2);
        */





        /*6------------------------------------print Thrust,maxThrust - v moving with h
        List<double> tas = new List<double>();
        List<double> t = new List<double>();
        List<double> tmax = new List<double>();
        List<double> tas1 = new List<double>();
        List<double> t1 = new List<double>();
        List<double> tmax1 = new List<double>();



        double h = 15000;
        double h1 = 32000;

        //double vmin = A306.Get_v_min(h, Aircraft.FlightPhase.Cruise);
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
            tas[i] = (int)tas[i];

        chart1.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
        chart1.Series[0].Points.DataBindXY(tas, t);
        chart1.Series[1].Points.DataBindXY(tas, tmax);
        chart1.Series[2].Points.DataBindXY(tas1, t1);
        chart1.Series[3].Points.DataBindXY(tas1, tmax1);
        */






        /*7---------------------------------------print v_e and v_MRC in Thrust - v fig
                    List<double> tas = new List<double>();
        List<double> t = new List<double>();
        List<double> slope = new List<double>();

        double h = 18000;
        //double vmin = A306.Get_v_min(h, Aircraft.FlightPhase.Cruise);
        double vmin = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);


        for (double TAS
            = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(vmin));
            TAS < Units.kt2mps(1000); TAS++)
        {
            tas.Add(TAS);
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

        for (int i = 0; i < tas.Count; i++)
            tas[i] = (int)tas[i];

        chart1.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
        chart1.Series[0].Points.DataBindXY(tas, t);

        chart1.Series[1].Points.DataBindXY(V_e_tas, V_e_t);
        chart1.Series[2].Points.DataBindXY(V_MRC_tas, V_MRC_t);
        */





        /*8-------------------------------print power(thrust * v), maxPower - v fig
        List<double> tas = new List<double>();
        List<double> w = new List<double>();
        List<double> wmax = new List<double>();


        double h = 18000;
        //double vmin = A306.Get_v_min(h, Aircraft.FlightPhase.Cruise);
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
            tas[i] = (int)tas[i];

        chart1.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
        chart1.Series[0].Points.DataBindXY(tas, w);
        chart1.Series[1].Points.DataBindXY(tas, wmax);

        */





        /*9---------------print thrust, power - v fig, mark v_e in two curves and check
                    List<double> tas = new List<double>();
        List<double> t = new List<double>();
        List<double> w = new List<double>();
        List<double> slope = new List<double>();

        double h = 32000;
        //double vmin = A306.Get_v_min(h, Aircraft.FlightPhase.Cruise);
        double vmin = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);


        for (double TAS
            = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(vmin));
            TAS < Units.kt2mps(700); TAS++)
        {
            tas.Add(TAS);
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

        List<double> V_e_FromPower_tas = new List<double>() { 0, tas[indexOfV_eFromPower], tas[indexOfV_eFromPower]  * 1.5};
        List<double> V_e_FromPower_power = new List<double>() { 0, w[indexOfV_eFromPower], w[indexOfV_eFromPower] * 1.5 };

        List<double> V_e_tas = new List<double>() { tas[indexOfV_eFromThrust], tas[indexOfV_eFromPower], tas[indexOfV_eFromPower]};
        List<double> V_e_ThrustPower = new List<double>() { 0, t[indexOfV_eFromThrust], w[indexOfV_eFromPower] };

        for (int i = 0; i < tas.Count; i++)
            tas[i] = (int)tas[i];

        chart1.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
        chart1.Series[0].Points.DataBindXY(tas, t);
        chart1.Series[2].Points.DataBindXY(tas, w);
        chart1.Series[4].Points.DataBindXY(V_e_FromThrust_tas, V_e_FromThrust_t);
        chart1.Series[5].Points.DataBindXY(V_e_FromPower_tas, V_e_FromPower_power);
        chart1.Series[6].Points.DataBindXY(V_e_tas, V_e_ThrustPower);
        */







        /*10------------------------------------print redundant thrust in thrust - v fig
                    List<double> tas = new List<double>();
        List<double> t = new List<double>();
        List<double> tmax = new List<double>();

        double h = 35000;
        //double vmin = A306.Get_v_min(h, Aircraft.FlightPhase.Cruise);
        double vmin = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);


        double initialTAS = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(vmin));
        double tryCAS = AtmosphereEnviroment.Get_CAS(h, initialTAS);
        double tryT = A306.Get_T(h, tryCAS, Aircraft.FlightPhase.Cruise);
        double tryTmax = A306.Get_T_max_cruise(h, tryCAS);
        if (tryT > tryTmax)
            for(; initialTAS < Units.kt2mps(1000); initialTAS += 0.1)
            {
                tryCAS = AtmosphereEnviroment.Get_CAS(h, initialTAS);
                tryT = A306.Get_T(h, tryCAS, Aircraft.FlightPhase.Cruise);
                tryTmax = A306.Get_T_max_cruise(h, tryCAS);
                if (tryTmax > tryT) break;
            }

        for (double TAS
            = initialTAS; 
            TAS < Units.kt2mps(1000); TAS++)
        {
            tas.Add(TAS);
            double CAS = AtmosphereEnviroment.Get_CAS(h, TAS);
            double T = A306.Get_T(h, CAS, Aircraft.FlightPhase.Cruise);
            double Tmax = A306.Get_T_max_cruise(h, CAS);

            t.Add(T);
            tmax.Add(Tmax);
            if (Tmax < T) break;
        }


        for (int i = 0; i < tas.Count; i++)
            tas[i] = (int)tas[i];

        chart1.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
        chart1.Series[0].Points.DataBindXY(tas, t);
        chart1.Series[1].Points.DataBindXY(tas, tmax);
        */







        /*11--------------------------print redundant thrust - v fig and mark v_e therein
                    List<double> tas = new List<double>();
        List<double> t = new List<double>();
        List<double> tmax = new List<double>();
        List<double> redundant_t = new List<double>();

        double h = 28000;
        //double vmin = A306.Get_v_min(h, Aircraft.FlightPhase.Cruise);
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
            if(redundant_t[i] < 0)
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
            else if(redundant_t.Max() <= 5)
                for (int i = 0; i < redundant_t.Count; i++)
                    redundant_t[i] *= 10;

        int indexOfV_e = redundant_t.IndexOf(redundant_t.Max());
        List<double> V_e_tas = new List<double>() { 0, tas[indexOfV_e] };
        List<double> V_e_redundantT = new List<double>() { redundant_t[indexOfV_e], redundant_t[indexOfV_e] };


        for (int i = 0; i < tas.Count; i++)
            tas[i] = (int)tas[i];

        chart1.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.2);
        chart1.Series[0].Points.DataBindXY(tas, redundant_t);
        chart1.Series[4].Points.DataBindXY(V_e_tas, V_e_redundantT);
        */






        /*12--------------print redundant power - v fig, mark v_e and v_fast_climb therein
                    List<double> tas = new List<double>();
        List<double> w = new List<double>();
        List<double> wmax = new List<double>();
        List<double> redundant_w = new List<double>();
        List<double> slope = new List<double>();

        double h = 18000;
        //double vmin = A306.Get_v_min(h, Aircraft.FlightPhase.Cruise);
        double vmin = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise);


        for (double TAS
            = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(vmin));
            TAS < Units.kt2mps(1000); TAS++)
        {
            tas.Add(TAS);
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


        for(int i = 0; i < tas.Count; i++)
            if(redundant_w[i] < 0)
            {
                tas.Remove(tas[i]);
                redundant_w.Remove(redundant_w[i]);
                i--;
            }


        if (redundant_w.Max() <= 1)
            for (int i = 0; i < redundant_w.Count; i++)
                redundant_w[i] *= 100;
        else if(redundant_w.Max() <= 5)
            for (int i = 0; i < redundant_w.Count; i++)
                redundant_w[i] *= 10;


        for(int i = 0; i < redundant_w.Count; i++)
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



        for (int i = 0; i < tas.Count; i++)
            tas[i] = (int)tas[i];

        chart1.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.2);
        chart1.Series[0].Points.DataBindXY(tas, redundant_w);
        chart1.Series[4].Points.DataBindXY(v_e_tas, v_e_redundantW);
        chart1.Series[5].Points.DataBindXY(v_fast_climb_tas, v_fast_climb_redundantW);
        */






        /*13----------------------------------------------------print h-v envelope without trim
         public double GetRedundantT(string fileName, double h, double CAS)
    {
        Aircraft A306 = new Aircraft(fileName);
        return A306.Get_T_max_cruise(h, Units.kt2mps(CAS)) - A306.Get_T(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Cruise);
    }






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

            for(double h = 32000; ; h++)
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

                if(thrust.Min() >= tmax)
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
                double upperBounday = 600;
                double step = 1;


                const double EPS = 1;
                List<double> vRoot = new List<double>();
                double vToCalculate = lowerBoundary;
                double redundantT = GetRedundantT(fileName, h, vToCalculate);
                while (vToCalculate <= upperBounday + step / 2)
                    if (Math.Abs(redundantT) < EPS)
                    {
                        vRoot.Add(vToCalculate);
                        vToCalculate = vToCalculate + step / 2;
                        redundantT = GetRedundantT(fileName, h, vToCalculate);
                    }
                    else
                    {
                        double nextvToCalculate = vToCalculate + step;
                        double nextRedundantT = GetRedundantT(fileName, h, nextvToCalculate);
                        if (Math.Abs(nextRedundantT) < EPS)
                        {
                            vRoot.Add(nextvToCalculate);
                            vToCalculate = nextvToCalculate + step / 2;
                            redundantT = GetRedundantT(fileName, h, vToCalculate);
                        }
                        else if (redundantT * nextRedundantT > 0)
                        {
                            vToCalculate = nextvToCalculate;
                            redundantT = nextRedundantT;
                        }
                        else
                        {
                            bool find = false;
                            while (!find)
                                if (Math.Abs(nextvToCalculate - vToCalculate) < EPS)
                                {
                                    vRoot.Add((vToCalculate + nextvToCalculate) / 2);
                                    vToCalculate = nextvToCalculate + step / 2;
                                    redundantT = GetRedundantT(fileName, h, vToCalculate);
                                    find = true;
                                }
                                else
                                {
                                    double vMiddle = (vToCalculate + nextvToCalculate) / 2;
                                    double redundantTMiddle = GetRedundantT(fileName, h, vMiddle);
                                    if (Math.Abs(redundantTMiddle) < EPS)
                                    {
                                        vRoot.Add(vMiddle);
                                        find = true;
                                        vToCalculate = vMiddle + step / 2;
                                        redundantT = GetRedundantT(fileName, h, vToCalculate);
                                    }
                                    else if (redundantT * redundantTMiddle < 0)
                                    {
                                        nextvToCalculate = vMiddle;
                                        nextRedundantT = redundantTMiddle;
                                    }
                                    else
                                    {
                                        vToCalculate = vMiddle;
                                        redundantT = redundantTMiddle;
                                    }
                                }
                        }
                    }
                if (vRoot.Count >= 1)
                {
                    v_max_t.Add(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(vRoot.Max())));
                    if (h >= initialh && vRoot.Count >= 2)
                        v_min_t.Add(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(vRoot.Min())));
                }
            }


            for (double h = 15000; h <= h_ceiling_boundary; h += 1000)
            {
                altitude_v_min.Add(h);


                v_buffet.Add(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(A306.Get_v_min(h, Aircraft.FlightPhase.Cruise))));

                //double M = A306.SolveBuffetingM(h);
                //v_buffet.Add(M * AtmosphereEnviroment.Get_a(h));
            }




            double h_ceiling_middle = h_ceiling_boundary / 2 + h_ceiling / 2;


            double lowerBoundary1 = A306.Get_v_stall(h_ceiling_middle, Aircraft.FlightPhase.Cruise);
            double upperBounday1 = 600;
            double step1 = 1;


            const double EPS1 = 1;
            List<double> vRoot1 = new List<double>();
            double vToCalculate1 = lowerBoundary1;
            double redundantT1 = GetRedundantT(fileName, h_ceiling_middle, vToCalculate1);
            while (vToCalculate1 <= upperBounday1 + step1 / 2)
                if (Math.Abs(redundantT1) < EPS1)
                {
                    vRoot1.Add(vToCalculate1);
                    vToCalculate1 = vToCalculate1 + step1 / 2;
                    redundantT1 = GetRedundantT(fileName, h_ceiling_middle, vToCalculate1);
                }
                else
                {
                    double nextvToCalculate = vToCalculate1 + step1;
                    double nextRedundantT = GetRedundantT(fileName, h_ceiling_middle, nextvToCalculate);
                    if (Math.Abs(nextRedundantT) < EPS1)
                    {
                        vRoot1.Add(nextvToCalculate);
                        vToCalculate1 = nextvToCalculate + step1 / 2;
                        redundantT1 = GetRedundantT(fileName, h_ceiling_middle, vToCalculate1);
                    }
                    else if (redundantT1 * nextRedundantT > 0)
                    {
                        vToCalculate1 = nextvToCalculate;
                        redundantT1 = nextRedundantT;
                    }
                    else
                    {
                        bool find = false;
                        while (!find)
                            if (Math.Abs(nextvToCalculate - vToCalculate1) < EPS1)
                            {
                                vRoot1.Add((vToCalculate1 + nextvToCalculate) / 2);
                                vToCalculate1 = nextvToCalculate + step1 / 2;
                                redundantT1 = GetRedundantT(fileName, h_ceiling_middle, vToCalculate1);
                                find = true;
                            }
                            else
                            {
                                double vMiddle = (vToCalculate1 + nextvToCalculate) / 2;
                                double redundantTMiddle = GetRedundantT(fileName, h_ceiling_middle, vMiddle);
                                if (Math.Abs(redundantTMiddle) < EPS1)
                                {
                                    vRoot1.Add(vMiddle);
                                    find = true;
                                    vToCalculate1 = vMiddle + step1 / 2;
                                    redundantT1 = GetRedundantT(fileName, h_ceiling_middle, vToCalculate1);
                                }
                                else if (redundantT1 * redundantTMiddle < 0)
                                {
                                    nextvToCalculate = vMiddle;
                                    nextRedundantT = redundantTMiddle;
                                }
                                else
                                {
                                    vToCalculate1 = vMiddle;
                                    redundantT1 = redundantTMiddle;
                                }
                            }
                    }
                }

            if(vRoot1.Count == 2)
            {
                altitude.Add(h_ceiling_middle);
                v_max_t.Add(AtmosphereEnviroment.Get_TAS(h_ceiling_middle, Units.kt2mps(vRoot1.Max())));
            }



            v_min_t.Add(AtmosphereEnviroment.Get_TAS(h_ceiling, Units.kt2mps(v_ceiling_CAS)));
            altitude_v_min_t.Add(h_ceiling);
            v_max_t.Add(AtmosphereEnviroment.Get_TAS(h_ceiling, Units.kt2mps(v_ceiling_CAS)));
            altitude.Add(h_ceiling);




            List<double> altitude_v_MO = new List<double>();
            List<double> v_v_MO = new List<double>();
            List<double> altitude_M_MO = new List<double>();
            List<double> v_M_MO = new List<double>();


            double h_cross = AtmosphereEnviroment.Get_h_cross(Units.kt2mps(A306.v_MO), A306.M_MO);

            for(double h = 15000; h <= h_ceiling_boundary + 1000; h += 1000)
            {
                if(h == ((int)h_cross / 1000) * 1000)
                {
                    altitude_v_MO.Add(h_cross);
                    v_v_MO.Add(AtmosphereEnviroment.Get_TAS(h_cross, Units.kt2mps(A306.v_MO)));
                    altitude_M_MO.Add(h_cross);
                    v_M_MO.Add(A306.M_MO * AtmosphereEnviroment.Get_a(h_cross));
                    h = h_cross;
                }
                else if(h < h_cross)
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




            chart1.ChartAreas[0].AxisX.Minimum = (int)(vStall[0] - vStall[0] * 0.05);
            chart1.ChartAreas[0].AxisY.Minimum = 15000;
            chart1.ChartAreas[0].AxisY.LabelStyle.Interval = 2000;
            chart1.Series[0].Points.DataBindXY(v_min_t, altitude_v_min_t);
            chart1.Series[1].Points.DataBindXY(vStall, altitude_stall);
            chart1.Series[2].Points.DataBindXY(v_buffet, altitude_v_min);
            chart1.Series[3].Points.DataBindXY(v_max_t, altitude);
            chart1.Series[7].Points.DataBindXY(v_v_MO, altitude_v_MO);
            chart1.Series[8].Points.DataBindXY(v_M_MO, altitude_M_MO);

    */







        /*14----------------------------------------------print h - v final envelope

        public double GetRedundantT(string fileName, double h, double CAS)
        {
            Aircraft A306 = new Aircraft(fileName);
            return A306.Get_T_max_cruise(h, Units.kt2mps(CAS)) - A306.Get_T(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Cruise);
        }





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
                double upperBounday = 600;
                double step = 1;


                const double EPS = 1;
                List<double> vRoot = new List<double>();
                double vToCalculate = lowerBoundary;
                double redundantT = GetRedundantT(fileName, h, vToCalculate);
                while (vToCalculate <= upperBounday + step / 2)
                    if (Math.Abs(redundantT) < EPS)
                    {
                        vRoot.Add(vToCalculate);
                        vToCalculate = vToCalculate + step / 2;
                        redundantT = GetRedundantT(fileName, h, vToCalculate);
                    }
                    else
                    {
                        double nextvToCalculate = vToCalculate + step;
                        double nextRedundantT = GetRedundantT(fileName, h, nextvToCalculate);
                        if (Math.Abs(nextRedundantT) < EPS)
                        {
                            vRoot.Add(nextvToCalculate);
                            vToCalculate = nextvToCalculate + step / 2;
                            redundantT = GetRedundantT(fileName, h, vToCalculate);
                        }
                        else if (redundantT * nextRedundantT > 0)
                        {
                            vToCalculate = nextvToCalculate;
                            redundantT = nextRedundantT;
                        }
                        else
                        {
                            bool find = false;
                            while (!find)
                                if (Math.Abs(nextvToCalculate - vToCalculate) < EPS)
                                {
                                    vRoot.Add((vToCalculate + nextvToCalculate) / 2);
                                    vToCalculate = nextvToCalculate + step / 2;
                                    redundantT = GetRedundantT(fileName, h, vToCalculate);
                                    find = true;
                                }
                                else
                                {
                                    double vMiddle = (vToCalculate + nextvToCalculate) / 2;
                                    double redundantTMiddle = GetRedundantT(fileName, h, vMiddle);
                                    if (Math.Abs(redundantTMiddle) < EPS)
                                    {
                                        vRoot.Add(vMiddle);
                                        find = true;
                                        vToCalculate = vMiddle + step / 2;
                                        redundantT = GetRedundantT(fileName, h, vToCalculate);
                                    }
                                    else if (redundantT * redundantTMiddle < 0)
                                    {
                                        nextvToCalculate = vMiddle;
                                        nextRedundantT = redundantTMiddle;
                                    }
                                    else
                                    {
                                        vToCalculate = vMiddle;
                                        redundantT = redundantTMiddle;
                                    }
                                }
                        }
                    }
                if (vRoot.Count >= 1)
                {
                    v_max_t.Add(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(vRoot.Max())));
                    if (h >= initialh && vRoot.Count >= 2)
                        v_min_t.Add(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(vRoot.Min())));
                }
            }


            for (double h = 15000; h <= h_ceiling_boundary; h += 1000)
            {
                altitude_v_min.Add(h);


                v_buffet.Add(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(A306.Get_v_min(h, Aircraft.FlightPhase.Cruise))));

                //double M = A306.SolveBuffetingM(h);
                //v_buffet.Add(M * AtmosphereEnviroment.Get_a(h));
            }




            double h_ceiling_middle = h_ceiling_boundary / 2 + h_ceiling / 2;


            double lowerBoundary1 = A306.Get_v_stall(h_ceiling_middle, Aircraft.FlightPhase.Cruise);
            double upperBounday1 = 600;
            double step1 = 1;


            const double EPS1 = 1;
            List<double> vRoot1 = new List<double>();
            double vToCalculate1 = lowerBoundary1;
            double redundantT1 = GetRedundantT(fileName, h_ceiling_middle, vToCalculate1);
            while (vToCalculate1 <= upperBounday1 + step1 / 2)
                if (Math.Abs(redundantT1) < EPS1)
                {
                    vRoot1.Add(vToCalculate1);
                    vToCalculate1 = vToCalculate1 + step1 / 2;
                    redundantT1 = GetRedundantT(fileName, h_ceiling_middle, vToCalculate1);
                }
                else
                {
                    double nextvToCalculate = vToCalculate1 + step1;
                    double nextRedundantT = GetRedundantT(fileName, h_ceiling_middle, nextvToCalculate);
                    if (Math.Abs(nextRedundantT) < EPS1)
                    {
                        vRoot1.Add(nextvToCalculate);
                        vToCalculate1 = nextvToCalculate + step1 / 2;
                        redundantT1 = GetRedundantT(fileName, h_ceiling_middle, vToCalculate1);
                    }
                    else if (redundantT1 * nextRedundantT > 0)
                    {
                        vToCalculate1 = nextvToCalculate;
                        redundantT1 = nextRedundantT;
                    }
                    else
                    {
                        bool find = false;
                        while (!find)
                            if (Math.Abs(nextvToCalculate - vToCalculate1) < EPS1)
                            {
                                vRoot1.Add((vToCalculate1 + nextvToCalculate) / 2);
                                vToCalculate1 = nextvToCalculate + step1 / 2;
                                redundantT1 = GetRedundantT(fileName, h_ceiling_middle, vToCalculate1);
                                find = true;
                            }
                            else
                            {
                                double vMiddle = (vToCalculate1 + nextvToCalculate) / 2;
                                double redundantTMiddle = GetRedundantT(fileName, h_ceiling_middle, vMiddle);
                                if (Math.Abs(redundantTMiddle) < EPS1)
                                {
                                    vRoot1.Add(vMiddle);
                                    find = true;
                                    vToCalculate1 = vMiddle + step1 / 2;
                                    redundantT1 = GetRedundantT(fileName, h_ceiling_middle, vToCalculate1);
                                }
                                else if (redundantT1 * redundantTMiddle < 0)
                                {
                                    nextvToCalculate = vMiddle;
                                    nextRedundantT = redundantTMiddle;
                                }
                                else
                                {
                                    vToCalculate1 = vMiddle;
                                    redundantT1 = redundantTMiddle;
                                }
                            }
                    }
                }

            if (vRoot1.Count == 2)
            {
                altitude.Add(h_ceiling_middle);
                v_max_t.Add(AtmosphereEnviroment.Get_TAS(h_ceiling_middle, Units.kt2mps(vRoot1.Max())));
            }



            v_min_t.Add(AtmosphereEnviroment.Get_TAS(h_ceiling, Units.kt2mps(v_ceiling_CAS)));
            altitude_v_min_t.Add(h_ceiling);
            v_max_t.Add(AtmosphereEnviroment.Get_TAS(h_ceiling, Units.kt2mps(v_ceiling_CAS)));
            altitude.Add(h_ceiling);




            List<double> altitude_v_MO = new List<double>();
            List<double> v_v_MO = new List<double>();
            List<double> altitude_M_MO = new List<double>();
            List<double> v_M_MO = new List<double>();


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


            List<double> v_XAxis_max = new List<double>();
            v_XAxis_max.Add(v_max_t.Max());
            v_XAxis_max.Add(v_v_MO.Max());
            v_XAxis_max.Add(v_M_MO.Max());
            

            chart1.ChartAreas[0].AxisX.Minimum = (int)(vStall[0] - vStall[0] * 0.005);
            chart1.ChartAreas[0].AxisX.Maximum = (int)(v_XAxis_max.Max() + v_XAxis_max.Max() * 0.1);
            chart1.ChartAreas[0].AxisY.Minimum = 15000;
            chart1.ChartAreas[0].AxisY.Maximum = 41000;
            chart1.ChartAreas[0].AxisY.LabelStyle.Interval = 2000;
            chart1.Series[0].Points.DataBindXY(v_min_t, altitude_v_min_t);

            if (vBuffetWrapVStallFlag == false)
                chart1.Series[1].Points.DataBindXY(vStall, altitude_stall);

            chart1.Series[2].Points.DataBindXY(v_buffet, altitude_v_min);
            chart1.Series[3].Points.DataBindXY(v_max_t, altitude);
            chart1.Series[7].Points.DataBindXY(v_v_MO, altitude_v_MO);
            chart1.Series[8].Points.DataBindXY(v_M_MO, altitude_M_MO);

        */








        /*15-----------------------------------print C_L - v fig, but how to draw C_L_buffet - v?
                    List<double> c_L = new List<double>();
        List<double> tas = new List<double>();


        double h = 18000;

        for(double CAS = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise); CAS <= 500; CAS += 10)
        {
            double TAS = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(CAS));
            tas.Add(TAS);

            double C_L = A306.Get_C_L(h, Units.kt2mps(CAS));
            c_L.Add(C_L);
        }



        chart1.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
        chart1.ChartAreas[0].AxisY.LabelStyle.Interval = 0.0000001;
        chart1.Series[0].Points.DataBindXY(tas, c_L);
        */







        /*16--------------------------------------------------------print CG - v fig
                    List<double> cg = new List<double>();
        List<double> tas = new List<double>();


        double W = A306.m_ref * AtmosphereEnviroment.g;
        double h = 18000;


        double initialCAS = 0;
        for (double CAS = 0; CAS <= 500; CAS++)
        {
            double T = A306.Get_T(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Climb);
            double D = A306.Get_D(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Climb);
            if(T > D)
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
            tas.Add(TAS);

            double T = A306.Get_T(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Climb);
            double D = A306.Get_D(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Climb);
            double FM = A306.Get_functionM(h, Aircraft.SpeedMode.CAS, Units.kt2mps(CAS));

            double CG = (T - D) / W / FM;
            cg.Add(CG);
        }


        chart1.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
        chart1.ChartAreas[0].AxisY.Minimum = 0;
        chart1.ChartAreas[0].AxisY.LabelStyle.Interval = 0.01;
        chart1.Series[0].Points.DataBindXY(tas, cg);
        */




        /*17------------------------------------------------------print ROCD - v during climb
                    List<double> rocd = new List<double>();
        List<double> tas = new List<double>();



        double h = 18000;


        double initialCAS = 0;
        for(double CAS = 0; CAS <= 600; CAS++)
        {
            double ROCD = A306.Get_ROCD(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Climb);
            if(ROCD > 0)
            {
                initialCAS = --CAS;
                break;
            }
        }


        double endCAS = 0;
        for (double CAS = 600; CAS >= 0; CAS--)
        {
            double ROCD = A306.Get_ROCD(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Climb);
            if (ROCD > 0)
            {
                endCAS = ++CAS;
                break;
            }
        }

        for (double CAS = initialCAS; CAS <= endCAS; CAS++)
        {
            double TAS = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(CAS));
            tas.Add(TAS);

            double ROCD = A306.Get_ROCD(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Climb);
            rocd.Add(ROCD);
        }



        chart1.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
        chart1.ChartAreas[0].AxisY.Minimum = 0;
        chart1.ChartAreas[0].AxisY.LabelStyle.Interval = 100;
        chart1.Series[0].Points.DataBindXY(tas, rocd);
        */






        /*18----------------------------------------------print DG - v fig and mark v_e
            List<double> tas = new List<double>();
            List<double> dg = new List<double>();

            double W = A306.m_ref * AtmosphereEnviroment.g;
            double h = 18000;

            for (double CAS = A306.Get_v_stall(h, Aircraft.FlightPhase.Descent); CAS <= 600; CAS++)
            {
                double TAS = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(CAS));
                tas.Add(TAS);


                double D = A306.Get_D(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Descent);
                double T = A306.Get_T(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Descent);
                double FM = A306.Get_functionM(h, Aircraft.SpeedMode.CAS, Units.kt2mps(CAS));

                double DG = (D - T) / W / FM;
                dg.Add(DG);
            }


            int v_e_index = dg.IndexOf(dg.Min());
            List<double> v_e_tas = new List<double>() { tas[v_e_index], tas[v_e_index] };
            List<double> v_e_dg = new List<double>() { 0, dg[v_e_index] };


            chart1.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
            chart1.ChartAreas[0].AxisY.LabelStyle.Interval = 0.02;
            chart1.Series[0].Points.DataBindXY(tas, dg);
            chart1.Series[4].Points.DataBindXY(v_e_tas, v_e_dg);
        */





        /*19------------------------------------print ROCD - v during descent and mark v_e
                    List<double> tas = new List<double>();
        List<double> rocd = new List<double>();
        List<double> slope = new List<double>();

        double h = 18000;

        for(double CAS = A306.Get_v_stall(h, Aircraft.FlightPhase.Descent) * 0.5; CAS <= 600; CAS++)
        {
            double TAS = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(CAS));
            tas.Add(TAS);

            double ROCD = (-1) * A306.Get_ROCD(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Descent);
            rocd.Add(ROCD);

            double Slope = ROCD / TAS;
            slope.Add(Slope);
        }



        int v_e_index = slope.IndexOf(slope.Min());
        List<double> v_e_tas = new List<double>() { 0, tas[v_e_index], tas[v_e_index] * 1.5 };
        List<double> v_e_dg = new List<double>() { 0, rocd[v_e_index], rocd[v_e_index] * 1.5 };


        chart1.ChartAreas[0].AxisX.Minimum = 0;
        chart1.ChartAreas[0].AxisY.LabelStyle.Interval = 1000;
        chart1.Series[0].Points.DataBindXY(tas, rocd);
        chart1.Series[4].Points.DataBindXY(v_e_tas, v_e_dg);
        */








        /*20---------------------------------print SR - v fig and mark MRC, LRC, ConstM cruise
                    List<double> tas = new List<double>();
        List<double> sr = new List<double>();
        List<double> sr1 = new List<double>();
        List<double> sr2 = new List<double>();


        double h = 33000;

        for(double CAS = A306.Get_v_stall(h, Aircraft.FlightPhase.Cruise); CAS <= 500; CAS++)
        {
            double TAS = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(CAS));
            tas.Add(TAS);

            double ff = A306.Get_ff(Aircraft.FlightPhase.Cruise, h: h, CAS: Units.kt2mps(CAS));
            ff /= 60;

            double SR = TAS / ff;
            sr.Add(SR);

            ff = A306.Get_ff(Aircraft.FlightPhase.Cruise, m: A306.m_ref - (A306.m_ref - A306.m_min) * 0.3, h: h, CAS: Units.kt2mps(CAS));
            ff /= 60;

            SR = TAS / ff;
            sr1.Add(SR);

            ff = A306.Get_ff(Aircraft.FlightPhase.Cruise, m: A306.m_ref - (A306.m_ref - A306.m_min) * 0.6, h: h, CAS: Units.kt2mps(CAS));
            ff /= 60;

            SR = TAS / ff;
            sr2.Add(SR);
        }



        int index_sr_max = sr.IndexOf(sr.Max());
        int index_sr1_max = sr1.IndexOf(sr1.Max());
        int index_sr2_max = sr2.IndexOf(sr2.Max());

        List<double> v_sr_max = new List<double>() { tas[index_sr_max], tas[index_sr1_max], tas[index_sr2_max] };
        List<double> sr_max = new List<double>() { sr[index_sr_max], sr1[index_sr1_max], sr2[index_sr2_max] };




        int index_sr_99max = index_sr_max;
        for(int i = index_sr_max; i < sr.Count; i++)
            if(sr[i] <= sr.Max() * 0.99)
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



        List<double> v_sr_99max = new List<double>() { tas[index_sr_99max], tas[index_sr1_99max], tas[index_sr2_99max] };
        List<double> sr_99max = new List<double>() { sr[index_sr_99max], sr1[index_sr1_99max], sr2[index_sr2_99max] };




        List<double> v_constM = new List<double>() { tas[index_sr_99max], tas[index_sr_99max], tas[index_sr_99max] };
        List<double> sr_constM = new List<double>() { sr[index_sr_99max], sr1[index_sr_99max], sr2[index_sr_99max] };

        chart1.ChartAreas[0].AxisX.Minimum = (int)(tas[0] - tas[0] * 0.05);
        chart1.ChartAreas[0].AxisY.LabelStyle.Interval = 20;
        chart1.Series[0].Points.DataBindXY(tas, sr);
        chart1.Series[1].Points.DataBindXY(tas, sr1);
        chart1.Series[2].Points.DataBindXY(tas, sr2);
        chart1.Series[4].Points.DataBindXY(v_sr_max, sr_max);
        chart1.Series[5].Points.DataBindXY(v_sr_99max, sr_99max);
        chart1.Series[7].Points.DataBindXY(v_constM, sr_constM);
        */





        /*21---------------------------------------print h - SR fig and optimal altitude
        List<double> h = new List<double>();
        List<double> sr = new List<double>();
        List<double> sr1 = new List<double>();
        List<double> sr2 = new List<double>();


        for (double alt = 30000; alt <= 43000; alt += 1000)
        {
            h.Add(alt);

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


        chart1.ChartAreas[0].AxisX.Minimum = (int)(sr[0] - sr[0] * 0.02);
        chart1.ChartAreas[0].AxisX.Maximum = (int)(sr2[sr.Count - 1] + sr2[sr.Count - 1] * 0.05);
        chart1.ChartAreas[0].AxisY.Minimum = 29000;
        chart1.ChartAreas[0].AxisY.Maximum = 44000;
        chart1.ChartAreas[0].AxisY.LabelStyle.Interval = 1000;
        chart1.Series[0].Points.DataBindXY(sr, h);
        chart1.Series[1].Points.DataBindXY(sr1, h);
        chart1.Series[2].Points.DataBindXY(sr2, h);
        chart1.Series[4].Points.DataBindXY(sr_sr_max, h_sr_max);
        */









        /*22-----------------------print balanced field length fig and mark BFL and BFL_v1
            List<double> v1 = new List<double>();
            List<double> bflAccelerateGo = new List<double>();
            List<double> bflAccelerateStop = new List<double>();


            double V1_max = Units.kt2mps(A306.Get_V_1_Maximum());
            for (double V1 = 0.9 * V1_max; V1 <= V1_max + 0.5; V1 += 0.1)
            {
                v1.Add(V1);
                bflAccelerateGo.Add(A306.Get_Balanced_Field_Length(2, V1, Aircraft.Balanced_Field_LengthType.AccelerateGo));
                bflAccelerateStop.Add(A306.Get_Balanced_Field_Length(2, V1, Aircraft.Balanced_Field_LengthType.AccelerateStop));
            }


            double lowerBoundary = 0.9 * V1_max;
            double upperBounday = V1_max + 0.5;
            double step = 0.01;
            const double EPS = 1;
            double BFL = 0;
            double BFL_V1 = 0;
            double vToCalculate = lowerBoundary;
            double bflSpread = A306.Get_Balanced_Field_Length(2, vToCalculate, Aircraft.Balanced_Field_LengthType.AccelerateGo) - A306.Get_Balanced_Field_Length(2, vToCalculate, Aircraft.Balanced_Field_LengthType.AccelerateStop);
            while (vToCalculate <= upperBounday + step / 2)
                if (Math.Abs(bflSpread) < EPS)
                {
                    BFL_V1 = vToCalculate;
                    vToCalculate = vToCalculate + step / 2;
                    bflSpread = A306.Get_Balanced_Field_Length(2, vToCalculate, Aircraft.Balanced_Field_LengthType.AccelerateGo) - A306.Get_Balanced_Field_Length(2, vToCalculate, Aircraft.Balanced_Field_LengthType.AccelerateStop);
                }
                else
                {
                    double nextvToCalculate = vToCalculate + step;
                    double nextBflSpread = A306.Get_Balanced_Field_Length(2, nextvToCalculate, Aircraft.Balanced_Field_LengthType.AccelerateGo) - A306.Get_Balanced_Field_Length(2, nextvToCalculate, Aircraft.Balanced_Field_LengthType.AccelerateStop);
                    if (Math.Abs(nextBflSpread) < EPS)
                    {
                        BFL_V1 = nextvToCalculate;
                        break;
                    }
                    else if (bflSpread * nextBflSpread > 0)
                    {
                        vToCalculate = nextvToCalculate;
                        bflSpread = nextBflSpread;
                    }
                    else
                    {
                        bool find = false;
                        while (!find)
                            if (Math.Abs(nextvToCalculate - vToCalculate) < EPS)
                            {
                                BFL_V1 = (vToCalculate + nextvToCalculate) / 2;
                                find = true;
                            }
                            else
                            {
                                double vMiddle = (vToCalculate + nextvToCalculate) / 2;
                                double bflSpreadMiddle = A306.Get_Balanced_Field_Length(2, vMiddle, Aircraft.Balanced_Field_LengthType.AccelerateGo) - A306.Get_Balanced_Field_Length(2, vMiddle, Aircraft.Balanced_Field_LengthType.AccelerateStop);
                                if (Math.Abs(bflSpreadMiddle) < EPS)
                                {
                                    BFL_V1 = vMiddle;
                                    find = true;
                                }
                                else if (bflSpread * bflSpreadMiddle < 0)
                                {
                                    nextvToCalculate = vMiddle;
                                    nextBflSpread = bflSpreadMiddle;
                                }
                                else
                                {
                                    vToCalculate = vMiddle;
                                    bflSpread = bflSpreadMiddle;
                                }
                            }
                    }
                }
            BFL = A306.Get_Balanced_Field_Length(2, BFL_V1, Aircraft.Balanced_Field_LengthType.AccelerateGo);
            
            int v1InsertIndex = 0, bflAccelerateGoInsertIndex = 0, bflAccelerateStopInsertIndex = 0;
            for (int i = 0; i < v1.Count; i++)
            {
                if (BFL_V1 < v1[i])
                    v1InsertIndex = i;
                if(BFL > bflAccelerateGo[i])
                    bflAccelerateGoInsertIndex = i;
                if(BFL < bflAccelerateStop[i])
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


            chart1.ChartAreas[0].AxisX.Minimum = (int)(v1[0] - v1[0] * 0.01);
            chart1.ChartAreas[0].AxisX.Maximum = (int)(v1[v1.Count - 1] + v1[v1.Count - 1] * 0.05);
            chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 2;
            chart1.ChartAreas[0].AxisY.Minimum = ((int)(bflAccelerateGo[bflAccelerateGo.Count - 1] - bflAccelerateGo[bflAccelerateGo.Count - 1] * 0.05) / 100) * 100;
            chart1.ChartAreas[0].AxisY.LabelStyle.Interval = 200;
            chart1.Series[0].Points.DataBindXY(v1, bflAccelerateGo);
            chart1.Series[1].Points.DataBindXY(v1, bflAccelerateStop);
            chart1.Series[4].Points.DataBindXY(bflHorizontalLine_v1, bflHorizontalLine_length);
            chart1.Series[5].Points.DataBindXY(bflVerticalLine_v1, bflVerticalLine_length);
        */








        private void button1_Click(object sender, EventArgs e)
        {
            // *************************************************************//
            string path = System.Environment.CurrentDirectory;
            string fileName = $@"{path}\A306.txt";
            Aircraft A306 = new Aircraft(fileName);
            // ************************************************************//

















        }
    }
}
