using MediatR;
using TombolaDevTechTest.Models;
using TombolaDevTechTest.Repositories;

namespace TombolaDevTechTest.CoffeeBeans.Queries.GetAllCoffeeBeans;

internal sealed class GetAllCoffeeBeansHandler(ICoffeeBeanRepository coffeeBeanRepository) : IRequestHandler<GetAllCoffeeBeansQuery, List<CoffeeBean>>
{
    private readonly ICoffeeBeanRepository _coffeeBeanRespository = coffeeBeanRepository;

    public async Task<List<CoffeeBean>> Handle(GetAllCoffeeBeansQuery request, CancellationToken cancellationToken)
    {
        return await _coffeeBeanRespository.GetAllBeansAsync();
    }
}
