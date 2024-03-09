using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DashBoard.API.Models
{
    [Table("__efmigrationshistory")]
    public partial class Efmigrationshistory
    {
        [Key]
        [StringLength(150)]
        public string MigrationId { get; set; }
        [Required]
        [StringLength(32)]
        public string ProductVersion { get; set; }
    }
}
