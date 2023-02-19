using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.ModelDtos
{
    public class AdminDto
    {
        public string AdminId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? Image { get; set; }
        public string Email { get; set; } = null!;
        public int? RoleId { get; set; }
        public bool? Status { get; set; }
    }
}
