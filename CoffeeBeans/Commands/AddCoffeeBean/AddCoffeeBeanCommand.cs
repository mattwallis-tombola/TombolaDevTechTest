using MediatR;
using TombolaDevTechTest.Models;

namespace TombolaDevTechTest.CoffeeBeans.Commands.AddCoffeeBean;

public class AddCoffeeBeanCommand(CoffeeBean coffeeBean) : IRequest<CoffeeBean>
{
    public CoffeeBean CoffeeBean { get; } = coffeeBean;
}
