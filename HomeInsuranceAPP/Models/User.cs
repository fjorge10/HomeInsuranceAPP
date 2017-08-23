using HomeInsuranceAPP.Interfaces;
using HomeInsuranceAPP.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace HomeInsuranceAPP.Models
{
    public class User
    {
        [Display(Name = "ID", ResourceType = typeof(Resources))]
        public int UserId { get; set; }

        [Required]
        [Display(Name ="FirstName",ResourceType = typeof(Resources))]
        public string FirstName { get; set; }

        [Required]
        [Display(Name ="LastName", ResourceType = typeof(Resources))]
        public string LastName { get; set; }

        [Required]
        [Display(Name ="Email", ResourceType = typeof(Resources))]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessageResourceName = "InvalidFieldErrorMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Email { get; set; }

        [Required]
        [Display(Name ="NIF", ResourceType = typeof(Resources))]
        public int NIF { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public Roles Role { get; set; }

        public List<PolicyModel> Policies { get; set; }
    }
}