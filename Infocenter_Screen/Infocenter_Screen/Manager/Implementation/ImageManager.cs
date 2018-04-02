using Infocenter_Screen.BLL.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Infocenter_Screen.BLL.Implementation
{
    public class ImageManager : IImageManager
    {
        private readonly string ImagesPath = @"C:\Users\jog\Desktop\SKY_INTRA\Billeder";
        private readonly string VirtualImagesPath = "~/Images/";

        public List<string> GetAllImagesFromPath()
        {
            List<string> allFiles = new List<string>();
            System.IO.DirectoryInfo allImagesDir = new System.IO.DirectoryInfo(ImagesPath);
            DirectoryInfo d = new DirectoryInfo(ImagesPath);
            IEnumerable<FileInfo> Files = FileExtension.GetFilesByExtensions(d, ".jpeg", ".png", ".jpg");
            string fileName = "";
            foreach (FileInfo file in Files)
            {
                fileName = file.Name;
                allFiles.Add(VirtualImagesPath + fileName);
            }
            return allFiles;
        }
    }
}