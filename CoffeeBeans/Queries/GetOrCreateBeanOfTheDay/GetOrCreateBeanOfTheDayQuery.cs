using MediatR;
using TombolaDevTechTest.Models;

namespace TombolaDevTechTest.CoffeeBeans.Queries.GetOrCreateBeanOfTheDay;
public sealed class GetOrCreateBeanOfTheDayQuery : IRequest<CoffeeBean?>
{
}
