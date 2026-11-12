using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class NhanVien
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự")]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; } = "";

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        [Display(Name = "Email")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Chức vụ không được để trống")]
        [StringLength(100, ErrorMessage = "Chức vụ không được vượt quá 100 ký tự")]
        [Display(Name = "Chức vụ")]
        public string ChucVu { get; set; } = "";

        [Required(ErrorMessage = "Phòng ban không được để trống")]
        [StringLength(100, ErrorMessage = "Phòng ban không được vượt quá 100 ký tự")]
        [Display(Name = "Phòng ban")]
        public string PhongBan { get; set; } = "";
    }
}