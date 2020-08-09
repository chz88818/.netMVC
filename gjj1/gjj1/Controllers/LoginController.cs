using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gjj1.Models;


namespace gjj1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult Ppp()
        {
            return RedirectToAction("Index","Home");

        }
        Database123Entities db = new Database123Entities();



        public JsonResult GetResult(string username,string password)
        {
            List<Models.Login> a = (from d in db.Logins where d.Id != null select d).ToList();
            var login1 = a.FirstOrDefault((p) => p.Name == username);

            //   var login2 = a.FirstOrDefault((p) => p.password == password);
            if (login1 == null) return null;
            if (login1.Name==username&login1.password==password)
            {
                HttpContext.Session["Name"] = username;
                HttpContext.Session["password"] = password;
                var res = new JsonResult
                {
                    Data = 1
                };
                return res;
            }
            else
            {
                return null;/*Json(new HttpNotFoundResult("optional description"));*/
            }
        }
        //[HttpGet]
        //public JsonResult GetResult()
        //{
        //    var login1 = a;

        //    //   var login2 = a.FirstOrDefault((p) => p.password == password);
        //    //  if (login1 == null) return null;
        //    // if (login1.Name == username & login1.password == password)

        //    var res = new JsonResult
        //    {
        //        Data = 1
        //    };
        //    return res;
            
           
        //}
        public ActionResult Loginout()
        {
            Session.Abandon();//清除全部Session
            return RedirectToAction("Index", "Login");
        }
        public ActionResult UserManage()
        {
            List<Models.Login> list = (from d in db.Logins where d.cz == false select d).ToList();
            //list.ForEach(i =>
            //{
            //    if (i.gid == 1)
            //    {
            //        i.fid = 2;
            //    }
            //    else if (i.fid == "1.2.3")
            //    {
            //        i.fid = "高级用户权限";
            //    }
            //    else if (i.fid == "1")
            //    {
            //        i.fid = "普通用户权限";
            //    }
            //});
            ViewData["DataList"] = list;
            return View();
        }
        //List<Loginp> a = new List<Loginp>()
        //{
        //    new Loginp(){Gid=1,Gname="程海洲",Fid="123456"},
        // };
        List<Loginp> A = new List<Loginp>();

        public ActionResult GroupManage()
        {
            List<Models.group> list = (from d in db.groups where d.gid!= null select d).ToList();
            //list.ForEach(i =>
            //{
            //    if(i.fid=="1.2.3.4")
            //    {
            //        i.fid = "管理员权限";
            //    }
            //    else if(i.fid == "1.2.3")
            //    {
            //        i.fid = "高级用户权限";
            //    }
            //    else if (i.fid == "1")
            //    {
            //        i.fid = "普通用户权限";
            //    }
            //});
            ViewData["DataList"] = list;
            return View();
        }
        public ActionResult Modifypermission()
        {
           
            return View();
        }
        public ActionResult ModifyGroup()
        {
            return View();
        }
        public ActionResult xiugaik(int ?getid,string getgname,string a1,string a2,string a3,string a4)
        {
            var a = getid;
            var b = getgname;
            var c = a1;
            var d = a2;
            var e = a3;
            var f = a4;
            return Content(a + "_" + b+a1+a2+a3+a4);
        }
        //public ActionResult Getc(string a1, string a2, string a3, string a4, string b1, string b2, string b3, string b4, string c1, string c2, string c3, string c4, string d1, string d2, string d3, string d4)//获取复选框值
        //{//判断修改先
        //    var a = a1;
        //    var a_2 = a2;
        //    Database123Entities entity = new Database123Entities();
        //    group groupget = entity.groups.Find(1); //获取原对象
        //    groupget.fid = "1.2.3.4"; //更新字段
        //    entity.SaveChanges(); //保存
        //    return Content(a1+"_"+a2);
        //   // return RedirectToAction("ModifyGroup","Login");
        //}
    }

}