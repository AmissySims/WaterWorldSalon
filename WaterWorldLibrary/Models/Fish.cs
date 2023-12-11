//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WaterWorldLibrary.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Fish
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fish()
        {
            this.Busket = new HashSet<Busket>();
            this.OrderFish = new HashSet<OrderFish>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public Nullable<int> CountFish { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<int> TypeFishId { get; set; }
        public string Description { get; set; }
        public Nullable<int> AquariumId { get; set; }
    
        public virtual Aquarium Aquarium { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Busket> Busket { get; set; }
        public virtual TypeFish TypeFish { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderFish> OrderFish { get; set; }
    }
}
