//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eBuy.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.OnlineListingOwns = new HashSet<OnlineListingOwn>();
        }
    
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdBranch { get; set; }
        public int Document { get; set; }
        public string Name { get; set; }
        public System.DateTime BornDate { get; set; }
        public int Phone { get; set; }
        public string AssignedBranch { get; set; }

        [JsonIgnore]
        public virtual Branch Branch { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<OnlineListingOwn> OnlineListingOwns { get; set; }
    }
}
