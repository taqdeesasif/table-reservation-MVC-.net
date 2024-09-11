namespace ReservationProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Feedback")]
    public partial class Feedback
    {
        [Key]
        public int FEEDBACK_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string REVIEW_TYPE { get; set; }

        [StringLength(2000)]
        public string FEEDBACK_MESSAGE { get; set; }

        [StringLength(50)]
        public string FEEDBACK_EMAIL { get; set; }

        [StringLength(50)]
       
        public string FEEDBACK_CONTACT { get; set; }

        public DateTime? FEDDBACK_DATE { get; set; }

        public int CUSTOMER_FIDD { get; set; }

        public DateTime? REPLY_DATE { get; set; }

        [StringLength(2000)]
        public string RECEIVER_MESSAGE { get; set; }

        public int? ADMIN_FIDD { get; set; }

        public virtual Admin Admin { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
