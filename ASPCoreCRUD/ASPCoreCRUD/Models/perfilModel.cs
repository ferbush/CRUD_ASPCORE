using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreCRUD.Models
{
    public class perfilModel : BaseEntity
    {
   
        public string idperfil { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public DateTime fechains { get; set; }
        [Required]
        public DateTime fechaact { get; set; }

        public Boolean activo { get; set; }

    }
}
