using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.Shared.Models
{
   

        /// <summary>
        /// Object to hold the result from any service calls
        /// </summary>
        /// <remarks>
        /// Will only contain Exception data in debug mode. When in release mode make sure you
        /// log an exception using Ebiquity.Logger as we don't want ugly call stacks displayed when live.
        /// </remarks>
        public class ResultMessage<T> : IResultMessage<T>
        {
            public ResultMessage()
            {
                Success = true;
            }

            private Exception _exception;
            private string _friendErrorMsg;

            public T Data { get; set; }

            public bool Success { get; set; }

            //[ScriptIgnore]
            public Exception Error
            {
                get
                {
                    return Success == false ? _exception : null;
                }
                set
                {
                    _exception = value;
                    Success = false;
                }
            }

            public string FriendlyErrorMessage
            {
                get
                {
#if DEBUG
                    if (string.IsNullOrWhiteSpace(_friendErrorMsg))
                    {
                        return _exception != null ? _exception.Message : string.Empty;
                    }
#endif
                    return _friendErrorMsg;
                }
                set
                {
                    _friendErrorMsg = value;
                    Success = false;
                }
            }



        }

        public static class ResultMessage
        {
            public static ResultMessage<T> ToSuccess<T>(T result)
            {
                return new ResultMessage<T> { Success = true, Data = result };
            }

            public static ResultMessage<T> ToError<T>(Exception ex, string friendlyMessage = null)
            {
                return new ResultMessage<T> { Success = false, Error = ex, FriendlyErrorMessage = friendlyMessage };
            }
        }
    
}
