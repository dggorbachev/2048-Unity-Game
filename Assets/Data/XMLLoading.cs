using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

public class XMLLoading
{
    private static string path = Environment.CurrentDirectory + @"\Data\saves.xml";
    public void XMLLoadingRating(out List<string[]> data)
    {
        data = new List<string[]>();
        XDocument doc = new XDocument();
        doc = XDocument.Load(path);
        int i;
        XElement root = doc.Element("players");
        i = int.Parse(root.Attribute("num")?.Value);
        bool flag = true;
        for (int j = 1; j <= i; j++)
            foreach (XElement xe in root.Elements("player").ToList())
            {
                for (int k = 0; k < data.Count; k++)
                    if (CompareNickname(data[k][0], xe.Element("nickname")?.Value))
                    {
                        flag = false;
                        data[k][0] = xe.Element("nickname")?.Value;
                        data[k][1] = xe.Element("score")?.Value;
                        data[k][2] = xe.Element("time")?.Value;
                    }
                if (flag)
                {
                    data.Add(new string[3]);
                    data[data.Count - 1][0] = xe.Element("nickname")?.Value;
                    data[data.Count - 1][1] = xe.Element("score")?.Value;
                    data[data.Count - 1][2] = xe.Element("time")?.Value;
                }

                flag = true;
            }
    }

    public void XMLLoadingToName(string nickname, out bool isGameOver, out int score, out float time, out int[] cells)
    {
        isGameOver = false;
        score = 0;
        time = 0;
        XDocument doc = new XDocument();
        cells = new int[16];
        doc = XDocument.Load(path);
        int i;
        XElement root = doc.Element("players");
        i = int.Parse(root.Attribute("num")?.Value);
        for (int j = 1; j <= i; j++)
            foreach (XElement xe in root.Elements("player").ToList())
                if (CompareNickname(nickname, xe.Element("nickname")?.Value))
                {
                    if (xe.Element("isGameOver")?.Value.Equals("true") == true)
                    {
                        isGameOver = true;
                        score = 0;
                        time = 0;
                        for (int k = 0; k < 16; k++)
                            cells[i] = 0;
                    }
                    Int32.TryParse(xe.Element("score")?.Value, out score);

                    Int32.TryParse(xe.Element("time")?.Value, out int intTime);

                    time = intTime;

                    for (int k = 0; k < 16; k++)
                        Int32.TryParse(xe.Element("cell" + k)?.Value, out cells[k]);
                }
    }

    public bool XMLSearchToName(string nickname)
    {
        XDocument doc = new XDocument();
        doc = XDocument.Load(path);
        int i;
        XElement root = doc.Element("players");
        i = int.Parse(root.Attribute("num")?.Value);
        for (int j = 1; j <= i; j++)
            foreach (XElement xe in root.Elements("player").ToList())
                if (CompareNickname(nickname, xe.Element("nickname")?.Value) && !CompareGameOver(xe.Element("isGameOver")?.Value))
                    return true;

        return false;
    }

    private bool CompareGameOver(string n)
    {
        int count = 0;
        for (int i = 0; i < n.Length; i++)
        {
            if (n[i] == 't' || n[i] == 'r' || n[i] == 'u' || n[i] == 'e')
                count++;
        }

        return count == 4;
    }
    private bool CompareNickname(string n1, string n2)
    {
        int countUseless1 = 0, countUseless2 = 0;
        string f = "", s = "";

        for (int k = 0; k < n1.Length; k++)
        {
            char temp = n1[k];
            if (temp >= 'a' && temp <= 'z' || temp >= 'A' && temp <= 'Z' ||
                temp >= 'à' && temp <= 'ÿ' || temp >= 'À' && temp <= 'ß' ||
                temp <= '9' && temp >= '0' || temp == ' ')
            {
                countUseless1++;
                f += temp;
            }
        }

        for (int k = 0; k < n2.Length; k++)
        {
            char temp = n2[k];
            if (temp >= 'a' && temp <= 'z' || temp >= 'A' && temp <= 'Z' ||
                temp >= 'à' && temp <= 'ÿ' || temp >= 'À' && temp <= 'ß' ||
                temp <= '9' && temp >= '0' || temp == ' ')
            {
                countUseless2++;
                s += temp;
            }
        }

        return countUseless1 == countUseless2 && f == s;
    }
}
