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
    [TestFixture]
    public class FrontPageRepositoryTests
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
                context.Database.ExecuteSqlCommand("Delete FROM FrontPage");
                context.Database.ExecuteSqlCommand("Delete FROM FileImage");

                FrontPage fp = new FrontPage
                {
                    Id = 1, 
                    EkstruderingFileImage = new FileImage
                    {
                        Id = 1, 
                        Title = "EkstrudTitle",
                        Path = "EkstrudPath"
                    },
                    TermoformFileImage = new FileImage
                    {
                        Id = 1,
                        Title = "TermoTitle",
                        Path = "TermoPath"
                    }
                };

                context.FrontPage.Add(fp);

                context.SaveChanges();
            }
        }

        [Test]
        public void GetFirst_retrievethefirstrow_ReturnTrue()
        {
            IFrontPageRepository repo = CreateRepository();

            var fp = repo.GetFirst();

            Assert.AreEqual(1, fp.Id);
        }

        public IFrontPageRepository CreateRepository()
        {
            var builder = new DbContextOptionsBuilder<SKYINTRA_DBContext>()
                .UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=SKY-INTRA_DB_TEST;Integrated Security=True;");
            var context = new SKYINTRA_DBContext(builder.Options);
            DBInitializer.Initialize(context);
            IFrontPageRepository frontpageRep = new FrontPageRepository(context);
            return frontpageRep;
        }
    }

}
