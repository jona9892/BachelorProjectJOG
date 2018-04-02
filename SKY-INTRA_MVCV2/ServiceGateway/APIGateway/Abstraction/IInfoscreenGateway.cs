using ServiceGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.APIGateway.Abstraction
{
    public interface IInfoscreenGateway
    {
        Infoscreen Read(int id);
        Infoscreen Read(string name);
        IEnumerable<Infoscreen> ReadAll();
        HttpResponseMessage Update(Infoscreen infoscreen);
        void InsertInformationRelation(InfoscreenInformation infoscreeninformation);
        void DeleteInformationRelation(InfoscreenInformation infoscreeninformation);
        void InsertFileImageRelation(InfoscreenFileImage infoscreenFileImage);
        void DeleteFileImageRelation(InfoscreenFileImage infoscreenFileImage);
    }
}
