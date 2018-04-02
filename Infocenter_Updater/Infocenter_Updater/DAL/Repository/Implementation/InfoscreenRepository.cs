using Infocenter_Updater.DAL.Context;
using Infocenter_Updater.DAL.Repository.Abstraction;
using DTOModel.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infocenter_Updater.DAL.Repository.Implementation
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
            Infoscreen infoscreen = ctx.Infoscreens.Where(c => c.Id == id).Include(x => x.EkstruderingFileImage).Include(x => x.TermoformFileImage).Include(x => x.InfoscreenInformations).ThenInclude(x => x.Information).FirstOrDefault();
            return infoscreen;
        }

        public Infoscreen Read(string name)
        {
            Infoscreen infoscreen = ctx.Infoscreens.Where(c => c.Name == name).Include(x => x.EkstruderingFileImage).Include(x => x.TermoformFileImage).Include(x => x.InfoscreenInformations).ThenInclude(x => x.Information).FirstOrDefault();
            return infoscreen;
        }

        public IEnumerable<Infoscreen> ReadAll()
        {
            IEnumerable<Infoscreen> infoscreens = ctx.Infoscreens.Include(x => x.EkstruderingFileImage).Include(x => x.TermoformFileImage).Include(x => x.InfoscreenInformations).ThenInclude(x => x.Information).ToList();
            return infoscreens;
        }
    }
}
