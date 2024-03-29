﻿using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.ModelDtos;

namespace StudentAttandanceLibrary.Repositories.IRepositories
{
    public interface IStudentRepository
    {
        public StudentDto? GetStudentByEmail(String id);
        public List<StudentDto> GetListStudents();
        public void AddStudents(List<Account> listA, List<Student> listS);
        public void RestoreStudent(string id);
        public void DeleteStudent(string id);
        public IQueryable<StudentDto> GetAllStudents();
        public List<Student> GetStudents();
        public IQueryable<Student> GetStudentsByConditions(int termId, int courseId, int groupId);
        public IQueryable<StudentDto> GetStudentsByK(string k);
        public IQueryable<StudentDto> DivideStudents(string k ,int offset, int total);

    }
}
