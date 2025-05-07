using MediatR;
using TombolaDevTechTest.Models;

namespace TombolaDevTechTest.CoffeeBeans.Queries.GetAllCoffeeBeans;

internal class GetAllCoffeeBeansQuery : IRequest<List<CoffeeBean>>;