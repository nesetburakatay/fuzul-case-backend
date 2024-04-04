using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class CustomResponseDTO<T>
    {
        public CustomResponseDTO()
        {
            StatusCode = HttpStatusCode.OK;
            IsSuccsess= true;
            Errors = new List<string>();
        }
        public T Data { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public ICollection<string> Errors { get; set; }

        public bool IsSuccsess { get; set; }

        public CustomResponseDTO<T> SetData(T data)
        {
            Data = data;
            return this;
        }
        public CustomResponseDTO<T> SetStatusCode(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            return this;
        }
        public CustomResponseDTO<T> SetIsSuccsess(bool status)
        {
            IsSuccsess = status;
            return this;
        }

        public CustomResponseDTO<T> AddError(string error)
        {
            Errors.Add(error);
            return this;
        }


    }
}
