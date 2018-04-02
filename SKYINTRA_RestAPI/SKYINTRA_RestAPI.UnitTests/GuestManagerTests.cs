using DomainModel;
using NSubstitute;
using NUnit.Framework;
using SKYINTRA_RestAPI.BLL.Implementation;
using SKYINTRA_RestAPI.DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace SKYINTRA_RestAPI.UnitTests
{
    public class GuestManagerTests
    {
        [Test]
        public void AddGuest_GuestDateIsBeforeToday_ReturnsNull()
        {            
            DateTime date = DateTime.Today.AddDays(-1);
            Guest guest = new Guest { Name = "Guest1", Company = "Company1", Country = "Country1", Dato = date };

            GuestManager manager = CreateGuestManager(guest);
            Guest result = manager.AddGuest(guest);
            Assert.IsNull(result);
        }

        [Test]
        public void AddGuest_Valid_ReturnsGuest()
        {
            DateTime date = DateTime.Today.AddDays(1);
            Guest guest = new Guest { Name = "Guest1", Company = "Company1", Country = "Country1", Dato = date };
            GuestManager manager = CreateGuestManager(guest);
            Guest result = manager.AddGuest(guest);
            Assert.AreEqual(result, guest);
        }

        private GuestManager CreateGuestManager(Guest guest)
        {

            // Fake GuestRepository using NSubstitute
            IGuestRepository guestRepository = Substitute.For<IGuestRepository>();
            guestRepository.Add(guest).Returns(guest);

            return new GuestManager(guestRepository);
        }
    }
}
