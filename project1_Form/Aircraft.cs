using System;
using AtmosphereModel;
using DataImport;

namespace AircraftModel
{
    class Aircraft
    {
        public int e_index { get; private set; }

        /// <summary> Wake of turbulence catagory: 0 for H, 1 for M and 2 for L.
        /// </summary>
        public int wakeCategory { get; private set; } = 0;

        public double m_min_t { get; private set; }
        public double m_min { get; private set; }
        public double m_max_t { get; private set; }
        public double m_max { get; private set; }
        public double m_ref_t { get; private set; }
        public double m_ref { get; private set; }
        public double m_pyld_t { get; private set; }
        public double m_pyld { get; private set; }

        public double v_MO { get; private set; }
        public double M_MO { get; private set; }
        public double h_MO { get; private set; }
        public double h_max { get; private set; }
        public double G_w { get; private set; }
        public double G_t { get; private set; }

        public double v_des_1 { get; private set; }
        public double v_des_2 { get; private set; }
        public double M_des { get; private set; }
        public double v_d_des_1 { get; private set; } = 5;
        public double v_d_des_2 { get; private set; } = 10;
        public double v_d_des_3 { get; private set; } = 20;
        public double v_d_des_4 { get; private set; } = 50;
        public double v_d_des_5 { get; private set; } = 5;
        public double v_d_des_6 { get; private set; } = 10;
        public double v_d_des_7 { get; private set; } = 20;

        public double k { get; private set; }
        public double C_lbo { get; private set; }

        public double v_stall_TO { get; private set; }
        public double v_stall_IC { get; private set; }
        public double v_stall_CR { get; private set; }
        public double v_stall_AP { get; private set; }
        public double v_stall_LD { get; private set; }
        public double H_max_TO { get; private set; } = 400;
        public double H_max_IC { get; private set; } = 2000;
        public double H_max_AP { get; private set; } = 8000;
        public double H_max_LD { get; private set; } = 3000;

        public double C_v_min { get; private set; } = 1.3;
        public double C_v_min_TO { get; private set; } = 1.2;

        public double S { get; private set; }

        public double C_D0_CR { get; private set; }
        public double C_D2_CR { get; private set; }
        public double C_D0_AP { get; private set; }
        public double C_D2_AP { get; private set; }
        public double C_D0_LDG { get; private set; }
        public double C_D0_DeltaLDG { get; private set; }
        public double C_D2_LDG { get; private set; }

        public double C_des_exp { get; private set; } = 1.6;

        public double C_TC_1 { get; private set; }
        public double C_TC_2 { get; private set; }
        public double C_TC_3 { get; private set; }
        public double C_TC_4 { get; private set; }
        public double C_TC_5 { get; private set; }

        public double C_T_cr { get; private set; } = 0.95;

        public double C_Tdes_high { get; private set; }
        public double C_Tdes_low { get; private set; }
        public double C_Tdes_app { get; private set; }
        public double C_Tdes_ld { get; private set; }
        public double h_des { get; private set; }

        public double C_reduce_piston { get; private set; } = 0;
        public double C_reduce_turbo { get; private set; } = 0.25;
        public double C_reduce_jet { get; private set; } = 0.15;

        public double C_f1 { get; private set; }
        public double C_f2 { get; private set; }
        public double C_f3 { get; private set; }
        public double C_f4 { get; private set; }
        public double C_fcr { get; private set; } = 1;

        /// <summary> FAR Take-Off Length(in meter) with MTOW on a dry, hard, 
        /// level runway under ISA conditions and no wind.
        /// </summary>
        public double TOL { get; private set; }
        /// <summary> FAR Landing Length(in meter) with MTOW on a dry, hard, 
        /// level runway under ISA conditions and no wind.
        /// </summary>
        public double LDL { get; private set; }
        public double span { get; private set; }
        public double length { get; private set; }
        /// <summary> Runway backtrack speed(CAS) in kt.
        /// </summary>
        public double v_backtrack { get; private set; } = 35;
        /// <summary> Taxi speed(CAS) in kt.
        /// </summary>
        public double v_taxi { get; private set; } = 15;
        /// <summary> Apron speed(CAS) in kt.
        /// </summary>
        public double v_apron { get; private set; } = 10;
        /// <summary> Gate speed(CAS) in kt.
        /// </summary>
        public double v_gate { get; private set; } = 5;

        public double v_cl_1 { get; private set; }
        public double v_cl_2 { get; private set; }
        public double M_cl { get; private set; }
        public double v_d_cl_1 { get; private set; } = 5;
        public double v_d_cl_2 { get; private set; } = 10;
        public double v_d_cl_3 { get; private set; } = 30;
        public double v_d_cl_4 { get; private set; } = 60;
        public double v_d_cl_5 { get; private set; } = 80;
        public double v_d_cl_6 { get; private set; } = 20;
        public double v_d_cl_7 { get; private set; } = 30;
        public double v_d_cl_8 { get; private set; } = 35;

        public double v_cr_1 { get; private set; }
        public double v_cr_2 { get; private set; }
        public double M_cr { get; private set; }

        public double acc_max_long { get; private set; } = 2.0;

        public double acc_max_norm { get; private set; } = 5.0;

        public double ang_norm_tl { get; private set; } = 15;
        public double ang_norm { get; private set; } = 35;
        public double ang_norm_mil { get; private set; } = 50;
        public double ang_max_tl { get; private set; } = 25;
        public double ang_max_hold { get; private set; } = 35;
        public double ang_max { get; private set; } = 45;
        public double ang_max_mil { get; private set; } = 70;

        public double v_hold_1 { get; private set; } = 230;
        public double v_hold_2 { get; private set; } = 240;
        public double v_hold_3 { get; private set; } = 265;
        public double v_hold_4 { get; private set; } = 0.83;

        public Aircraft(string fileName)
        {
            ReadData(fileName);
        }

