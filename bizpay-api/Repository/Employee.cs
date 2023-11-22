using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bizpay_api.Models;
using bizpay_api.Shared;
using Microsoft.AspNetCore.Identity;

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

        public string? PhoneNumber { get; set; }

        public string CellNumber { get; set; }

        public string? Address { get; set; }

        public HumanSexCodes Sex { get; set; }

        public string Password { get; set; }

        public DateTime AdmissionDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        public string Rg {get; set;}

        public string Nationality { get; set;}

        public MaritalStatus MaritalStatus { get; set; }

        public int NumberOfChildren { get; set; }

        public string? BankName { get; set; }

        public string? AccountNumber { get; set; }

        public string? AgencyNumber { get; set; }

        public string? PixKey { get; set; }

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
                AdmissionDate = AdmissionDate,
                TerminationDate = TerminationDate,
                Password = Password,
                MaritalStatus = MaritalStatus,
                Nationality = Nationality,
                NumberOfChildren = NumberOfChildren,
                Rg = Rg,
                PermitionId = PermitionId,
                RoleId = RoleId,
                BankName = BankName,
                AccountNumber = AccountNumber,
                AgencyNumber = AgencyNumber,
                PixKey = PixKey,
                Status = Status,
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
                AdmissionDate = employeeDTO.AdmissionDate;
                TerminationDate = employeeDTO.TerminationDate;
                Password = employeeDTO.Password;
                MaritalStatus = employeeDTO.MaritalStatus;
                Nationality = employeeDTO.Nationality;
                NumberOfChildren = employeeDTO.NumberOfChildren;
                Rg = employeeDTO.Rg;
                PermitionId = employeeDTO.PermitionId;
                RoleId = employeeDTO.RoleId;
                BankName = employeeDTO.BankName;
                AccountNumber = employeeDTO.AccountNumber;
                AgencyNumber = employeeDTO.AgencyNumber;
                PixKey = employeeDTO.PixKey;
                Status = employeeDTO.Status;

            return this;
        }

    }
}
