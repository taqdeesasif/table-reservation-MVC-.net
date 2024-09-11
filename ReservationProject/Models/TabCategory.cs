namespace ReservationProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("TabCategory")]
    public partial class TabCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TabCategory()
        {
            Reservations = new HashSet<Reservation>();
        }

        [Key]
        public int TABCATEGORY_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string TABCATEGORY_NAME { get; set; }

        [StringLength(100)]
        public string TABCATEGORY_DESCRIPTION { get; set; }

        [StringLength(200)]
        public string TABCATEGORY_PICTURE { get; set; }


        [NotMapped]
        public HttpPostedFileBase TAB_PIC { get; set; }


        [Column(TypeName = "numeric")]
        public decimal TABCATEGORY_PRICE { get; set; }

        public bool VALIDATE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
