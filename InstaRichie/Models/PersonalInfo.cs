using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
namespace StartFinance.Models
{
    class PersonalInfo
    {
        [PrimaryKey]
        public int PersonalID { get; set; }

        [Unique]
        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string DOB { get; set; }

        [NotNull]
        public string Gender { get; set; }

        [NotNull, Unique]
        public string Email { get; set; }

        [NotNull]
        public string phoneNumber { get; set; }
    }
}
