﻿using System;
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
        public string PersonalID { get; set; }

        [NotNull]
        public string FirstName { get; set; }

        [NotNull]
        public string LastName { get; set; }

        [NotNull]
        public string DOB { get; set; }

        [NotNull]
        public string Gender { get; set; }

        [NotNull]
        public string Email { get; set; }

        [NotNull]
        public string phoneNumber { get; set; }
    }
}
