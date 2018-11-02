using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FPS.Models;
using FPS.IServices;

namespace FPS.Services
{
    public class StudentServices:IStudent
    {
        public string GetStudentName()
        {
            var db = SugerBase.GetInstance();
            var name = db.Queryable<Student>().Single(m => m.ID == 1).Name;
            return name;
        }
    }
}
