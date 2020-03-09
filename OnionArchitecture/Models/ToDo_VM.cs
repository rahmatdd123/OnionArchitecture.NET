using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnionArchitecture.Models
{
    public class ToDo_VM
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string TaskName { get; set; }

        [Required]
        [MaxLength(250)]
        [MinLength(5)]
        public string TaskDetails { get; set; }

        [Required]
        public string TaskDate { get; set; }

        public int TaskStatusID { get; set; }

        public string TaskStatus { get; set; }

        public string CreatedBy { get; set; }

        public IEnumerable<SelectListItem> ListStatus { get; set; }
    }
}