namespace ReservationProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Blog")]
    public partial class Blog
    {
        [Key]
        public int BLOG_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string BLOG_TITLE { get; set; }

        [StringLength(200)]
        public string BLOG_PICTURE { get; set; }
        [NotMapped]
        public HttpPostedFileBase BLOG_PIC { get; set; }


        public DateTime? BLOG_DATE { get; set; }

        [StringLength(2000)]
        public string BLOG_DESCRIPTION { get; set; }

        public int ADMIN_FID { get; set; }

        public virtual Admin Admin { get; set; }
    }
}
