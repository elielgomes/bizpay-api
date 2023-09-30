using System.ComponentModel.DataAnnotations;

namespace bizpay_api.Models
{
    public class DepartmentDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O Nome do departamento deve ter entre 3 e 50 caracteres")]
        public string Name { get; set; }
    }
}
