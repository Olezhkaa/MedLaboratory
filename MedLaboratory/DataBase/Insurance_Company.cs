//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MedLaboratory.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Insurance_Company
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Insurance_Company()
        {
            this.Invoice = new HashSet<Invoice>();
            this.Patient = new HashSet<Patient>();
        }
    
        public int ID { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string INN { get; set; }
        public string Payment_account { get; set; }
        public string Bic { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patient> Patient { get; set; }
    }
}
