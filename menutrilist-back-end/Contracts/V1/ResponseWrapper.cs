using System.Collections.Generic;

namespace Menutrilist.Contracts.V1.Responses
{
    public class ResponseWrappers<T>
    {
        public T Data { get; set; }
        public bool Succeded { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}