        public void ReadData(string fileName)
        {
            DataReader reader = new DataReader(fileName);

            e_index = (int)reader.Import("indexengine");

            if (reader.Contain("wakecategory"))
                wakeCategory = (int)reader.Import("wakecategory");

            m_min_t = reader.Import("minweight");
            m_min = Units.t2kg(m_min_t);
            m_max_t = reader.Import("maxweight");
            m_max = Units.t2kg(m_max_t);
            m_ref_t = reader.Import("refweight");
            m_ref = Units.t2kg(m_ref_t);
            m_pyld_t = reader.Import("maxpayload");
            m_pyld = Units.t2kg(m_pyld_t);

            v_MO = reader.Import("VMO");
            M_MO = reader.Import("MMO");
            h_MO = reader.Import("MaxAlt");
            h_max = reader.Import("Hmax");
            G_w = reader.Import("massgrad");
            G_t = reader.Import("tempgrad");

            v_des_1 = reader.Import("vdes1");
            v_des_2 = reader.Import("vdes2");
            M_des = reader.Import("mdes");
            if (reader.Contain("V_des_1")) v_d_des_1 = reader.Import("V_des_1");
            if (reader.Contain("V_des_2")) v_d_des_2 = reader.Import("V_des_2");
            if (reader.Contain("V_des_3")) v_d_des_3 = reader.Import("V_des_3");
            if (reader.Contain("V_des_4")) v_d_des_4 = reader.Import("V_des_4");
            if (reader.Contain("V_des_5")) v_d_des_5 = reader.Import("V_des_5");
            if (reader.Contain("V_des_6")) v_d_des_6 = reader.Import("V_des_6");
            if (reader.Contain("V_des_7")) v_d_des_7 = reader.Import("V_des_7");

            k = reader.Import("k");
            C_lbo = reader.Import("Clbo");

            v_stall_TO = reader.Import("Vstallto");
            v_stall_IC = reader.Import("Vstallic");
            v_stall_CR = reader.Import("Vstallcr");
            v_stall_AP = reader.Import("Vstallap");
            v_stall_LD = reader.Import("Vstallld");
            if (reader.Contain("H_max_to")) H_max_TO = reader.Import("H_max_to");
            if (reader.Contain("H_max_ic")) H_max_IC = reader.Import("H_max_ic");
            if (reader.Contain("H_max_app")) H_max_AP = reader.Import("H_max_app");
            if (reader.Contain("H_max_ld")) H_max_LD = reader.Import("H_max_ld");

            if (reader.Contain("C_v_min")) C_v_min = reader.Import("C_v_min");
            if (reader.Contain("C_v_minto")) C_v_min_TO = reader.Import("C_v_minto");

            S = reader.Import("Surf");

            C_D0_CR = reader.Import("CD0cr");
            C_D2_CR = reader.Import("CD2cr");
            C_D0_AP = reader.Import("CD0ap");
            C_D2_AP = reader.Import("CD2ap");
            C_D0_LDG = reader.Import("CD0ld");
            C_D0_DeltaLDG = reader.Import("cdGear0");
            C_D2_LDG = reader.Import("CD2ld");

            if (reader.Contain("C_des_exp")) C_des_exp = reader.Import("C_des_exp");

            C_TC_1 = reader.Import("ctc1");
            C_TC_2 = reader.Import("ctc2");
            C_TC_3 = reader.Import("ctc3");
            C_TC_4 = reader.Import("ctc4");
            C_TC_5 = reader.Import("ctc5");

            if (reader.Contain("C_th_cr"))
                C_T_cr = reader.Import("C_th_cr");

            C_Tdes_high = reader.Import("Deschigh");
            C_Tdes_low = reader.Import("Desclow");
            C_Tdes_app = reader.Import("Descapp");
            C_Tdes_ld = reader.Import("Descld");
            h_des = reader.Import("Desclevel");

            if (reader.Contain("C_red_piston"))
                C_reduce_piston = reader.Import("C_red_piston");
            if (reader.Contain("C_red_turbo"))
                C_reduce_turbo = reader.Import("C_red_turbo");
            if (reader.Contain("C_red_jet"))
                C_reduce_jet = reader.Import("C_red_jet");

            C_f1 = reader.Import("cf1");
            C_f2 = reader.Import("cf2");
            C_f3 = reader.Import("cf3");
            C_f4 = reader.Import("cf4");
            if (reader.Contain("cfcr")) C_fcr = reader.Import("cfcr");

            TOL = reader.Import("tol");
            LDL = reader.Import("ldl");
            span = reader.Import("span");
            length = reader.Import("length");
            if (reader.Contain("V_backtrack"))
                v_backtrack = reader.Import("V_backtrack");
            if (reader.Contain("V_taxi"))
                v_taxi = reader.Import("V_taxi");
            if (reader.Contain("V_apron"))
                v_apron = reader.Import("V_apron");
            if (reader.Contain("V_gate"))
                v_gate = reader.Import("V_gate");

            v_cl_1 = reader.Import("vcl1");
            v_cl_2 = reader.Import("vcl2");
            M_cl = reader.Import("mcl");
            if (reader.Contain("V_cl_1")) v_d_cl_1 = reader.Import("V_cl_1");
            if (reader.Contain("V_cl_2")) v_d_cl_2 = reader.Import("V_cl_2");
            if (reader.Contain("V_cl_3")) v_d_cl_3 = reader.Import("V_cl_3");
            if (reader.Contain("V_cl_4")) v_d_cl_4 = reader.Import("V_cl_4");
            if (reader.Contain("V_cl_5")) v_d_cl_5 = reader.Import("V_cl_5");
            if (reader.Contain("V_cl_6")) v_d_cl_6 = reader.Import("V_cl_6");
            if (reader.Contain("V_cl_7")) v_d_cl_7 = reader.Import("V_cl_7");
            if (reader.Contain("V_cl_8")) v_d_cl_8 = reader.Import("V_cl_8");

            v_cr_1 = reader.Import("vcr1");
            v_cr_2 = reader.Import("vcr2");
            M_cr = reader.Import("mcr");

            if (reader.Contain("acc_long_max"))
                acc_max_long = reader.Import("acc_long_max");

            if (reader.Contain("acc_norm_max"))
                acc_max_norm = reader.Import("acc_norm_max");

            if (reader.Contain("ang_bank_nomtl"))
                ang_norm_tl = reader.Import("ang_bank_nomtl");
            if (reader.Contain("ang_bank_nom"))
                ang_norm = reader.Import("ang_bank_nom");
            if (reader.Contain("ang_bank_nommil"))
                ang_norm_mil = reader.Import("ang_bank_nommil");
            if (reader.Contain("ang_bank_maxtl"))
                ang_max_tl = reader.Import("ang_bank_maxtl");
            if (reader.Contain("ang_bank_maxhold"))
                ang_max_hold = reader.Import("ang_bank_maxhold");
            if (reader.Contain("ang_bank_max"))
                ang_max = reader.Import("ang_bank_max");
            if (reader.Contain("ang_bank_maxmil"))
                ang_max_mil = reader.Import("ang_bank_maxmil");

            if (reader.Contain("V_hold_1")) v_hold_1 = reader.Import("V_hold_1");
            if (reader.Contain("V_hold_2")) v_hold_2 = reader.Import("V_hold_2");
            if (reader.Contain("V_hold_3")) v_hold_3 = reader.Import("V_hold_3");
            if (reader.Contain("V_hold_4")) v_hold_1 = reader.Import("V_hold_4");
        }

        public enum FlightPhase
        {
            Ground,
            Taxi,
            Takeoff,
            Climb,
            Cruise,
            Descent,
            Approach,
            Landing,
            Holding
        }

        public enum AttitudeAndMovement
        {
            ConstantSpeed,
            AccelerationInClimb,
            DecelerationInClimb,
            DecelerationInDescent,
            AccelerationInDescent
        }

        /*--------------------------------------------------------------------
        description of some common parameters in methods below

        parameter               description                     unit
        h                       altitude                        ft

        CAS                     calibrated airspeed             m/s

        m                       actual weight                   kg

        DeltaISA                deviation from ISA              kelvin(celsius is fine)

        phi                     bank angle                      degr

        e_index                 index of engine:                [dimensionless]
                                1:jet
                                2:turboprop
                                3:piston
           
        ------------------------------------------------------------------------

           Note: All overload methods serve for the purpose of setting m to 
           defalut value.

        ----------------------------------------------------------------------*/

        /// <summary> [Manual p.15] Get maximun altitude in ft. 
        /// Aircraft weight is set to reference weight.
        /// </summary>
        public double Get_h_max_act(double DeltaISA = 0)
        {
            return Get_h_max_act(m_ref, DeltaISA);
        }
        /// <summary> [Manual p.15] Get maximun altitude in ft under acutual weight.
        /// </summary>
        public double Get_h_max_act(double m, double DeltaISA = 0)
        {
            if (h_max == 0) return h_MO;
            else
                return Math.Min(h_MO, h_max + G_t * Math.Max(0, DeltaISA - C_TC_4)
               + G_w * (m_max - m));
        }



        /// <summary> [Manual p.15] Get corrected airspeed under acutual weight, 
        /// return unit determined by parameter unit.
        /// </summary>
        public double CorrectV(double v_ref, double m)
        {
            return v_ref * Math.Sqrt(m / m_ref);
        }



