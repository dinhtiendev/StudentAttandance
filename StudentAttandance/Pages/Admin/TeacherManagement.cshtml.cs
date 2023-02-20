using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentAttandanceLibrary.CommonFunctions;
using StudentAttandanceLibrary.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using TeacherAttandanceLibrary.Repositories.IRepositories;

namespace StudentAttandance.Pages.Admin
{
    public class LecturerManagementModel : PageModel
    {
        [BindProperty]
        [Required]
        [DataType(DataType.Upload)]
        public IFormFile ExcelFile { get; set; }

        AutoGenerate generate = new AutoGenerate();

        private IExcelService _excelSerivce;
        private ITeacherRepository _teacherRepository;

        public LecturerManagementModel(IExcelService excelSerivce, ITeacherRepository teacherRepository)
        {
            _excelSerivce = excelSerivce;
            _teacherRepository = teacherRepository;
        }

        public IActionResult OnGet()
        {
            ViewData["TeacherList"] = _teacherRepository.GetAllTeachers().ToList();
            return Page();
        }

        public IActionResult OnPostAdd()
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/login");
            }
            return OnGet();
            // Form submission code here

        }

        public IActionResult OnGetDelete()
        {
            // Form submission code here
            return RedirectToPage();
        }

        public IActionResult OnGetUpdate()
        {
            // Form submission code here
            return RedirectToPage();
        }
    }
}
