namespace ReservationProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RoleAdm")]
    public partial class RoleAdm
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RoleAdm()
        {
            AssignRoles = new HashSet<AssignRole>();
        }

        [Key]
        public int ROLEADMIN_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string AD_ROLE_NAME { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssignRole> AssignRoles { get; set; }
    }
}
