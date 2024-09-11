namespace ReservationProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reservation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reservation()
        {
            ReservationDetails = new HashSet<ReservationDetail>();
        }

        [Key]
        public int RESERVATION_ID { get; set; }

        public int TABCATEGORY_FID { get; set; }

        public DateTime? DATE_TIME { get; set; }

        [Column(TypeName = "date")]
        public DateTime CHECKIN_DATE { get; set; }

        [StringLength(50)]
        public string RESERVATION_STATUS { get; set; }

        [StringLength(50)]
        public string STATUS { get; set; }
        [StringLength(50)]
        public string ORDER_STATUS { get; set; }

        public TimeSpan CHECKIN_TIME { get; set; }

        public int? ADULTS { get; set; }

        public int? KIDS { get; set; }

        [StringLength(1000)]
        public string SPECIAL_REQUEST { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TOTAL_BILL { get; set; }

        public bool WITH_PREORDER { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s\.]*$", ErrorMessage = "Invalid Name")]
        public string CUSTOMER_NAME { get; set; }

        [StringLength(50)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid Email Format")]
        public string CUSTOMER_EMAIL { get; set; }

        [StringLength(50)]
      
        public string CUSTOMER_CONTACT { get; set; }

        [StringLength(100)]
        public string CUSTOMER_ADDRESS { get; set; }

        public int? CUSTOMER_FID { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReservationDetail> ReservationDetails { get; set; }

        public virtual TabCategory TabCategory { get; set; }
    }
}
