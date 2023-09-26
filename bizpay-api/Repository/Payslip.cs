using System.ComponentModel.DataAnnotations.Schema;

namespace bizpay_api.Repository
{
    public class Payslip
    {

        public Guid Id { get; set; }

        public DateTime DateOfIssue { get; set; }

        public decimal GrossSalary { get; set; }

        public decimal NetSalary { get; set;}

        public decimal Discounts { get; set; }

        public decimal Bonus { get; set;}

        public decimal Inss { get; set; }

        public decimal Irrf { get; set; }

        [ForeignKey(nameof(Employee))]
        public string EmployeeCpf { get; set; }

        public Employee Employee { get; set; }
    }
}
