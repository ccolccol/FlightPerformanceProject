using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DataIO
{
    class DataContainer
    {
        public Dictionary<string, string[]> dataDictionary { get; private set; }
            = new Dictionary<string, string[]>();
        public string ACcode { get; private set; } = null;
        public string fileName { get; private set; }

        public void DataReader(string filename)
        {
            fileName = filename;
            try
            {
                FileStream stream = new FileStream(fileName, FileMode.Open);
                StreamReader reader = new StreamReader(stream);
                string line;
                string[] parts;
                string[] valueParts;
                char[] charSeparator = new char[] { ',' };
                char[] valueSeparator = new char[] { 'E' };
                while (reader.Peek() != -1)
                {
                    line = reader.ReadLine();
                    if (line.Contains("*"))
                    {
                        string headRemark = "A/C CODE";
                        if (line.Contains(headRemark))
                        {
                            line = line.Substring(line.IndexOf(headRemark) + headRemark.Length);
                            line = line.TrimStart();
                            ACcode = line.Substring(0, line.IndexOf(" "));
                        }
                        else
                            continue;
                    }
                    else if (string.IsNullOrWhiteSpace(line))
                        continue;
                    else
                    {
                        parts = line.Split(charSeparator, 2);
                        valueParts = parts[0].Trim().Split(valueSeparator, 2);
                        dataDictionary.Add(parts[1].Trim(), valueParts);
                    }
                }
                reader.Close();
            }
            catch (IOException exception)
            {
                throw exception;
            }
        }

        public void OPFReader(string filename)
        {
            try
            {
                FileStream stream = new FileStream(filename, FileMode.Open);
                StreamReader reader = new StreamReader(stream);

                while (reader.Peek() != -1)
                {
                    string text = reader.ReadLine();
                    if (text.Contains("Actype") && text.Contains("="))
                    {
                        string dataLine = reader.ReadLine();
                        string[] splitParts = dataLine.Split();
                        string[] data = new string[7];
                        int index = 0;
                        foreach (var part in splitParts)
                            if (part != "")
                                data.SetValue(part, index++);

                        ACcode = data[1].Replace("_", "");

                        switch (data[4])
                        {
                            case "Jet":
                                dataDictionary.Add("indexengine", new string[] { "1", null });
                                break;
                            case "Turboprop":
                                dataDictionary.Add("indexengine", new string[] { "2", null });
                                break;
                            default:
                                dataDictionary.Add("indexengine", new string[] { "3", null });
                                break;
                        }
                    }
                    else if (text.Contains("Mass") && text.Contains("="))
                    {
                        string dataLine = reader.ReadLine();
                        dataLine = reader.ReadLine();
                        string[] splitParts = dataLine.Split();
                        string[] data = new string[5];
                        int index = 0;
                        foreach (var part in splitParts)
                            if (part.Contains("E") && part.Contains("."))
                                data.SetValue(part.Trim().Insert(0, "0"), index++);

                        char[] separator = new char[] { 'E' };
                        dataDictionary.Add("refweight", data[0].Split(separator, 2));
                        dataDictionary.Add("minweight", data[1].Split(separator, 2));
                        dataDictionary.Add("maxweight", data[2].Split(separator, 2));
                        dataDictionary.Add("maxpayload", data[3].Split(separator, 2));
                        dataDictionary.Add("massgrad", data[4].Split(separator, 2));
                    }
                    else if (text.Contains("envelope") && text.Contains("="))
                    {
                        string dataLine = reader.ReadLine();
                        dataLine = reader.ReadLine();
                        string[] splitParts = dataLine.Split();
                        string[] data = new string[5];
                        int index = 0;
                        foreach (var part in splitParts)
                            if (part.Contains("E") && part.Contains("."))
                            {
                                if (index == 4)
                                {
                                    if (part.Length < 11)
                                        data.SetValue(part.Trim().Insert(1, "0").Insert(7, "0"), index++);
                                    else
                                        data.SetValue(part.Trim().Insert(1, "0"), index++);
                                }
                                else
                                    data.SetValue(part.Trim().Insert(0, "0"), index++);
                            }

                        char[] separator = new char[] { 'E' };
                        dataDictionary.Add("VMO", data[0].Split(separator, 2));
                        dataDictionary.Add("MMO", data[1].Split(separator, 2));
                        dataDictionary.Add("MaxAlt", data[2].Split(separator, 2));
                        dataDictionary.Add("Hmax", data[3].Split(separator, 2));
                        dataDictionary.Add("tempgrad", data[4].Split(separator, 2));
                    }
                    else if (text.Contains("Aerodynamics") && text.Contains("="))
                    {
                        string dataLine = reader.ReadLine();
                        for (int i = 1; i <= 2; i++)
                            dataLine = reader.ReadLine();
                        string[] splitParts = dataLine.Replace("CD", "").TrimStart().Split();

                        string[] ndrst = new string[] { $"0.{splitParts[0]}0000", "+01" };
                        dataDictionary.Add("ndrst", ndrst);

                        string[] data = new string[4];
                        int index = 0;
                        foreach (var part in splitParts)
                            if (part.Contains("E") && part.Contains("."))
                                data.SetValue(part.Trim().Insert(0, "0"), index++);

                        char[] separator = new char[] { 'E' };
                        dataDictionary.Add("Surf", data[0].Split(separator, 2));
                        dataDictionary.Add("Clbo", data[1].Split(separator, 2));
                        dataDictionary.Add("k", data[2].Split(separator, 2));
                        dataDictionary.Add("CM16", data[3].Split(separator, 2));

                        for (int i = 1; i <= 3; i++)
                            dataLine = reader.ReadLine();

                        splitParts = dataLine.Split();
                        data = new string[4];
                        index = 0;
                        foreach (var part in splitParts)
                            if (part.Contains("E") && part.Contains("."))
                                data.SetValue(part.Trim().Insert(0, "0"), index++);

                        dataDictionary.Add("Vstallcr", data[0].Split(separator, 2));
                        dataDictionary.Add("CD0cr", data[1].Split(separator, 2));
                        dataDictionary.Add("CD2cr", data[2].Split(separator, 2));

                        dataLine = reader.ReadLine();

                        splitParts = dataLine.Split();
                        data = new string[4];
                        index = 0;
                        foreach (var part in splitParts)
                            if (part.Contains("E") && part.Contains("."))
                                data.SetValue(part.Trim().Insert(0, "0"), index++);

                        dataDictionary.Add("Vstallic", data[0].Split(separator, 2));
                        dataDictionary.Add("CD0ic", data[1].Split(separator, 2));
                        dataDictionary.Add("CD2ic", data[2].Split(separator, 2));

                        dataLine = reader.ReadLine();

                        splitParts = dataLine.Split();
                        data = new string[4];
                        index = 0;
                        foreach (var part in splitParts)
                            if (part.Contains("E") && part.Contains("."))
                                data.SetValue(part.Trim().Insert(0, "0"), index++);

                        dataDictionary.Add("Vstallto", data[0].Split(separator, 2));
                        dataDictionary.Add("CD0to", data[1].Split(separator, 2));
                        dataDictionary.Add("CD2to", data[2].Split(separator, 2));

                        dataLine = reader.ReadLine();

                        splitParts = dataLine.Split();
                        data = new string[4];
                        index = 0;
                        foreach (var part in splitParts)
                            if (part.Contains("E") && part.Contains("."))
                                data.SetValue(part.Trim().Insert(0, "0"), index++);

                        dataDictionary.Add("Vstallap", data[0].Split(separator, 2));
                        dataDictionary.Add("CD0ap", data[1].Split(separator, 2));
                        dataDictionary.Add("CD2ap", data[2].Split(separator, 2));

                        dataLine = reader.ReadLine();

                        splitParts = dataLine.Split();
                        data = new string[4];
                        index = 0;
                        foreach (var part in splitParts)
                            if (part.Contains("E") && part.Contains("."))
                                data.SetValue(part.Trim().Insert(0, "0"), index++);

                        dataDictionary.Add("Vstallld", data[0].Split(separator, 2));
                        dataDictionary.Add("CD0ld", data[1].Split(separator, 2));
                        dataDictionary.Add("CD2ld", data[2].Split(separator, 2));

                        for (int i = 1; i <= 3; i++)
                            dataLine = reader.ReadLine();

                        splitParts = dataLine.Split();
                        data = new string[2];
                        index = 0;
                        foreach (var part in splitParts)
                            if (part.Contains("E") && part.Contains("."))
                                data.SetValue(part.Trim().Insert(0, "0"), index++);

                        dataDictionary.Add("cdSpoiler", data[0].Split(separator, 2));

                        for (int i = 1; i <= 3; i++)
                            dataLine = reader.ReadLine();

                        splitParts = dataLine.Split();
                        data = new string[3];
                        index = 0;
                        foreach (var part in splitParts)
                            if (part.Contains("E") && part.Contains("."))
                                data.SetValue(part.Trim().Insert(0, "0"), index++);

                        dataDictionary.Add("cdGear0", data[0].Split(separator, 2));
                        dataDictionary.Add("cdgear2", data[1].Split(separator, 2));
                    }
                    else if (text.Contains("Thrust") && text.Contains("="))
                    {
                        string dataLine = reader.ReadLine();
                        dataLine = reader.ReadLine();
                        string[] splitParts = dataLine.Split();
                        string[] data = new string[5];
                        int index = 0;
                        foreach (var part in splitParts)
                            if (part.Contains("E") && part.Contains("."))
                            {
                                if (index == 2 && part.StartsWith("-"))
                                {
                                    if(part.Length < 11)
                                        data.SetValue(part.Trim().Insert(1, "0").Insert(7, "0"), index++);
                                    else
                                        data.SetValue(part.Trim().Insert(1, "0"), index++);
                                }
                                else
                                    data.SetValue(part.Trim().Insert(0, "0"), index++);
                            }

                        char[] separator = new char[] { 'E' };

                        dataDictionary.Add("ctc1", data[0].Split(separator, 2));
                        dataDictionary.Add("ctc2", data[1].Split(separator, 2));
                        dataDictionary.Add("ctc3", data[2].Split(separator, 2));
                        dataDictionary.Add("ctc4", data[3].Split(separator, 2));
                        dataDictionary.Add("ctc5", data[4].Split(separator, 2));

                        for (int i = 1; i <= 2; i++)
                            dataLine = reader.ReadLine();

                        splitParts = dataLine.Split();
                        data = new string[5];
                        index = 0;
                        foreach (var part in splitParts)
                            if (part.Contains("E") && part.Contains("."))
                                data.SetValue(part.Trim().Insert(0, "0"), index++);

                        dataDictionary.Add("Desclow", data[0].Split(separator, 2));
                        dataDictionary.Add("Deschigh", data[1].Split(separator, 2));
                        dataDictionary.Add("Desclevel", data[2].Split(separator, 2));
                        dataDictionary.Add("Descapp", data[3].Split(separator, 2));
                        dataDictionary.Add("Descld", data[4].Split(separator, 2));

                        for (int i = 1; i <= 2; i++)
                            dataLine = reader.ReadLine();

                        splitParts = dataLine.Split();
                        data = new string[5];
                        index = 0;
                        foreach (var part in splitParts)
                            if (part.Contains("E") && part.Contains("."))
                                data.SetValue(part.Trim().Insert(0, "0"), index++);

                        dataDictionary.Add("DescCAS", data[0].Split(separator, 2));
                        dataDictionary.Add("DescMach", data[1].Split(separator, 2));
                    }
                    else if (text.Contains("Fuel") && text.Contains("="))
                    {
                        string dataLine = reader.ReadLine();
                        dataLine = reader.ReadLine();
                        string[] splitParts = dataLine.Split();
                        string[] data = new string[2];
                        int index = 0;
                        foreach (var part in splitParts)
                            if (part.Contains("E") && part.Contains("."))
                                data.SetValue(part.Trim().Insert(0, "0"), index++);

                        char[] separator = new char[] { 'E' };

                        dataDictionary.Add("cf1", data[0].Split(separator, 2));
                        dataDictionary.Add("cf2", data[1].Split(separator, 2));

                        for (int i = 1; i <= 2; i++)
                            dataLine = reader.ReadLine();

                        splitParts = dataLine.Split();
                        data = new string[2];
                        index = 0;
                        foreach (var part in splitParts)
                            if (part.Contains("E") && part.Contains("."))
                            {
                                if (index == 1 && part.StartsWith("-"))
                                {
                                    if(part.Length < 11)
                                        data.SetValue(part.Trim().Insert(1, "0").Insert(7, "0"), index++);
                                    else
                                        data.SetValue(part.Trim().Insert(1, "0"), index++);
                                }
                                else
                                    data.SetValue(part.Trim().Insert(0, "0"), index++);
                            }

                        dataDictionary.Add("cf3", data[0].Split(separator, 2));
                        dataDictionary.Add("cf4", data[1].Split(separator, 2));

                        for (int i = 1; i <= 2; i++)
                            dataLine = reader.ReadLine();

                        splitParts = dataLine.Split();
                        data = new string[5];
                        index = 0;
                        foreach (var part in splitParts)
                            if (part.Contains("E") && part.Contains("."))
                                data.SetValue(part.Trim().Insert(0, "0"), index++);

                        dataDictionary.Add("cfcr", data[0].Split(separator, 2));
                    }
                    else if (text.Contains("Ground") && text.Contains("="))
                    {
                        string dataLine = reader.ReadLine();
                        dataLine = reader.ReadLine();
                        string[] splitParts = dataLine.Split();
                        string[] data = new string[5];
                        int index = 0;
                        foreach (var part in splitParts)
                            if (part.Contains("E") && part.Contains("."))
                                data.SetValue(part.Trim().Insert(0, "0"), index++);

                        char[] separator = new char[] { 'E' };

                        dataDictionary.Add("tol", data[0].Split(separator, 2));
                        dataDictionary.Add("ldl", data[1].Split(separator, 2));
                        dataDictionary.Add("span", data[2].Split(separator, 2));
                        dataDictionary.Add("length", data[3].Split(separator, 2));
                    }
                }
                reader.Close();
            }
            catch (IOException exception)
            {
                throw exception;
            }
        }

        public void APFReader(string filename)
        {
            try
            {
                FileStream stream = new FileStream(filename, FileMode.Open);
                StreamReader reader = new StreamReader(stream);

                while (reader.Peek() != -1)
                {
                    string text = reader.ReadLine();
                    if (text.Contains("File_name"))
                    {
                        string dataLine = text;
                        string[] splitParts = dataLine.Split();
                        string[] data = new string[4];
                        int index = 0;
                        foreach (var part in splitParts)
                            if (part != "")
                                data.SetValue(part, index++);

                        ACcode = data[2].Replace("_", " ").Split()[0];
                    }
                    else if (text.Contains("AV") && text.Contains(ACcode))
                    {
                        string dataLine = text;
                        string[] splitParts = dataLine.Split();
                        string[] data = new string[18];
                        int index = 0;
                        foreach (var part in splitParts)
                            if (part != "")
                                data.SetValue(part, index++);

                        int initialIndex = 0;
                        for (int i = 0; i < data.Length; i++)
                            if (data[i] == "AV" && Regex.IsMatch(data[i + 1], @"[a-zA-z]") == false)
                            {
                                initialIndex = ++i;
                                break;
                            }

                        dataDictionary.Add("vcl1", new string[] { $"0.{data[initialIndex++]}00", "+03" });
                        dataDictionary.Add("vcl2", new string[] { $"0.{data[initialIndex++]}00", "+03" });
                        dataDictionary.Add("mcl", new string[] { $"0.{data[initialIndex++]}000", "+00" });
                        dataDictionary.Add("vcr1", new string[] { $"0.{data[initialIndex++]}00", "+03" });
                        dataDictionary.Add("vcr2", new string[] { $"0.{data[initialIndex++]}00", "+03" });
                        dataDictionary.Add("mcr", new string[] { $"0.{data[initialIndex++]}000", "+00" });
                        dataDictionary.Add("mdes", new string[] { $"0.{data[initialIndex++]}000", "+00" });
                        dataDictionary.Add("vdes1", new string[] { $"0.{data[initialIndex++]}00", "+03" });
                        dataDictionary.Add("vdes2", new string[] { $"0.{data[initialIndex]}00", "+03" });
                    }
                }
                reader.Close();
            }
            catch (IOException exception)
            {
                throw exception;
            }
        }

        public void GPFReader(string filename, string[] keysGPF)
        {
            try
            {
                FileStream stream = new FileStream(filename, FileMode.Open);
                StreamReader reader = new StreamReader(stream);

                int keysIndex = 0;
                while (reader.Peek() != -1)
                {
                    string text = reader.ReadLine();
                    char[] separator = new char[] { 'E' };

                    if (text.Contains("ang_bank_nom"))
                    {
                        if (text.Contains("to,lnd"))
                        {
                            string[] splitParts = text.Split();
                            foreach (var part in splitParts)
                                if (part.Contains(".") && part.Contains("E"))
                                {
                                    dataDictionary.Add("ang_bank_nomtl", part.Insert(0, "0").Split(separator, 2));
                                    keysIndex++;
                                }
                        }
                        else if (text.Contains("civ") && text.Contains("ic,cl,cr,des,hold,app"))
                        {
                            string[] splitParts = text.Split();
                            foreach (var part in splitParts)
                                if (part.Contains(".") && part.Contains("E"))
                                {
                                    dataDictionary.Add("ang_bank_nom", part.Insert(0, "0").Split(separator, 2));
                                    keysIndex++;
                                }
                        }
                        else if (text.Contains("to,ic,cl,cr,des,hold,app,lnd"))
                        {
                            string[] splitParts = text.Split();
                            foreach (var part in splitParts)
                                if (part.Contains(".") && part.Contains("E"))
                                {
                                    dataDictionary.Add("ang_bank_nommil", part.Insert(0, "0").Split(separator, 2));
                                    keysIndex++;
                                }
                        }
                    }
                    else if (text.Contains("ang_bank_max"))
                    {
                        if (text.Contains("to,lnd"))
                        {
                            string[] splitParts = text.Split();
                            foreach (var part in splitParts)
                                if (part.Contains(".") && part.Contains("E"))
                                {
                                    dataDictionary.Add("ang_bank_maxtl", part.Insert(0, "0").Split(separator, 2));
                                    keysIndex++;
                                }
                        }
                        else if (text.Contains("hold") && text.Contains("to,ic,cl,cr") == false)
                        {
                            string[] splitParts = text.Split();
                            foreach (var part in splitParts)
                                if (part.Contains(".") && part.Contains("E"))
                                {
                                    dataDictionary.Add("ang_bank_maxhold", part.Insert(0, "0").Split(separator, 2));
                                    keysIndex++;
                                }
                        }
                        else if (text.Contains("civ") && text.Contains("ic,cl,cr,des,app"))
                        {
                            string[] splitParts = text.Split();
                            foreach (var part in splitParts)
                                if (part.Contains(".") && part.Contains("E"))
                                {
                                    dataDictionary.Add("ang_bank_max", part.Insert(0, "0").Split(separator, 2));
                                    keysIndex++;
                                }
                        }
                        else if (text.Contains("to,ic,cl,cr,des,hold,app,lnd"))
                        {
                            string[] splitParts = text.Split();
                            foreach (var part in splitParts)
                                if (part.Contains(".") && part.Contains("E"))
                                {
                                    dataDictionary.Add("ang_bank_maxmil", part.Insert(0, "0").Split(separator, 2));
                                    keysIndex++;
                                }
                        }
                    }
                    else if (text.Contains("C_v_min_to"))
                    {
                        string[] splitParts = text.Split();
                        foreach (var part in splitParts)
                            if (part.Contains(".") && part.Contains("E"))
                            {
                                dataDictionary.Add("C_v_minto", part.Insert(0, "0").Split(separator, 2));
                                keysIndex++;
                            }
                    }
                    else if (text.Contains("C_red_piston") || text.Contains("C_red_turbo") || text.Contains("C_red_jet"))
                    {
                        string[] splitParts = text.Split();
                        foreach (var part in splitParts)
                            if (part.Contains("."))
                            {
                                string baseValue = part.Substring(0, 6).Insert(0, "0");
                                string powerValue = part.Substring(7);
                                dataDictionary.Add(keysGPF[keysIndex], new string[] { baseValue, powerValue } );
                                keysIndex++;
                            }
                    }
                    else if (keysIndex < keysGPF.Length && text.Contains(keysGPF[keysIndex]))
                    {
                        string[] splitParts = text.Split();
                        foreach (var part in splitParts)
                            if (part.Contains(".") && part.Contains("E"))
                            {
                                dataDictionary.Add(keysGPF[keysIndex], part.Insert(0, "0").Split(separator, 2));
                                keysIndex++;
                            }
                    }
                }
                reader.Close();
            }
            catch (IOException exception)
            {
                throw exception;
            }
        }

        public bool DataWriter(string[] keysAll)
        {
            bool saveSuccess = false;
            try
            {
                FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate);
                StreamWriter writer = new StreamWriter(stream);


                string headElement1 = "*****A/C CODE ";
                string headElement2 = ACcode;
                string headElement3 = "   BADA DATA INCLUDING OPF, APF AND GPF DATA*****";
                string headelementsCombined = $"{headElement1}{headElement2}{headElement3}";
                string headSpace = "";
                string headSimple = "*****A/C CODE A306   BADA DATA INCLUDING OPF, APF AND GPF DATA*****                                                             ";
                for (int i = 0; i < headSimple.Length - headelementsCombined.Length; i++)
                    headSpace += " ";
                string head = $"{headelementsCombined}{headSpace}";
                writer.WriteLine(head);


                string line = null;
                string element1;
                string element2;
                string element3 = "E";
                string element4;
                string element5 = " ,";
                string element6;
                string elementFirstFewCombined;
                string element7 = "";
                string element8 = "          ";
                string simple = "     0.87000E+02 ,minweight                                                                                                     ";

                writer.WriteLine(" *********AIRCRAFT BASIC PERFORMANCE DATA*********                                                                              ");

                if (Contain(keysAll[0]))
                {
                    element1 = "     ";
                    element2 = dataDictionary[keysAll[0]][0];
                    element6 = keysAll[0];
                    elementFirstFewCombined = $"{element1}{element2}{element8}{element5}{element6}";
                    for (int i = 0; i < simple.Length - elementFirstFewCombined.Length; i++)
                        element7 += " ";
                    line = $"{elementFirstFewCombined}{element7}";
                    writer.WriteLine(line);
                }

                for (int i = 1; i < keysAll.Length; i++)
                {
                    if (keysAll[i] == "vcl1")
                        writer.WriteLine(" **********AIRLINES OPERATION DATA**********                                                                                    ");
                    else if (keysAll[i] == "acc_long_max")
                        writer.WriteLine(" ****************COMMON DATA****************                                                                                    ");

                    if (Contain(keysAll[i]))
                    {
                        element2 = dataDictionary[keysAll[i]][0];
                        element4 = dataDictionary[keysAll[i]][1];
                        element6 = keysAll[i];
                        element7 = "";
                        switch (keysAll[i])
                        {
                            case "tempgrad":
                            case "ctc3":
                            case "cf4":
                                element1 = dataDictionary[keysAll[i]][0].StartsWith("-") ? "    " : "     ";
                                break;
                            default:
                                element1 = "     ";
                                break;
                        }
                        elementFirstFewCombined = $"{element1}{element2}{element3}{element4}{element5}{element6}";
                        for (int j = 0; j < simple.Length - elementFirstFewCombined.Length; j++)
                            element7 += " ";
                        line = $"{elementFirstFewCombined}{element7}";
                        writer.WriteLine(line);
                    }
                }
                writer.Close();
                saveSuccess = true;
            }
            catch (IOException exception)
            {
                throw exception;
            }
            return saveSuccess;
        }

        public bool Modify_Save(string[] keys, string[] bases, string[] powers,
            string[] keysAll, bool createFlag = false)
        {
            bool saveOk = false;

            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i] == "indexengine")
                {
                    if (Contain(keys[i])) dataDictionary[keys[i]][0] = bases[i];
                    else
                        dataDictionary.Add(keys[i], new string[] { bases[i], null });
                }
                else
                {
                    if (Contain(keys[i]))
                    {
                        dataDictionary[keys[i]][0] = bases[i];
                        dataDictionary[keys[i]][1] = powers[i];
                    }
                    else
                        dataDictionary.Add(keys[i], new string[] { bases[i], powers[i] });
                }
            }

            if (createFlag)
            {
                return saveOk = true;
            }
            else
                return saveOk = DataWriter(keysAll);
        }


        public bool Contain(string key)
        {
            return dataDictionary.ContainsKey(key);
        }

        public string[] Import(string key)
        {
            string[] values;
            dataDictionary.TryGetValue(key, out values);
            return values;
        }

        public void SetACcode(string acCode)
        {
            ACcode = acCode;
        }

        public void SetFileName(string filename)
        {
            fileName = filename;
        }

        public void ModifyData(string key, string[] value)
        {
            if (dataDictionary.ContainsKey(key))
                dataDictionary[key] = value;
            else
                dataDictionary.Add(key, value);
        }
    }
}