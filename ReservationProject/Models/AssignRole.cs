namespace ReservationProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AssignRole")]
    public partial class AssignRole
    {
        [Key]
        public int ASSIGN_ROLE_ID { get; set; }

        public int ADMIN_FID { get; set; }

        public int ROLE_FID { get; set; }

        public virtual Admin Admin { get; set; }

        public virtual RoleAdm RoleAdm { get; set; }
    }
}
