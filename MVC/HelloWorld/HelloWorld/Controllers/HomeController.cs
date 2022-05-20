using HelloWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorld.Controllers
{
    // đều phải kế thừa từ class Controller
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            // trả về kết quả cho View
            // Khi Go to View nó sẽ chuyển đến Index.chstml
            // Có thể tạo mới đối tượng thông qua ViewBag sau đó @tên biến ở bên View là được
            // Cách 2 là tạo mới 1 model 
            var message = new MessageModel();
            message.Welcome = "Xin chào Nguyễn Văn Nam";
            return View(message);
        }
    }
}