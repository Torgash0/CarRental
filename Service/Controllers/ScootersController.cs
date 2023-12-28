using AutoMapper;

using BL;
using BL.Cars.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Service.Controllers.Entities.Cars;
using Service.Controllers.Entities.Users;

namespace Service.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CarsController(IProvider<CarModel, CarsModelFilter> provider, IManager<CarModel, CreateCarModel> manager, IMapper mapper, ILogger logger) : ControllerBase
{
    private readonly IProvider<CarModel, CarsModelFilter> _provider = provider;
    private readonly IManager<CarModel, CreateCarModel> _manager = manager;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    [HttpGet]
    public IActionResult GetAllCars()
    {
        var Cars = _provider.Get();

        return Ok(new CarsListResponse()
        {
            Cars = Cars.ToList()
        });
    }

    [HttpGet]
    [Route("filter")]
    public IActionResult GetFilteredCars([FromQuery] CarsFilter filter)
    {
        var Cars = _provider.Get(_mapper.Map<CarsModelFilter>(filter));

        return Ok(new CarsListResponse()
        {
            Cars = Cars.ToList()
        });
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetCarInfo([FromRoute] Guid id)
    {
        try
        {
            var Car = _provider.GetInfo(id);

            return Ok(Car);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex.ToString());

            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult CreateCar([FromBody] CreateUserRequest request)
    {
        try
        {
            var Car = _manager.Create(_mapper.Map<CreateCarModel>(request));

            return Ok(Car);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex.ToString());

            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult UpdateCarInfo([FromRoute] Guid id, UpdateUserRequest request)
    {
        try
        {
            var Car = _provider.GetInfo(id);

            if (Car is null)
            {
                return NotFound($"Car with ID {id} not found.");
            }

            _mapper.Map(request, Car);

            var updatedCar = _manager.Update(id, Car);

            return Ok(updatedCar);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex.ToString());

            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteCar([FromRoute] Guid id)
    {
        try
        {
            var Car = _provider.GetInfo(id);

            if (Car is null)
            {
                return NotFound($"Car with ID {id} not found.");
            }

            _manager.Delete(id);

            return Ok($"Car with ID {id} deleted successfully.");
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex.ToString());

            return BadRequest(ex.Message);
        }
    }
}
