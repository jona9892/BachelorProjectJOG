using DomainModel;
using SKYINTRA_RestAPI.BLL.Abstraction;
using SKYINTRA_RestAPI.DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYINTRA_RestAPI.BLL.Implementation
{
    public class GuestManager : IGuestManager
    {
        private readonly IGuestRepository guestRep;

        public GuestManager(IGuestRepository gr)
        {
            guestRep = gr ?? throw new ArgumentNullException(nameof(gr));
        }

        public Guest AddGuest(Guest guest)
        {
            DateTime todaysDate = DateTime.Today.Date;

            if(guest.Dato < todaysDate)
            {
                return null;
            }
            else
            {
                Guest addedGuest = guestRep.Add(guest);
                return addedGuest;
            }
        }
    }
}
