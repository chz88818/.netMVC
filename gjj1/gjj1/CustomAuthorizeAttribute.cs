using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gjj1.Models;

namespace gjj1
{
    //用于实现权限访问控制
    public class CustomAuthorizeAttribute:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["Name"]!=null && filterContext.HttpContext.Session["password"]!=null) {
                Database123Entities db = new Database123Entities();//新建对象
                List<Models.Login> a = (from d in db.Logins where d.Id != null select d).ToList();//用户列表
                List<Models.group> b = (from d in db.groups where d.gid != null select d).ToList();//用户组表
                List<Models.function> c = (from d in db.functions where d.fId != null select d).ToList();//权限表
                string username = (string)filterContext.HttpContext.Session["Name"];//获取用户名
                var user = a.FirstOrDefault((p) => p.Name == username);//查找用户列表中的匹配项
                var groupnumber = user.gid;//查找用户所属组的id
                var group = b.FirstOrDefault((p) => p.gid == groupnumber);//查找所属组的匹配项
                var function = group.fid;
                if (function.Contains("1.2.3.4"))
                {
                    filterContext.HttpContext.Session["permission"] = "管理员权限";
                    return;
                }
                else if (function.Contains("1.2.3"))
                {
                    filterContext.HttpContext.Session["permission"] = "高级权限";
                    return;
                }
                else if (function.Contains("1"))
                {
                    filterContext.HttpContext.Session["permission"] = "普通权限";
                    return;
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
            }
        }
    }
}