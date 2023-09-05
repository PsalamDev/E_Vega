using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Vega.Models;

namespace Vega.Controllers.Resources
{
    public class MakeResources : KeyValuePairResources
    {
   
       public ICollection<KeyValuePairResources> Models { get; set; }

       public MakeResources()
       {
            Models = new Collection<KeyValuePairResources>();
        }
    }
}

