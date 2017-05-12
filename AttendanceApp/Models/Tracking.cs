using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceApp.Models
{
    public class Tracking
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int PersonID { get; set; }
        public short Code { get; set; }
    }
}
