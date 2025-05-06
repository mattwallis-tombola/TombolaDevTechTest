using MediatR;
using TombolaDevTechTest.Models;
using TombolaDevTechTest.Repositories;

namespace TombolaDevTechTest.CoffeeBeans.Commands.UpdateCoffeeBean;

internal class UpdateCoffeeBeanHandler(ICoffeeBeanRepository coffeeBeanRepository) : IRequestHandler<UpdateCoffeeBeanCommand, CoffeeBean?>
{
    private readonly ICoffeeBeanRepository _coffeeBeanRepository = coffeeBeanRepository;

    public async Task<CoffeeBean?> Handle(UpdateCoffeeBeanCommand request, CancellationToken cancellationToken)
    {
        return await _coffeeBeanRepository.UpdateCoffeeBeanAsync(request.CoffeeBean, cancellationToken);
    }
}
