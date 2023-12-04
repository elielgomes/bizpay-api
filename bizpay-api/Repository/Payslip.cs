using bizpay_api.Models;
using bizpay_api.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Net;
using System.Xml.Linq;

namespace bizpay_api.Repository
{
    public class Payslip
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime DateOfIssue { get; set; }

        private DateTime? LastEdition { get; set; }

        public decimal GrossSalary { get; set; }

        public decimal NetSalary { get; set; }

        public decimal Discounts { get; set; }

        public decimal Bonus { get; set; }

        public decimal Inss { get; set; }

        public decimal Irrf { get; set; }

        [ForeignKey(nameof(Employee))]
        public string EmployeeCpf { get; set; }

        public Employee Employee { get; set; }


        public decimal IrrfCalculation(decimal value)
        {

            decimal aliquot;
            decimal deduction;

            if (value >= 0 && value <= 2112m)
            {
                aliquot = 0m;
                deduction = 0m;
            }
            else if (value >= 2112.01m && value <= 2826.65m)
            {
                aliquot = 7.5m;
                deduction = 158.40m;

            }
            else if (value >= 2826.66m && value <= 3751.05m)
            {
                aliquot = 15m;
                deduction = 370.40m;
            }
            else if (value >= 3751.06m && value <= 4664.68m)
            {
                aliquot = 22.5m;
                deduction = 651.73m;
            }
            else
            {
                aliquot = 27.5m;
                deduction = 884.96m;
            }

            var discountIRRF = (value * (aliquot / 100)) - (deduction);
            Irrf = discountIRRF;

            var salaryDeductedIRRF = value - discountIRRF;

            return salaryDeductedIRRF;
        }

        public decimal InssCalculation(decimal value)
        {
            decimal aliquot;

            if (value >= 0 && value <= 1320m)
            {
                aliquot = 7.5m;
            }
            else if (value >= 1320m && value <= 2571.29m)
            {
                aliquot = 9m;
            }
            else if (value >= 2571.30m && value <= 3856.94m)
            {
                aliquot = 12m;
            }
            else if (value >= 3856.95m && value <= 7507.49m)
            {
                aliquot = 14m;
            } else
            {
                aliquot = 14m;
            }
            var discountINSS = value * (aliquot / 100);
            Inss = discountINSS;

            var salaryDeductedINSS = value - discountINSS;

            return salaryDeductedINSS;
        }

        public void SalaryCalculation()
        {
            var baseSalary = GrossSalary + Bonus - Discounts;
            var salaryDeductedINSS = InssCalculation(baseSalary);
            var salaryDeductedIRRF = IrrfCalculation(salaryDeductedINSS);
            var netSalary = salaryDeductedIRRF;
            NetSalary = netSalary;
        }

        public PayslipDTO ToDTO()
        {
            var payslipDTO = new PayslipDTO()
            {
                Id = Id,
                DateOfIssue = DateOfIssue,
                GrossSalary = GrossSalary,
                Discounts = Discounts,
                Bonus = Bonus,
                EmployeeCpf = EmployeeCpf,
            };

            return payslipDTO;
        }

        public Payslip FromDTO(PayslipDTO payslipDTO)
        {
            Id = payslipDTO.Id;
            DateOfIssue = payslipDTO.DateOfIssue;
            GrossSalary = payslipDTO.GrossSalary;
            Discounts = payslipDTO.Discounts;
            Bonus = payslipDTO.Bonus;
            EmployeeCpf = payslipDTO.EmployeeCpf;
            
            return this;
        }
    }
}
