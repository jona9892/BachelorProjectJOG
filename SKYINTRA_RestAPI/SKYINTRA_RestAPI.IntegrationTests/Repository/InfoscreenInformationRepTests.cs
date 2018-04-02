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
    public class InfoscreenInformationRepTests
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

                var informations = new Information[]
                {
                    new Information {Id = 1001, Header = "Header1", Body = "Body1", CreatedBy = "User1", CreatedDate = DateTime.Today, ModifiedDate = DateTime.Today},
                    new Information {Id = 1002, Header = "Header2", Body = "Body2", CreatedBy = "User2", CreatedDate = DateTime.Today, ModifiedDate = DateTime.Today},
                    new Information {Id = 1003, Header = "Header3", Body = "Body3", CreatedBy = "User3", CreatedDate = DateTime.Today, ModifiedDate = DateTime.Today},
                    new Information {Id = 1004, Header = "Header4", Body = "Body4", CreatedBy = "User4", CreatedDate = DateTime.Today, ModifiedDate = DateTime.Today}
                };
                context.Informations.AddRange(informations);

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
        public void InsertRelationship_SetInfoscreenIdToInformationId_ReturnTrue()
        {
            IManyToMany<Infoscreen, Information> repo = CreateRepository();
            IInfoscreenRepository infoscreenRepo = CreateInfoscreenRepository();
            repo.InsertRelationship(1001,1001);

            var infoscreen = infoscreenRepo.Read(1001);

            Assert.AreEqual(1, infoscreen.InfoscreenInformations.Count);
        }

        [Test]
        public void DeleteRelationship_RemoveInfoscreenIdAndInformationId_Return0Informations()
        {
            IManyToMany<Infoscreen, Information> repo = CreateRepository();
            IInfoscreenRepository infoscreenRepo = CreateInfoscreenRepository();
            repo.DeleteRelationship(1001, 1001);

            var infoscreen = infoscreenRepo.Read(1001);

            Assert.AreEqual(0, infoscreen.InfoscreenInformations.Count);
        }

        public IInfoscreenRepository CreateInfoscreenRepository()
        {
            var builder = new DbContextOptionsBuilder<SKYINTRA_DBContext>()
                .UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=SKY-INTRA_DB_TEST;Integrated Security=True;");
            var context = new SKYINTRA_DBContext(builder.Options);
            IInfoscreenRepository infoscreenRep = new InfoscreenRepository(context);
            return infoscreenRep;
        }

        public IManyToMany<Infoscreen, Information> CreateRepository()
        {
            var builder = new DbContextOptionsBuilder<SKYINTRA_DBContext>()
                .UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=SKY-INTRA_DB_TEST;Integrated Security=True;");
            var context = new SKYINTRA_DBContext(builder.Options);

            IManyToMany<Infoscreen, Information> infoscreenInformationRep = new InfoscreenInformationRepository(context);
            return infoscreenInformationRep;
        }
    }
}
