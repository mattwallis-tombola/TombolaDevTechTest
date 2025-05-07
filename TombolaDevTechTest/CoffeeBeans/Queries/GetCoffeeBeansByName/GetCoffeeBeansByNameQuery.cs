using MediatR;
using TombolaDevTechTest.Models;

namespace TombolaDevTechTest.CoffeeBeans.Queries.GetCoffeeBeansByName;

internal class GetCoffeeBeansByNameQuery(string beanName) : IRequest<IList<CoffeeBean>?>
{
    internal string BeanName { get; } = beanName;
}
