namespace ReservationProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Resturant")]
    public partial class Resturant
    {
        [Key]
        public int RESTURANT_ID { get; set; }

        [StringLength(50)]
        public string RESTURANT_NAME { get; set; }

        [StringLength(50)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid Email Format")]
        public string RESTURANT_EMAIL { get; set; }

        [StringLength(50)]
        [RegularExpression(@"^\+[1-9]{1}[0-9]{3,14}$", ErrorMessage = "Numbers Only")]
        public string RESTURANT_CONTACT { get; set; }

        [StringLength(200)]
        public string RESTURANT_ADDRESS { get; set; }

        [StringLength(200)]
        public string RESTURANT_LOGO { get; set; }

        [NotMapped]
        public HttpPostedFileBase RESTURANT_PIC { get; set; }



        public bool IS_VALID { get; set; }
    }
}
