using CleanArchitecture.WebApiFramework.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.WebApiFramework.Domain.Entities
{
    public class ClaveRegistro : AuditableBaseEntity
    {
        public ClaveRegistro()
        { }

     
       public string Clave1 { get; set; }
    
       public string Clave2 { get; set; }
    
       public string Clave3 { get; set; }
       
       public string Clave4 { get; set; }
     
       public string Clave5 { get; set; }
     
       public string Valor { get; set; }
     
       public string Comentario { get; set; }
     
       public string ValoresPosibles { get; set; }
     
       public string ValorPredeterminado { get; set; }

        public string FullPath()
        {
        
            const string SLASH = @"\";
            if (Clave1 != string.Empty && Clave2 != string.Empty && Clave3 != string.Empty && Clave4 != string.Empty && Clave5 != string.Empty)
            {
                return Clave1 + SLASH + Clave2 + SLASH + Clave3 + SLASH + Clave4 + SLASH + Clave5;
            }
            else if (Clave1 != string.Empty && Clave2 != string.Empty && Clave3 != string.Empty && Clave4 != string.Empty)
            {
                return Clave1 + SLASH + Clave2 + SLASH + Clave3 + SLASH + Clave4;
            }
            else if (Clave1 != string.Empty && Clave2 != string.Empty && Clave3 != string.Empty)
            {
                return Clave1 + SLASH + Clave2 + SLASH + Clave3;
            }
            else if (Clave1 != string.Empty && Clave2 != string.Empty)
            {
                return Clave1 + SLASH + Clave2;
            }
            else if (Clave1 != string.Empty)
            {
                return Clave1;
            }
            else return string.Empty;
        }

    }

}
