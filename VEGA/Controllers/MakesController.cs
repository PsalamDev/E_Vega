using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Controllers.Resources;
using Vega.Models;
using Vega.Persistence;

namespace Vega.Controllers
{
    public class MakesController : Controller
    {

        private readonly VegaDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public MakesController(VegaDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet("Api/Makes")]
        public async Task<IEnumerable<MakeResources>> GetMakes()
        {
            var makes = await _dbContext.Makes.Include(m => m.Models).ToListAsync();
            return _mapper.Map<List<Make>, List<MakeResources>>(makes);
        }
    }
}
