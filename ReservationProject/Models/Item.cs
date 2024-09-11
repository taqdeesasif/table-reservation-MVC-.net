namespace ReservationProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Item")]
    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            ReservationDetails = new HashSet<ReservationDetail>();
        }

        [Key]
        public int ITEM_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ITEM_NAME { get; set; }

        [StringLength(100)]
        public string ITEM_DESCRIPTION { get; set; }

        [Column(TypeName = "numeric")]
        public decimal ITEM_SALEPRICE { get; set; }

        [StringLength(200)]
        public string ITEM_PICTURE { get; set; }

        [NotMapped]
        public HttpPostedFileBase ITEM_PIC { get; set; }
        [NotMapped]
        public int IT_QUANTITY { get; set; }


        public int ITEMCATEGORY_FID { get; set; }

        public virtual ItemCategory ItemCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReservationDetail> ReservationDetails { get; set; }
    }
}
