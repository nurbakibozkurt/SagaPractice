using System.Runtime.Serialization;

namespace Shared
{
    [Serializable]
    public class BaseCorrelation
    {
        public BaseCorrelation() { }
        public BaseCorrelation(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        [DataMember]
        public Guid CorrelationId { get; set; }
    }
}
