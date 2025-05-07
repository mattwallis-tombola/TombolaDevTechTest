using MediatR;
using Microsoft.AspNetCore.Mvc;
using TombolaDevTechTest.CoffeeBeans.Commands.AddCoffeeBean;
using TombolaDevTechTest.CoffeeBeans.Commands.DeleteCoffeeBean;
using TombolaDevTechTest.CoffeeBeans.Commands.UpdateCoffeeBean;
using TombolaDevTechTest.CoffeeBeans.Queries.GetAllCoffeeBeans;
using TombolaDevTechTest.CoffeeBeans.Queries.GetBeanById;
using TombolaDevTechTest.CoffeeBeans.Queries.GetCoffeeBeansByName;
using TombolaDevTechTest.CoffeeBeans.Queries.GetOrCreateBeanOfTheDay;
using TombolaDevTechTest.Models;

namespace TombolaDevTechTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CoffeeBeansController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<List<CoffeeBean>>> GetAllBeans()
    {
        var beans = await _mediator.Send(new GetAllCoffeeBeansQuery());
        return Ok(beans);
    }

    [HttpGet]
    [Route("GetBeanById")]
    public async Task<ActionResult<CoffeeBean>> GetCoffeeBeanById([FromQuery] string id)
    {
        var bean = await _mediator.Send(new GetCoffeeBeanByIdQuery(id));
        if (bean == null)
            return NotFound();

        return Ok(bean);
    }

    [HttpGet]
    [Route("GetBeansByName")]
    public async Task<ActionResult<List<CoffeeBean>>?> GetCoffeeBeansByName([FromQuery] string name)
    {
        var beans = await _mediator.Send(new GetCoffeeBeansByNameQuery(name));
        return Ok(beans);
    }

    [HttpPost]
    public async Task<ActionResult> AddCoffeeBean([FromBody] CoffeeBean bean)
    {
        if (bean == null)
            return BadRequest("Bean cannot be null");

        try
        {
            var createdBean = await _mediator.Send(new AddCoffeeBeanCommand(bean));
            return CreatedAtAction(nameof(AddCoffeeBean), new { id = createdBean.Id }, createdBean);
        }
        catch (Exception)
        {
            return BadRequest("An error occurred while adding a new CoffeeBean. Please ensure you have provided a valid CoffeeBean.");
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateCoffeeBean([FromBody] CoffeeBean bean)
    {
        if (bean == null)
            return BadRequest("Bean cannot be null");

        try
        {
            var updatedBean = await _mediator.Send(new UpdateCoffeeBeanCommand(bean));
            if (updatedBean == null)
                return NotFound();

            return Ok(updatedBean);
        }
        catch (Exception)
        {
            return BadRequest($"An error occurred while updating the CoffeeBean(Id: {bean.Id}). Please ensure you have provided a valid CoffeeBean.");
        }
    }

    [HttpGet("BeanOfTheDay")]
    public async Task<ActionResult<CoffeeBean>> GetBeanOfTheDay()
    {
        var bean = await _mediator.Send(new GetOrCreateBeanOfTheDayQuery());
        if (bean == null)
            return NotFound();

        return Ok(bean);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCoffeeBean(string id)
    {
        var success = await _mediator.Send(new DeleteCoffeeBeanCommand(id));
        if (!success)
            return NotFound();

        return Ok();
    }
}
