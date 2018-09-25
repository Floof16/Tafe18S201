using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace StartFinance.Models
{
    public class AppointmentInfo
    {

        [PrimaryKey]
        public int AppointmentID { get; set; }


        public string EventName { get; set; }


        public string Location { get; set; }


        public string EventDate { get; set; }


        public TimeSpan StartTime { get; set; }


        public TimeSpan EndTime { get; set; }


    }

}

