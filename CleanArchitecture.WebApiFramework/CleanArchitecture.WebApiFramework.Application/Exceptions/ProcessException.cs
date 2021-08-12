using System;
using System.Collections.Generic;
using System.Text;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.WebApiFramework.Application.Wrappers;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApiFramework.Application.Exceptions
{
    public class ProcessException<T> //: Response<T>
    {
        Exception ee;
        Response<T> r;

        public ProcessException()
        {

        }
        public ProcessException(Exception e)
        {
            ee = e;
        }

        public Response<T> Process()
        {
            r = new Response<T>(ee.Message);

            if (ee.GetType() == typeof(UniqueConstraintException))
            {
                r.Message = "Clave Única o Primaria Duplicada";
            }
            if (ee.GetType() == typeof(CannotInsertNullException))
            {
                r.Message = "No se pueden insertar valores nulos en un campo que no lo permite";
            }
            if (ee.GetType() == typeof(MaxLengthExceededException))
            {
                r.Message = "Se excedió la longitud máxima permitida de un campo alfanumérico";
            }
            if (ee.GetType() == typeof(NumericOverflowException))
            {
                r.Message = "Overflow en campo numérico";
            }
            if (ee.GetType() == typeof(ReferenceConstraintException))
            {
                r.Message = "Referencia no encontrada. El elemento que se intenta registrar hace referencia a un entidad inexistente o bien se intenta eliminar una entidad que tiene otras referencias";
            }
            if (ee.GetType() == typeof(DbUpdateException))
            {
                r.Message = "Excepción no controlada";
            }

            r.Errors.Add(ee.InnerException.Message);
            r.StackTrace = ee.StackTrace;

            return r;

        }

    }
}

