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
    
    public partial class Analyzer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Analyzer()
        {
            this.Order_Service = new HashSet<Order_Service>();
        }
    
        public int ID { get; set; }
        public System.DateTime received_datetime { get; set; }
        public System.DateTime completion_date { get; set; }
        public int execution_time { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Service> Order_Service { get; set; }
    }
}
