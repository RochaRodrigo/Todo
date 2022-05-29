using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
    public class Workspace
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigátorio")]
        [MinLength(3, ErrorMessage = "O campo deve ter entre 3 e 20 caracteres")]
        [MaxLength(60, ErrorMessage = "O campo deve ter entre 3 e 20 caracteres")]
        public string Title { get; set; }

        public virtual IEnumerable<TodoItem>? Todos { get; set; }

        [Required(ErrorMessage = "O campo é obrigátorio")]
        public int UserId { get; set; }

        public virtual User? User { get; set; }
    }
}