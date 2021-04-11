using System;

namespace AtmosphereModel
{
    class AtmosphereEnviroment
    {
        public static double P_0 { get; } = 101325;
        public static double T_0 { get; } = 288.15;
        public static double rho_0 { get; } = 1.225;
        public static double a_0 { get; } = 340.29;
        public static double g { get; } = 9.8;
        public static double gamma { get; } = 1.4;
        public static double k_T { get; } = -0.0065;
        public static double k_T_ft { get; } = -0.0019812;
        public static double R { get; } = 287.05287;
        public static double mu { get; } = 0.285714; // [Manual p.12] mu = 1 / 3.5
        public static double T_trop { get; } = 216.65;
        public static double a_trop { get; } = 295.07;
        public static double h_trop { get; private set; } = 36089;


        /*--------------------------------------------------------------------
        description of common parameters in methods below

        parameter               description                     unit
        h                       altitude                        ft
        
        CAS                     calibrated airspeed             m/s
        
        DeltaISA                deviation from ISA              kelvin(celsius is fine)
        
        ----------------------------------------------------------------------*/



        /// <summary> [Manual p.10] Get tropopause height in ft.
        /// </summary>
        public static double Get_h_trop(double DeltaISA = 0)
        {
            return h_trop = 36089 - DeltaISA / k_T_ft;
        }

        /// <summary> [Manual p.11] Get temperature on given altitude in kelvin.
        /// </summary>
        public static double Get_T(double h, double DeltaISA = 0)
        {
            Get_h_trop(DeltaISA);
            if (h < h_trop) return T_0 + h * k_T_ft;
            else return T_trop;
        }

        /// <summary> [Manual p.13] Get pressure on given altitude in pascal.
        /// </summary>
        public static double Get_P(double h, double DeltaISA = 0)
        {
            Get_h_trop(DeltaISA);
            if (h < h_trop) return P_0 * Math.Pow(Get_T(h, DeltaISA) / T_0, 5.25583);
            else
            {
                double P_trop = P_0 * Math.Pow(T_trop / T_0, 5.25583);
                return P_trop * Math.Exp((-1) * g / (R * T_trop) * Units.ft2m(h - h_trop));
            }
        }

        /// <summary> [Manual p.11] Get air density on given altitude in kg/m3.
        /// </summary>
        public static double Get_rho(double h, double DeltaISA = 0)
        {
            Get_h_trop(DeltaISA);
            if (h < h_trop) return rho_0 * Math.Pow(Get_T(h, DeltaISA) / T_0, 4.25583);
            else
            {
                double rho_trop = rho_0 * Math.Pow(T_trop / T_0, 4.25583);
                return rho_trop * Math.Exp((-1) * g / (R * T_trop) * Units.ft2m(h - h_trop));
            }
        }

        /// <summary> [Manual p.12] Get speed of sound on given altitude in m/s. 
        /// </summary>
        public static double Get_a(double h, double DeltaISA = 0)
        {
            Get_h_trop(DeltaISA);
            if (h < h_trop) return a_0 * Math.Sqrt(Get_T(h, DeltaISA) / T_0);
            else return a_trop;
        }

        /// <summary> [Manual p.12] Convert from CAS to TAS, return TAS in m/s.
        /// </summary>
        public static double Get_TAS(double h, double CAS, double DeltaISA = 0)
        {
            Get_h_trop(DeltaISA);
            double P = Get_P(h, DeltaISA);
            double rho = Get_rho(h, DeltaISA);
            return Math.Sqrt(2 / mu * P / rho * (Math.Pow(1 + P_0 / P
                * (Math.Pow(1 + mu / 2 * rho_0 / P_0 * CAS * CAS, 1 / mu) - 1), mu) - 1));
        }

        /// <summary> [Manual p.12] Convert from TAS to CAS, return CAS in m/s.
        /// </summary>
        public static double Get_CAS(double h, double TAS, double DeltaISA = 0)
        {
            Get_h_trop(DeltaISA);
            double P = Get_P(h, DeltaISA);
            double rho = Get_rho(h, DeltaISA);
            return Math.Sqrt(2 / mu * P_0 / rho_0 * (Math.Pow(1 + P / P_0
                * (Math.Pow(1 + mu / 2 * rho / P * TAS * TAS, 1 / mu) - 1), mu) - 1));
        }

        /// <summary> [Manual p.13] Get Mach number.
        /// </summary>
        public static double Get_M(double h, double CAS, double DeltaISA = 0)
        {
            return Math.Round(Get_TAS(h, CAS, DeltaISA) / Get_a(h, DeltaISA), 3);
        }

        /// <summary> [Manual p.13-14] Get cross-over altitude in ft for given CAS/Mach 
        /// transition.
        /// </summary>
        public static double Get_h_cross(double CAS, double M)
        {
            double delta_cross = (Math.Pow(1 + (gamma - 1) / 2 * (CAS / a_0) * (CAS / a_0),
                gamma / (gamma - 1)) - 1) / (Math.Pow(1 + (gamma - 1) / 2 * M * M,
                gamma / (gamma - 1)) - 1);
            double theta_cross = Math.Pow(delta_cross, (-1) * k_T * R / g);
            return T_0 * (theta_cross - 1) / (0.3048 * k_T);
        }
    }

    class Units
    {
        public static double m2ft(double meter) { return meter * 3.28084; }
        public static double ft2m(double ft) { return ft * 0.3048; }
        public static double mps2kt(double mps) { return mps * 1.94384; }
        public static double kt2mps(double kt) { return kt * 0.514444; }
        public static double kmphr2kt(double kmphr) { return kmphr * 0.539957; }
        public static double kt2kmphr(double kt) { return kt * 1.852; }
        public static double kmphr2mps(double kmphr) { return kmphr * 0.277778; }
        public static double mps2kmphr(double mps) { return mps * 3.6; }
        public static double mps2fpm(double mps) { return mps * 196.85; }
        public static double fpm2mps(double fpm) { return fpm * 0.00508; }
        public static double mpm2fpm(double mpm) { return mpm * 3.28084; }
        public static double fpm2mpm(double fpm) { return fpm * 0.3048; }
        public static double mps2mpmin(double mps) { return mps * 60; }
        public static double mpmin2mps(double mpmin) { return mpmin * 0.0166667; }
        public static double kg2t(double kg) { return kg * 0.001; }
        public static double t2kg(double t) { return t * 1000; }
        public static double lbs2kg(double lbs) { return lbs * 0.453592; }
        public static double lbs2t(double lbs) { return lbs * 0.000453592; }
        public static double ftsquare2msquare(double ft2) { return ft2 * 0.092903; }
        public static double s2min(double s) { return s * 0.0166667; }
        public static double min2s(double min) { return min * 60; }
        public static double deg2rad(double deg) { return deg / 180 * Math.PI; }
        public static double rad2deg(double rad) { return rad / Math.PI * 180; }
        public static double degps2radps(double degps) { return degps / 180 * Math.PI; }
        public static double radps2degps(double radps) { return radps / Math.PI * 180; }
    }
}
