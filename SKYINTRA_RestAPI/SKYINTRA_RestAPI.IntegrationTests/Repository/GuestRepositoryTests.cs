using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SKYINTRA_RestAPI.DAL.Context;
using SKYINTRA_RestAPI.DAL.Repository.Abstraction;
using SKYINTRA_RestAPI.DAL.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Transactions;

namespace SKYINTRA_RestAPI.IntegrationTests.Repository
{
    [TestFixture]
    public class GuestRepositoryTests
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
                context.Database.ExecuteSqlCommand("DELETE FROM Guest");
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();

                var guests = new Guest[]
                {
                    new Guest {Name = "Guest1", Company = "Company1", Country = "Country1", Dato = DateTime.Now.AddDays(1)},
                    new Guest {Name = "Guest2", Company = "Company2", Country = "Country2", Dato = DateTime.Now.AddDays(1)},
                    new Guest {Name = "Guest3", Company = "Company3", Country = "Country3", Dato = DateTime.Today},
                    new Guest {Name = "Guest4", Company = "Company4", Country = "Country4", Dato = DateTime.Today}
                };

                foreach (var guest in guests)
                {
                    context.Guests.Add(guest);
                }
                context.SaveChanges();
            }
        }

        [Test]
        public void Add_GuestValid_GuestAdded()
        {
            DateTime date = DateTime.Today;

            Guest guest = new Guest
            {
                Name = "Jonathan Gjøl",
                Company = "SKY-LIGHT",
                Country = "Danmark",
                Dato = date
            };

            IGuestRepository repo = CreateRepository();
            var addedGuest = repo.Add(guest);
            Assert.AreEqual(addedGuest, guest);
        }

        [Test]
        public void ReadAll_3GuestInDatabase_ReturnGuests()
        {
            IGuestRepository repo = CreateRepository();
            DateTime date = new DateTime(2018, 2, 25, 0, 0, 0);
            Guest guest = new Guest
            {
                Name = "Jonathan Gjøl 2",
                Company = "SKY-LIGHT 2",
                Country = "Danmark2",
                Dato = date
            };
            repo.Add(guest);


            IEnumerable<Guest> guests = repo.ReadAll();
            Assert.AreEqual(3, guests.Count());
        }

        [Test]
        public void ReadTodaysGuests_2GuestToday_Return2Guests()
        {
            IGuestRepository repo = CreateRepository();
            
            IEnumerable<Guest> guests = repo.ReadTodaysGuests();
            Assert.AreEqual(2, guests.Count());
        }

        public void DeleteGuests()
        {
            IGuestRepository repo = CreateRepository();

            IEnumerable<Guest> guests = repo.ReadAll();
            foreach (var guest in guests)
            {
                repo.Delete(guest);
            }
        }

        [Test]
        public void Delete_DeleteGuestsInDatabase_Return0Guests()
        {
            DeleteGuests();
            var builder = new DbContextOptionsBuilder<SKYINTRA_DBContext>().UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=SKY-INTRA_DB_TEST;Integrated Security=True;");

            using (var context = new SKYINTRA_DBContext(builder.Options))
            {
                IGuestRepository guestRep = new GuestRepository(context);
                IEnumerable<Guest> guestsAfterDelete = guestRep.ReadAll();
                Assert.AreEqual(0, guestsAfterDelete.Count());
            }
        }

        public IGuestRepository CreateRepository()
        {
            var builder = new DbContextOptionsBuilder<SKYINTRA_DBContext>()
                .UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=SKY-INTRA_DB_TEST;Integrated Security=True;");
            var context = new SKYINTRA_DBContext(builder.Options);
            DBInitializer.Initialize(context);
            IGuestRepository guestRep = new GuestRepository(context);
            return guestRep;
        }

    }
}
