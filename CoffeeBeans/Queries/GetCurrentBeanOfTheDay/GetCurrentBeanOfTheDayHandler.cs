using MediatR;
using TombolaDevTechTest.Models;
using TombolaDevTechTest.Repositories;

namespace TombolaDevTechTest.CoffeeBeans.Queries.GetCurrentBeanOfTheDay;

internal sealed class GetCurrentBeanOfTheDayHandler(ICoffeeBeanRepository repository) : IRequestHandler<GetCurrentBeanOfTheDayQuery, CoffeeBean?>
{
    private readonly ICoffeeBeanRepository _repository = repository;

    public async Task<CoffeeBean?> Handle(GetCurrentBeanOfTheDayQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetCurrentBeanOfTheDayAsync();
    }
}
