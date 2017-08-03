using AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Dal
{
    public interface IStudentRepositoryService 
    {
        void SaveStudent(Student student);
        IEnumerable<Student> GetAllStudents();
        Student GetStudent(int id);
        void DeleteStudent(int id);
        void UpdateStudent(Student student);
    }
}
