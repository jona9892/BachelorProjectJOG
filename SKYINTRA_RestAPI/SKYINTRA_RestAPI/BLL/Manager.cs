using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SKYINTRA_RestAPI.BLL
{
    public class Manager
    {


        public List<string> GetFiles(string dirPath, params string[] extensions)
        {
            List<string> allFiles = new List<string>();
            DirectoryInfo d = new DirectoryInfo(dirPath);

            IEnumerable<FileInfo> Files = StaticManager.GetFilesByExtensions(d, extensions); 
            foreach (FileInfo file in Files)
            {
                allFiles.Add(file.Name);
            }
            return allFiles;
        }
    }
}
