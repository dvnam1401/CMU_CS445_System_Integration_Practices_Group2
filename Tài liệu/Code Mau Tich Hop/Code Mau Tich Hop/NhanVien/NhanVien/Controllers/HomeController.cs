using MySql.Data.MySqlClient;
using NhanVien.ConnectMySQL;
using NhanVien.Models.EF;
using NhanVien.Models.MySQL;
using NhanVien.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NhanVien.Controllers
{
    public class HomeController : Controller
    {
        private LuongNhanVienDbContext db = new LuongNhanVienDbContext();
        private MySqlConnection conn = DBUtils.GetDBConnection();

        public ActionResult Index()
        {
            conn.Open();

            var Luongs = db.LUONGs.ToList();
            var nhanviens = getListNhanVien();
            var phongbans = getListPhongban();

            var LuongNhanViens = Luongs.Join(nhanviens, l => l.MANHANVIEN, n => n.MANHANVIEN, (l, n) => new { l, n })
                .Join(phongbans, n => n.n.MAPHONGBAN, p => p.MAPHONGBAN, (n, p) => new { n, p })
                .Select(x => new BangLuongNhanVienViewModel
                {
                    MANHANVIEN = x.n.n.MANHANVIEN,
                    HO = x.n.n.HO,
                    TEN = x.n.n.TEN,
                    NGAYSINH = Convert.ToDateTime(x.n.n.NGAYSINH),
                    GIOITINH = x.n.n.GIOITINH,
                    TENPHONGBAN = x.p.TENPHONGBAN,
                    HESOLUONG = x.n.l.HESOLUONG,
                    LUONGCOBAN = x.n.l.LUONGCOBAN,
                    SONGAYCONG = x.n.l.SONGAYCONG,
                    LUONGTHUCLINH = x.n.l.LUONGTHUCLINH
                }).ToList();
                

            conn.Close();
            return View(LuongNhanViens);
        }

        private List<nhanvien> getListNhanVien()
        {
            string sql = "Select MANHANVIEN, HO, TEN, NGAYSINH, GIOITINH, DIACHI, MAPHONGBAN from nhanvien";

            // Tạo một đối tượng Command.
            MySqlCommand cmd = new MySqlCommand();

            // Liên hợp Command với Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;

            List<nhanvien> lst = new List<nhanvien>();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int MANHANVIEN = Convert.ToInt32(reader.GetValue(0));
                        string HO = reader.GetString(1);
                        string TEN = reader.GetString(2);
                        DateTime NGAYSINH = Convert.ToDateTime(reader.GetValue(3));
                        int GIOITINH = Convert.ToInt32(reader.GetValue(4));
                        string DIACHI = reader.GetString(5);
                        //int MAPHONGBANIndex = reader.GetOrdinal("MAPHONGBAN");                        
                        int MAPHONGBAN = Convert.ToInt32(reader.GetString(6));
                        lst.Add(new nhanvien() {
                            MANHANVIEN = MANHANVIEN, HO = HO, TEN = TEN, NGAYSINH = NGAYSINH, GIOITINH = GIOITINH,
                            DIACHI = DIACHI, MAPHONGBAN = MAPHONGBAN });
                    }
                }
            }
            return lst;
        }

        private List<phongban> getListPhongban()
        {
            string sql = "Select MAPHONGBAN, TENPHONGBAN from phongban";

            // Tạo một đối tượng Command.
            MySqlCommand cmd = new MySqlCommand();

            // Liên hợp Command với Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;

            List<phongban> lst = new List<phongban>();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int MAPHONGBAN = Convert.ToInt32(reader.GetValue(0));
                        string TENPHONGBAN = reader.GetString(1);
                        lst.Add(new phongban()
                        {
                            MAPHONGBAN = MAPHONGBAN,
                            TENPHONGBAN = TENPHONGBAN
                        });
                    }
                }
            }
            return lst;
        }
    }
}