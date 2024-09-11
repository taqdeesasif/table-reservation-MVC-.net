namespace ReservationProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReservationDetail")]
    public partial class ReservationDetail
    {
        [Key]
        public int RESERVATIONDETAIL_ID { get; set; }

        public int RESERVATION_FID { get; set; }

        public int ITEM_FID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SALE_PRICE { get; set; }

        public int? QUANTITY { get; set; }

        public virtual Item Item { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}
