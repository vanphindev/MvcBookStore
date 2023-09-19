using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBookStore.Models;

namespace MvcBookStore.Controllers
{
    public class CartController : Controller
    {
        //tạo đối tượng để quản lý data
        dbQLBansachDataContext data = new dbQLBansachDataContext();

        // GET: GioHang
        public List<Cart> LayGiohang()
        {
            List<Cart> lstGiohang = Session["Cart"] as List<Cart>;
            //nếu tồn tại,gán vào giỏ hàng
            if (lstGiohang == null)
            {
                lstGiohang = new List<Cart>();
                Session["Cart"] = lstGiohang;
            }
            return lstGiohang;
        }
        public ActionResult ThemGiohang(int iMasach, string strURL)
        {
            //lấy session
            List<Cart> lstGiohang = LayGiohang();
            //kiểm tra sách đã chọn vào giỏ hàng hay chưa
            Cart sanpham = lstGiohang.Find(n => n.iMasach == iMasach);
            if (sanpham == null)
            {
                sanpham = new Cart(iMasach);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
        }
        //tổng số lượng 
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Cart> lstGiohang = Session["Cart"] as List<Cart>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        //tính tổng tiền 
        private double TongTien()
        {
            double iTongTien = 0;
            List<Cart> lstGiohang = Session["Cart"] as List<Cart>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }
        //tạo giỏ hàng
        public ActionResult Giohang()
        {
            List<Cart> lstGiohang = LayGiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "BookStore");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }
        //Xóa giỏ hàng
        public ActionResult XoaGiohang(int iMaSP)
        {
            List<Cart> lstGiohang = LayGiohang();
            //kiểm tra sách đã chọn vào giỏ hàng hay chưa
            Cart sanpham = lstGiohang.SingleOrDefault(n => n.iMasach == iMaSP);
            if (lstGiohang != null)
            {
                lstGiohang.RemoveAll(n => n.iMasach == iMaSP);
                return RedirectToAction("GioHang");
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "BookStore");
            }
            return RedirectToAction("Cart");
        }
        // xóa tất cả giỏ hàng
        public ActionResult XoaTatcaGiohang()
        {
            List<Cart> lstGiohang = LayGiohang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "BookStore");
        }
        //cập nhật giỏ hàng
        public ActionResult CapnhatGiohang(int iMaSP, FormCollection f)
        {
            List<Cart> lstGiohang = LayGiohang();
            //kiểm tra sách đã chọn vào giỏ hàng hay chưa
            Cart sanpham = lstGiohang.SingleOrDefault(n => n.iMasach == iMaSP);
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        [HttpGet]
        public ActionResult Dathang()
        {
            //kiểm tra việc đăng nhập
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "BookStore");
            }
            //Lấy giỏ hàng
            List<Cart> lstGiohang = LayGiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        [HttpPost]
        public ActionResult Dathang(FormCollection collection)
        {
            //Thêm đơn hàng
            DonDatHang ddh = new DonDatHang();
            KhachHang kh = (KhachHang)Session["Taikhoan"];
            List<Cart> gh = LayGiohang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDH = DateTime.Now;
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
            ddh.NgayGiao = DateTime.Parse(ngaygiao);
            ddh.TinhTrangGiaoHang = false;
            ddh.DaThanhToan = false;
            data.DonDatHangs.InsertOnSubmit(ddh);
            data.SubmitChanges();
            //thêm chi tiết đơn hàng 
            foreach (var item in gh)
            {
                ChiTietDatHang ctdh = new ChiTietDatHang();
                ctdh.SoDH = ddh.SoDH;
                ctdh.MaSach = item.iMasach;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.DonGia = (decimal)item.dDongia;
                data.ChiTietDatHangs.InsertOnSubmit(ctdh);
            }
            data.SubmitChanges();
            Session["Cart"] = null;
            return RedirectToAction("Xacnhandonhang", "Cart");
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
    }
}