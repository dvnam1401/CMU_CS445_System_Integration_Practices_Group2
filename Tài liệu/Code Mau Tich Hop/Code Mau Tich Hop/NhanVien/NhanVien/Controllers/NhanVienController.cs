using MySql.Data.MySqlClient;
using NhanVien.ConnectMySQL;
using NhanVien.Models.EF;
using NhanVien.Models.MySQL;
using NhanVien.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NhanVien.Controllers
{
    public class NhanVienController : Controller
    {

        private LuongNhanVienDbContext db = new LuongNhanVienDbContext();
        private MySqlConnection conn = DBUtils.GetDBConnection();

        // GET: NhanVien
        public ActionResult Index()
        {
            conn.Open();
            var nhanviens = getListNhanVien();
            conn.Close();
            conn.Dispose();
            conn = null;
            return View(nhanviens);
        }
        public ActionResult ThemMoi()
        {
            conn.Open();
            ViewBag.phongbans = getListPhongban();
            conn.Close();
            conn.Dispose();
            conn = null;
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoi(nhanvien nv)
        {
            conn.Open();
            if (nv.HO != null && nv.TEN != null && nv.NGAYSINH != null && nv.DIACHI != null)
            {
                if (insertMySQL(nv) > 0)
                {
                    NHANVIEN nv2 = new NHANVIEN();
                    nv2.HO = nv.HO;
                    nv2.TEN = nv.TEN;
                    nv2.NGAYSINH = Convert.ToDateTime(nv.NGAYSINH);
                    db.NHANVIENs.Add(nv2);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                };
            }
            conn.Close();
            conn.Dispose();
            conn = null;
            return View(nv);
        }

        public ActionResult Sua(int id)
        {
            conn.Open();
            ViewBag.phongbans = getListPhongban();
            var nv = getNhanVien(id);
            conn.Close();
            conn.Dispose();
            conn = null;
            return View(nv);
        }
        [HttpPost]
        public ActionResult Sua(nhanvien nv)
        {
            conn.Open();
            if (nv.HO != null && nv.TEN != null && nv.NGAYSINH != null && nv.DIACHI != null)
            {
                if (updateMySQL(nv) > 0)
                {
                    var a = db.NHANVIENs.SingleOrDefault(x => x.MANHANVIEN == nv.MANHANVIEN);
                    a.HO = nv.HO;
                    a.TEN = nv.TEN;
                    a.NGAYSINH = Convert.ToDateTime(nv.NGAYSINH);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                };
            }
            conn.Close();
            conn.Dispose();
            conn = null;
            return View(nv);
        }

        public ActionResult Xoa(int id)
        {
            conn.Open();
            var nv = getNhanVien(id);
            conn.Close();
            return View(nv);
        }

        [HttpPost]
        public ActionResult Xoa(nhanvien nv)
        {
            conn.Open();
            if (deleteMySQL(nv.MANHANVIEN) > 0)
            {
                var a = db.NHANVIENs.SingleOrDefault(x => x.MANHANVIEN == nv.MANHANVIEN);
                db.NHANVIENs.Remove(a);
                db.SaveChanges();
                return RedirectToAction("Index");
            };
            conn.Close();
            conn.Dispose();
            conn = null;
            return View(nv);
        }


        private nhanvien getNhanVien(int id)
        {
            string sql = "Select * from nhanvien where MANHANVIEN = " + id;

            // Tạo một đối tượng Command.
            MySqlCommand cmd = new MySqlCommand();

            // Liên hợp Command với Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;

            var phongbans = getListPhongban();
            nhanvien nv = new nhanvien();

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

                        var pb = phongbans.SingleOrDefault(x => x.MAPHONGBAN == MAPHONGBAN);
                        if (pb != null)
                        {
                            nv = new nhanvien()
                            {
                                MANHANVIEN = MANHANVIEN,
                                HO = HO,
                                TEN = TEN,
                                NGAYSINH = NGAYSINH,
                                GIOITINH = GIOITINH,
                                DIACHI = DIACHI,
                                MAPHONGBAN = MAPHONGBAN,
                                phongban = pb

                            };
                        }
                        else
                        {
                            nv = new nhanvien()
                            {
                                MANHANVIEN = MANHANVIEN,
                                HO = HO,
                                TEN = TEN,
                                NGAYSINH = NGAYSINH,
                                GIOITINH = GIOITINH,
                                DIACHI = DIACHI,
                                MAPHONGBAN = MAPHONGBAN
                            };
                        }
                    }
                }
            }

            return nv;
        }

        private List<nhanvien> getListNhanVien()
        {
            string sql = "Select MANHANVIEN, HO, TEN, NGAYSINH, GIOITINH, DIACHI, MAPHONGBAN from nhanvien";

            // Tạo một đối tượng Command.
            MySqlCommand cmd = new MySqlCommand();

            // Liên hợp Command với Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;

            var phongbans = getListPhongban();
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

                        var pb = phongbans.SingleOrDefault(x => x.MAPHONGBAN == MAPHONGBAN);
                        if (pb != null)
                        {
                            lst.Add(new nhanvien()
                            {
                                MANHANVIEN = MANHANVIEN,
                                HO = HO,
                                TEN = TEN,
                                NGAYSINH = NGAYSINH,
                                GIOITINH = GIOITINH,
                                DIACHI = DIACHI,
                                MAPHONGBAN = MAPHONGBAN,
                                phongban = pb

                            });
                        }
                        else
                        {
                            lst.Add(new nhanvien()
                            {
                                MANHANVIEN = MANHANVIEN,
                                HO = HO,
                                TEN = TEN,
                                NGAYSINH = NGAYSINH,
                                GIOITINH = GIOITINH,
                                DIACHI = DIACHI,
                                MAPHONGBAN = MAPHONGBAN
                            });
                        }
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

        private int insertMySQL(nhanvien nv)
        {
            string sql = "Insert into `nhanvien` (`HO`,`TEN`,`NGAYSINH`,`GIOITINH`,`DIACHI`,`MAPHONGBAN`) "
                + " values ('" + nv.HO + "', '" + nv.TEN + "','" + nv.NGAYSINH.ToString("yyyy-MM-dd") + "', '" + nv.GIOITINH + "','" 
                + nv.DIACHI + "','" + nv.MAPHONGBAN + "')";

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            // Thực thi Command (Dùng cho delete, insert, update).
            int rowCount = cmd.ExecuteNonQuery();

            return rowCount;
        }

        private int updateMySQL(nhanvien nv)
        {
            string sql = "Update `nhanvien` set `HO`='" + nv.HO + "',`TEN`='" + nv.TEN + "',`NGAYSINH`='" + nv.NGAYSINH.ToString("yyyy-MM-dd")
                + "',`GIOITINH`='" + nv.GIOITINH + "',`DIACHI`='" + nv.DIACHI + "',`MAPHONGBAN`='" + nv.MAPHONGBAN
                + "' where `MANHANVIEN`='" + nv.MANHANVIEN + "'";
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = conn;

            cmd.CommandText = sql;

            // Thực thi Command (Dùng cho delete, insert, update).
            int rowCount = cmd.ExecuteNonQuery();

            return rowCount;
        }

        private int deleteMySQL(int id)
        {
            string sql = "Delete from `nhanvien` where `MANHANVIEN`='" + id + "'";

            // Tạo đối tượng Command.
            MySqlCommand cmd = new MySqlCommand();


            cmd.Connection = conn;

            cmd.CommandText = sql;

            // Thực thi Command (Dùng cho delete,insert, update).
            int rowCount = cmd.ExecuteNonQuery();
            return rowCount;
        }
    }
}