namespace ReservationProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ContactU
    {
        [Key]
        public int CONTACTUS_ID { get; set; }

        public int CUSTOMER_FIDDD { get; set; }

        [Required]
        [StringLength(50)]
        public string CONTACTUS_EMAIL { get; set; }

        [StringLength(2000)]
        public string CONTACTUS_MESSAGE { get; set; }

        public DateTime? CONTACTUS_DATE { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
