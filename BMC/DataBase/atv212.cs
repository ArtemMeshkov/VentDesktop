namespace BMC
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("public.atv212")]
    public partial class ATV212
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        
        public double pumppower { get; set; }
        [StringLength(20)]
        public string pch { get; set; }

        [StringLength(20)]
        public string auto { get; set; }
        
        [StringLength(20)]
        public string contactor { get; set; }

        public double elecpower { get; set; }

       

    }
}
