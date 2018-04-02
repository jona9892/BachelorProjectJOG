using DomainModel;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SKYINTRA_RestAPI.DAL.Context;
using SKYINTRA_RestAPI.DAL.Repository.Abstraction;
using SKYINTRA_RestAPI.DAL.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SKYINTRA_RestAPI.IntegrationTests.Repository
{
    class InforscreenFileImageRepTests
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
                context.Database.ExecuteSqlCommand("Delete FROM Infoscreen");
                context.Database.ExecuteSqlCommand("Delete FROM Infomation");

                var fileImages = new FileImage[]
               {
                    new FileImage {Id = 1001, Title = "Title1", Path = "Path1"},
                    new FileImage {Id = 1001, Title = "Title2", Path = "Path2"},
                    new FileImage {Id = 1001, Title = "Title3", Path = "Path3"},
                    new FileImage {Id = 1001, Title = "Title4", Path = "Path4"}
               };
                context.FileImages.AddRange(fileImages);

                context.SaveChanges();

                var infoscreens = new Infoscreen[]
                {
                    new Infoscreen {Id = 1001, Name = "Kundeservice" },
                    new Infoscreen {Id = 1002, Name = "Kantine"},
                    new Infoscreen {Id = 1003, Name = "Ekstrudering"},
                    new Infoscreen {Id = 1004, Name = "Termoform"}
                };

                context.Infoscreens.AddRange(infoscreens);

                context.SaveChanges();
            }
        }

        [Test]
        public void InsertRelationship_SetInfoscreenIdToFileImageId_ReturnTrue()
        {
            IManyToMany<Infoscreen, FileImage> repo = CreateRepository();
            IInfoscreenRepository infoscreenRepo = CreateInfoscreenRepository();
            repo.InsertRelationship(1001, 1001);

            var infoscreen = infoscreenRepo.Read(1001);

            Assert.AreEqual(1, infoscreen.InfoscreenFileImages.Count);
        }

        [Test]
        public void DeleteRelationship_RemoveInfoscreenIdAndFileImagesId_Return0FileImages()
        {
            IManyToMany<Infoscreen, FileImage> repo = CreateRepository();
            IInfoscreenRepository infoscreenRepo = CreateInfoscreenRepository();
            repo.DeleteRelationship(1001, 1001);

            var infoscreen = infoscreenRepo.Read(1001);

            Assert.AreEqual(0, infoscreen.InfoscreenFileImages.Count);
        }

        public IInfoscreenRepository CreateInfoscreenRepository()
        {
            var builder = new DbContextOptionsBuilder<SKYINTRA_DBContext>()
                .UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=SKY-INTRA_DB_TEST;Integrated Security=True;");
            var context = new SKYINTRA_DBContext(builder.Options);
            IInfoscreenRepository infoscreenRep = new InfoscreenRepository(context);
            return infoscreenRep;
        }

        public IManyToMany<Infoscreen, FileImage> CreateRepository()
        {
            var builder = new DbContextOptionsBuilder<SKYINTRA_DBContext>()
                .UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=SKY-INTRA_DB_TEST;Integrated Security=True;");
            var context = new SKYINTRA_DBContext(builder.Options);

            IManyToMany<Infoscreen, FileImage> infoscreenInformationRep = new InfoscreenFileImageRepository(context);
            return infoscreenInformationRep;
        }
    }
}
