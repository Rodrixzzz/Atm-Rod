using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Entities.Response
{
    public class CustomResponse<T>
    {
        public bool Succeded { get; set; } = true;
        public string Message { get; set; } = "The request was successful";

        public List<string>? Errors { get; set; }
        public T? Data { get; set; }


        public CustomResponse()
        {
        }

        public CustomResponse(string message)
        {
            Message = message;
        }
        public CustomResponse(string message, T? data)
        {
            Message = message;
            Data = data;
        }
        public CustomResponse(bool succeded, string message, List<string>? errors)
        {
            Succeded = succeded;
            Message = message;
            Errors = errors;
        }

        public CustomResponse(bool succeded, string message)
        {
            Succeded = succeded;
            Message = message;
        }
    }
}
