using MediatR;
using TombolaDevTechTest.Models;
using TombolaDevTechTest.Repositories;

namespace TombolaDevTechTest.CoffeeBeans.Queries.GetCoffeeBeansByName;

internal class GetCoffeeBeansByNameHandler(ICoffeeBeanRepository coffeeBeanRepository) : IRequestHandler<GetCoffeeBeansByNameQuery, IList<CoffeeBean>?>
{
    private readonly ICoffeeBeanRepository _coffeeBeanRepository = coffeeBeanRepository;

    public async Task<IList<CoffeeBean>?> Handle(GetCoffeeBeansByNameQuery request, CancellationToken cancellationToken)
    {
        return await _coffeeBeanRepository.GetCoffeeBeansByNameAsync(request.BeanName);
    }
}
