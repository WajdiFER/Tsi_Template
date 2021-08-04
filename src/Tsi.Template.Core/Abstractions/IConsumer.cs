using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsi.Template.Core.Abstractions
{
    public interface IConsumer<T>
    {
        /// <summary>
        /// Handle event
        /// </summary>
        /// <param name="eventMessage">Event</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task HandleEventAsync(T eventMessage);
    }
}
