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

        int AddCallPolice(Alarm alarm);

        UserAndRole Login(string name, string pwd);
        List<Authority> GetAuthority(int id);
    }
}
