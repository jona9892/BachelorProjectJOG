using DomainModel;
using SKYINTRA_RestAPI.DAL.Context;
using SKYINTRA_RestAPI.DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYINTRA_RestAPI.DAL.Repository.Implementation
{
    public class InfoscreenInformationRepository : IManyToMany<Infoscreen, Information>
    {
        private SKYINTRA_DBContext ctx;

        public InfoscreenInformationRepository(SKYINTRA_DBContext context)
        {
            ctx = context;
        }

        public void DeleteRelationship(int InfoscreenId, int InformationId)
        {
            InfoscreenInformation infoscreeninformation = ctx.InfoscreenInformations.FirstOrDefault(x => x.InfoscreenId == InfoscreenId && x.InformationId == InformationId );
            ctx.InfoscreenInformations.Remove(infoscreeninformation);
            ctx.SaveChanges();
        }

        public void InsertRelationship(int tId, int iId)
        {
            var information = ctx.Informations.Where(x => x.Id == iId).FirstOrDefault();
            var infoscreen = ctx.Infoscreens.Where(x => x.Id == tId).FirstOrDefault();

            InfoscreenInformation newRelation = new InfoscreenInformation();
            newRelation.Infoscreen = infoscreen;
            newRelation.InfoscreenId = tId;

            newRelation.Information = information;
            newRelation.InformationId = iId;

            ctx.InfoscreenInformations.Add(newRelation);

            ctx.SaveChanges();
        }
    }
}
