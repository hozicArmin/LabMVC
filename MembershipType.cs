using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabMVC.Models
{
    public class MembershipType
    {
        //U ovoj klasi svaki property pribavlja postavljene podatke iz tabele
        //"Pay as You Go  Id = 1,   0,  0,  0
        //"Monthly        Id = 2,
        //"Quarterly"     Id = 3,  90, 15,

        public byte Id { get; set; } //Ovo je nas Membership Tip 
        public string Name { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }


        public static readonly byte Unknown = 0; //koristimo u Min18YearsIfAMember 
        public static readonly byte PayAsYouGo = 1;
    }
}