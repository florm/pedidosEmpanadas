using System;
using System.Net;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace Exceptions
{
    [Serializable]
    public class PedidosEmpanadasException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        protected PedidosEmpanadasException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
            serializationInfo, streamingContext)
        {
        }

        public PedidosEmpanadasException()
        {
        }

        public PedidosEmpanadasException(string mensaje) : base(mensaje)
        {
            HttpStatusCode = HttpStatusCode.InternalServerError;
        }

        public PedidosEmpanadasException(string mensaje, HttpStatusCode httpStatusCode) : base(mensaje)
        {
            HttpStatusCode = httpStatusCode;
        }



    }
}
