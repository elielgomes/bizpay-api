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


        public RoleDTO ToDTO()
        {
            var roleDTO = new RoleDTO
            {
                Id = Id,
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

            return this;
        }
    }
}
