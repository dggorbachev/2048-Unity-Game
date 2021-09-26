using System;
using System.Xml.Linq;

namespace Assets.Data
{
    public class XMLSaving
    {
        public XMLSaving(string nickname, bool isGameOver, int score, float time, int[] cells)
        {
            XDocument doc = new XDocument();
            doc = XDocument.Load(Environment.CurrentDirectory + @"\Data\saves.xml");
            XElement root = doc.Element("players");
            int i = int.Parse(root.Attribute("num")?.Value);

            root.Attribute("num").Value = (++i).ToString();
            XElement xe = new XElement("player", new XElement("nickname", nickname),
                new XElement("isGameOver", isGameOver), new XElement("score", score), new XElement("time", (int)time),
                new XElement("cell0", cells[0]), new XElement("cell1", cells[1]), new XElement("cell2", cells[2]),
                new XElement("cell3", cells[3]), new XElement("cell4", cells[4]), new XElement("cell5", cells[5]),
                new XElement("cell6", cells[6]), new XElement("cell7", cells[7]), new XElement("cell8", cells[8]),
                new XElement("cell9", cells[9]), new XElement("cell10", cells[10]), new XElement("cell11", cells[11]),
                new XElement("cell12", cells[12]), new XElement("cell13", cells[13]), new XElement("cell14", cells[14]),
                new XElement("cell15", cells[15]));
            root.Add(xe);
            doc.Save(Environment.CurrentDirectory + @"\Data\saves.xml");
        }
    }
}
