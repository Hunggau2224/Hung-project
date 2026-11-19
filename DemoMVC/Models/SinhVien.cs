using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class SinhVien
    {
        public int Id { get; set; }

        [Display(Name = "Mã sinh viên")]
        public string MaSinhVien { get; set; } = "";

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự")]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; } = "";

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        [Display(Name = "Email")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Lớp không được để trống")]
        [StringLength(50, ErrorMessage = "Tên lớp không được vượt quá 50 ký tự")]
        [Display(Name = "Lớp")]
        public string Lop { get; set; } = "";

        [Display(Name = "Khoa")]
        public int KhoaId { get; set; }

        public Khoa? Khoa { get; set; }
    }
}