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

        [Range(1, 30)]
        public int? NumberSlot { get; set; }

        public string KS { get; set; }

        //public int? TeacherId { get; set; }

        public int TermId { get; set; }

        public int CourseId { get; set; }

        //public int? RoomId { get; set; }

        //public int SlotId { get; set; }

    }
}
