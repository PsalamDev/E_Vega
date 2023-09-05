using AutoMapper;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Controllers.Resources;
using Vega.Models;
using Vega.Persistence;
using Vega.Persistence.Interfaces;
using Vega.Persistence.Repository;

namespace Vega.Controllers
{
    [Route("api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;
        public VehiclesController(IMapper mapper, IVehicleRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _vehicleRepository = repository;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResources vehicleResources)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var vehicle = _mapper.Map<SaveVehicleResources, Vehicle>(vehicleResources);
            vehicle.LastUpdate = DateTime.Now;

            _vehicleRepository.Add(vehicle);
            await _unitOfWork.CompleteAsync();

            vehicle = await _vehicleRepository.GetVehicle(vehicle.Id);


            var result = Mapper.Map<Vehicle, SaveVehicleResources>(vehicle);
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResources vehicleResources)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await _vehicleRepository.GetVehicle(id);


            if (vehicle == null)
                return NotFound();  

            _mapper.Map<SaveVehicleResources, Vehicle>(vehicleResources, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await _unitOfWork.CompleteAsync();

            vehicle = await _vehicleRepository.GetVehicle(vehicle.Id);

            var result = _mapper.Map<Vehicle, VehicleResources>(vehicle);

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _vehicleRepository.GetVehicle(id, vehicleisAvailable : false);

            if (vehicle == null)
                return NotFound();

            _vehicleRepository.Remove(vehicle);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await _vehicleRepository.GetVehicle(id);
            if (vehicle == null)
                return NotFound();

            var vehicleResources = _mapper.Map<Vehicle, SaveVehicleResources>(vehicle);
            return Ok(vehicleResources);

        }
    }
}
