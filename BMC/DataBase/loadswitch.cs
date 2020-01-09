namespace BMC
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("public.loadswitch")]
    public partial class LoadSwitch
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        public int PoleNumber { get; set; }

        public double MaxCurrent { get; set; }

        [StringLength(20)]
        public string Article { get; set; }


    }
}
