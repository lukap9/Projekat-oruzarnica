﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Projekat.Context
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OruzarnicaEntities : DbContext
    {
        public OruzarnicaEntities()
            : base("name=OruzarnicaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<IsporukaMunicije> IsporukaMunicijes { get; set; }
        public virtual DbSet<IsporukaOruzja> IsporukaOruzjas { get; set; }
        public virtual DbSet<KorisnickiPodaci> KorisnickiPodacis { get; set; }
        public virtual DbSet<KupovinaMunicije> KupovinaMunicijes { get; set; }
        public virtual DbSet<KupovinaOruzja> KupovinaOruzjas { get; set; }
        public virtual DbSet<Logovanje> Logovanjes { get; set; }
        public virtual DbSet<Municija> Municijas { get; set; }
        public virtual DbSet<Oruzjee> Oruzjees { get; set; }
        public virtual DbSet<SkladisteMunicije> SkladisteMunicijes { get; set; }
        public virtual DbSet<SkladisteOruzja> SkladisteOruzjas { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}