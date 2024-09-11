namespace ReservationProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Staff")]
    public partial class Staff
    {
        [Key]
        public int STAFF_ID { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s\.]*$", ErrorMessage = "Invalid Name")]
        public string STAFF_NAME { get; set; }

        [StringLength(200)]
        public string STAFF_PICTURE { get; set; }

        [NotMapped]
        public HttpPostedFileBase STAFF_PIC { get; set; }


        [StringLength(200)]
        public string SERVICES { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid Email Format")]
        public string STAFF_EMAIL { get; set; }

        [Required]
        [StringLength(50)]
        public string STAFF_ADDRESS { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^\+[1-9]{1}[0-9]{3,14}$", ErrorMessage = "Numbers Only")]
        public string STAFF_CONTACT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SALARY { get; set; }

        public bool IS_WORKING { get; set; }

        public int STAFFJOB_FID { get; set; }

        public virtual StaffJob StaffJob { get; set; }
    }
}
