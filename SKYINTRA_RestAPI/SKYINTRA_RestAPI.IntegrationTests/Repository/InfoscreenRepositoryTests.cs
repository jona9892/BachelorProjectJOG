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
    public class InfoscreenRepositoryTests
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
                context.Database.ExecuteSqlCommand("Delete FROM FileImage");

                var fileImages = new FileImage[]
                {
                    new FileImage {Id = 1, Title = "Title1", Path = "Path1"},
                    new FileImage {Id = 2, Title = "Title2", Path = "Path2"},
                };
                context.FileImages.AddRange(fileImages);

                context.SaveChanges();

                var infoscreens = new Infoscreen[]
                {
                    new Infoscreen {Name = "Kundeservice", EkstruderingFileImage = fileImages[0], TermoformFileImage = fileImages[1], RSSFeed = "" },
                    new Infoscreen {Name = "Kantine", EkstruderingFileImage = new FileImage{ Title = "EkstrudFile1", Path = "EkstrudFilePath1"}, EkstruderingFileImageId = 1, TermoformFileImage = new FileImage{ Title = "TermoFile1", Path = "TermoFilePath1"}, TermoformFileImageId = 2, RSSFeed = ""},
                    new Infoscreen {Name = "Ekstrudering", EkstruderingFileImage = new FileImage{ Title = "EkstrudFile2", Path = "EkstrudFilePath2"}, TermoformFileImage = new FileImage{ Title = "TermoFile2", Path = "TermoFilePath2"}, RSSFeed = ""},
                    new Infoscreen {Name = "Termoform", EkstruderingFileImage = new FileImage{ Title = "EkstrudFile3", Path = "EkstrudFilePath3"}, TermoformFileImage = new FileImage{ Title = "TermoFile3", Path = "TermoFilePath3"}, RSSFeed = ""}
                };

                context.Infoscreens.AddRange(infoscreens);

                context.SaveChanges();
            }
        }

        [Test]
        public void Read_readkantineInfoscreen_ReturnInfoscreenKantine()
        {
            IInfoscreenRepository repo = CreateRepository();

            Infoscreen infoscreen = repo.Read("Kantine");
            Assert.AreEqual("Kantine", infoscreen.Name);
        }

        [Test]
        public void ReadAll_4Infoscreenscreens_ReturnTrue()
        {
            IInfoscreenRepository repo = CreateRepository();

            IEnumerable<Infoscreen> infoscreens = repo.ReadAll();
            Assert.AreEqual(4, infoscreens.Count());
        }

        [Test]
        public void Update_ReadInforscreenAndUpdateProperty_ReturnTrue()
        {
            var builder = new DbContextOptionsBuilder<SKYINTRA_DBContext>().UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=SKY-INTRA_DB_TEST;Integrated Security=True;");

            using (var context = new SKYINTRA_DBContext(builder.Options))
            {
                var fileImage = new FileImage
                {
                    Id = 1001,
                    Title = "Title1001",
                    Path = "Path1001"
                };
                context.FileImages.AddRange(fileImage);

                Infoscreen information = new Infoscreen
                {
                    Id = 10023,
                    Name = "Tester",
                    EkstruderingFileImage = new FileImage { Title = "EkstrudFile", Path = "EkstrudFilePath" },
                    TermoformFileImage = new FileImage { Title = "TermoFile", Path = "TermoFilePath" },
                    RSSFeed = ""
                };
                context.Infoscreens.Add(information);

                IInfoscreenRepository repo = CreateRepository();                

                var infoscreenToUpdate = repo.Read(10023);

                infoscreenToUpdate.TermoformFileImage = fileImage;

                var updatedInfoscreen = repo.Update(infoscreenToUpdate);

                Assert.AreEqual("Title1001", updatedInfoscreen.TermoformFileImage.Title);
            }
        }

        public IInfoscreenRepository CreateRepository()
        {
            var builder = new DbContextOptionsBuilder<SKYINTRA_DBContext>()
                .UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=SKY-INTRA_DB_TEST;Integrated Security=True;");
            var context = new SKYINTRA_DBContext(builder.Options);
            IInfoscreenRepository infoscreenRep = new InfoscreenRepository(context);
            return infoscreenRep;
        }
    }
}
