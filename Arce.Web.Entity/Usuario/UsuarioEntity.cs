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
        public string? Usr_Doc_Nro { get; set; }
        public int? Usr_Cen_Cos_Id { get; set; }
        public int? Cen_Cos_Id { get; set; }
        public string? Cen_Cos_Des { get; set; }
        public string? Existe { get; set; }
        public string? Respuesta { get; set; }
        public string? Usr_Pass { get; set; }
        public string? Usr_Apr { get; set; }
        public string? Usr_Corr { get; set; }
        public string? Usr_Prf { get; set; }
        public int? Usr_Crg { get; set; }
        public string? Cargo_Nombre { get; set; }
    }
}