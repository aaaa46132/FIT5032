//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FIT5032Assignment.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Activity
    {
        public System.DateTime DateTime { get; set; }
        public int StudentId { get; set; }
        public int StaffId { get; set; }
        public int ActivityId { get; set; }
    
        public virtual Staff Staff { get; set; }
        public virtual Student Student { get; set; }
    }
}
