using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreCRUD.Models
{
    public class usuarioModel :BaseEntity
    {
       
        public int idusuario { get; set; }
        [Required]
        public string nombrecompleto { get; set; }
        [Required]
        public string iniciosesion{ get; set; }
        [Required]
        public string clave { get; set; }

        [Required]
        public DateTime fechanamimiento { get; set; }

        public Boolean activo { get; set; }

        public perfilModel perfil { get; set; }
    }
}
