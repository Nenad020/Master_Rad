//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MesDbAccess.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class AlarmMe
    {
        public int Id { get; set; }
        public int BreakerId { get; set; }
        public System.DateTime Timestamp { get; set; }
        public string Message { get; set; }
    }
}