using bizpay_api.Repository;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bizpay_api.Models
{
    public class RoleDTO : IValidatableObject
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O Nome do cargo deve ter entre 3 e 50 caracteres")]
        public string Name { get; set; }

        public decimal WeeklyWorkload { get; set; }

        public Guid DepartamentId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> result = new List<ValidationResult>();
            
            if (WeeklyWorkload <= 0 && WeeklyWorkload > 44 )
            {
                result.Add(new ValidationResult(errorMessage: "A carga horária semanal deve ser maior que 0 e menor que 44 horas!", memberNames: new[] { nameof(WeeklyWorkload) }));

            }

            return result;
        }
    }
}
