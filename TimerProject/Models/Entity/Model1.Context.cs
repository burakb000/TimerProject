﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TimerProject.Models.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TIMERPROJECTDBEntities : DbContext
    {
        public TIMERPROJECTDBEntities()
            : base("name=TIMERPROJECTDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TBLDATEDATAS> TBLDATEDATAS { get; set; }
        public virtual DbSet<TBLSYSTEMINFO> TBLSYSTEMINFO { get; set; }
    }
}
