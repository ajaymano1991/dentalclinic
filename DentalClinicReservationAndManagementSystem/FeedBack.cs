//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class FeedBack
    {
        public long Id { get; set; }
        public string PatientName { get; set; }
        public string FeedbackText { get; set; }
        public int Rating { get; set; }
        public System.DateTime CreatedAt { get; set; }
    }
}
