using bizpay_api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bizpay_api.Repository
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal WeeklyWorkload { get; set; }

        [ForeignKey(nameof(Department))]
        public Guid DepartamentId { get; set; }

        public Department Department { get; set; }

        public RoleDTO ToDTO()
        {
            var roleDTO = new RoleDTO
            {
                Id = Id,
                DepartamentId = DepartamentId,
                WeeklyWorkload = WeeklyWorkload,
                Name = Name
            };

            return roleDTO;
        }

        public Role FromDTO(RoleDTO roleDTO)
        {
            Id = roleDTO.Id;
            Name = roleDTO.Name;
            WeeklyWorkload = roleDTO.WeeklyWorkload;
            DepartamentId = roleDTO.DepartamentId;

            return this;
        }
    }
}
