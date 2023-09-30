using bizpay_api.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bizpay_api.Models
{
    public class PayslipDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A data de emissão do holerite é obrigatória!")]
        public DateTime DateOfIssue { get; set; }

        [Required(ErrorMessage = "O valor do salário bruto é obrigatório!")]
        public decimal GrossSalary { get; set; }

        public decimal Discounts { get; set; } = 0;

        public decimal Bonus { get; set; } = 0;

        public string EmployeeCpf { get; set; }

    }
}
