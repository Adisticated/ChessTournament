﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DotNetAppSqlDb.Models
{
    public class Todo
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; } 

        [Required]
        public string Email { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
    }
}