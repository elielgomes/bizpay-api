using System.ComponentModel.DataAnnotations;

namespace bizpay_api.Repository
{
    public class Department
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
