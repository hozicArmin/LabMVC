using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LabMVC.Models;

namespace LabMVC.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }

        public string Title
        {
            get
            {
                return Customer.Id != 0 ? "Edit Customer" : "New Custome";
            }
        }

    }
}
