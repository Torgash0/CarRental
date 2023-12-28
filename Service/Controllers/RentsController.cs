using AutoMapper;

using BL;
using BL.Rents.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Service.Controllers.Entities.Rents;

namespace CarRental.Service.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class RentsController(IProvider<RentModel, RentsModelFilter> provider, IManager<RentModel, CreateRentModel> manager, IMapper mapper, ILogger logger) : ControllerBase
{
    private readonly IProvider<RentModel, RentsModelFilter> _provider = provider;
    private readonly IManager<RentModel, CreateRentModel> _manager = manager;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    [Authorize]
    [HttpGet]
    public IActionResult GetAllRents()
    {
        var rents = _provider.Get();

        return Ok(new RentsListResponse()
        {
            Rents = rents.ToList()
        });
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("filter")]
    public IActionResult GetFilteredRents([FromQuery] RentsFilter filter)
    {
        var rents = _provider.Get(_mapper.Map<RentsModelFilter?>(filter));

        return Ok(new RentsListResponse()
        {
            Rents = rents.ToList()
        });
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetRentInfo([FromRoute] Guid id)
    {
        try
        {
            var rent = _provider.GetInfo(id);

            return Ok(rent);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex.ToString());

            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult CreateRent([FromBody] CreateRentRequest request)
    {
        try
        {
            var rent = _manager.Create(_mapper.Map<CreateRentModel>(request));

            return Ok(rent);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex.ToString());

            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult UpdateRentInfo([FromRoute] Guid id, UpdateRentRequest request)
    {
        try
        {
            var rent = _provider.GetInfo(id);

            if (rent is null)
            {
                return NotFound($"Rent with ID {id} not found.");
            }

            _mapper.Map(request, rent);

            var updatedRent = _manager.Update(id, rent);

            return Ok(updatedRent);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex.ToString());

            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteRent([FromRoute] Guid id)
    {
        try
        {
            var rent = _provider.GetInfo(id);

            if (rent is null)
            {
                return NotFound($"Rent with ID {id} not found.");
            }

            _manager.Delete(id);

            return Ok($"Rent with ID {id} deleted successfully.");
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex.ToString());

            return BadRequest(ex.Message);
        }
    }
}
