using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.Shared.Models
{
    public interface IResultMessage<T>
    {
        T Data { get; set; }
        bool Success { get; set; }
        Exception Error { get; set; }
        string FriendlyErrorMessage { get; set; }
    }
}
