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
    
    public partial class Employee_Service
    {
        public int ID { get; set; }
        public int ID_Employee { get; set; }
        public int ID_Service { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Service Service { get; set; }
    }
}
