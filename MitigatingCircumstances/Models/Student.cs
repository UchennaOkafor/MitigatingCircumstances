using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MitigatingCircumstances.Models
{
    public class Student
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }
    }
}
