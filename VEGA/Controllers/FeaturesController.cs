using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Controllers.Resources;
using Vega.Models;
using Vega.Persistence;

namespace Vega.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly VegaDbContext _vegaDbContext;
        private readonly IMapper _mapper;
     
       public FeaturesController(VegaDbContext dbContext, IMapper mapper)
       {
            _mapper = mapper;
            _vegaDbContext = dbContext;
       }

        [HttpGet("/api/features")]
        public async Task<IEnumerable<KeyValuePairResources>> GetFeatures()
        {
            var features = await _vegaDbContext.Features.ToListAsync(); 

            return _mapper.Map<List<Feature>, List<KeyValuePairResources>>(features);
        }
    }
}
