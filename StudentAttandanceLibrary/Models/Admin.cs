using System;
using System.Collections.Generic;

namespace StudentAttandanceLibrary.Models
{
    public partial class Admin
    {
        public string AdminId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? Image { get; set; }

        public virtual Account AdminNavigation { get; set; } = null!;
    }
}
