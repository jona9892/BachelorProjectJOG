using DTOModel.DomainModel;
using Infocenter_Updater.BLL.Abstraction;
using Infocenter_Updater.DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infocenter_Updater.BLL.Implementation
{
    public class GuestManager : IGuestManager
    {
        public List<Guest> cachedGuest;
        private readonly IGuestRepository guestRep;

        public GuestManager(IGuestRepository gr)
        {
            guestRep = gr;
            cachedGuest = new List<Guest>();
        }

        public List<Guest> GetTodaysGuest()
        {
            List<Guest> todaysGuest = guestRep.ReadAllTodaysGuests().ToList();

            if (!todaysGuest.OrderBy(x => x.Id).Select(x => x.Id).SequenceEqual(cachedGuest.OrderBy(x => x.Id).Select(x => x.Id)))
            {
                cachedGuest.Clear();

                foreach (var guest in todaysGuest)
                {
                    cachedGuest.Add(guest);
                }

                return cachedGuest;
            }
            return null;
        }
    }
}
