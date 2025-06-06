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
    
    public partial class InStoreSale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InStoreSale()
        {
            this.InStoreSaleDetails = new HashSet<InStoreSaleDetail>();
            this.InStoreSaleInvoices = new HashSet<InStoreSaleInvoice>();
        }
    
        public int Id { get; set; }
        public int IdSale { get; set; }
        public int IdBranch { get; set; }
        public string BranchName { get; set; }

        [JsonIgnore]
        public virtual Branch Branch { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<InStoreSaleDetail> InStoreSaleDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<InStoreSaleInvoice> InStoreSaleInvoices { get; set; }
        [JsonIgnore]
        public virtual Sale Sale { get; set; }
    }
}
