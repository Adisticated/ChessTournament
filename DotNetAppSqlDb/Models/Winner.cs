using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DotNetAppSqlDb.Models
{
    public class Winner
    {
        public string Name { get; set; }
        public int ID { get; set; }
        [DisplayName("Wins")]
        public int Count { get; set; }

    }
}