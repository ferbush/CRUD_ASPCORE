using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreCRUD.Models
{
    public class menuModel : BaseEntity
    {

        public int idmenu { get; set; }
        [Required]
        public string titulo { get; set; }
        [Required]
        public string url { get; set; }
        [Required]
        public string icono { get; set; }
        public Boolean activo { get; set; }
        public int idpadre { get; set; }


    }
}
