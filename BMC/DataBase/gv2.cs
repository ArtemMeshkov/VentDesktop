namespace BMC
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("public.gv2")]
    public partial class GV2
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(20)]
        public string contactor { get; set; }

        [StringLength(20)]
        public string auto { get; set; }

        public double lowcurrent { get; set; }

        public double highcurrent { get; set; }

        public double power { get; set; }
    }
}
