using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gjj1;//引入对应命名空间
using gjj1.Models;

namespace gjj1.Controllers
{
    public class EmailController : Controller
    {
        Database123Entities db = new Database123Entities();
        public ActionResult Index()
        {
            return View();
        }
        // GET: Email
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="title">邮件主题</param>
        /// <param name="email">要发送对象的邮箱</param>
        /// <param name="content">邮件内容</param>
        /// <returns></returns>
        public ActionResult SendMail(string title, string email, string content)
        {
            string senderServerIp = "smtp.qq.com";    //使用163代理邮箱服务器（也可是使用qq的代理邮箱服务器，但需要与具体邮箱对相应）
            string toMailAddress = email;              //要发送对象的邮箱
            string fromMailAddress = "";              //你的邮箱
            string subjectInfo = title;                  //邮件主题
            string bodyInfo = "<p>" + content + "</p>";//以Html格式发送的邮件
            string mailUsername = "";              //登录邮箱的用户名 同你的邮箱
            string mailPassword = ""; //对应的登录邮箱的第三方密码（你的邮箱不论是163还是qq邮箱，都需要自行开通stmp服务 教程自行百度）
            string mailPort = "25";                      //发送邮箱的端口号
                                                         //string attachPath = "E:\\123123.txt; E:\\haha.pdf";

            //创建发送邮箱的对象
            Email myEmail = new Email(senderServerIp, toMailAddress, fromMailAddress, subjectInfo, bodyInfo, mailUsername, mailPassword, mailPort, false, false);

            //添加附件
            //email.AddAttachments(attachPath);

            if (myEmail.Send())//之后修改
            {
                return Content("<script>alert('邮件已成功发送!')</script>");
            }
            else
            {
                return Content("<script>alert('邮件发送失败!')</script>");
            }

        }

        public ActionResult Test()
        {
            return View();
        }
        public JsonResult GetResult()
        {
            List<Models.Login> a = (from d in db.Logins where d.Id != null select d).ToList();
            var res = new JsonResult
            {
                Data = a
             };
              return res;

        }
    }
}
