using System;
using AircraftModel;
using AtmosphereModel;

namespace project1
{
    class Program
    {


        /*print PTF Table-------------------------------------------------------------*/

        /*
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


                Console.WriteLine($"{h}\t{TAS}\t{ff_low}\t{ff_nominal}\t{ff_high}");

                if (h == 3000)
                    h = 2000;
                else if (h == 28000)
                    h = 27000;
            }
        */





        /*
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
                if(h > h_cross)
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
                if(h > h_cross)
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


                Console.WriteLine($"{h}\t{TAS}\t{ROCD_low}\t{ROCD_nominal}\t{ROCD_high}\t{ff}");


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
        */




        /*
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
                else if(h > 2000)
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


                Console.WriteLine($"{h}\t{TAS}\t{ROCD}\t{ff}");


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
        */




        /*LBO---------------------------------------------------------------------------
                    for (double h = 16000; h <= 39000; h += 1000)
        {
            Console.WriteLine($"{A306.Get_Low_Buffet_M(h, factor: 0.98)}");
        }


        for (double h = 16000; h <= 30000; h += 1000)
        {
            double TAS_buffet = A306.Get_Low_Buffet_M(h, factor: 0.98) * AtmosphereEnviroment.Get_a(h);
            double TAS_stall = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(A306.Get_v_stall(h, Aircraft.FlightPhase.Climb)));
            double TAS_vmin = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(A306.Get_v_min(h, Aircraft.FlightPhase.Climb)));
            TAS_buffet = Math.Round(TAS_buffet, 3);
            TAS_stall = Math.Round(TAS_stall, 3);
            TAS_vmin = Math.Round(TAS_vmin, 3);
            Console.WriteLine($"{TAS_stall}\t\t{TAS_buffet}\t\t{TAS_vmin}");
        }

        */


        static void Main()
        {
            string path = System.Environment.CurrentDirectory;
            string fileName = $@"{path}\A306.txt";
            Aircraft A306 = new Aircraft(fileName);
            Console.WriteLine("File loading succeeded.");






            
        }
    }
}
