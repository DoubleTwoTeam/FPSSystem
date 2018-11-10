using FPS.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPS.UI.Common
{
    public interface IPageHelper
    {
        PageList<T>  InfoList<T>(PageParams pageParams);
    }
}
