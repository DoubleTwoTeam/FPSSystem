using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPS.Models;

namespace FPS.IServices
{
    public interface IStudent
    {
        string GetStudentName();
        //List<Student> Fen();
        int AddCallPolice(Alarm alarm);
    }
}
