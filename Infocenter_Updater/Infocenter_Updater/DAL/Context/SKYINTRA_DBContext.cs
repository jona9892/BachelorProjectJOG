using DTOModel.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Infocenter_Updater.DAL.Context
{
    public class SKYINTRA_DBContext : DbContext
    {
        public SKYINTRA_DBContext(DbContextOptions<SKYINTRA_DBContext> options) : base(options)
        {
        }
        
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Information> Informations { get; set; }
        public DbSet<FileImage> FileImages { get; set; }
        public DbSet<Infoscreen> Infoscreens { get; set; }
        public DbSet<InfoscreenInformation> InfoscreenInformations { get; set; }
        public DbSet<InfoscreenFileImage> InfoscreenFileImages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guest>().ToTable("Guest");
            modelBuilder.Entity<Information>().ToTable("Information");
            modelBuilder.Entity<FileImage>().ToTable("FileImage");
            modelBuilder.Entity<Infoscreen>().ToTable("InfoScreen");


            modelBuilder.Entity<FileImage>()
                    .HasMany(t => t.TermoInfoscreens)
                    .WithOne(m => m.TermoformFileImage)
                    .HasForeignKey(x => x.TermoformFileImageId);

            modelBuilder.Entity<FileImage>()
                    .HasMany(t => t.EkstrudInfoscreens)
                    .WithOne(m => m.EkstruderingFileImage)
                    .HasForeignKey(x => x.EkstruderingFileImageId);


            modelBuilder.Entity<InfoscreenInformation>()
                .HasKey(ii => new { ii.InfoscreenId, ii.InformationId });

            modelBuilder.Entity<InfoscreenInformation>()
                .HasOne(ii => ii.Infoscreen)
                .WithMany(ii => ii.InfoscreenInformations)
                .HasForeignKey(ii => ii.InfoscreenId);

            modelBuilder.Entity<InfoscreenInformation>()
                .HasOne(ii => ii.Information)
                .WithMany(i => i.InfoscreenInformations)
                .HasForeignKey(ii => ii.InformationId);



            modelBuilder.Entity<InfoscreenFileImage>()
                .HasKey(ii => new { ii.InfoscreenId, ii.FileImageId });

            modelBuilder.Entity<InfoscreenFileImage>()
                .HasOne(ii => ii.Infoscreen)
                .WithMany(ii => ii.InfoscreenFileImages)
                .HasForeignKey(ii => ii.InfoscreenId);

            modelBuilder.Entity<InfoscreenFileImage>()
                .HasOne(ii => ii.FileImage)
                .WithMany(i => i.InfoscreenFileImages)
                .HasForeignKey(ii => ii.FileImageId);

        }
    }
}
