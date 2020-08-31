using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FacilityMVCnew.Models
{
    public class Facility
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name cannot be empty.")]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Phone is required.")]
        [Phone]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int FacilityStatusId { get; set; }
        public virtual FacilityStatus FacilityStatus { get; set; }

    }
}
