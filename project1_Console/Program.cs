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

        for(double h = 3000; h <= 41000; h += 2000)
        {
            double CAS = A306.Get_v_cruise(h);
            double TAS = Units.mps2kt(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(CAS)));
            TAS = Math.Round(TAS);


            double ff_low;
            ff_low = A306.Get_ff(Aircraft.FlightPhase.Cruise, m_low, h: h, CAS: Units.kt2mps(CAS));
            ff_low = Math.Round(ff_low, 1);


            double ff_nominal;
            if (h == 41000)
                ff_nominal = A306.Get_ff(Aircraft.FlightPhase.Cruise, h: h, CAS: Units.kt2mps(CAS), thrustMode: Aircraft.ThrustMode.MaximumCruiseThrust);
            else
                ff_nominal = A306.Get_ff(Aircraft.FlightPhase.Cruise, h: h, CAS: Units.kt2mps(CAS));
            ff_nominal = Math.Round(ff_nominal, 1);


            double ff_high;
            if(h >= 35000)
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
        double m_nominal = 140000;
        double m_high = 171700;


        for (double h = 0; h <= 41000;)
        {
            double CAS = A306.Get_v_climb(h);
            double TAS = Units.mps2kt(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(CAS)));
            TAS = Math.Round(TAS);


            double ROCD_low;
            ROCD_low = A306.Get_ROCD(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Climb, m: m_low);
            ROCD_low = Math.Round(ROCD_low);
            if (ROCD_low < 0) ROCD_low = 0;


            double ROCD_nominal;
            ROCD_nominal = A306.Get_ROCD(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Climb, m: m_nominal);
            ROCD_nominal = Math.Round(ROCD_nominal);
            if (ROCD_nominal < 0) ROCD_nominal = 0;


            double ROCD_high;
            ROCD_high = A306.Get_ROCD(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Climb, m: m_high);
            ROCD_high = Math.Round(ROCD_high);
            if (ROCD_high < 0) ROCD_high = 0;


            double ff;
            ff = A306.Get_ff(Aircraft.FlightPhase.Climb, h: h, CAS: Units.kt2mps(CAS));
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
            for (double h = 0; h <= 41000;)
            {
                double CAS = A306.Get_v_descent(h);
                double TAS = Units.mps2kt(AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(CAS)));
                TAS = Math.Round(TAS);


                double ROCD;
                if(h > 2000)
                    ROCD = (-1) * A306.Get_ROCD(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Descent);
                else if(h == 2000)
                    ROCD = (-1) * A306.Get_ROCD(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Approach);
                else
                    ROCD = (-1) * A306.Get_ROCD(h, Units.kt2mps(CAS), Aircraft.FlightPhase.Landing);
                ROCD = Math.Round(ROCD);


                double ff;
                if(h > 2000)
                    ff = A306.Get_ff(Aircraft.FlightPhase.Descent, h: h, CAS: Units.kt2mps(CAS), thrustMode: Aircraft.ThrustMode.IdleThrust);
                else if(h == 2000)
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




        static void Main()
        {
            string path = System.Environment.CurrentDirectory;
            string fileName = $@"{path}\A306.txt";
            Aircraft A306 = new Aircraft(fileName);
            Console.WriteLine("File loading succeeded.");


            //for (double h = 16000; h <= 38000; h += 1000)
            //{
            //    Console.WriteLine($"{A306.SolveBuffetingM(h)}");
            //}


            //for (double h = 16000; h <= 30000; h += 1000)
            //{
            //    double TAS_buffet = A306.SolveBuffetingM(h) * AtmosphereEnviroment.Get_a(h);
            //    double TAS_stall = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(A306.Get_v_stall(h, Aircraft.FlightPhase.Climb)));
            //    double TAS_vmin = AtmosphereEnviroment.Get_TAS(h, Units.kt2mps(A306.Get_v_min(h, Aircraft.FlightPhase.Climb)));
            //    TAS_buffet = Math.Round(TAS_buffet, 3);
            //    TAS_stall = Math.Round(TAS_stall, 3);
            //    TAS_vmin = Math.Round(TAS_vmin, 3);
            //    Console.WriteLine($"{TAS_stall}\t\t{TAS_buffet}\t\t{TAS_vmin}");
            //}





            
            return;
        }
    }
}
