using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Validation.ModelValidation
{
    public class AddGroupValidation
    {
        public DateTime DateStart { get; set; }

        public int? NumberSlot { get; set; }

        public string KS { get; set; }

        public int TermId { get; set; }

        public int CourseId { get; set; }

    }
}
