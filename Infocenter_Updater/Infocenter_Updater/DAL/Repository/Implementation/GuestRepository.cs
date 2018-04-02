using Infocenter_Updater.DAL.Context;
using Infocenter_Updater.DAL.Repository.Abstraction;
using DTOModel.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Infocenter_Updater.DAL.Repository.Implementation
{
    public class GuestRepository : IGuestRepository
    {
        private SKYINTRA_DBContext ctx;

        public GuestRepository(SKYINTRA_DBContext context)
        {
            ctx = context;
            
        }

        public IEnumerable<Guest> ReadAllTodaysGuests()
        {
            DateTime todaysdate = DateTime.Today;
            IEnumerable<Guest> guests = ctx.Guests.Where(g => g.Dato == todaysdate);
            return guests;
        }
    }
}
