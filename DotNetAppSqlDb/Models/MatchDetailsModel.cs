using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetAppSqlDb.Models
{
    public class MatchDetailsModel
    {
        public string PlayerOneName { get; set; }
        public string PlayerTwoName { get; set; }
        public string WinnerName { get; set; }
        public DateTime entryDate { get; set; }
    }
}