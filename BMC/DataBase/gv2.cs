namespace BMC
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("public.gv2")]
    public partial class GV2
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(20)]
        public string Contactor { get; set; }

        [StringLength(20)]
        public string Auto { get; set; }

        public double Lowcurrent { get; set; }

        public double Highcurrent { get; set; }

        public double Power { get; set; }
    }
}
