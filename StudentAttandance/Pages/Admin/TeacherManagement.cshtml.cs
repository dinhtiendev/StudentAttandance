using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StudentAttandanceLibrary.CommonFunctions;
using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;
using StudentAttandanceLibrary.Services.Interfaces;
using StudentAttandanceLibrary.Validation.CustomValidation;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace StudentAttandance.Pages.Admin
{
    public class LecturerManagementModel : PageModel
    {
        [BindProperty]
        [DataType(DataType.Upload)]
        [IsExcel(ErrorMessage = "Please select a valid Excel file (.xlsx).")]
        public IFormFile ExcelFile { get; set; }

        private IExcelService _excelSerivce;
        private ITeacherRepository _teacherRepository;

        public LecturerManagementModel(IExcelService excelSerivce, ITeacherRepository teacherRepository)
        {
            _excelSerivce = excelSerivce;
            _teacherRepository = teacherRepository;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Account") != null)
            {
                var account = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString("Account"));

                if (account.RoleId == 1)
                {
                    //ViewData["Message"] = null;
                    ViewData["TeacherList"] = _teacherRepository.GetAllTeachers().ToList();
                    ViewData["TotalTeachers"] = _teacherRepository.GetAllTeachers().ToList().Count();
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
                return OnGet();
            }
            var sheetTemplate = _excelSerivce.GetSheetFromTemplate("TeacherList.xlsx");
            var sheetUpload = _excelSerivce.GetSheetFromUpload(ExcelFile);
            var columnsTemplate = _excelSerivce.GetHeaderColumns(sheetTemplate);
            var columnsUpload = _excelSerivce.GetHeaderColumns(sheetUpload);
            var checkHeader = _excelSerivce.CheckHeader(columnsTemplate, columnsUpload);
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
                t.FullName = row[0].ToString().Trim();
                if (string.IsNullOrWhiteSpace(t.FullName))
                {
                    ViewData["Message"] = "FullName is required !!!";
                    return OnGet();
                }
                t.TeacherId = generate.GenerateTeacherCode(teachers, listT, row[0].ToString().Trim());
                t.UserName = t.TeacherId;
                t.Image = null;
                try
                {
                    t.Dob = DateTime.Parse(row[1].ToString());
                }
                catch (Exception e)
                {
                    ViewData["Message"] = "Date is not a valid format!!!";
                    return OnGet();
                }
                if (!row[2].ToString().Trim().Equals("0") && !row[2].ToString().Trim().Equals("1"))
                {
                    ViewData["Message"] = "Gender must be 0 or 1 !!!";
                    return OnGet();
                }
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

        public IActionResult OnGetRestore(string id)
        {
            _teacherRepository.RestoreTeacher(id);
            return RedirectToPage();
        }
    }
}
