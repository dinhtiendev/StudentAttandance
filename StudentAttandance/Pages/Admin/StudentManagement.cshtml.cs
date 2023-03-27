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

        public IActionResult OnGet(string message, string k)
        {
            if (HttpContext.Session.GetString("Account") != null)
            {
                var account = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString("Account"));

                if (account.RoleId == 1)
                {
                    ViewData["Message"] = message;
                    var listK = DBHelper.GetAllK(_studentRepository.GetStudents().ToList());
                    ViewData["ListK"] = listK;
                    if (k == null)
                    {
                        k = listK.Last().ToString();
                    }
                    ViewData["K"] = k;
                    TempData["K"] = k;
                    ViewData["StudentList"] = _studentRepository.GetStudentsByK(k).ToList();
                    ViewData["TotalStudents"] = _studentRepository.GetStudentsByK(k).ToList().Count();
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
                return OnGet(null, (string)ViewData["K"]);
            }

            var sheetTemplate = _excelSerivce.GetSheetFromTemplate("StudentList.xlsx");
            var sheetUpload = _excelSerivce.GetSheetFromUpload(ExcelFile);
            var columnsTemplate = _excelSerivce.GetHeaderColumns(sheetTemplate);
            var columnsUpload = _excelSerivce.GetHeaderColumns(sheetUpload);
            var checkHeader = _excelSerivce.CheckHeader(columnsTemplate, columnsUpload);
            if (!checkHeader)
            {
                return OnGet("The file upload header does not match the template !!!", (string)ViewData["K"]);
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
                    return OnGet("FullName is required !!!", (string)ViewData["K"]);
                }
                s.StudentId = generate.GenerateStudentCode(students, listS);
                if (s.StudentId == null)
                {
                    return OnGet("The number of students this time is enough !!!", (string)ViewData["K"]);
                }
                s.UserName = generate.GenerateDisplayName(row[0].ToString().Trim()) + s.StudentId;
                s.Image = null;

                try
                {
                    s.Dob = DateTime.Parse(row[1].ToString());
                }
                catch (Exception e)
                {
                    return OnGet("Date is not a valid format!!!", (string)ViewData["K"]);
                }

                if (!row[2].ToString().Trim().Equals("0") && !row[2].ToString().Trim().Equals("1"))
                {
                    return OnGet("Gender must be 0 or 1 !!!", (string)ViewData["K"]);
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
            //string currentK = (string)TempData["K"];
            _studentRepository.AddStudents(listA, listS);
            return RedirectToAction("/studentmanagement", new { Message = "", K = (string)TempData["K"] });
        }

        public IActionResult OnGetDelete(string id)
        {
            _studentRepository.DeleteStudent(id);
            return RedirectToAction("/studentmanagement", new { Message = "", K = (string)TempData["K"] });
        }

        public IActionResult OnGetRestore(string id)
        {
            _studentRepository.RestoreStudent(id);
            return RedirectToAction("/studentmanagement", new { Message = "", K = (string)TempData["K"] });
        }
    }
}
