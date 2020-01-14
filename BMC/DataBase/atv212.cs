namespace BMC
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("public.atv212")]
    public partial class ATV212
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        
        public double PumpPower { get; set; }
        [StringLength(20)]
        public string Ach { get; set; }

        [StringLength(20)]
        public string Auto { get; set; }
        
        [StringLength(20)]
        public string Contactor { get; set; }

        public double Elecpower { get; set; }

       

    }
}
