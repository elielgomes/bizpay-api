using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bizpay_api.Repository
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string WeeklyWorkload { get; set; }

        [ForeignKey(nameof(Department))]
        public Guid DepartamentId { get; set; }

        public Department Department { get; set; }
    }
}
