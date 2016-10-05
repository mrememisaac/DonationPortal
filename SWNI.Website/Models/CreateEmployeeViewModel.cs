using SWNI.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SWNI.Website.Models
{
    public class CreateEmployeeViewModel
    {
        public class UserRole
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public CreateEmployeeViewModel()
        {

        }
        public CreateEmployeeViewModel(List<UserRole> roles)
        {           
            this.roles = roles;
        }

        public static List<UserRole> GetRoles()
        {
            List<UserRole> roles = new List<UserRole>();
            roles.Add(new UserRole { Id = 1, Name = "Admin" });
            roles.Add(new UserRole { Id = 2, Name = "Executive" });
            roles.Add(new UserRole { Id = 3, Name = "Staff" });
            return roles;
        }
        
        private readonly List<UserRole> roles;

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Designation { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [Display(Name = "Agency")]
        public int AgencyId { get; set; }

        //private string _role { get; set; }

        [Display(Name = "Role")]
        public string Role
        {
            get;
            set;          
        }

        public IEnumerable<SelectListItem> Roles
        {
            get
            {
                return DefaultRole.Concat(new SelectList(roles, "Name", "Name"));
            }
        }

        
        public IEnumerable<SelectListItem> DefaultRole
        {
            get
            {
                return Enumerable.Repeat(new SelectListItem { Value = "-1", Text = "Select a role" }, count: 1);
            }
        }
    }
}
