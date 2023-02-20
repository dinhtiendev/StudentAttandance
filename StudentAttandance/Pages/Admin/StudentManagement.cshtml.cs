using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentAttandanceLibrary.CommonFunctions;
using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;
using StudentAttandanceLibrary.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StudentAttandance.Pages.Admin
{
    public class StudentManagementModel : PageModel
    {
        [BindProperty]
        [Required]
        [DataType(DataType.Upload)]
        public IFormFile ExcelFile { get; set; }

        AutoGenerate generate = new AutoGenerate();

        private IExcelService _excelSerivce;
        private IStudentRepository _studentRepository;

        public StudentManagementModel(IExcelService excelSerivce, IStudentRepository studentRepository)
        {
            _excelSerivce = excelSerivce;
            _studentRepository = studentRepository;
        }

        public IActionResult OnGet()
        {
            ViewData["StudentList"] = _studentRepository.GetAllStudents().ToList();
            ViewData["TotalStudents"] = _studentRepository.GetAllStudents().ToList().Count();
            return Page();
        }

        public IActionResult OnPostAdd()
        {
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
            List<Student> students = _studentRepository.GetStudents();
            List<Student> listS = new List<Student>();
            List<Account> listA = new List<Account>();
            for (int i = 1; i < sheetUpload.Rows.Count; i++)
            {
                var row = sheetUpload.Rows[i];
                Student s = new Student();
                s.StudentId = generate.GenerateStudentCode(students, listS);
                s.FullName = row[0].ToString().Trim();
                s.UserName = generate.GenerateDisplayName(row[0].ToString().Trim()) + s.StudentId;
                s.Image = null;
                s.Dob = DateTime.Parse(row[1].ToString());
                s.Gender = (row[2].ToString().Trim() == "0") ? false : true;
                s.Address = row[3].ToString().Trim();
                listS.Add(s);

                Account a = new Account();
                a.AccountId = s.StudentId;
                a.Email = s.UserName.ToLower() + "@fpt.edu.vn";
                a.Password = "123123";
                a.RoleId = 3;
                a.Status = true;
                listA.Add(a);
            }
            _studentRepository.AddStudent(listA, listS);
            return RedirectToPage();

        }

        public IActionResult OnGetDelete()
        {
            return RedirectToPage();
        }

        public IActionResult OnGetUpdate()
        {
            // Form submission code here
            return RedirectToPage();
        }
    }
}
