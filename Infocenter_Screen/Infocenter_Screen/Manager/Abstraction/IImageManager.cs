using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infocenter_Screen.BLL.Abstraction
{
    public interface IImageManager
    {
        List<string> GetAllImagesFromPath();
    }
}