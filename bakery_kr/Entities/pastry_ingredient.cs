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
    
    public partial class pastry_ingredient
    {
        public int id_pastry { get; set; }
        public int id_ingredient { get; set; }
        public Nullable<int> quantity { get; set; }
    
        public virtual ingredients ingredients { get; set; }
        public virtual Pastrys Pastrys { get; set; }
    }
}
