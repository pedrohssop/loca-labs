using System;
using System.Collections.Generic;

namespace LocaLabs.Api.Models
{
    /// <summary>
    /// Base Output Api
    /// </summary>
    public class Output
    {
        /// <summary>
        /// Api response value
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Request exception
        /// </summary>
        public Exception Error { get; set; }

        /// <summary>
        /// True if request produce an unexpected error
        /// </summary>
        public bool IsRequestError { get; set; }

        /// <summary>
        /// True if request produce notification messages
        /// </summary>
        public bool HasNotifications { get; set; }

        /// <summary>
        /// Request notification messages
        /// </summary>
        public IEnumerable<string> Notifications { get; set; }
    }
}
