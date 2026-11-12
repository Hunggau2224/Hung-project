
using Microsoft.AspNetCore.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers
{
    public class DemoController : Controller
    {
        // Trang chính của Demo

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.TieuDe = "Thực hành ViewBag";
            ViewBag.ThongBao = "Dữ liệu này được truyền từ Controller sang View.";
            ViewBag.HoTen = "Nguyễn Văn Hùng";

            return View();
        }


        // Ví dụ truyền dữ liệu từ Controller sang View qua Model
        [HttpGet]
        public IActionResult Hello()
        {
            var model = new ThongTinViewModel
            {
                HoTen = "Nguyễn Văn Hùng",
                Tuoi = 22,
                ThongBao = "Dữ liệu được truyền từ Controller sang View."
            };

            return View(model);
        }

        // Hiển thị form nhập thông tin
        [HttpGet]
        public IActionResult NhapThongTin()
        {
            return View(new ThongTinViewModel());
        }

        // Nhận dữ liệu từ form và trả kết quả về View
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NhapThongTin(ThongTinViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.ThongBao =
                $"Xin chào {model.HoTen}, bạn {model.Tuoi} tuổi!";

            return View(model);
        }

        // Trang Privacy

        [HttpGet]
        public IActionResult Privacy()
        {
            ViewData["TieuDe"] = "Thực hành ViewData";
            ViewData["ThongBao"] = "Dữ liệu được truyền bằng ViewData.";
            ViewData["HoTen"] = "Nguyễn Văn Hùng";

            return View();
        }


        [HttpGet]
        public IActionResult ThucHanhTempData()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GuiThongBao()
        {
            TempData["ThongBao"] = "Bạn đã gửi thông tin thành công!";

            return RedirectToAction("ThucHanhTempData");
        }


    }
}
