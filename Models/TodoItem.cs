using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo é obrigátorio")]
        [MinLength(3, ErrorMessage = "O campo deve ter entre 3 e 30 caracteres")]
        [MaxLength(30, ErrorMessage = "O campo deve ter entre 3 e 30 caracteres")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O campo é obrigátorio")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "O campo é obrigátorio")]
        public int UserId { get; set; }

        public virtual User? User { get; set; }
    }
}