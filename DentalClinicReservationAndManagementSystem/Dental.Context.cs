﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DentalClinicReservationAndManagementSystem
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DentalClinicEntities : DbContext
    {
        public DentalClinicEntities()
            : base("name=DentalClinicEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PatientRegister> PatientRegisters { get; set; }
        public virtual DbSet<Dentist> Dentists { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
    }
}
