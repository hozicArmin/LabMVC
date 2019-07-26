using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LabMVC.Models;

namespace LabMVC.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public byte MembershipTypeId { get; set; }  //Foreign Key

        //Izbacujeno tip MembershipType o kojem smo ovisni, ako bi postavljali to zvao bi se MembershipTypeDto
        //public MembershipType MembershipType { get; set; }

        //Postavljamo MembershipTypeDto za 
        public MembershipTypeDto MembershipType { get; set; }


        //[Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }
    }
}