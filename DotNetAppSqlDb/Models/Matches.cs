using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetAppSqlDb.Models
{
    public class Matches
    {
        [Key, Column(Order =0)]
        [Required]
        public int Player1Id { get; set; }
        [Key, Column(Order = 1)]
        [Required]
        public int Player2Id { get; set; }
        [Required]
        public int WinnerId { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
    }
}