
using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class Khoa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên khoa không được để trống")]
        [StringLength(100, ErrorMessage = "Tên khoa không được vượt quá 100 ký tự")]
        [Display(Name = "Tên khoa")]
        public string TenKhoa { get; set; } = "";

        [StringLength(200, ErrorMessage = "Mô tả không được vượt quá 200 ký tự")]
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; } = "";

        public ICollection<SinhVien> SinhViens { get; set; } = new List<SinhVien>();
    }
}
