using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Todo.Models
{
    [Index(nameof(Email), Name = "UQ_EmailUser", IsUnique = true)]
    [Index(nameof(Email), Name = "IX_EmailUser")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo é obrigátorio")]
        [MinLength(3, ErrorMessage = "O campo deve ter entre 3 e 60 caracteres")]
        [MaxLength(60, ErrorMessage = "O campo deve ter entre 3 e 60 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo é obrigátorio")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo é obrigátorio")]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "O campo é obrigátorio")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "O campo é obrigátorio")]
        public string Roles { get; set; }
    }
}