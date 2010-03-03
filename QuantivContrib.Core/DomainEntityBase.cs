using Quantiv.Runtime;

namespace QuantivContrib.Core
{
    public class DomainEntityBase
    {
        public Entity QuantivEntity { get; set; }
        public bool Dirty { get; set; }
    }
}