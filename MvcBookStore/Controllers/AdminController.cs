using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBookStore.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace MvcBookStore.Controllers
{
    public class AdminController : Controller
    {
        //tạo đối tượng để quản lý data
        dbQLBansachDataContext db = new dbQLBansachDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        //lap9
        public ActionResult Sach(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.Saches.ToList().OrderBy(n => n.MaSach).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Tên đăng nhập không được để trống";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Mật khẩu không được để trống";
            }
            else
            {
                //gán giá trị và lấy sesstion
                Admin admin = db.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (admin != null)
                {
                    ViewBag.ThongBao = "Bạn đã đăng nhập thành công ";
                    Session["Taikhoan"] = admin;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Themmoisach()
        {
            ViewBag.MaCD = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChude");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoisach(Sach sach, HttpPostedFileBase fileupload)
        {
            ViewBag.MaCD = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChude");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            if (fileupload == null)
            {
                ViewBag.Thongbao = "Chọn hình ảnh vào";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //Lưu tên file
                    var fileName = Path.GetFileName(fileupload.FileName);
                    //Lưu dường dẫn
                    var path = Path.Combine(Server.MapPath("~/product_imgs"), fileName);
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    sach.AnhBia = fileName;
                    //Lưu file
                    db.Saches.InsertOnSubmit(sach);
                    db.SubmitChanges();
                }
                return RedirectToAction("Sach");
            }
        }
        //hiển thị sản phẩm
        public ActionResult Chitietsach(int id)
        {
            //lấy ra đối tượng sách theo mã
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == id);
            ViewBag.Masach = sach.MaSach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        //Xóa sản phẩm
        [HttpGet]
        public ActionResult Xoasach(int id)
        {
            //lấy ra đối tượng sách cần xóa theo mã
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == id);
            ViewBag.Masach = sach.MaSach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpPost, ActionName("Xoasach")]
        public ActionResult Xacnhanxoa(int id)
        {
            //lấy ra đối tượng sách cần xóa theo mã
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == id);
            ViewBag.Masach = sach.MaSach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Saches.DeleteOnSubmit(sach);
            db.SubmitChanges();
            return RedirectToAction("Sach");
        }
        //chỉnh sửa sản phẩm
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //-----
            Sach sachh = db.Saches.SingleOrDefault(n => n.MaSach == id);
            ViewBag.MaCD = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChude", sachh.MaCD);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sachh.MaNXB);
            //------
            var sach = db.Saches.Select(p => p).Where(p => p.MaSach == id).FirstOrDefault();
            return View(sach);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Sach editsach, HttpPostedFileBase fileUpload)
        {
            try
            {
                var sach = db.Saches.Select(p => p).Where(p => p.MaSach == editsach.MaSach).FirstOrDefault();
                sach.TenSach = editsach.TenSach;
                sach.GiaBan = editsach.GiaBan;
                sach.MoTa = editsach.MoTa;
                sach.AnhBia = editsach.AnhBia;
                sach.NgayCapNhat = editsach.NgayCapNhat;
                sach.SoLuongTon = editsach.SoLuongTon;
                sach.MaCD = editsach.MaCD;
                sach.MaNXB = editsach.MaNXB;
                db.SubmitChanges();
                return RedirectToAction("sach");
            }
            catch
            {
                return View();
            }
        }
    }
}