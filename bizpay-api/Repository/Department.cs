using bizpay_api.Models;
using System.ComponentModel.DataAnnotations;

namespace bizpay_api.Repository
{
    public class Department
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DepartmentDTO ToDTO()
        {
            var departmentDTO = new DepartmentDTO
            {
                Id = Id,
                Name = Name
            };

            return departmentDTO;
        }

        public Department FromDTO(DepartmentDTO departmentDTO)
        {
            Id = departmentDTO.Id;
            Name = departmentDTO.Name;

            return this;
        }

    }
}