        /// <summary> [Manual p.29] Get descent speed(CAS) in kt. 
        /// Aircraft weight is set to reference weight.
        /// </summary>
        public double Get_v_descent(double h, double DeltaISA = 0)
        {
            return Get_v_descent(h, m_ref, DeltaISA);
        }
        /// <summary> [Manual p.29] Get descent speed(CAS) in kt.
        /// </summary>
        public double Get_v_descent(double h, double m, double DeltaISA = 0)
        {
            double v_descent = 0;
            double h_cross = AtmosphereEnviroment.Get_h_cross(Units.kt2mps(v_des_1), M_des);
            double v_stall_LD1 = CorrectV(v_stall_LD, m);
            switch (e_index)
            {
                case 1:
                case 2:
                    if (h >= 0 && h <= 999)
                        v_descent = C_v_min * v_stall_LD1 + v_d_des_1;
                    else if (h >= 1000 && h <= 1499)
                        v_descent = C_v_min * v_stall_LD1 + v_d_des_2;
                    else if (h >= 1500 && h <= 1999)
                        v_descent = C_v_min * v_stall_LD1 + v_d_des_3;
                    else if (h >= 2000 && h <= 2999)
                        v_descent = C_v_min * v_stall_LD1 + v_d_des_4;
                    else if (h >= 3000 && h <= 6000)
                        v_descent = Math.Min(v_des_2, 220);
                    else if (h > 6000 && h <= 10000)
                        v_descent = Math.Min(v_des_2, 250);
                    else if (h > 10000 && h <= h_cross)
                        v_descent = v_des_1;
                    else
                        v_descent = Units.mps2kt(AtmosphereEnviroment.Get_CAS(h,
                            M_des * AtmosphereEnviroment.Get_a(h, DeltaISA)));
                    break;

                default:
                    if (h >= 0 && h <= 499)
                        v_descent = C_v_min * v_stall_LD1 + v_d_des_5;
                    else if (h >= 500 && h <= 999)
                        v_descent = C_v_min * v_stall_LD1 + v_d_des_6;
                    else if (h >= 1000 && h <= 1499)
                        v_descent = C_v_min * v_stall_LD1 + v_d_des_7;
                    else if (h >= 1500 && h <= 10000)
                        v_descent = v_des_2;
                    else if (h > 10000 && h <= h_cross)
                        v_descent = v_des_1;
                    else
                        v_descent = Units.mps2kt(AtmosphereEnviroment.Get_CAS(h,
                            M_des * AtmosphereEnviroment.Get_a(h, DeltaISA)));
                    break;
            }
            return Math.Round(v_descent, 0);
        }



