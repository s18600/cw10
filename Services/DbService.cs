using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public class DbService : IDbService
    {
        public string DeleteStudent(String id)
        {
            var db = new s18600Context();
            try
            {
                var students = db.Students.FirstOrDefault(s => s.IndexNumber == id);
                if (students != null)
                {
                    db.Students.Remove(students);
                    db.SaveChanges();
                    return "Ok";
                }
                

                return "Index number does not exist";
            }
            catch (SqlException e)
            {
                return "Database error occured";
            }
        }

        public string EnrollStudent(StudentEnrollmentRequest request)
        {
            var db = new s18600Context();
            try
            {
                Nullable<int> studiesId = db.Studies.First(s => s.Name == request.StudiesName).IdStudy;
                if (studiesId == null) return "Stuedies does not exist";
                var enr = new Enrollment
                {
                    IdEnrollment = db.Enrollments.Max(e => e.IdEnrollment + 1),
                    Semester = 0,
                    IdStudy = (int)studiesId,
                    StartDate = DateTime.Now
                };
                db.Enrollments.Add(enr);
                request.Student.IdEnrollment = enr.IdEnrollment;
                db.Students.Add(request.Student);
                db.SaveChanges();
                return "Ok";
            }
            catch(SqlException e)
            {
                return e.Message;
            }
            catch(InvalidOperationException oe)
            {
                return oe.Message;
            }
        }

        public List<Student> GetAllStudtens()
        {
            var db = new s18600Context();
            return db.Students.ToList();
        }

        public string PromoteStudents(StudentsPromotionRequest request)
        {
            var db = new s18600Context();
            try
            {
                Nullable<int> studiesId = db.Studies.First(s => s.Name == request.StudiesName).IdStudy;
                if (studiesId == null) return "StudiesId does not exist";
                var enr = db.Enrollments.Where(e => e.IdStudy == studiesId).Where(e=>e.Semester == request.Semester);

                if (enr.Count() < 1) return "No such enrollments";

                foreach (var e in enr)
                {
                    e.Semester += 1;
                }
                db.SaveChanges();
                return "Ok";

            }
            catch (SqlException e)
            {

                return e.Message;

            }
            catch (InvalidOperationException oe)
            {

                return oe.Message;

            }
        }

        public string UpdateStudent(Student student)
        {
            var db = new s18600Context();

            try
            {
                Student student1 = db.Students.First(e => e.IndexNumber == student.IndexNumber);
                student1.FirstName = student.FirstName;
                student1.LastName = student.LastName;
                student1.BirthDate = student.BirthDate;
                student1.IdEnrollmentNavigation = student.IdEnrollmentNavigation;
                db.SaveChanges();
                return "Ok";
            }catch(InvalidOperationException e)
            {
                return e.Message;
            }
        }

    }
}
