using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcBookStore.Models
{
    public class Cart
    {
        //tạo 1 đối tượng để quản lý data
        dbQLBansachDataContext data = new dbQLBansachDataContext();
        public int iMasach { set; get; }
        public string sTensach { set; get; }
        public string sAnhbia { set; get; }
        public Double dDongia { set; get; }
        public int iSoLuong { set; get; }
        public Double dThanhtien
        {
            get { return iSoLuong * dDongia; }
        }
        public Cart(int Masach)
        {
            iMasach = Masach;
            Sach sach = data.Saches.Single(n => n.MaSach == iMasach);
            sTensach = sach.TenSach;
            sAnhbia = sach.AnhBia;
            dDongia = double.Parse(sach.GiaBan.ToString());
            iSoLuong = 1;

        }
    }
}