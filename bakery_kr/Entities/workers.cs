//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace bakery_kr.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class workers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public workers()
        {
            this.sale = new HashSet<sale>();
        }
    
        public int id_worker { get; set; }
        public string f { get; set; }
        public string i { get; set; }
        public string o { get; set; }
        public Nullable<int> phone { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sale> sale { get; set; }
    }
}
