using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NhanVien.Models.MySQL
{
    public class phongban
    {
        [Key]
        public int MAPHONGBAN { get; set; }

        public string TENPHONGBAN { get; set; }
    }
}