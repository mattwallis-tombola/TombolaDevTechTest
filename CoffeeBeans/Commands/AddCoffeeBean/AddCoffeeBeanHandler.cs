using MediatR;
using TombolaDevTechTest.Models;
using TombolaDevTechTest.Repositories;

namespace TombolaDevTechTest.CoffeeBeans.Commands.AddCoffeeBean;

public sealed class AddCoffeeBeanHandler(ICoffeeBeanRepository coffeeBeanRepository) : IRequestHandler<AddCoffeeBeanCommand, CoffeeBean?>
{
    private readonly ICoffeeBeanRepository _coffeeBeanRepository = coffeeBeanRepository;

    public async Task<CoffeeBean?> Handle(AddCoffeeBeanCommand request, CancellationToken cancellationToken)
    {
        return await _coffeeBeanRepository.AddCoffeeBeanAsync(request.CoffeeBean, cancellationToken);
    }
}
