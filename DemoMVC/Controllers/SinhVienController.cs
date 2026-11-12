
using DemoMVC.Data;
using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Controllers
{
    public class SinhVienController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SinhVienController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Nạp danh sách khoa cho dropdown
        private void NapDanhSachKhoa(int? khoaId = null)
        {
            ViewBag.KhoaId = new SelectList(
                _context.Khoas.OrderBy(k => k.TenKhoa).ToList(),
                "Id",
                "TenKhoa",
                khoaId
            );
        }

        // 1. Danh sách, tìm kiếm, lọc và phân trang
        [HttpGet]
        public IActionResult Index(
            string? searchString,
            string? lop,
            int page = 1)
        {
            int pageSize = 5;

            var danhSachSinhVien = _context.SinhViens
                .Include(sv => sv.Khoa)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                danhSachSinhVien = danhSachSinhVien.Where(sv =>
                    sv.HoTen.Contains(searchString) ||
                    sv.Email.Contains(searchString));
            }

            if (!string.IsNullOrWhiteSpace(lop))
            {
                danhSachSinhVien = danhSachSinhVien.Where(sv =>
                    sv.Lop == lop);
            }

            ViewBag.DanhSachLop = _context.SinhViens
                .Select(sv => sv.Lop)
                .Distinct()
                .OrderBy(l => l)
                .ToList();

            ViewBag.SearchString = searchString;
            ViewBag.Lop = lop;

            // Phân trang trong C# để giữ cách xử lý hiện tại
            var tatCaSinhVien = danhSachSinhVien
                .OrderBy(sv => sv.Id)
                .ToList();

            int totalStudents = tatCaSinhVien.Count;
            int totalPages = (int)Math.Ceiling(
                (double)totalStudents / pageSize);

            if (page < 1)
                page = 1;

            if (totalPages > 0 && page > totalPages)
                page = totalPages;

            var danhSachPhanTrang = tatCaSinhVien
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalStudents = totalStudents;

            return View(danhSachPhanTrang);
        }

        // 2. Hiển thị form thêm sinh viên
        [HttpGet]
        public IActionResult Create()
        {
            NapDanhSachKhoa();
            return View();
        }

        // 3. Xử lý thêm sinh viên
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SinhVien sv)
        {
            if (ModelState.IsValid)
            {
                _context.SinhViens.Add(sv);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            NapDanhSachKhoa(sv.KhoaId);
            return View(sv);
        }

        // 4. Hiển thị form sửa sinh viên
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var sinhVien = _context.SinhViens.Find(id);

            if (sinhVien == null)
                return NotFound();

            NapDanhSachKhoa(sinhVien.KhoaId);
            return View(sinhVien);
        }

        // 5. Xử lý sửa sinh viên
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SinhVien sv)
        {
            if (ModelState.IsValid)
            {
                _context.SinhViens.Update(sv);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            NapDanhSachKhoa(sv.KhoaId);
            return View(sv);
        }

        // 6. Chi tiết sinh viên
        [HttpGet]
        public IActionResult Details(int id)
        {
            var sinhVien = _context.SinhViens
                .Include(sv => sv.Khoa)
                .FirstOrDefault(sv => sv.Id == id);

            if (sinhVien == null)
                return NotFound();

            return View(sinhVien);
        }

        // 7. Hiển thị trang xác nhận xóa
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var sinhVien = _context.SinhViens
                .Include(sv => sv.Khoa)
                .FirstOrDefault(sv => sv.Id == id);

            if (sinhVien == null)
                return NotFound();

            return View(sinhVien);
        }

        // 8. Xử lý xóa sinh viên
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var sinhVien = _context.SinhViens.Find(id);

            if (sinhVien == null)
                return NotFound();

            _context.SinhViens.Remove(sinhVien);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
