using bizpay_api.Shared;
using System.ComponentModel.DataAnnotations;

namespace bizpay_api.Repository
{
    public class Permition
    {

        [Key]
        public EmployeePermitions Id { get; set; }

        public string Name { get; set; }

    }
}
