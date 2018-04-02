using DomainModel;
using SKYINTRA_RestAPI.DAL.Context;
using SKYINTRA_RestAPI.DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYINTRA_RestAPI.DAL.Repository.Implementation
{
    public class InfoscreenFileImageRepository : IManyToMany<Infoscreen, FileImage>
    {
        private SKYINTRA_DBContext ctx;

        public InfoscreenFileImageRepository(SKYINTRA_DBContext context)
        {
            ctx = context;
        }


        public void DeleteRelationship(int InfoscreenId, int FileImageId)
        {

            InfoscreenFileImage infoscreenFileImage = ctx.InfoscreenFileImages.FirstOrDefault(x => x.InfoscreenId == InfoscreenId && x.FileImageId == FileImageId);
            ctx.InfoscreenFileImages.Remove(infoscreenFileImage);
            ctx.SaveChanges();
        }

        public void InsertRelationship(int tId, int iId)
        {
            var fileImage = ctx.FileImages.Where(x => x.Id == iId).FirstOrDefault();
            var infoscreen = ctx.Infoscreens.Where(x => x.Id == tId).FirstOrDefault();

            InfoscreenFileImage newRelation = new InfoscreenFileImage();
            newRelation.Infoscreen = infoscreen;
            newRelation.InfoscreenId = tId;

            newRelation.FileImage = fileImage;
            newRelation.FileImageId = iId;

            ctx.InfoscreenFileImages.Add(newRelation);

            ctx.SaveChanges();
        }
    }
}
