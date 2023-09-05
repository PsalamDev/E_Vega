using System.Collections.ObjectModel;
using Vega.Models;

namespace Vega.Controllers.Resources
{
    public class VehicleResources
    {

        public int Id { get; set; } 
        public KeyValuePairResources  Model { get; set; }
        public KeyValuePairResources Make { get; set; }
        public bool isRegistered { get; set; }
        public ContactResources Contact { get; set; }
        public DateTime LastUpdate { get; set; }
        public ICollection<KeyValuePairResources> Features { get; set; }
         
        public VehicleResources()
        {
            Features = new Collection<KeyValuePairResources>();
        }
    }
}