        /// <summary> [Manual p.19 and 83] Get low speed buffeting limit Mach. 
        /// Aircraft weight is set to reference weight.
        /// </summary>
        public double SolveBuffetingM(double h, double DeltaISA = 0)
        {
            return SolveBuffetingM(h, m_ref, DeltaISA);
        }
        /// <summary> [Manual p.19 and 83] Get low speed buffeting limit Mach.
        /// </summary>
        public double SolveBuffetingM(double h, double m, double DeltaISA = 0)
        {
            double factor = 1.3; // ????


            double M = 0;
            double W = m * AtmosphereEnviroment.g;
            double P = AtmosphereEnviroment.Get_P(h, DeltaISA);
            double a1 = (-1) * C_lbo / k;
            double a2 = 0; 
            double a3 = W / (factor * S) / (0.583 * k * P);
            double Q = (3 * a2 - a1 * a1) / 9;
            double R = (9 * a1 * a2 - 27 * a3 - 2 * a1 * a1 * a1) / 54;
            double Delta = Q * Q * Q + R * R;
            if (Delta < 0)
            {
                // formulas in Manual p.83 are inconsistent with his 
                // reference book(Mathematical Handbook p.32)
                double theta = Math.Acos(R / Math.Sqrt((-1) * Q * Q * Q));
                double[] x = new double[3];
                double[] x_positive = new double[3];
                x.SetValue(2 * Math.Sqrt((-1) * Q)
                    * Math.Cos(theta / 3.0) - a1 / 3.0, 0);
                x.SetValue(2 * Math.Sqrt((-1) * Q)
                    * Math.Cos(theta / 3.0 + 2 / 3.0 * Math.PI - a1 / 3.0), 1);
                x.SetValue(2 * Math.Sqrt((-1) * Q)
                    * Math.Cos(theta / 3.0 + 4 / 3.0 * Math.PI - a1 / 3.0), 2);
                // look for the maximum positive value in three solutions
                int j = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] > 0)
                    {
                        x_positive[j] = x[i];
                        j++;
                    }
                }
                M = x_positive[0];
                for (int i = 1; i < j; i++)
                {
                    M = M < x_positive[i] ? M : x_positive[i];
                }
            }
            return Math.Round(M, 3);
        }



        /// <summary> [Manual p.16-17] Get stall airspeed(CAS) in kt during different 
        /// flight phases. Aircraft weight is set to reference weight.
        /// </summary>
        public double Get_v_stall(double h, FlightPhase flightPhase, double DeltaISA = 0)
        {
            return Get_v_stall(h, flightPhase, m_ref, DeltaISA);
        }
        /// <summary> [Manual p.16-17] Get stall airspeed(CAS) in kt during different 
        /// flight phases.
        /// </summary>
        public double Get_v_stall(double h, FlightPhase flightPhase, double m,
            double DeltaISA = 0)
        {
            double v_stall = 0;
            double v_descent = Get_v_descent(h, m, DeltaISA);
            if (h >= 0 && h <= H_max_TO && (flightPhase == FlightPhase.Takeoff ||
                flightPhase == FlightPhase.Climb))
                v_stall = v_stall_TO;
            else if (h > H_max_TO && h <= H_max_IC && flightPhase == FlightPhase.Climb)
                v_stall = v_stall_IC;
            else if ((h > H_max_IC && flightPhase == FlightPhase.Climb)
                || (h > H_max_AP && (flightPhase == FlightPhase.Cruise ||
                                                        flightPhase == FlightPhase.Descent))
                || (h < H_max_AP && flightPhase == FlightPhase.Descent
                                 && v_descent >= C_v_min * CorrectV(v_stall_CR, m) + 10))
                v_stall = v_stall_CR;
            else if (h >= H_max_LD && h <= H_max_AP && flightPhase == FlightPhase.Descent
                && v_descent < C_v_min * CorrectV(v_stall_CR, m) + 10)
                v_stall = v_stall_AP;
            else if (h < H_max_LD && flightPhase == FlightPhase.Descent)
            {
                if (v_descent >= C_v_min * CorrectV(v_stall_AP, m) + 10)
                    v_stall = v_stall_AP;
                else v_stall = v_stall_LD;
            }
            return v_stall;
        }



        /// <summary> [Manual p.16] Get minimum airspeed(CAS) in kt during different 
        /// flight phases. Aircraft weight is set to reference weight.
        /// </summary>
        public double Get_v_min(double h, FlightPhase flightPhase, double DeltaISA = 0)
        {
            return Get_v_min(h, flightPhase, m_ref, DeltaISA);
        }
        /// <summary> [Manual p.16] Get minimum airspeed(CAS) in kt during different 
        /// flight phases.
        /// </summary>
        public double Get_v_min(double h, FlightPhase flightPhase, double m,
            double DeltaISA = 0)
        {
            double v_min = 0;
            if (h >= 0 && h <= H_max_TO && flightPhase == FlightPhase.Takeoff)
                v_min = C_v_min_TO * CorrectV(Get_v_stall(h, flightPhase, m, DeltaISA), m);
            else
            {
                if ((e_index == 1 || C_lbo != 0) && h > 15000)
                    v_min = Math.Max(
                        C_v_min * CorrectV(Get_v_stall(h, flightPhase, m, DeltaISA), m),
                        CorrectV(Units.mps2kt(AtmosphereEnviroment.Get_CAS(h,
                        SolveBuffetingM(h, m, DeltaISA)
                        * AtmosphereEnviroment.Get_a(h, DeltaISA), DeltaISA)), m));
                else
                    v_min = C_v_min * CorrectV(Get_v_stall(h, flightPhase, m, DeltaISA), m);
            }
            return v_min;
            //return Math.Round(v_min, 0);
        }



        /// <summary> [Manual p.18] Get lift coefficient. 
        /// Aircraft weight is set to reference weight.
        /// </summary>
        public double Get_C_L(double h, double CAS, double DeltaISA = 0, double phi = 0)
        {
            return Get_C_L(h, CAS, m_ref, DeltaISA, phi);
        }
        /// <summary> [Manual p.18] Get lift coefficient.
        /// </summary>
        public double Get_C_L(double h, double CAS, double m, double DeltaISA = 0,
            double phi = 0)
        {
            double rho = AtmosphereEnviroment.Get_rho(h, DeltaISA);
            double TAS = AtmosphereEnviroment.Get_TAS(h, CAS, DeltaISA);
            return 2 * m * AtmosphereEnviroment.g /
                (rho * TAS * TAS * S * Math.Cos(Units.deg2rad(phi)));
        }



        /// <summary> [Manual p.18] Get drag coefficient. 
        /// Aircraft weight is set to reference weight.
        /// </summary>
        public double Get_C_D(double h, double CAS, FlightPhase flightPhase,
            double DeltaISA = 0, double phi = 0)
        {
            return Get_C_D(h, CAS, flightPhase, m_ref, DeltaISA, phi);
        }
        /// <summary> [Manual p.18] Get drag coefficient.
        /// </summary>
        public double Get_C_D(double h, double CAS, FlightPhase flightPhase, double m,
            double DeltaISA = 0, double phi = 0)
        {
            double C_D = 0;
            if (C_D0_AP == 0 && C_D2_AP == 0 && C_D0_LDG == 0 && C_D0_DeltaLDG == 0
                && C_D2_LDG == 0)
                C_D = C_D0_CR + C_D2_CR * Math.Pow(Get_C_L(h, CAS, m, DeltaISA, phi), 2);
            else
            {
                switch (flightPhase)
                {
                    case FlightPhase.Approach:
                        C_D = C_D0_AP + C_D2_AP * Math.Pow(
                            Get_C_L(h, CAS, m, DeltaISA, phi), 2);
                        break;
                    case FlightPhase.Landing:
                        C_D = C_D0_LDG + C_D0_DeltaLDG
                            + C_D2_LDG * Math.Pow(Get_C_L(h, CAS, m, DeltaISA, phi), 2);
                        break;
                    default:
                        C_D = C_D0_CR + C_D2_CR * Math.Pow(
                            Get_C_L(h, CAS, m, DeltaISA, phi), 2);
                        break;
                }
            }
            return C_D;
        }



        /// <summary> [Manual p.18] Get drag force in N during different phases. 
        /// Aircraft weight is set to reference weight.
        /// </summary>
        public double Get_D(double h, double CAS, FlightPhase flightPhase, double DeltaISA = 0,
            double phi = 0)
        {
            return Get_D(h, CAS, flightPhase, m_ref, DeltaISA, phi);
        }
        /// <summary> [Manual p.18] Get drag force in N during different phases.
        /// </summary>
        public double Get_D(double h, double CAS, FlightPhase flightPhase, double m,
            double DeltaISA = 0, double phi = 0)
        {
            double rho = AtmosphereEnviroment.Get_rho(h, DeltaISA);
            double TAS = AtmosphereEnviroment.Get_TAS(h, CAS, DeltaISA);
            return Get_C_D(h, CAS, flightPhase, m, DeltaISA, phi) * rho * TAS * TAS * S / 2;
        }



        /// <summary> [Manual p.32] Get drag force in N during expedited descent. 
        /// Aircraft weight is set to reference weight.
        /// </summary>
        public double Get_D_descent_exp(double h, double CAS, double DeltaISA = 0,
            double phi = 0)
        {
            return Get_D_descent_exp(h, CAS, m_ref, DeltaISA, phi);
        }
        /// <summary> [Manual p.32] Get drag force in N during expedited descent.
        /// </summary>
        public double Get_D_descent_exp(double h, double CAS, double m, double DeltaISA = 0,
            double phi = 0)
        {
            return C_des_exp * Get_D(h, CAS, FlightPhase.Descent, m, DeltaISA, phi);
        }



        /// <summary> [Manual p.20] Get maximum climb and take-off thrust in N.
        /// </summary>
        public double Get_T_max_climb(double h, double CAS, double DeltaISA = 0)
        {
            double T_max_climb = 0;
            double TAS = Units.mps2kt(AtmosphereEnviroment.Get_TAS(h, CAS, DeltaISA));
            switch (e_index)
            {
                case 1:
                    T_max_climb = C_TC_1 * (1 - h / C_TC_2 + C_TC_3 * h * h);
                    break;
                case 2:
                    T_max_climb = C_TC_1 * (1 - h / C_TC_2) / TAS + C_TC_3;
                    break;
                default:
                    T_max_climb = C_TC_1 * (1 - h / C_TC_2) + C_TC_3 / TAS;
                    break;
            }
            double DeltaISA_eff = DeltaISA - C_TC_4;
            if (DeltaISA_eff * C_TC_5 >= 0 && DeltaISA_eff * C_TC_5 <= 0.4)
                T_max_climb *= (1 - C_TC_5 * DeltaISA_eff);
            return T_max_climb;
        }



        /// <summary> [Manual p.21] Get maximum cruise thrust in N.
        /// </summary>
        public double Get_T_max_cruise(double h, double CAS, double DeltaISA = 0)
        {
            return C_T_cr * Get_T_max_climb(h, CAS, DeltaISA);
        }



        /// <summary> [Manual p.21] Get normal cruise thrust in N. 
        /// Aircraft weight is set to reference weight.
        /// </summary>
        public double Get_T_cruise(double h, double CAS, double DeltaISA = 0, double phi = 0)
        {
            return Get_T_cruise(h, CAS, m_ref, DeltaISA, phi);
        }
        /// <summary> [Manual p.21] Get normal cruise thrust in N.
        /// </summary>
        public double Get_T_cruise(double h, double CAS, double m, double DeltaISA = 0,
            double phi = 0)
        {
            return Get_D(h, CAS, FlightPhase.Cruise, m, DeltaISA, phi);
        }



        /// <summary> [Manual p.21] Get descent thrust in N.
        /// </summary>
        public double Get_T_descent(double h, double CAS, FlightPhase flightPhase,
            double DeltaISA = 0)
        {
            double T_descent = 0;
            if (h > h_des && flightPhase == FlightPhase.Descent)
                T_descent = C_Tdes_high * Get_T_max_climb(h, CAS, DeltaISA);
            else
                switch (flightPhase)
                {
                    case FlightPhase.Descent:
                        T_descent = C_Tdes_low * Get_T_max_climb(h, CAS, DeltaISA);
                        break;
                    case FlightPhase.Approach:
                        T_descent = C_Tdes_app * Get_T_max_climb(h, CAS, DeltaISA);
                        break;
                    case FlightPhase.Landing:
                        T_descent = C_Tdes_ld * Get_T_max_climb(h, CAS, DeltaISA);
                        break;
                }
            return T_descent;
        }



        /// <summary> [Added] Get thrust in N during different flight phases. 
        /// If maximum cruise thrust deployed, set maxCruiseThrustFlag to true. 
        /// Aircraft weight is set to reference weight.
        /// </summary>
        public double Get_T(double h, double CAS, FlightPhase flightPhase, double DeltaISA = 0,
            double phi = 0, bool maxCruiseThrustFlag = false)
        {
            return Get_T(h, CAS, flightPhase, m_ref, DeltaISA, phi, maxCruiseThrustFlag);
        }
        /// <summary> [Added] Get thrust in N during different flight phases. 
        /// If maximum cruise thrust deployed, set maxCruiseThrustFlag to true. 
        /// </summary>
        public double Get_T(double h, double CAS, FlightPhase flightPhase, double m,
                    double DeltaISA = 0, double phi = 0, bool maxCruiseThrustFlag = false)
        {
            double T = 0;
            switch (flightPhase)
            {
                case FlightPhase.Climb:
                    T = Get_T_max_climb(h, CAS, DeltaISA);
                    break;
                case FlightPhase.Cruise:
                    if (maxCruiseThrustFlag)
                        T = Get_T_max_cruise(h, CAS, DeltaISA);
                    else
                        T = Get_T_cruise(h, CAS, m, DeltaISA, phi);
                    break;
                case FlightPhase.Descent:
                case FlightPhase.Approach:
                case FlightPhase.Landing:
                    T = Get_T_descent(h, CAS, flightPhase, DeltaISA);
                    break;
            }
            return T;
        }



        /// <summary> [Manual p.34] Get reduced power coefficient.
        /// </summary>
        public double Get_C_pow_reduce(double m)
        {
            double C_reduce = 0;
            switch (e_index)
            {
                case 1:
                    C_reduce = C_reduce_jet;
                    break;
                case 2:
                    C_reduce = C_reduce_turbo;
                    break;
                default:
                    C_reduce = C_reduce_piston;
                    break;
            }
            return 1 - C_reduce * (m_max - m) / (m_max - m_min);
        }



        public enum SpeedMode
        {
            CAS,
            Mach
        }

        /// <summary> [Manual p.9-10] Get value of f{M}. Select SpeedMode to identify 
        /// type of airspeed. Note that CAS is in m/s if selecting CAS mode.
        /// </summary>
        public double Get_functionM(double h, SpeedMode speedMode, double airspeed,
            double DeltaISA = 0, AttitudeAndMovement attitudeAndMovement =
            AttitudeAndMovement.ConstantSpeed)
        {
            double functionM = 0;
            double gamma = AtmosphereEnviroment.gamma;
            double R = AtmosphereEnviroment.R;
            double k_T = AtmosphereEnviroment.k_T;
            double g = AtmosphereEnviroment.g;

            switch (attitudeAndMovement)
            {
                case AttitudeAndMovement.AccelerationInClimb:
                case AttitudeAndMovement.DecelerationInDescent:
                    functionM = 0.3;
                    break;
                case AttitudeAndMovement.DecelerationInClimb:
                case AttitudeAndMovement.AccelerationInDescent:
                    functionM = 1.7;
                    break;

                default:
                    double h_trop = AtmosphereEnviroment.Get_h_trop(DeltaISA);
                    if (speedMode == SpeedMode.Mach)
                    {
                        if (airspeed >= 0 && airspeed <= 2)
                        {
                            if (h > h_trop) functionM = 1.0;
                            else
                                functionM = 1 / (1 + gamma * R * k_T / (2 * g)
                                   * airspeed * airspeed);
                        }
                    }
                    else
                    {
                        double M = AtmosphereEnviroment.Get_TAS(h, airspeed, DeltaISA) /
                            AtmosphereEnviroment.Get_a(h, DeltaISA);
                        double subItem = Math.Pow(1 + (gamma - 1) / 2 * M * M,
                            (-1) / (gamma - 1)) * (Math.Pow(1 + (gamma - 1) / 2 * M * M,
                            gamma / (gamma - 1)) - 1);
                        if (h > h_trop)
                            functionM = 1 / (1 + subItem);
                        else
                            functionM = 1 / (1 + subItem + gamma * R * k_T
                                / (2 * g) * M * M);
                    }
                    break;
            }
            return functionM;
        }



        /// <summary> [Manual p.8 and 22] Get rate of climb(positive value) or 
        /// descent(negative value) in ft/min. Set reducedFlag to true when reduced 
        /// climb power deployed. Set expediteDescentFlag to true in expedited descent. 
        /// Aircraft weight is set to reference weight.
        /// </summary>
        public double Get_ROCD(double h, double CAS, FlightPhase flightPhase,
            AttitudeAndMovement attitudeAndMovement = AttitudeAndMovement.ConstantSpeed,
            double DeltaISA = 0, double phi = 0, bool reduceFlag = false,
            bool expediteDescentFlag = false)
        {
            return Get_ROCD(h, CAS, flightPhase, m_ref,
                attitudeAndMovement, DeltaISA, phi, reduceFlag, expediteDescentFlag);
        }
        /// <summary> [Manual p.8 and 22] Get rate of climb(positive value) or 
        /// descent(negative value) in ft/min. Set reducedFlag to true when reduced 
        /// climb power deployed. Set expediteDescentFlag to true in expedited descent.
        /// </summary>
        public double Get_ROCD(double h, double CAS, FlightPhase flightPhase, double m,
            AttitudeAndMovement attitudeAndMovement = AttitudeAndMovement.ConstantSpeed,
            double DeltaISA = 0, double phi = 0, bool reduceFlag = false,
            bool expediteDescentFlag = false)
        {
            double ROCD = 0;
            double h_max = Get_h_max_act(m, DeltaISA);

            double D = 0;
            if (expediteDescentFlag)
                D = Get_D_descent_exp(h, CAS, m, DeltaISA, phi);
            else
                D = Get_D(h, CAS, flightPhase, m, DeltaISA, phi);

            double TAS = AtmosphereEnviroment.Get_TAS(h, CAS, DeltaISA);
            double functionM = Get_functionM(h, SpeedMode.CAS, CAS, DeltaISA,
                attitudeAndMovement);
            double g = AtmosphereEnviroment.g;

            if (h < 0.8 * h_max && flightPhase == FlightPhase.Climb && reduceFlag)
            {
                double C_pow_reduce = Get_C_pow_reduce(m);
                ROCD = (Get_T_max_climb(h, CAS, DeltaISA) - D) /
                    (m * g) * TAS * C_pow_reduce * functionM;
            }
            else
                ROCD = (Get_T(h, CAS, flightPhase, m, DeltaISA, phi) - D) /
                   (m * g) * TAS * functionM;
            return Units.mps2fpm(ROCD);
        }



        public enum ThrustMode
        {
            Normal,
            MaximumCruiseThrust,
            IdleThrust
        }

        /// <summary> [Manual p.23-24] Get fuel flow in kg/min. Select ThrustMode to 
        /// thrust mode being used. Aircraft weight is set to reference weight.
        /// </summary>
        public double Get_ff(FlightPhase flightPhase, double h = -1, double CAS = -1, 
            ThrustMode thrustMode = ThrustMode.Normal, double DeltaISA = 0, double phi = 0)
        {
            return Get_ff(flightPhase, m_ref, h, CAS, thrustMode, DeltaISA, phi);
        }
        /// <summary> [Manual p.23-24] Get fuel flow in kg/min. Select ThrustMode to 
        /// thrust mode being used.
        /// </summary>
        public double Get_ff(FlightPhase flightPhase, double m, double h = -1, double CAS = -1,
            ThrustMode thrustMode = ThrustMode.Normal, double DeltaISA = 0, double phi = 0)
        {
            double ff = 0;
            if (e_index == 3)
                switch (flightPhase)
                {
                    case FlightPhase.Descent:
                        ff = C_f3;
                        break;
                    case FlightPhase.Cruise:
                        ff = C_f1 * C_fcr;
                        break;
                    default:
                        ff = C_f1;
                        break;
                }
            else
            {
                if (flightPhase == FlightPhase.Descent && 
                    thrustMode == ThrustMode.IdleThrust && h != -1)
                    ff = C_f3 * (1 - h / C_f4);
                else
                {
                    if (h != -1 && CAS != -1)
                    {
                        double TAS = Units.mps2kt(AtmosphereEnviroment.Get_TAS(h,
                            CAS, DeltaISA));
                        bool maxCruiseThrustFlag = false;
                        if (thrustMode == ThrustMode.MaximumCruiseThrust)
                            maxCruiseThrustFlag = true;
                        double T = Get_T(h, CAS, flightPhase, m, DeltaISA, phi, 
                            maxCruiseThrustFlag) / 1000;
                        double eta = 0;
                        switch (e_index)
                        {
                            case 1:
                                eta = C_f1 * (1 + TAS / C_f2);
                                break;
                            case 2:
                                eta = C_f1 * (1 - TAS / C_f2) * TAS / 1000;
                                break;
                        }
                        switch (flightPhase)
                        {
                            case FlightPhase.Cruise:
                                ff = eta * C_fcr * T;
                                break;
                            default:
                                ff = eta * T;
                                break;
                        }
                    }
                }
            }
            return ff;
        }



        /// <summary> [Manual p.27-28] Get climb speed(CAS) in kt. 
        /// Aircraft weight is set to reference weight.
        /// </summary>
        public double Get_v_climb(double h, double DeltaISA = 0)
        {
            return Get_v_climb(h, m_ref, DeltaISA);
        }
        /// <summary> [Manual p.27-28] Get climb speed(CAS) in kt.
        /// </summary>
        public double Get_v_climb(double h, double m, double DeltaISA = 0)
        {
            double v_climb = 0;
            double h_cross = AtmosphereEnviroment.Get_h_cross(Units.kt2mps(v_cl_2), M_cl);
            double v_stall_TO1 = CorrectV(v_stall_TO, m);
            switch (e_index)
            {
                case 1:
                    if (h >= 0 && h <= 1499)
                        v_climb = C_v_min * v_stall_TO1 + v_d_cl_1;
                    else if (h >= 1500 && h <= 2999)
                        v_climb = C_v_min * v_stall_TO1 + v_d_cl_2;
                    else if (h >= 3000 && h <= 3999)
                        v_climb = C_v_min * v_stall_TO1 + v_d_cl_3;
                    else if (h >= 4000 && h <= 4999)
                        v_climb = C_v_min * v_stall_TO1 + v_d_cl_4;
                    else if (h >= 5000 && h <= 5999)
                        v_climb = C_v_min * v_stall_TO1 + v_d_cl_5;
                    else if (h >= 6000 && h <= 10000)
                        v_climb = Math.Min(v_cl_1, 250);
                    else if (h > 10000 && h <= h_cross)
                        v_climb = v_cl_2;
                    else
                        v_climb = Units.mps2kt(AtmosphereEnviroment.Get_CAS(h,
                            M_cl * AtmosphereEnviroment.Get_a(h, DeltaISA), DeltaISA));
                    break;

                default:
                    if (h >= 0 && h <= 499)
                        v_climb = C_v_min * v_stall_TO1 + v_d_cl_6;
                    else if (h >= 500 && h <= 999)
                        v_climb = C_v_min * v_stall_TO1 + v_d_cl_7;
                    else if (h >= 1000 && h <= 1499)
                        v_climb = C_v_min * v_stall_TO1 + v_d_cl_8;
                    else if (h >= 1500 && h <= 9999)
                        v_climb = Math.Min(v_cl_1, 250);
                    else if (h >= 10000 && h <= h_cross)
                        v_climb = v_cl_2;
                    else
                        v_climb = Units.mps2kt(AtmosphereEnviroment.Get_CAS(h,
                            M_cl * AtmosphereEnviroment.Get_a(h, DeltaISA), DeltaISA));
                    break;
            }
            return Math.Round(v_climb, 0);
        }



        /// <summary> [Manual p.28-29] Get cruise speed(CAS) in kt.
        /// </summary>
        public double Get_v_cruise(double h, double DeltaISA = 0)
        {
            double v_cruise = 0;
            double h_cross = AtmosphereEnviroment.Get_h_cross(Units.kt2mps(v_cr_2), M_cr);
            switch (e_index)
            {
                case 1:
                    if (h >= 0 && h <= 2999)
                        v_cruise = 170;
                    else if (h >= 3000 && h <= 5999)
                        v_cruise = Math.Min(v_cr_1, 220);
                    else if (h >= 6000 && h <= 14000)
                        v_cruise = Math.Min(v_cr_1, 250);
                    else if (h > 14000 && h <= h_cross)
                        v_cruise = v_cr_2;
                    else
                        v_cruise = Units.mps2kt(AtmosphereEnviroment.Get_CAS(h,
                            M_cr * AtmosphereEnviroment.Get_a(h, DeltaISA), DeltaISA));
                    break;
                case 2:
                    if (h >= 0 && h <= 2999)
                        v_cruise = 150;
                    else if (h >= 3000 && h <= 5999)
                        v_cruise = Math.Min(v_cr_1, 180);
                    else if (h >= 6000 && h <= 9999)
                        v_cruise = Math.Min(v_cr_1, 250);
                    else if (h >= 10000 && h <= h_cross)
                        v_cruise = v_cr_2;
                    else
                        v_cruise = Units.mps2kt(AtmosphereEnviroment.Get_CAS(h,
                            M_cr * AtmosphereEnviroment.Get_a(h, DeltaISA), DeltaISA));
                    break;
            }
            return Math.Round(v_cruise, 0);
        }



        /// <summary> [Manual p.31] Return true if longitudinal acceleration within 
        /// required limit. Unit of timeDuration is min, maintainH is ft, originalV 
        /// and targetV are CAS in m/s.
        /// </summary>
        public bool AccelerationLimit(double timeDuration, double originalV, double targetV,
            double maintainH, double DeltaISA = 0)
        {
            double t = Units.min2s(timeDuration);
            originalV = Units.mps2fpm(AtmosphereEnviroment.Get_TAS(maintainH,
                originalV, DeltaISA));
            targetV = Units.mps2fpm(AtmosphereEnviroment.Get_TAS(maintainH,
                targetV, DeltaISA));
            return Math.Abs(targetV - originalV) <= acc_max_long * t;
        }

        /// <summary> [Manual p.31] Return true if longitudinal and normal acceleration 
        /// within limit. Unit of timeDuration is min, originalV and targetV are CAS in m/s, 
        /// originalH and targetH are ft.
        /// </summary>
        public bool AccelerationLimit(double timeDuration, double originalV, double targetV,
            double originalH, double targetH, double DeltaISA = 0)
        {
            double t = Units.min2s(timeDuration);
            originalV = Units.mps2fpm(AtmosphereEnviroment.Get_TAS(originalH,
                originalV, DeltaISA));
            targetV = Units.mps2fpm(AtmosphereEnviroment.Get_TAS(targetH,
                targetV, DeltaISA));
            double originalGamma = 1 / Math.Sin(originalH / originalV);
            double targetGamma = 1 / Math.Sin(targetH / targetV);
            double v = (originalV + targetV) / 2;
            return Math.Abs(targetV - originalV) <= acc_max_long * t
                && Math.Abs(targetGamma - originalGamma) <= acc_max_norm * t / v;
        }



        /// <summary> [Manual p.32] Get nominal bank angle in degr. 
        /// If maximum bank angle is used, set maxFlag to true.
        /// </summary>
        public double Get_phi(FlightPhase flightPhase, bool maxFlag = false)
        {
            double phi = 0;
            if (maxFlag)
                switch (flightPhase)
                {
                    case FlightPhase.Takeoff:
                    case FlightPhase.Landing:
                        phi = ang_max_tl;
                        break;
                    case FlightPhase.Holding:
                        phi = ang_max_hold;
                        break;
                    default:
                        phi = ang_max;
                        break;
                }
            else
                switch (flightPhase)
                {
                    case FlightPhase.Takeoff:
                    case FlightPhase.Landing:
                        phi = ang_norm_tl;
                        break;
                    default:
                        phi = ang_norm;
                        break;
                }
            return phi;
        }

        /// <summary> [Added] Get certain bank angle specified by user in degr. 
        /// If bankAngleValue beyond maximum angle, will return the maximum bank angle.
        /// </summary>
        public double Get_phi(FlightPhase flightPhase, double bankAngleValue)
        {
            double phi = 0;
            switch (flightPhase)
            {
                case FlightPhase.Takeoff:
                case FlightPhase.Landing:
                    phi = Math.Min(bankAngleValue, ang_max_tl);
                    break;
                case FlightPhase.Holding:
                    phi = Math.Min(bankAngleValue, ang_max_hold);
                    break;
                default:
                    phi = Math.Min(bankAngleValue, ang_max);
                    break;
            }
            return phi;
        }



        /// <summary> [Manual p.32] Get rate of turn in deg/s under nominal 
        /// or maximum bank angle. If maximum bank angle is used, set maxFlag to true.
        /// </summary>
        public double Get_varphi(double h, double CAS, FlightPhase flightPhase,
            bool maxFlag = false, double DeltaISA = 0)
        {
            double TAS = AtmosphereEnviroment.Get_TAS(h, CAS, DeltaISA);
            double phi = Units.deg2rad(Get_phi(flightPhase, maxFlag));
            return Units.radps2degps(AtmosphereEnviroment.g * Math.Tan(phi) / TAS);
        }

        /// <summary> [Added] Get rate of turn in deg/s under certain bank angle(in degr).
        /// </summary>
        public double Get_varphi(double h, double CAS, FlightPhase flightPhase,
            double bankAngleValue, double DeltaISA = 0)
        {
            double TAS = AtmosphereEnviroment.Get_TAS(h, CAS, DeltaISA);
            double phi = Units.deg2rad(Get_phi(flightPhase, bankAngleValue));
            return Units.radps2degps(AtmosphereEnviroment.g * Math.Tan(phi) / TAS);
        }



        /// <summary> [Added] Get turning radius in meters under nominal or 
        /// maximum bank angle. If maximum bank angle is used, set maxFlag to true.
        /// </summary>
        public double Get_turningR(double h, double CAS, FlightPhase flightPhase,
            bool maxFlag = false, double DeltaISA = 0)
        {
            double TAS = AtmosphereEnviroment.Get_TAS(h, CAS, DeltaISA);
            double phi = Units.deg2rad(Get_phi(flightPhase, maxFlag));
            return TAS * TAS / (AtmosphereEnviroment.g * Math.Tan(phi));
        }

        /// <summary> [Added] Get rturning radius in meters under certain bank angle(in degr).
        /// </summary>
        public double Get_turningR(double h, double CAS, FlightPhase flightPhase,
            double bankAngleValue, double DeltaISA = 0)
        {
            double TAS = AtmosphereEnviroment.Get_TAS(h, CAS, DeltaISA);
            double phi = Units.deg2rad(Get_phi(flightPhase, bankAngleValue));
            return TAS * TAS / (AtmosphereEnviroment.g * Math.Tan(phi));
        }



        /// <summary> [Manual p.34] Get airspeed(CAS) during hold in kt.
        /// </summary>
        public double Get_v_hold(double h, double DeltaISA = 0)
        {
            double v_hold = 0;
            if (h >= 0 && h <= 14000)
                v_hold = v_hold_1;
            else if (h > 14000 && h <= 20000)
                v_hold = v_hold_2;
            else if (h > 20000 && h <= 34000)
                v_hold = v_hold_3;
            else
                v_hold = Units.mps2kt(AtmosphereEnviroment.Get_CAS(h,
                    v_hold_4 * AtmosphereEnviroment.Get_a(h, DeltaISA), DeltaISA));
            return Math.Round(v_hold, 0);
        }



        public enum TakeoffRunwayCondition
        {
            Paved,
            HardTurfGravel,
            ShortDryGrass,
            LongGrass,
            SoftGround
        }

        /// <summary> [Book p.228-232, 247-248] Get take-off distance in m. 
        /// Take-off weight is set to maximum weight.
        /// </summary>
        public double Get_Takeoff_Distance(int numberOfEngines, double h = 0, 
            double climbEngineLapseRate = 0.803, 
            TakeoffRunwayCondition takeoffRunwayCondition = TakeoffRunwayCondition.Paved, 
            double n = 1.2, double screenHeight = 35, double DeltaISA = 0)
        {
            return Get_Takeoff_Distance(numberOfEngines, m_max, h, climbEngineLapseRate, 
                takeoffRunwayCondition, n, screenHeight, DeltaISA);
        }
        /// <summary> [Book p.228-232, 247-248] Get take-off distance in m.
        /// </summary>
        public double Get_Takeoff_Distance(int numberOfEngines, double takeoffWeight, 
            double h = 0, double climbEngineLapseRate = 0.803, 
            TakeoffRunwayCondition takeoffRunwayCondition = TakeoffRunwayCondition.Paved, 
            double n = 1.2, double screenHeight = 35, double DeltaISA = 0)
        {
            double totalDistance = 0;
            double mu;
            switch (takeoffRunwayCondition)
            {
                case TakeoffRunwayCondition.Paved:
                    mu = 0.02;
                    break;
                case TakeoffRunwayCondition.HardTurfGravel:
                    mu = 0.04;
                    break;
                case TakeoffRunwayCondition.ShortDryGrass:
                    mu = 0.05;
                    break;
                case TakeoffRunwayCondition.LongGrass:
                    mu = 0.1;
                    break;
                default:
                    mu = 0.2;
                    break;
            }
            double v_LOF = Units.kt2mps(1.1 * v_stall_TO);
            double T = numberOfEngines * Get_T(h, v_LOF, FlightPhase.Climb, 
                m : takeoffWeight, DeltaISA: DeltaISA);
            double W = takeoffWeight * AtmosphereEnviroment.g;
            double K_T = T / W - mu;
            double rho = AtmosphereEnviroment.Get_rho(h, DeltaISA);
            double C_L_max = Get_C_L(h, Units.kt2mps(v_stall_TO), DeltaISA : DeltaISA);
            double C_D = C_D0_CR + C_D2_CR * C_L_max * C_L_max;
            double K_A = rho * (-1) * (C_D + mu * C_L_max) / (2 * W / (numberOfEngines * S));
            double S_G = 1 / (2 * AtmosphereEnviroment.g * K_A) * Math.Log((K_T + 
                K_A * v_LOF * v_LOF) / K_T);

            double v_2 = Units.kt2mps(1.2 * v_stall_TO);
            double v_trans = (v_LOF + v_2) / 2;
            double r = v_trans * v_trans / AtmosphereEnviroment.g / (n - 1);
            double D = 0.5 * rho * v_2 * v_2 * S * C_D;
            T *= climbEngineLapseRate;
            double gamma = (T - D) / W;
            double S_T = r * gamma;

            double h_T = r * gamma * gamma / 2;
            double h_S = Units.ft2m(screenHeight);
            if (h_T > h_S)
                S_T = Math.Sqrt((r + h_S) * (r + h_S) - r * r);
            else
                S_T += (h_S - h_T) / gamma;

            return totalDistance = (S_G + S_T) * 1.15;
        }



        /// <summary> [Added] Get maximum engine failure speed(v1, CAS) in kt.
        /// </summary>
        public double Get_V_1_Maximum()
        {
            return 1.1 * v_stall_TO;
        }


        
        public enum Balanced_Field_LengthType
        {
            AccelerateGo,
            AccelerateStop
        }

        /*
        a problem!!!    (numberOfEngines - 1) * S??
        */

        /// <summary> [Book p.232-233, 248-250] Get balanced field length in m with one engine 
        /// out. Engine failure speed(v1) is m/s and cannot exceed maximum v1. 
        /// Take-off weight is set to maximum weight.
        /// </summary>
        public double Get_Balanced_Field_Length(int numberOfEngines, double v_1, 
            Balanced_Field_LengthType balancedFieldLengthType, double h = 0, 
            double runEngineLapseRate = 1, double transitionEngineLapseRate = 1, 
            double climbEngineLapseRate = 1, 
            TakeoffRunwayCondition takeoffRunwayCondition = TakeoffRunwayCondition.Paved, 
            double n = 1.2, double screenHeight = 35, double DeltaISA = 0)
        {
            return Get_Balanced_Field_Length(numberOfEngines, v_1, balancedFieldLengthType, 
                m_max, h, runEngineLapseRate, transitionEngineLapseRate, climbEngineLapseRate, 
                takeoffRunwayCondition, n, screenHeight, DeltaISA);
        }
        /// <summary> [Book p.232-233, 248-250] Get balanced field length in m with one engine 
        /// out. Engine failure speed(v1) is m/s and cannot exceed maximum v1.
        /// </summary>
        public double Get_Balanced_Field_Length(int numberOfEngines, double v_1, 
            Balanced_Field_LengthType balancedFieldLengthType, double takeoffWeight, 
            double h = 0, double runEngineLapseRate = 1, double transitionEngineLapseRate = 1, 
            double climbEngineLapseRate = 1, 
            TakeoffRunwayCondition takeoffRunwayCondition = TakeoffRunwayCondition.Paved, 
            double n = 1.2, double screenHeight = 35, double DeltaISA = 0)
        {
            double totalDistance = 0;
            const double C_D_windmilling = 0.003486;
            const double C_D_asymmetric = 0.00125;

            double mu;
            switch (takeoffRunwayCondition)
            {
                case TakeoffRunwayCondition.Paved:
                    mu = 0.02;
                    break;
                case TakeoffRunwayCondition.HardTurfGravel:
                    mu = 0.04;
                    break;
                case TakeoffRunwayCondition.ShortDryGrass:
                    mu = 0.05;
                    break;
                case TakeoffRunwayCondition.LongGrass:
                    mu = 0.1;
                    break;
                default:
                    mu = 0.2;
                    break;
            }
            double T = runEngineLapseRate * numberOfEngines * Get_T(h, v_1, FlightPhase.Climb, 
                DeltaISA: DeltaISA);
            double W = takeoffWeight * AtmosphereEnviroment.g;
            double K_T = T / W - mu;
            double rho = AtmosphereEnviroment.Get_rho(h, DeltaISA);
            double C_L_max = Get_C_L(h, Units.kt2mps(v_stall_TO), DeltaISA: DeltaISA);
            double C_D = C_D0_CR + C_D2_CR * C_L_max * C_L_max;
            double K_A = rho * (-1) * (C_D + mu * C_L_max) / (2 * W / (2 * S));
            double S_G = 1 / (2 * AtmosphereEnviroment.g * K_A) * Math.Log((K_T + 
                K_A * v_1 * v_1) / K_T);

            double S_reaction = 2 * v_1;

            double v_LOF = Units.kt2mps(1.1 * v_stall_TO);
            double v_m = (v_1 + v_LOF) / 2;
            T = transitionEngineLapseRate * (numberOfEngines - 1) * Get_T(h, v_m, 
                FlightPhase.Climb, DeltaISA: DeltaISA);
            K_T = T / W - mu;
            C_D += C_D_windmilling + C_D_asymmetric;
            K_A = rho * (-1) * (C_D + mu * C_L_max) / (2 * W / ((numberOfEngines - 1) * S));
            double acceleration = (K_T + K_A * v_m * v_m) * AtmosphereEnviroment.g;
            double DeltaTime = (v_LOF - v_1) / acceleration;
            double DeltaS = v_1 * DeltaTime + 0.5 * acceleration * DeltaTime * DeltaTime;

            double v_2 = Units.kt2mps(1.2 * v_stall_TO);
            v_m = (v_LOF + v_2) / 2;
            double r = v_m * v_m / AtmosphereEnviroment.g / (n - 1);
            T = climbEngineLapseRate * (numberOfEngines - 1) * Get_T(h, v_m, FlightPhase.Climb, 
                DeltaISA: DeltaISA);
            double C_L = Get_C_L(h, v_LOF, DeltaISA: DeltaISA);
            C_D = C_D0_CR + C_D2_CR * C_L * C_L;
            double D = 0.5 * rho * v_2 * v_2 * S * C_D;
            double gamma = (T - D) / W;
            double S_T = r * gamma;
            
            double h_T = r * gamma * gamma / 2;
            double h_S = Units.ft2m(screenHeight);
            if (h_T > h_S)
                S_T = Math.Sqrt((r + h_S) * (r + h_S) - r * r);
            else
                S_T += (h_S - h_T) / gamma;

            K_T = - 0.3;
            C_L = Get_C_L(h, v_1, DeltaISA: DeltaISA);
            C_D = C_D0_CR + C_D2_CR * C_L * C_L;
            K_A = rho * C_D / (2 * W / ((numberOfEngines - 1) * S));
            v_m = v_1 / 2;
            acceleration = (K_T + K_A * v_m * v_m) * AtmosphereEnviroment.g;
            double S_Stop = (-1) * v_1 * v_1 / (2 * acceleration);
            
            if (balancedFieldLengthType == Balanced_Field_LengthType.AccelerateGo)
                return totalDistance = (S_G + S_reaction + DeltaS + S_T) * 1.05;
            else
                return totalDistance = S_G + S_reaction + S_Stop;
        }



        public enum LandingRunwayCondition
        {
            Dry,
            Wet,
            Icy
        }

        /// <summary> [Book p.241-243, 263-264] Get landing distance in m. 
        /// Unit of approachAngle is degr. Landing weight is set to reference weight.
        /// </summary>
        public double Get_Landing_Distance(double h = 0, 
            LandingRunwayCondition landingRunwayCondition = LandingRunwayCondition.Dry, 
            double n = 1.2, double approachAngle = 3, double screenHeight = 50, 
            double DeltaISA = 0)
        {
            return Get_Landing_Distance(m_ref, h, landingRunwayCondition, n, approachAngle, 
                screenHeight, DeltaISA);
        }
        /// <summary> [Book p.241-243, 263-264] Get landing distance in m. Unit of 
        /// approachAngle is degr.
        /// </summary>
        public double Get_Landing_Distance(double landingWeight, double h = 0, 
            LandingRunwayCondition landingRunwayCondition = LandingRunwayCondition.Dry, 
            double n = 1.2, double approachAngle = 3, double screenHeight = 50, 
            double DeltaISA = 0)
        {
            double totalDistance = 0;
            double mu = 0;
            switch (landingRunwayCondition)
            {
                case LandingRunwayCondition.Dry:
                    mu = 0.3;
                    break;
                case LandingRunwayCondition.Wet:
                    mu = 0.1;
                    break;
                default:
                    mu = 0.05;
                    break;
            }
            double v_TD = Units.kt2mps(1.15 * v_stall_LD);
            double v_A = Units.kt2mps(1.3 * v_stall_LD);
            double v_F = (v_TD + v_A) / 2;
            double r = v_F * v_F / AtmosphereEnviroment.g / (n - 1);
            double gamma = Units.deg2rad(approachAngle);
            double h_F = r * gamma * gamma / 2;
            double h_S = Units.ft2m(screenHeight);
            double S_A = (h_S - h_F) / gamma;
            double S_F = r * gamma;

            double S_FR = 2 * v_TD;
            double K_T = -1 * mu;
            double W = landingWeight * AtmosphereEnviroment.g;
            double rho = AtmosphereEnviroment.Get_rho(h, DeltaISA);
            double C_D = C_D0_LDG + C_D0_DeltaLDG;
            double K_A = rho * (-1) * C_D / (2 * W / (2 * S));
            double S_B = (-1) / (2 * AtmosphereEnviroment.g * K_A) * Math.Log((K_T 
                + K_A * v_TD * v_TD) / K_T);
            
            return totalDistance = (S_A + S_F + S_FR + S_B) * 1.66;
        }
    }
}
