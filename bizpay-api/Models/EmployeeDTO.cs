using System.ComponentModel.DataAnnotations;
using bizpay_api.Services;
using bizpay_api.Shared;

namespace bizpay_api.Models
{
    public class EmployeeDTO : IValidatableObject
    {
        private string _rg;
        private string _cpf;
        private string _phoneNumber;
        private string _cellNumber;

        public string Cpf {
            get { return _cpf; }
            set { _cpf = Format.FormatCPF(value); }
        }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo Nome deve ter entre 3 e 100 caracteres")]
        public string Name { get; set; }

        public DateTime? TerminationDate { get; set; }

        private Status Status => !TerminationDate.HasValue ? Status.Active : Status.Inactive;

        [Required(ErrorMessage = "O campo Data de nascimento é obrigatório")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório")]
        public string Email { get; set; }

        [StringLength(15, MinimumLength = 10, ErrorMessage = "O campo Telefone deve ter entre 10 e 11 caracteres")]
        public string PhoneNumber {
            get { return _phoneNumber; }
            set { _phoneNumber = Format.FormatPhoneNumber(value); }
        }

        [Required(ErrorMessage = "O campo Celular é obrigatório")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "O campo Celular deve ter entre 10 e 11 caracteres")]
        public string CellNumber {
            get { return _cellNumber; }
            set { _cellNumber = Format.FormatCellNumber(value); }
        }

        [Required(ErrorMessage = "O campo Endereço é obrigatório")]
        public string Address { get; set; }

        public HumanSexCodes Sex { get; set; } = HumanSexCodes.NotKnown;

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "O campo Senha deve ter entre 8 e 20 caracteres")]
        public string Password { get; set; }

        public decimal HourlyPayment { get; set; }

        public DateTime AdmissionDate { get; set; } = GetBrasiliaTime.Time();

        public string Rg {
            get { return _rg; }
            set { _rg = Format.FormatRG(value); }
        }

        public string Nationality { get; set; } = "Brasileiro";

        public string? Religion { get; set; }

        public MaritalStatus MaritalStatus { get; set; }

        public int NumberOfChildren { get; set; } = 0;

        public string? PartnerName { get; set; }

        public string? BankName { get; set; }

        public string? AccountNumber { get; set; }

        public string? AgencyNumber { get; set; }

        public string? PixKey { get; set; }

        public EmployeePermitions PermitionId { get; set; }

        public Guid RoleId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<HumanSexCodes> Genres = Enum.GetValues<HumanSexCodes>().ToList();
            List<Status> EmployeeStatus = Enum.GetValues<Status>().ToList();
            List<MaritalStatus> EmployeeMaritalStatus = Enum.GetValues<MaritalStatus>().ToList();

            List<ValidationResult> result = new List<ValidationResult>();

            if (!Genres.Contains(Sex))
            {
                result.Add(new ValidationResult(errorMessage: "O sexo informado é inválido!", memberNames: new [] { nameof(Sex) }));
            }

            if (HourlyPayment <= 0)
            {
                result.Add(new ValidationResult(errorMessage: "O valor do pagamento hora deve ser maior que 0!", memberNames: new[] { nameof(HourlyPayment) }));
            }

            if (!EmployeeStatus.Contains(Status))
            {
                result.Add(new ValidationResult(errorMessage: "O status do funcionário é inválido!", memberNames: new[] { nameof(Status) }));
            }

            if (!EmployeeMaritalStatus.Contains(MaritalStatus))
            {
                result.Add(new ValidationResult(errorMessage: "O estado civil do funcionário é inválido!", memberNames: new[] { nameof(MaritalStatus) }));
            }

            return result;
        }
    }
}
