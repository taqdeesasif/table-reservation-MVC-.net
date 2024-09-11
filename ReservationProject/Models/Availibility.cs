namespace ReservationProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Availibility")]
    public partial class Availibility
    {
        [Key]
        public int AVAILABLE_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime UNAVAILABLE_DATE { get; set; }

        public TimeSpan? UNAVAILABLE_TIME { get; set; }
    }
}
