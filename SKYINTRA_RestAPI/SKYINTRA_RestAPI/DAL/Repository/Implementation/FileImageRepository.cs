using DomainModel;
using SKYINTRA_RestAPI.DAL.Context;
using SKYINTRA_RestAPI.DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYINTRA_RestAPI.DAL.Repository.Implementation
{
    public class FileImageRepository : IRepository<FileImage>
    {
        private SKYINTRA_DBContext ctx;

        public FileImageRepository(SKYINTRA_DBContext context)
        {
            ctx = context;
        }

        public FileImage Add(FileImage t)
        {
            FileImage newFileImage = ctx.FileImages.Add(t).Entity;
            ctx.SaveChanges();
            return newFileImage;
        }

        public void Delete(FileImage t)
        {
            FileImage fileImage = ctx.FileImages.FirstOrDefault(x => x.Id == t.Id);
            ctx.FileImages.Remove(fileImage);
            ctx.SaveChanges();
        }

        public FileImage Read(int id)
        {
            FileImage fileImage = ctx.FileImages.Where(c => c.Id == id).FirstOrDefault();
            return fileImage;
        }

        public IEnumerable<FileImage> ReadAll()
        {
            IEnumerable<FileImage> fileImages = ctx.FileImages.ToList();
            return fileImages;
        }

        public FileImage Update(FileImage t)
        {
            var fileImageDB = ctx.FileImages.FirstOrDefault(g => g.Id == t.Id);
            fileImageDB.Title = t.Title;
            fileImageDB.Path = t.Path;
            
            ctx.SaveChanges();
            return fileImageDB;
        }
    }
}
