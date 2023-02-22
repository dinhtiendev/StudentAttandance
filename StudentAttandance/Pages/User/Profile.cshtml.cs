using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StudentAttandanceLibrary.ModelDtos;
using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;

namespace StudentAttandance.Pages.User
{
    public class ProfileModel : PageModel
    {
        private IAdminRepository adminRepository;
        private ITeacherRepository teacherRepository;
        private IStudentRepository studentRepository;

        public ProfileModel(IAdminRepository adminRepository, ITeacherRepository teacherRepository, IStudentRepository studentRepository)
        {
            this.adminRepository = adminRepository;
            this.teacherRepository = teacherRepository;
            this.studentRepository = studentRepository;
        }

        public void OnGet()
        {
            var account = HttpContext.Session.GetString("Account");
            if (account != null)
            {
                Account? acc = JsonConvert.DeserializeObject<Account>(account.ToString());
                if (acc.RoleId == 1)
                {
                    AdminDto? admin = adminRepository.GetAdminByEmail(acc.Email);
                    ViewData["Admin"] = admin;
                } else if (acc.RoleId == 2)
                {
                    TeacherDto? teacher = teacherRepository.GetTeacherByEmail(acc.Email);
                    ViewData["Teacher"] = teacher;
                } else if (acc.RoleId == 3)
                {
                    StudentDto? student = studentRepository.GetStudentByEmail(acc.Email);
                    ViewData["Student"] = student;
                } else
                {

                }
            }
        }
    }
}
