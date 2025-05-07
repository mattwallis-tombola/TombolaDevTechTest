using MediatR;
using TombolaDevTechTest.Models;

namespace TombolaDevTechTest.CoffeeBeans.Queries.GetCurrentBeanOfTheDay;

internal sealed class GetCurrentBeanOfTheDayQuery : IRequest<CoffeeBean?>
{
}
