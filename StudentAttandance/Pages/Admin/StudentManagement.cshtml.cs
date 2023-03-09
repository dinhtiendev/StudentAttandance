using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StudentAttandanceLibrary.CommonFunctions;
using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;
using StudentAttandanceLibrary.Services.Interfaces;
using StudentAttandanceLibrary.Validation.CustomValidation;
using StudentAttandanceLibrary.Validation.ModelValidation;
using System.ComponentModel.DataAnnotations;

namespace StudentAttandance.Pages.Admin
{
    public class StudentManagementModel : PageModel
    {
        [BindProperty]
        [DataType(DataType.Upload)]
        [IsExcel(ErrorMessage = "Please select a valid Excel file (.xlsx).")]
        public IFormFile ExcelFile { get; set; }

        private IExcelService _excelSerivce;
        private IStudentRepository _studentRepository;

        public StudentManagementModel(IExcelService excelSerivce, IStudentRepository studentRepository)
        {
            _excelSerivce = excelSerivce;
            _studentRepository = studentRepository;
        }

        public IActionResult OnGet(string message)
        {
            if (HttpContext.Session.GetString("Account") != null)
            {
                var account = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString("Account"));

                if (account.RoleId == 1)
                {
                    ViewData["Message"] = message;
                    ViewData["StudentList"] = _studentRepository.GetAllStudents().ToList();
                    ViewData["TotalStudents"] = _studentRepository.GetAllStudents().ToList().Count();
                    return Page();
                }
            }
            return RedirectToPage("/error");
        }

        public IActionResult OnPostAdd()
        {
            AutoGenerate generate = new AutoGenerate();

            if (!ModelState.IsValid)
            {
                return OnGet(null);
            }

            var sheetTemplate = _excelSerivce.GetSheetFromTemplate("StudentList.xlsx");
            var sheetUpload = _excelSerivce.GetSheetFromUpload(ExcelFile);
            var columnsTemplate = _excelSerivce.GetHeaderColumns(sheetTemplate);
            var columnsUpload = _excelSerivce.GetHeaderColumns(sheetUpload);
            var checkHeader = _excelSerivce.CheckHeader(columnsTemplate, columnsUpload);
            if (!checkHeader)
            {
                return OnGet("The file upload header does not match the template !!!");
            }

            List<Student> students = _studentRepository.GetStudents();
            List<Student> listS = new List<Student>();
            List<Account> listA = new List<Account>();
            List<StudentList> list = new List<StudentList>();
            for (int i = 1; i < sheetUpload.Rows.Count; i++)
            {
                var row = sheetUpload.Rows[i];
                Student s = new Student();
                s.FullName = row[0].ToString().Trim();
                if (string.IsNullOrWhiteSpace(s.FullName))
                {
                    return OnGet("FullName is required !!!");
                }
                s.StudentId = generate.GenerateStudentCode(students, listS);
                if (s.StudentId == null)
                {
                    return OnGet("The number of students this time is enough !!!");
                }
                s.UserName = generate.GenerateDisplayName(row[0].ToString().Trim()) + s.StudentId;
                s.Image = null;

                try
                {
                    s.Dob = DateTime.Parse(row[1].ToString());
                }
                catch (Exception e)
                {
                    return OnGet("Date is not a valid format!!!");
                }

                if (!row[2].ToString().Trim().Equals("0") && !row[2].ToString().Trim().Equals("1"))
                {
                    return OnGet("Gender must be 0 or 1 !!!");
                }
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
            _studentRepository.AddStudents(listA, listS);
            return RedirectToPage();
        }

        public IActionResult OnGetDelete(string id)
        {
            _studentRepository.DeleteStudent(id);
            return RedirectToPage();
        }

        public IActionResult OnGetRestore(string id)
        {
            _studentRepository.RestoreStudent(id);
            return RedirectToPage();
        }
    }
}
