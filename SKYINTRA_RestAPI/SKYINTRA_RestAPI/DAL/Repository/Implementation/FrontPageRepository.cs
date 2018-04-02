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
    public class FrontPageRepository : IFrontPageRepository
    {
        private SKYINTRA_DBContext ctx;

        public FrontPageRepository(SKYINTRA_DBContext context)
        {
            ctx = context;
        }

        public FrontPage GetFirst()
        {
            FrontPage frontpage = ctx.FrontPage.Include(x => x.EkstruderingFileImage).Include(x => x.TermoformFileImage).FirstOrDefault();
            return frontpage;
        }

        public FrontPage Update(FrontPage t)
        {
            var frontPageDB = ctx.FrontPage.FirstOrDefault(g => g.Id == t.Id);
            var fileImageEkstrud = ctx.FileImages.FirstOrDefault(x => x.Id == t.EkstruderingFileImage.Id);
            var fileImageTermo = ctx.FileImages.FirstOrDefault(x => x.Id == t.TermoformFileImage.Id);
            frontPageDB.EkstruderingFileImage = t.EkstruderingFileImage;
            frontPageDB.TermoformFileImage = t.TermoformFileImage;
            

            ctx.SaveChanges();
            return frontPageDB;
        }
    }
}
