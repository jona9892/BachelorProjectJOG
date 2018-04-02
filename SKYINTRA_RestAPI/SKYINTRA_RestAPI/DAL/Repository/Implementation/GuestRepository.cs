
using DomainModel;
using Microsoft.EntityFrameworkCore;
using SKYINTRA_RestAPI.DAL.Context;
using SKYINTRA_RestAPI.DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYINTRA_RestAPI.DAL.Repository.Implementation
{
    public class GuestRepository : IGuestRepository
    {

        private SKYINTRA_DBContext ctx;

        public GuestRepository(SKYINTRA_DBContext context)
        {
            ctx = context;
        }

        public Guest Add(Guest t)
        {
            Guest newGuest = ctx.Guests.Add(t).Entity;
            ctx.SaveChanges();
            return newGuest;
                
        }

        public void Delete(Guest t)
        {
            Guest guest = ctx.Guests.FirstOrDefault(x => x.Id == t.Id);
            ctx.Guests.Remove(guest);
            ctx.SaveChanges();
        }

        public Guest Read(int id)
        {
            Guest guest = ctx.Guests.Where(c => c.Id == id).FirstOrDefault();
            return guest;
        }

        public IEnumerable<Guest> ReadAll()
        {
            IEnumerable<Guest> guests = ctx.Guests.ToList();
            return guests;
        }

        public IEnumerable<Guest> ReadTodaysGuests()
        {
            DateTime today = DateTime.Today;
            IEnumerable<Guest> guests = ctx.Guests.Where(x => x.Dato == today).ToList();
            return guests;
        }

        public Guest Update(Guest t)
        {
            var guestDB = ctx.Guests.FirstOrDefault(g => g.Id == t.Id);
            guestDB.Name = t.Name;
            guestDB.Company = t.Company;
            guestDB.Country = t.Country;
            guestDB.Dato = t.Dato;

            ctx.SaveChanges();
            return guestDB;
        }
    }
}
