using MediatR;
using TombolaDevTechTest.Repositories;

namespace TombolaDevTechTest.CoffeeBeans.Commands.DeleteCoffeeBean;

internal sealed class DeleteCoffeeBeanHandler(ICoffeeBeanRepository coffeeBeanRepository) : IRequestHandler<DeleteCoffeeBeanCommand, bool>
{
    private readonly ICoffeeBeanRepository _coffeeBeanRepository = coffeeBeanRepository;
    public Task<bool> Handle(DeleteCoffeeBeanCommand request, CancellationToken cancellationToken)
    {
        return _coffeeBeanRepository.DeleteCoffeeBeanByIdAsync(request.Id, cancellationToken);
    }
}
