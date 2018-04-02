using DomainModel;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SKYINTRA_RestAPI.DAL.Context;
using SKYINTRA_RestAPI.DAL.Repository.Abstraction;
using SKYINTRA_RestAPI.DAL.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SKYINTRA_RestAPI.IntegrationTests.Repository
{
    [TestFixture]
    public class InformationRepositoryTests
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
                context.Database.ExecuteSqlCommand("Delete FROM Information");
                var informations = new Information[]
                {
                    new Information {Header = "Header1", Body = "Body1", CreatedBy = "User1", CreatedDate = DateTime.Today, ModifiedDate = DateTime.Today},
                    new Information {Header = "Header2", Body = "Body2", CreatedBy = "User2", CreatedDate = DateTime.Today, ModifiedDate = DateTime.Today},
                    new Information {Header = "Header3", Body = "Body3", CreatedBy = "User3", CreatedDate = DateTime.Today, ModifiedDate = DateTime.Today},
                    new Information {Header = "Header4", Body = "Body4", CreatedBy = "User4", CreatedDate = DateTime.Today, ModifiedDate = DateTime.Today}
                };

                context.Informations.AddRange(informations);

                context.SaveChanges();
            }
        }

        [Test]
        public void Add_InformationValid_InformationAdded()
        {
            Information info = new Information
            {
                Header = "Header5",
                Body = "Body5",
                CreatedBy = "User5",
                CreatedDate = DateTime.Today,
                ModifiedDate = DateTime.Today
            };

            IInformationRepository repo = CreateRepository();
            var addedInformation = repo.Add(info);
            Assert.AreEqual(addedInformation, info);
        }


        [Test]
        public void ReadAll_5InfoInDatabase_ReturnTrue()
        {
            IInformationRepository repo = CreateRepository();
            IEnumerable<Information> infos = repo.ReadAll();
            Assert.AreEqual(5, infos.Count());
        }

        public void DeleteInformations()
        {
            IInformationRepository repo = CreateRepository();

            IEnumerable<Information> informations = repo.ReadAll();
            foreach (var info in informations)
            {
                repo.Delete(info);
            }
        }

        [Test]
        public void Delete_DeleteInformationsInDatabase_Return0Guests()
        {
            DeleteInformations();
            var builder = new DbContextOptionsBuilder<SKYINTRA_DBContext>().UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=SKY-INTRA_DB_TEST;Integrated Security=True;");

            using (var context = new SKYINTRA_DBContext(builder.Options))
            {
                IInformationRepository infoRep = new InformationRepository(context);
                IEnumerable<Information> infosAfterDelete = infoRep.ReadAll();
                Assert.AreEqual(0, infosAfterDelete.Count());
            }
        }

        [Test]
        public void ReadLatestInformations_Latest5Informations_ReturnTrue()
        {
            IInformationRepository repo = CreateRepository();
            IEnumerable<Information> infos = repo.GetLatestInforamtions();
            Assert.AreEqual(5, infos.Count());
        }

        [Test]
        public void Update_ReadInformationAndUpdate_ReturnTrue()
        {
            Information information = new Information
            {
                Id = 10023,
                Header = "Header7",
                Body = "Body7",
                CreatedBy = "User7",
                CreatedDate = DateTime.Today,
                ModifiedDate = DateTime.Today
            };

            IInformationRepository repo = CreateRepository();
            var addedInformation = repo.Add(information);

            var informationToUpdate = repo.Read(10023);

            informationToUpdate.Header = "HeaderChanged";

            var updatedInformation = repo.Update(informationToUpdate);

            Assert.AreEqual("HeaderChanged", updatedInformation.Header);
        }

        public IInformationRepository CreateRepository()
        {
            var builder = new DbContextOptionsBuilder<SKYINTRA_DBContext>()
                .UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=SKY-INTRA_DB_TEST;Integrated Security=True;");
            var context = new SKYINTRA_DBContext(builder.Options);
            DBInitializer.Initialize(context);
            IInformationRepository informationRep = new InformationRepository(context);
            return informationRep;
        }
    }
}
