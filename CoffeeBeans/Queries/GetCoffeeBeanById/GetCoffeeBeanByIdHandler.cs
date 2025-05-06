using MediatR;
using TombolaDevTechTest.Models;
using TombolaDevTechTest.Repositories;

namespace TombolaDevTechTest.CoffeeBeans.Queries.GetBeanById;

internal sealed class GetCoffeeBeanByIdHandler(ICoffeeBeanRepository repository) : IRequestHandler<GetCoffeeBeanByIdQuery, CoffeeBean?>
{
    private readonly ICoffeeBeanRepository _repository = repository;

    public async Task<CoffeeBean?> Handle(GetCoffeeBeanByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetBeanByIdAsync(request.BeanId);
    }
}
