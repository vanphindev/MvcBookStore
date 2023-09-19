using MvcBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
namespace MvcBookStore.Controllers
{
    public class BookStoreController : Controller
    {
        dbQLBansachDataContext data = new dbQLBansachDataContext();
        private List<Sach> Laysachmoi(int count)
        {
            return data.Saches.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        // GET: BookStore
        public ActionResult Index(int? page)
        {
            //tạo biến quy định số  lượng sp trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNum = (page ?? 1);
            var sachmoi = Laysachmoi(6);
            return View(sachmoi.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Chude()
        {
            var chude = from cd in data.ChuDes select cd;
            return PartialView(chude);
        }
        public ActionResult Nhaxuatban()
        {
            var nhaxuatban = from cd in data.NhaXuatBans select cd;
            return PartialView(nhaxuatban);
        }
        public ActionResult SPTheoChude(int id)
        {
            var sach = from s in data.Saches where s.MaCD == id select s;
            return View(sach);
        }
        public ActionResult SPTheoNXB(int id)
        {
            var sach = from s in data.Saches where s.MaNXB == id select s;
            return View(sach);
        }
        public ActionResult Details(int id)
        {
            var sach = from s in data.Saches where s.MaSach == id select s;
            return View(sach.Single());
        }
    }
}