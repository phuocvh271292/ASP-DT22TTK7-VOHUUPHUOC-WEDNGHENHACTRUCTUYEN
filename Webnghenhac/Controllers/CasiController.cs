﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{   [Authorize]
    public class CasiController : Controller
    {
       
        public ActionResult createcs()
        {
            DBcontextDataContext context = new DBcontextDataContext();
                  if (Request.Form.Count > 0)
            {
                CaSi ca = new CaSi();
                ca.TenCS = Request.Form["TenCS"];
                bool check = context.CaSis.Any(x => x.TenCS == ca.TenCS);
                if (check == false) { 
                context.CaSis.InsertOnSubmit(ca);
                context.SubmitChanges();
                ViewBag.sucess = "Thêm ca sĩ thành công !";
                return View();
                }
                else
                {
                    ViewBag.sucess = "Ca sĩ đã có trong danh sách";
                    return View();
                }
            }
                  
            return View();
        }
      


        public ActionResult Editcs(int id)
        {
            DBcontextDataContext context = new DBcontextDataContext();
            CaSi ca = context.CaSis.FirstOrDefault(x => x.MaCS == id);
            if (Request.Form.Count == 0)
            {
                return View(ca);
            }
            ca.MaCS = int.Parse(Request.Form["MaCS"]);
            ca.TenCS = Request.Form["TenCS"];
            context.SubmitChanges();
            ViewBag.ok = "Chỉnh sửa thành công";
            return View();
        }

        public ActionResult Deletecs(int id)
        {
            DBcontextDataContext context = new DBcontextDataContext();
            var data = context.CaSis.FirstOrDefault(x => x.MaCS == id);
            var nhacData = context.Nhacs.FirstOrDefault(x => x.MaCS == id);
            if (nhacData != null)
            {
                return JavaScript("alert('Ca sĩ đang sở hữu bài hát, không thể xóa.')");
            }
            if (data != null)
            {
                context.CaSis.DeleteOnSubmit(data);
                context.SubmitChanges();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
       


    }
}