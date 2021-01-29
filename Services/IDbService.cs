using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public interface IDbService
    {
        List<Student> GetAllStudtens();

        String UpdateStudent(Student student);

        String DeleteStudent(String id);

        String EnrollStudent(StudentEnrollmentRequest request);

        String PromoteStudents(StudentsPromotionRequest request);
    }
}
