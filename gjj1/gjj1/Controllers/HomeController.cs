using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gjj1.Models;
using System.Data.Entity;
namespace gjj1.Controllers
{
    
    public class HomeController : Controller
    {
        // GET: Home
        Database123Entities db = new Database123Entities();
        [CustomAuthorizeAttribute]
        public ActionResult Index()
        {
            List<Models.Login> list = (from d in db.Logins where d.cz == false select d).ToList();
            ViewData["DataList"] = list;
            return View();
        }
        
        public ActionResult Del(int id)//此id会根据路由配置
        {
            try
            {  //创建要删除的对象
                Login logindel = new Login() { Id = id };
                //将对象添加到EF管理容器
                db.Logins.Attach(logindel);
                //将对象的状态标识为删除状态
                db.Logins.Remove(logindel);
                //跟新到数据库
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            catch(Exception e)
            {
                return Content("删除失败");
            }
            
        }
        
        [HttpGet]
        public ActionResult Modify(int? id)
        {
            Login art = (from a in db.Logins where a.Id == id select a).FirstOrDefault();
            return View(art);
        }
        
        [HttpPost]
        public ActionResult Modify(Login model)
        {
            try
            {
                //将实体加入对象容器中并获取伪包装类对象
                DbEntityEntry<Login> entry = db.Entry<Login>(model);
                //将包装类对象的状态设置为 unchanged
                entry.State = System.Data.Entity.EntityState.Unchanged;
                //设置被改变的属性
                entry.Property(a => a.Id).IsModified = true;
                entry.Property(a => a.Name).IsModified = true;
                //提交
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                return Content("失败");
            }
           
        }
        
        [HttpGet]
        public ActionResult Add(int? id)
        {
           
            return View();

        }
        
        [HttpPost]
        public ActionResult Add([Bind(Include = "Id,Name,cz")] Login add)
        {
            if (ModelState.IsValid)
            {
                db.Logins.Add(add);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(add);
        }
        public ActionResult Welcome()
        {
            return View();
        }
        [CustomAuthorizeAttribute]
        public ActionResult TW()
        {
            Database123Entities db = new Database123Entities();//新建对象
            List<Models.Login> a = (from d in db.Logins where d.Id != null select d).ToList();//用户列表
            string username = (string)HttpContext.Session["Name"];//获取用户名
            var user = a.FirstOrDefault((p) => p.Name == username);//查找用户列表中的匹配项
            var work = user.workcellname;//查找用户所属的workcell
            if (work.Contains("workcell1"))//后续更改三个方法简单着来
            {
                HttpContext.Session["workcell"] = "workcell1";
            }
            else if(work == "workcell2")
            {
                HttpContext.Session["workcell"] = "workcell2";
            }
            else if (work == "workcell3")
            {
                HttpContext.Session["workcell"] = "workcell3";
            }
            return RedirectToAction("Index");
        }
    }
}