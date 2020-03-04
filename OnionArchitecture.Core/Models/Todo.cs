using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Core.Models
{
    public class Todo
    {
        public int Id { get; set; }

        public string TaskName { get; set; }

        public string TaskDate { get; set; }

        public string TaskDetails { get; set; }

        public string TaskStatus { get; set; }

    }
}
