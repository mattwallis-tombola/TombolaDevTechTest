using MediatR;
using TombolaDevTechTest.CoffeeBeans.Commands.UpdateBeanOfTheDay;
using TombolaDevTechTest.CoffeeBeans.Queries.GetCurrentBeanOfTheDay;
using TombolaDevTechTest.Models;

namespace TombolaDevTechTest.CoffeeBeans.Queries.GetOrCreateBeanOfTheDay;

public sealed class GetOrCreateBeanOfTheDayHandler(IMediator mediator) : IRequestHandler<GetOrCreateBeanOfTheDayQuery, CoffeeBean?>
{
    private readonly IMediator _mediator = mediator;

    public async Task<CoffeeBean?> Handle(GetOrCreateBeanOfTheDayQuery request, CancellationToken cancellationToken)
    {
        var bean = await _mediator.Send(new GetCurrentBeanOfTheDayQuery(), cancellationToken);

        if (bean == null)
        {
            var updated = await _mediator.Send(new UpdateBeanOfTheDayCommand(), cancellationToken);
            if (updated)
            {
                bean = await _mediator.Send(new GetCurrentBeanOfTheDayQuery(), cancellationToken);
            }
        }

        return bean;
    }
}
