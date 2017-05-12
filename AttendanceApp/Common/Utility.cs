using AttendanceApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AttendanceApp.Common
{
    public class Utility
    {
        public static void WriteLog(List<Tracking> trackings, string logPath)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(logPath, true);
                foreach (var item in trackings)
                {
                    var message = item.Date + "," + item.Time + "," + item.PersonID + "," + item.Code;
                    sw.WriteLine(message);
                }
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Tracking> ReadXMLFile(string path)
        {
            List<Tracking> trackings = new List<Tracking>();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                XDocument document = XDocument.Load(stream);
                var elements = document.Root.Elements();
                var strRecordID = document.Root.Attribute("id").Value;
                int recordID = 0;
                bool isRecordID = false;
                if (!string.IsNullOrEmpty(strRecordID))
                {
                    isRecordID = int.TryParse(strRecordID, out recordID);
                }
                foreach (var item in elements)
                {
                    string type = item.Attribute("type").Value;
                    short code = 0;
                    if (!string.IsNullOrEmpty(type))
                    {
                        if (type.ToLower() == "out")
                        {
                            code = 2;
                        }
                        else if (type.ToLower() == "in")
                        {
                            code = 1;
                        }
                    }
                    DateTime date = DateTime.MinValue;
                    bool isDate = false;
                    string time = item.Value;
                    string strDate = item.Attribute("date").Value;
                    string strPersonID = item.Attribute("cardId").Value;
                    int personID = 0;
                    bool isPersonID = false;
                    if (!string.IsNullOrEmpty(strDate))
                    {
                        isDate = DateTime.TryParse(strDate, out date);
                    }
                    if (!string.IsNullOrEmpty(strPersonID))
                    {
                        isPersonID = int.TryParse(item.Attribute("cardId").Value, out personID);
                    }
                    if (isPersonID && isDate && code > 0 && !string.IsNullOrEmpty(time))
                    {
                        Tracking tracking = new Tracking()
                        {
                            PersonID = personID,
                            Date = date.ToString("dd.MM.yyyy"),
                            Code = code,
                            Time = time
                        };
                        trackings.Add(tracking);
                    }
                }
            }

            return trackings;
        }
    }
}
