namespace NhanVien.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LUONG")]
    public partial class LUONG
    {
        public int ID { get; set; }

        public int MANHANVIEN { get; set; }

        public double? HESOLUONG { get; set; }

        public decimal? LUONGCOBAN { get; set; }

        public int? SONGAYCONG { get; set; }

        public int? THANG { get; set; }

        public int? NAM { get; set; }

        public decimal? LUONGTHUCLINH { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
