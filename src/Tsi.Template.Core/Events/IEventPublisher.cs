using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsi.Template.Core.Events
{
    public partial interface IEventPublisher
    { 
        Task PublishAsync<TEvent>(TEvent @event);
    }
}
