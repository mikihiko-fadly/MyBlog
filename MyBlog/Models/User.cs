using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required]
        public string? Username { get; set; }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username Minimal 3 karakter")]

        [Required]
        public string? Password { get; set; }
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password Minimal 3 karakter")]


        [Required]
        public string? Name { get; set; }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nama Minimal 3 karakter")]


        [Required]
        public string? Role { get; set; }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Role Minimal 3 karakter")]


        [Required]
        public string? Alamat { get; set; }
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Alamat Minimal 3 karakter")]


        [Required]
        [RegularExpression(@"/^[\w\-\.]+@([\w -]+\.)+[\w-]{2,}$/gm", ErrorMessage = "format tidak valid")]
        public string? Email { get; set; }
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Email Minimal 3 karakter")]


        [Required]
        [RegularExpression(@"(\+62 ((\d{3}([ -]\d{3,})([- ]\d{4,})?)|(\d+)))|(\(\d+\) \d+)|\d{3}( \d+)+|(\d+[ -]\d+)|\d+", ErrorMessage ="No Telepon Tidak valid")]
        public string? NoTelepon { get; set; }
        [StringLength(100, MinimumLength = 11, ErrorMessage = "Notelepon Minimal 3 karakter")]


        [Required]
        public string? TanggalLahir { get; set; }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Tanggal Lahir Minimal 3 karakter")]

        [NotMapped]
        public string FormatTanggalLahir { get; set; }



    }
}
