using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNetAppSqlDb.Models
{
    public class CreateMatchResultModel
    {
        [Required]
        public string PlayerOne { get; set; }
        [Required]
        public string PlayerOnePwd { get; set; }
        [Required]
        public int PlayerOneID { get; set; }
        [Required]
        public string PlayerTwo { get; set; }
        [Required]
        public string PlayerTwoPwd { get; set; }
        [Required]
        public int PlayerTwoID { get; set; }
        [Required]
        public string Winner { get; set; }
        [Required]
        public int WinnerID { get; set; }

        public List<Todo> playerSelectionList { get; set; }

    }
}