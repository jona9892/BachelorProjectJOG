using DomainModel;
using SKYINTRA_RestAPI.DAL.Context;
using SKYINTRA_RestAPI.DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SKYINTRA_RestAPI.DAL.Repository.Implementation
{
    public class InformationRepository : IInformationRepository
    {
        private SKYINTRA_DBContext ctx;

        public InformationRepository(SKYINTRA_DBContext context)
        {
            ctx = context;
        }

        public Information Add(Information t)
        {
            Information newInformation = ctx.Informations.Add(t).Entity;
            ctx.SaveChanges();
            return newInformation;
        }

        public void Delete(Information t)
        {
            Information information = ctx.Informations.FirstOrDefault(x => x.Id == t.Id);
            ctx.Informations.Remove(information);
            ctx.SaveChanges();
        }

        public IEnumerable<Information> GetLatestInforamtions()
        {
            IEnumerable<Information> informations = ctx.Informations.OrderByDescending( x => x.CreatedDate).Take(5).ToList();
            return informations;
        }

        public Information Read(int id)
        {
            Information information = ctx.Informations.Where(c => c.Id == id).FirstOrDefault();
            return information;
        }

        public IEnumerable<Information> ReadAll()
        {
            IEnumerable<Information> informations = ctx.Informations.ToList();
            return informations;
        }

        public Information Update(Information t)
        {
            var informationDB = ctx.Informations.FirstOrDefault(g => g.Id == t.Id);
            informationDB.Title = t.Title;
            informationDB.Header = t.Header;
            informationDB.Body = t.Body;
            informationDB.ModifiedDate = DateTime.Now;

            ctx.SaveChanges();
            return informationDB;
        }
    }
}
