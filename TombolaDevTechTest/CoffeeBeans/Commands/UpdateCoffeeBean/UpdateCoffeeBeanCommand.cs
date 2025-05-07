using MediatR;
using TombolaDevTechTest.Models;

namespace TombolaDevTechTest.CoffeeBeans.Commands.UpdateCoffeeBean;

internal sealed class UpdateCoffeeBeanCommand(CoffeeBean coffeeBean) : IRequest<CoffeeBean?>
{
    internal CoffeeBean CoffeeBean { get; } = coffeeBean;
}
