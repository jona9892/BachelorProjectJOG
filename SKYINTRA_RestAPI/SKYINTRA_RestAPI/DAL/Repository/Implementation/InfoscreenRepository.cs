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
    public class InfoscreenRepository : IInfoscreenRepository
    {
        private SKYINTRA_DBContext ctx;

        public InfoscreenRepository(SKYINTRA_DBContext context)
        {
            ctx = context;
        }

        public Infoscreen Read(int id)
        {
            Infoscreen infoscreen = ctx.Infoscreens.Where(c => c.Id == id).Include(x => x.EkstruderingFileImage).Include(x => x.TermoformFileImage).Include(x => x.InfoscreenFileImages).ThenInclude(y => y.FileImage).Include(x => x.InfoscreenInformations).ThenInclude(x => x.Information).FirstOrDefault();
            return infoscreen;
        }

        public Infoscreen Read(string name)
        {
            Infoscreen infoscreen = ctx.Infoscreens.Where(c => c.Name == name).Include(x => x.EkstruderingFileImage).Include(x => x.TermoformFileImage).Include(x => x.InfoscreenFileImages).ThenInclude(y => y.FileImage).Include(x => x.InfoscreenInformations).ThenInclude(x => x.Information).FirstOrDefault();
            return infoscreen;
        }

        public IEnumerable<Infoscreen> ReadAll()
        {
            IEnumerable<Infoscreen> infoscreens = ctx.Infoscreens.Include(x => x.EkstruderingFileImage).Include(x => x.TermoformFileImage).Include(x => x.InfoscreenFileImages).ThenInclude(y => y.FileImage).Include(x => x.InfoscreenInformations).ThenInclude(x=> x.Information).ToList();
            return infoscreens;
        }

        public Infoscreen Update(Infoscreen infoscreen)
        {
            var infoscreenDB = ctx.Infoscreens.FirstOrDefault(g => g.Id == infoscreen.Id);           
            infoscreenDB.Name = infoscreen.Name;
            infoscreen.RSSFeed = infoscreen.RSSFeed;
            ctx.SaveChanges();
            return infoscreenDB;
        }
    }
}
