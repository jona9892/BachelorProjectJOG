using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYINTRA_RestAPI.DAL.Context
{
    public class DBInitializer 
    {
        public static void Initialize(SKYINTRA_DBContext context) 
        {
            context.Database.EnsureCreated();

            if (context.Guests.Any())
            {
                return;   // DB has been seeded
            }

            var guests = new Guest[]
            {
                new Guest {Name = "Jonathan", Company = "SKY-LIGHT", Country = "Danmark", Dato = DateTime.Now.AddDays(1)},
                new Guest {Name = "Jonathan Niano Bastida Gjøl", Company = "SKY-LIGHT", Country = "Italien", Dato = DateTime.Now.AddDays(1)}
            };

            foreach (var guest in guests)
            {
                context.Guests.Add(guest);
            }
            context.SaveChanges();

            //var infoscreens = new Infoscreen[]
            //{
            //    new Infoscreen {Id = 1, Name = "Kundeservice" },
            //    new Infoscreen {Id = 2, Name = "Kantine" },
            //    new Infoscreen {Id = 3, Name = "Ekstrudering" },
            //    new Infoscreen {Id = 4, Name = "Termoform" }
            //};

            //foreach (var infoscreen in infoscreens)
            //{
            //    context.Infoscreens.Add(infoscreen);
            //}

            //context.SaveChanges();

            //var infoscreenInformations = new InfoscreenInformation[]
            //{
            //    new InfoscreenInformation {InfoscreenId = 2, InformationId = 1},
            //    new InfoscreenInformation {InfoscreenId = 2, InformationId = 2}
            //};

            //foreach (var infoscreenInformation in infoscreenInformations)
            //{
            //    context.InfoscreenInformations.Add(infoscreenInformation);
            //}

            //context.SaveChanges();
        }
    }
}
