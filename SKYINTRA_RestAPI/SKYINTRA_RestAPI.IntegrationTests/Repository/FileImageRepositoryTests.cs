using DomainModel;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SKYINTRA_RestAPI.DAL.Context;
using SKYINTRA_RestAPI.DAL.Repository.Abstraction;
using SKYINTRA_RestAPI.DAL.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SKYINTRA_RestAPI.IntegrationTests.Repository
{
    public class FileImageRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
            InitializeDatabaseWithDataTest();
        }

        public void InitializeDatabaseWithDataTest()
        {
            var builder = new DbContextOptionsBuilder<SKYINTRA_DBContext>().UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=SKY-INTRA_DB_TEST;Integrated Security=True;");

            using (var context = new SKYINTRA_DBContext(builder.Options))
            {
                context.Database.ExecuteSqlCommand("Delete FROM FileImage");
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();

                var fileImages = new FileImage[]
                {
                    new FileImage {Title = "Title1", Path = "Path1"},
                    new FileImage {Title = "Title2", Path = "Path2"},
                    new FileImage {Title = "Title3", Path = "Path3"},
                    new FileImage {Title = "Title4", Path = "Path4"}
                };

                context.FileImages.AddRange(fileImages);

                context.SaveChanges();
            }
        }

        [Test]
        public void Add_FileImageValid_FileImageAdded()
        {
            FileImage fileImage = new FileImage
            {
                Title = "Title5",
                Path = "Path5"
            };

            IRepository<FileImage> repo = CreateRepository();
            var addedFileImage = repo.Add(fileImage);
            Assert.AreEqual(addedFileImage, fileImage);
        }

        [Test]
        public void ReadAll_4FileImagesInDatabase_Return4FileImagess()
        {
            IRepository<FileImage> repo = CreateRepository();

            IEnumerable<FileImage> fileimages = repo.ReadAll();
            Assert.AreEqual(4, fileimages.Count());
        }

        public void DeleteFileImages()
        {
            IRepository<FileImage> repo = CreateRepository();

            IEnumerable<FileImage> fileImages = repo.ReadAll();
            foreach (var fileImage in fileImages)
            {
                repo.Delete(fileImage);
            }
        }

        [Test]
        public void Delete_DeleteGuestsInDatabase_Return0Guests()
        {
            DeleteFileImages();
            var builder = new DbContextOptionsBuilder<SKYINTRA_DBContext>().UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=SKY-INTRA_DB_TEST;Integrated Security=True;");

            using (var context = new SKYINTRA_DBContext(builder.Options))
            {
                IRepository<FileImage> fileImagesRep = new FileImageRepository(context);
                IEnumerable<FileImage> fileImagessAfterDelete = fileImagesRep.ReadAll();
                Assert.AreEqual(0, fileImagessAfterDelete.Count());
            }
        }

        [Test]
        public void Update_ReadFileImageAndUpdate_ReturnTrue()
        {
            FileImage fileImage = new FileImage
            {
                Id = 10023,
                Title = "Title5",
                Path = "Path5"
            };

            IRepository<FileImage> repo = CreateRepository();
            var addedFileImage = repo.Add(fileImage);

            var fileImageToUpdate = repo.Read(10023);

            fileImageToUpdate.Path = "Path6";

            var updatedFileImage = repo.Update(fileImageToUpdate);

            Assert.AreEqual("Path6", updatedFileImage.Path);
        }

        public IRepository<FileImage> CreateRepository()
        {
            var builder = new DbContextOptionsBuilder<SKYINTRA_DBContext>()
                .UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=SKY-INTRA_DB_TEST;Integrated Security=True;");
            var context = new SKYINTRA_DBContext(builder.Options);
            DBInitializer.Initialize(context);
            IRepository<FileImage> fileimageRep = new FileImageRepository(context);
            return fileimageRep;
        }

    }
}
