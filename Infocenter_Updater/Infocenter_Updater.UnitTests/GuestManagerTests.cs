using DTOModel.DomainModel;
using Infocenter_Updater.BLL.Implementation;
using Infocenter_Updater.DAL.Repository.Abstraction;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Infocenter_Updater.UnitTests
{
    [TestFixture]
    public class GuestManagerTests
    {

        [Test]
        public void GetTodaysGuest_GuestsEqualsCache_ReturnsNull()
        {
            GuestManager manager = CreateGuestManager();
            List<Guest> guests1 = manager.GetTodaysGuest();
            List<Guest> guests2 = manager.GetTodaysGuest();

            Assert.IsNull(guests2);
        }

        [Test]
        public void GetTodaysGuest_GuestsIsNotEqualsCache_Returns3Guests()
        {
            GuestManager manager = CreateGuestManager();
            List<Guest> guests = manager.GetTodaysGuest();
            Assert.AreEqual(3, guests.Count);
        }

        private GuestManager CreateGuestManager()
        {
            var guests = new Guest[]
               {
                    new Guest {Name = "Guest1", Company = "Company1", Country = "Danmark", Dato = DateTime.Today},
                    new Guest {Name = "Guest2", Company = "Company2", Country = "Italien", Dato = DateTime.Today},
                    new Guest {Name = "Guest3", Company = "SKY-LIGHT", Country = "Italien", Dato = DateTime.Today}
               };

            // Fake GuestRepository using NSubstitute
            IGuestRepository guestRepository = Substitute.For<IGuestRepository>();
            guestRepository.ReadAllTodaysGuests().Returns(guests);

            return new GuestManager(guestRepository);
        }
    }
}
