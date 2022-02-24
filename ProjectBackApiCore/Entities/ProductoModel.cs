using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBackApiCore.Entities
{
    [Table("Producto")]
    public class ProductoModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id_Producto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public string Imagen_Url { get; set; }
        public bool Estado { get; set; }
        public DateTime Fc_reg { get; set; }
    }
}
