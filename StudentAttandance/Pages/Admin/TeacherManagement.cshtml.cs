using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentAttandanceLibrary.CommonFunctions;
using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;
using StudentAttandanceLibrary.Services.Interfaces;
using StudentAttandanceLibrary.Validation.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace StudentAttandance.Pages.Admin
{
    public class LecturerManagementModel : PageModel
    {
        [BindProperty]
        [Required]
        [DataType(DataType.Upload)]
        [Excel(ErrorMessage = "Please select a valid Excel file (.xlsx).")]
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
            ViewData["TotalTeachers"] = _teacherRepository.GetAllTeachers().ToList().Count();
            return Page();
        }

        public IActionResult OnPostAdd(IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return OnGet();
            }
            if (!ModelState.IsValid)
            {
                return OnGet();
            }

            var sheetTemplate = _excelSerivce.GetSheetFromTemplate("StudentList.xlsx");
            var sheetUpload = _excelSerivce.GetSheetFromFile(ExcelFile);
            var columnsTemplate = _excelSerivce.GetColumnsInHeader(sheetTemplate);
            var columnsUpload = _excelSerivce.GetColumnsInHeader(sheetUpload);
            var checkHeader = _excelSerivce.CheckHeaderFileExcel(columnsTemplate, columnsUpload);
            if (!checkHeader)
            {
                ViewData["Message"] = "The file upload header does not match the template !!!";
                return OnGet();
            }
            List<Teacher> teachers = _teacherRepository.GetTeachers();
            List<Teacher> listT = new List<Teacher>();
            List<Account> listA = new List<Account>();
            for (int i = 1; i < sheetUpload.Rows.Count; i++)
            {
                var row = sheetUpload.Rows[i];
                Teacher t = new Teacher();
                t.TeacherId = generate.GenerateTeacherCode(teachers, listT, row[0].ToString().Trim());
                t.FullName = row[0].ToString().Trim();
                t.UserName = t.TeacherId;
                t.Image = null;
                t.Dob = DateTime.Parse(row[1].ToString());
                t.Gender = (row[2].ToString().Trim() == "0") ? false : true;
                listT.Add(t);

                Account a = new Account();
                a.AccountId = t.TeacherId;
                a.Email = t.UserName.ToLower() + "@fe.edu.vn";
                a.Password = "123123";
                a.RoleId = 2;
                a.Status = true;
                listA.Add(a);
            }
            _teacherRepository.AddTeacher(listA, listT);
            return RedirectToPage();
        }

        public IActionResult OnGetDelete(string id)
        {
            _teacherRepository.DeleteTeacher(id);
            return RedirectToPage();
        }

        public IActionResult OnGetUpdate()
        {
            return RedirectToPage();
        }
    }
}
