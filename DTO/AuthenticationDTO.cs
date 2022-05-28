using System.ComponentModel.DataAnnotations;

namespace Todo.Dto
{
    public class AuthenticationDTO
    {
        [Required(ErrorMessage = "O campo é obrigátorio")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo é obrigátorio")]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }
    }
}