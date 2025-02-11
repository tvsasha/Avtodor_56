//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Avtodor_56
{
    using System;
    using System.Collections.Generic;
    
    public partial class Purchases
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Purchases()
        {
            this.PurchaseDetails = new HashSet<PurchaseDetails>();
        }
    
        public int PurchaseID { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public decimal TotalAmount { get; set; }
        public System.DateTime PurchaseDate { get; set; }
    
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseDetails> PurchaseDetails { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
