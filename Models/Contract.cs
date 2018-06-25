using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcContract.Models
{
    public class Contract
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string LastName { get; set; }

        public string JobTitle { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }


        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
    }

    public class ContractJobTitleViewModel
    {
        public List<Contract> contracts;
        public SelectList jobtitles;
        public string contractJobTitle { get; set; }
    }
}
