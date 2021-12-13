using System;

namespace SWI.SoftStock.Common.Dto2
{
    /// <summary>
    /// Response from service
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Code=0 - OK, else Error
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// If Code!=0 then Message contains error details
        /// </summary>
        public string Message { get; set; }

        public Guid UniqueId { get; set; }
    }

}