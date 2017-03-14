using NCSafety.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace NCSafety.DAL.NCSafetyEntities
{
    public class NCSafetyCFEntities : DbContext
    {
        public NCSafetyCFEntities() : base("name = NCSafetyCFEntities")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<Inspection> Inspections { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Hazard> Hazards { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<FAQ> FAQs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<NCSafety.Models.UploadedPhotos> UploadedPhotos { get; set; }
    }
}