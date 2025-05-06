using MediatR;
using TombolaDevTechTest.Models;

namespace TombolaDevTechTest.CoffeeBeans.Queries.GetBeanById;

internal sealed class GetCoffeeBeanByIdQuery(string beanId) : IRequest<CoffeeBean?>
{
    internal string BeanId { get; } = beanId;
}
