using System;
using System.Collections.Generic;

namespace StudentAttandanceLibrary.Models
{
    public partial class Term
    {
        public Term()
        {
            Groups = new HashSet<Group>();
        }

        public int TermId { get; set; }
        public string TermName { get; set; } = null!;

        public virtual ICollection<Group> Groups { get; set; }
    }
}
