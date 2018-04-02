using DTOModel.DomainModel;
using Infocenter_Updater.DAL.Context;
using Infocenter_Updater.DAL.Repository.Abstraction;
using Infocenter_Updater.DAL.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Infocenter_Updater.IntegrationTests
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
            var builder = new DbContextOptionsBuilder<SKYINTRA_DBContext>().UseSqlServer(@"Data Source=localhost\SQLEXPRESS;
                                                                  Initial Catalog=SKY-INTRA_DB_TEST;Integrated Security=True;");
            using (var context = new SKYINTRA_DBContext(builder.Options))
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE Guest");

                var guests = new Guest[]
                {
                    new Guest {Name = "Guest1", Company = "Company1", Country = "Danmark", Dato = DateTime.Today},
                    new Guest {Name = "Guest2", Company = "Company2", Country = "Italien", Dato = DateTime.Today},
                    new Guest {Name = "Guest3", Company = "SKY-LIGHT", Country = "Italien", Dato = DateTime.Today}
                };
                context.Guests.AddRange(guests);
                context.SaveChanges();
            }
        }


        [Test]
        public void ReadAllTodaysGuest_3GuestInDatabase_Return3Guests()
        {
            IGuestRepository repo = CreateRepository();

            IEnumerable<Guest> guests = repo.ReadAllTodaysGuests();
            Assert.AreEqual(3, guests.Count());
        }

        public IGuestRepository CreateRepository()
        {
            var builder = new DbContextOptionsBuilder<SKYINTRA_DBContext>()
                .UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=SKY-INTRA_DB_TEST;Integrated Security=True;");
            var context = new SKYINTRA_DBContext(builder.Options);

            IGuestRepository guestRep = new GuestRepository(context);
            return guestRep;
        }
    }
}
