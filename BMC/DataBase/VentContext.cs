namespace BMC
{
    using System.Data.Entity;

    public class VentContext : DbContext
    {     
        public VentContext()
          : base("VentProperties")
        {
        }

        public DbSet<ATV212> ATVs212 { get; set; }

        public DbSet<GV2> GV2s { get; set; }

        public DbSet<LoadSwitch> LoadSwitches { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            
            base.OnModelCreating(builder);
        }
    }
}
