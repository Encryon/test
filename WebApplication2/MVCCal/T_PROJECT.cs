//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCCal
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_PROJECT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_PROJECT()
        {
            this.T_CONSULTANT = new HashSet<T_CONSULTANT>();
        }
    
        public int PROJECT_ID { get; set; }
        public string PROJECT_NAME { get; set; }
        public bool PROJECT_ACTIF { get; set; }
        public Nullable<System.DateTime> PROJECT_CREATION { get; set; }
        public Nullable<System.DateTime> PROJECT_UPDATE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_CONSULTANT> T_CONSULTANT { get; set; }
    }
}
