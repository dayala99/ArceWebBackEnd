using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Web.Entity.Usuario
{
    public class UsuarioEntity
    {
        public string? Usr_Id { get; set; }
        public string? Usr_Cod {get; set; }
        public string? Usr_Nom { get; set; }
        public string? Flg_Est { get; set; }
        public string? Usr_Reg { get; set; }
        public DateTime? Fec_Reg { get; set; }
        public string? Usr_Mod { get; set; }
        public DateTime? Fec_Mod { get; set; }
    }
}
