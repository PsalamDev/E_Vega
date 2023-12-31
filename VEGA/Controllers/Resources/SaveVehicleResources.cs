﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Vega.Models;

namespace Vega.Controllers.Resources
{
    public class SaveVehicleResources
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }

        [Required]
        public ContactResources Contact { get; set; }
        public ICollection<int> Features { get; set; }

        public SaveVehicleResources()
        {
            Features = new Collection<int>();
        }
     }
    
}

