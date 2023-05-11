namespace CRUDEntityFramework.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("plato")]
    public partial class plato
    {
        public int id { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }

        [Column(TypeName = "numeric")]
        [Display(Name = "Precio")]
        public decimal precio { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Imagen")]
        public string imagen { get; set; }

        [Display(Name = "Categoria")]
        public int idcategoria { get; set; }

        public virtual categoria categoria { get; set; }
    }
}
