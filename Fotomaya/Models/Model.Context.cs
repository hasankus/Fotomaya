﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fotomaya.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class fotomayaEntities : DbContext
    {
        public fotomayaEntities()
            : base("name=fotomayaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<A_Foto> A_Foto { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Ayar> Ayar { get; set; }
        public virtual DbSet<FotoDurum> FotoDurum { get; set; }
        public virtual DbSet<Fotograf> Fotograf { get; set; }
        public virtual DbSet<Fotograf_Kategori> Fotograf_Kategori { get; set; }
        public virtual DbSet<Hizmet> Hizmet { get; set; }
        public virtual DbSet<K_Foto> K_Foto { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Hakkimizda> Hakkimizda { get; set; }
    }
}
