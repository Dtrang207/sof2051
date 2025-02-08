using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_NhanVien
    {
        DAL_NhanVien dalNhanVien = new DAL_NhanVien();
        public bool NVDangNhap(DTO_NhanVien nv)
        {
            return dalNhanVien.NVDangNhap(nv);
        }
        public bool CapNhatMatKhau(string email, string mkcu, string mkmoi)
        {
            return dalNhanVien.CapNhatMatKhau(email, mkcu, mkmoi);
        }
        public bool QuenMK(string email)
        {
            return dalNhanVien.QuenMK(email);
        }
        public DataTable VaiTroNV(string email)
        {
            return dalNhanVien.VaiTroNV(email);
        }
        public bool TaoMatKhau(string email, string mkmoi)
        {
            return dalNhanVien.TaoMatKhau(email, mkmoi);
        }
    }

    //internal class DAL_NhanVien : DTO_NhanVien
    //{
    //}
}
