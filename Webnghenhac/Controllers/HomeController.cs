using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Newtonsoft.Json;


namespace WebApplication1.Controllers
{   [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index1()
        {   
            DBcontextDataContext context = new DBcontextDataContext();           
            var theloai = context.TheLoais.ToList();
            ViewBag.test = theloai;
            var nhac = context.Nhacs.ToList();
            ViewBag.jointable = nhac;
            return PartialView("Index1");
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            DBcontextDataContext context = new DBcontextDataContext();
            var theloai = context.TheLoais.ToList();
            ViewBag.test = theloai;
            var nhac = context.Nhacs.ToList();
            ViewBag.jointable = nhac;
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Search()
        {
            DBcontextDataContext context = new DBcontextDataContext();
            ViewBag.Chude = context.ChuDes.ToList();

            return PartialView("Search");
        }
        [Authorize]
        [HttpGet]
        public ActionResult Library()
        {
            DBcontextDataContext context = new DBcontextDataContext();
            account a = context.accounts.FirstOrDefault(x => x.Ten == User.Identity.Name);
            ViewBag.Thuvien = context.PlayLists.Where(x => x.Matk == a.MaTK).ToList().GroupBy(x => x.TenPL).Select(group => group.First());
          

            return PartialView("Library");
        }

      
        public ActionResult Themyeuthich(int id)
        {
            DBcontextDataContext context = new DBcontextDataContext();
            account a = context.accounts.FirstOrDefault(x => x.Ten == User.Identity.Name);
            PlayList p = new PlayList();
            p.Matk = a.MaTK;
            p.MaBH = id;
            p.TenPL = "Bài hát yêu thích";
            context.PlayLists.InsertOnSubmit(p);
            context.SubmitChanges();
            return RedirectToAction("Baihat", "Home", new { id = id });
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult TimKiem(string SearchString)
        {
            DBcontextDataContext context = new DBcontextDataContext();
            List<Nhac> a = context.Nhacs.ToList();
            var link = from l in a select l;
            if (!string.IsNullOrEmpty(SearchString))
            {
                link = link.Where(s => s.TenBH.Contains(SearchString));
            }
             return View(link);
        }
        [AllowAnonymous]
        public ActionResult Baihat(int id)
        {
            DBcontextDataContext context = new DBcontextDataContext();
            account a = context.accounts.FirstOrDefault(x => x.Ten == User.Identity.Name);
            ViewBag.playlists = context.PlayLists.Where(x => x.Matk == a.MaTK).ToList().GroupBy(x => x.TenPL).Select(group => group.First());
            Nhac n = context.Nhacs.FirstOrDefault(x => x.MaBH == id);
            ViewBag.baihat = n;
            var nhac = context.Nhacs.Where(x => x.MaCS == n.MaCS).ToList();
            ViewBag.list = nhac;
            return View();
        }
        [HttpGet]
        public ActionResult Baihatyeuthich()
        {
            DBcontextDataContext context = new DBcontextDataContext();
            account a = context.accounts.FirstOrDefault(x => x.Ten == User.Identity.Name );
            List<PlayList> PL = context.PlayLists.Where(x => x.Matk == a.MaTK &&  x.TenPL == "Bài hát yêu thích").ToList();
            ViewBag.playlist = PL;
            ViewBag.s = PL.Count();
            ViewBag.tk = a;
            return PartialView("Baihatyeuthich");
        }

        public ActionResult Playlist(string tenPL)
        {
            DBcontextDataContext context = new DBcontextDataContext();
            account a = context.accounts.FirstOrDefault(x => x.Ten == User.Identity.Name);
            List<PlayList> PL = context.PlayLists.Where(x => x.Matk == a.MaTK && x.TenPL == tenPL).ToList();
            ViewBag.playlist = PL;
            ViewBag.s = PL.Count()-1;
            ViewBag.tk = a;
            ViewBag.ten = tenPL;
            return View();
        }


        public ActionResult ThemPL()
        {
            return PartialView("ThemPL");
        }
    }
}

   
