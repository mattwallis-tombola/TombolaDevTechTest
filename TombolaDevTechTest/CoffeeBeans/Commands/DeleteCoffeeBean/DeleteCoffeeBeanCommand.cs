using MediatR;

namespace TombolaDevTechTest.CoffeeBeans.Commands.DeleteCoffeeBean;

internal sealed class DeleteCoffeeBeanCommand(string id) : IRequest<bool>
{
    internal string Id { get; } = id;
}
