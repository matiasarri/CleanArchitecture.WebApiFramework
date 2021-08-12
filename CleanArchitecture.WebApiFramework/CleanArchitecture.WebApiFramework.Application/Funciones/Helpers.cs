using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using CleanArchitecture.WebApiFramework.Domain.Entities;

namespace CleanArchitecture.WebApiFramework.Application.Funciones
{
    public static class Helpers
    {

        public static ClaveRegistro EliminarAcentos(ClaveRegistro clave)
        {
            clave.Clave1 = Regex.Replace(clave.Clave1.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
            clave.Clave2 = Regex.Replace(clave.Clave2.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
            clave.Clave3 = Regex.Replace(clave.Clave3.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
            clave.Clave4 = Regex.Replace(clave.Clave4.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
            clave.Clave5 = Regex.Replace(clave.Clave5.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");

            return clave;
        }

    }
}
