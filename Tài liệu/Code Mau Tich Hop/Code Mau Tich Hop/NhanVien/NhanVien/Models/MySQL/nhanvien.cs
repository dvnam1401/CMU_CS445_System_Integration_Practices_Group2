using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NhanVien.Models.MySQL
{
    public class nhanvien
    {
        [Key]
        public int MANHANVIEN { get; set; }
        
        public string HO { get; set; }
        
        public string TEN { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime NGAYSINH { get; set; }

        public int GIOITINH { set; get; }

        public string DIACHI { get; set; }

        public int MAPHONGBAN { get; set; }

        public virtual phongban phongban { get; set; }
    }
}