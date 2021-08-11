using System;
using Tsi.Template.Core.Abstractions;

namespace Tsi.Template.Domain.Common
{
    /// <summary>
    /// Represents a log record
    /// </summary>
    public partial class Log : BaseEntity, ICommonEntity
    {
        public int LogLevelId { get; set; }
        public string ShortMessage { get; set; }
        public string FullMessage { get; set; }
        public string IpAddress { get; set; }
        public int? CustomerId { get; set; }
        public string PageUrl { get; set; }
        public string ReferrerUrl { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public LogLevel LogLevel
        {
            get => (LogLevel)LogLevelId;
            set => LogLevelId = (int)value;
        }
    }
}
