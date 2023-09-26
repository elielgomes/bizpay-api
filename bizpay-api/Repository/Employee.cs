using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bizpay_api.Models;
using bizpay_api.Shared;

namespace bizpay_api.Repository
{
    public class Employee
    {
        [Key]
        public string Cpf { get; set; }

        public string Name { get; set; }

        public Status Status { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string CellNumber { get; set; }

        public string Address { get; set; }

        public HumanSexCodes Sex { get; set; }

        public string Password { get; set; }

        public decimal HourlyPayment { get; set; }

        public DateTime AdmissionDate { get; set; }

        public DateTime TerminationDate { get; set; }

        public string Rg {get; set;}

        public string Nationality { get; set;}

        public string Religion { get; set;}

        public MaritalStatus MaritalStatus { get; set; }

        public int NumberOfChildren { get; set; }

        public string PartnerName { get; set; }

        [ForeignKey(nameof(Permition))]
        public EmployeePermitions PermitionId { get; set; }

        public Permition Permition { get; set; }

        [ForeignKey(nameof(Role))]
        public Guid RoleId { get; set; }

        public Role Role { get; set; }

        public EmployeeDTO ToDTO()
        {
            var employeeDTO = new EmployeeDTO()
            {
                Cpf = Cpf,
                Name = Name,
                DateOfBirth = DateOfBirth,
                Email = Email,
                PhoneNumber = PhoneNumber,
                CellNumber = CellNumber,
                Address = Address,
                Sex = Sex,
                HourlyPayment = HourlyPayment,
                AdmissionDate = AdmissionDate,
                TerminationDate = TerminationDate,
                Password = Password,
                MaritalStatus = MaritalStatus,
                Nationality = Nationality,
                NumberOfChildren = NumberOfChildren,
                PartnerName = PartnerName,
                Religion = Religion,
                Rg = Rg,
                Status  = Status,
                PermitionId = PermitionId,
                RoleId = RoleId,
               
            };

            return employeeDTO;
        }

        public Employee FromDTO(EmployeeDTO employeeDTO)
        {
                Cpf = employeeDTO.Cpf;
                Name = employeeDTO.Name;
                DateOfBirth = employeeDTO.DateOfBirth;
                Email = employeeDTO.Email;
                PhoneNumber = employeeDTO.PhoneNumber;
                CellNumber = employeeDTO.CellNumber;
                Address = employeeDTO.Address;
                Sex = employeeDTO.Sex;
                HourlyPayment = employeeDTO.HourlyPayment;
                AdmissionDate = employeeDTO.AdmissionDate;
                TerminationDate = employeeDTO.TerminationDate;
                Password = employeeDTO.Password;
                MaritalStatus = employeeDTO.MaritalStatus;
                Nationality = employeeDTO.Nationality;
                NumberOfChildren = employeeDTO.NumberOfChildren;
                PartnerName = employeeDTO.PartnerName;
                Religion = employeeDTO.Religion;
                Rg = employeeDTO.Rg;
                Status = employeeDTO.Status;
                PermitionId = employeeDTO.PermitionId;
                RoleId = employeeDTO.RoleId;

            return this;
        }

    }
}
