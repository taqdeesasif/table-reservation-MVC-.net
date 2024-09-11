namespace ReservationProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model5 : DbContext
    {
        public Model5()
            : base("name=Model5")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<AssignRole> AssignRoles { get; set; }
        public virtual DbSet<Availibility> Availibilities { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<ContactU> ContactUs { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemCategory> ItemCategories { get; set; }
        public virtual DbSet<ReservationDetail> ReservationDetails { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Resturant> Resturants { get; set; }
        public virtual DbSet<RoleAdm> RoleAdms { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<StaffJob> StaffJobs { get; set; }
        public virtual DbSet<TabCategory> TabCategories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .HasMany(e => e.AssignRoles)
                .WithRequired(e => e.Admin)
                .HasForeignKey(e => e.ADMIN_FID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Admin>()
                .HasMany(e => e.Blogs)
                .WithRequired(e => e.Admin)
                .HasForeignKey(e => e.ADMIN_FID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Admin>()
                .HasMany(e => e.Feedbacks)
                .WithOptional(e => e.Admin)
                .HasForeignKey(e => e.ADMIN_FIDD);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.ContactUs)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.CUSTOMER_FIDDD)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Feedbacks)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.CUSTOMER_FIDD)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Reservations)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.CUSTOMER_FID);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.ReservationDetails)
                .WithRequired(e => e.Item)
                .HasForeignKey(e => e.ITEM_FID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ItemCategory>()
                .HasMany(e => e.Items)
                .WithRequired(e => e.ItemCategory)
                .HasForeignKey(e => e.ITEMCATEGORY_FID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reservation>()
                .HasMany(e => e.ReservationDetails)
                .WithRequired(e => e.Reservation)
                .HasForeignKey(e => e.RESERVATION_FID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoleAdm>()
                .HasMany(e => e.AssignRoles)
                .WithRequired(e => e.RoleAdm)
                .HasForeignKey(e => e.ROLE_FID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffJob>()
                .HasMany(e => e.Staffs)
                .WithRequired(e => e.StaffJob)
                .HasForeignKey(e => e.STAFFJOB_FID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TabCategory>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.TabCategory)
                .HasForeignKey(e => e.TABCATEGORY_FID)
                .WillCascadeOnDelete(false);
        }
    }
}
