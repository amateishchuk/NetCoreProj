using AspNetCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Dal
{
    public class StudentRepository : IStudentRepositoryService
    {
        private ApplicationContext context;
        private DbSet<Student> studentEntity;
        public StudentRepository(ApplicationContext context)
        {
            this.context = context;
            studentEntity = context.Set<Student>();
        }


        public int InsertStudent(Student student)
        {
            studentEntity.Add(student);
            context.SaveChanges();
            return student.Id;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return studentEntity.AsEnumerable();
        }

        public Student GetStudent(int id)
        {
            return studentEntity.SingleOrDefault(s => s.Id == id);
        }
        public void DeleteStudent(int id)
        {
            Student student = GetStudent(id);
            studentEntity.Remove(student);
            context.SaveChanges();
        }
        public void UpdateStudent(Student student)
        {
            var stdInDb = studentEntity.Find(student.Id);

            if (stdInDb != null)
            {
                context.Entry(stdInDb).CurrentValues.SetValues(student);
                context.Entry(stdInDb).State = EntityState.Modified;
                context.SaveChanges();
            }            
            
        }       
            
    }
}
