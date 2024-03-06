using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NhanVien.Models.ViewModel
{
    public class BangLuongNhanVienViewModel
    {
        public int MANHANVIEN { get; set; }
        
        public string HO { get; set; }

        public string TEN { get; set; }

        public DateTime? NGAYSINH { get; set; }

        public int GIOITINH { get; set; }

        public string TENPHONGBAN { get; set; }

        public double? HESOLUONG { get; set; }

        public decimal? LUONGCOBAN { get; set; }

        public int? SONGAYCONG { get; set; }

        public decimal? LUONGTHUCLINH { get; set; }
    }
}