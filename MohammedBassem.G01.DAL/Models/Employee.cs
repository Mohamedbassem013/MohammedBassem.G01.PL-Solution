using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohammedBassem.G01.DAL.Models
{
   public class Employee :BaseEntity
    { 
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Adderss { get; set; }
        public decimal salary { get; set; }
        public string Email { get; set; }
        public string? ImageName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime HiringData { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public int? WorkForId { get; set; } // FK
        public Department? WorkFor { get; set; } // Navigational Property
   }
}
