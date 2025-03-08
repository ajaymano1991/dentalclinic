namespace DentalClinicReservationAndManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Appointment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string PatientName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public string ReasonForAppointment { get; set; }
        public DateTime PreferredDateTime { get; set; }
    }
}
