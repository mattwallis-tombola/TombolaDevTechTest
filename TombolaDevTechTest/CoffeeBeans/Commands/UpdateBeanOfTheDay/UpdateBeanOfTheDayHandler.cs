using MediatR;
using TombolaDevTechTest.Models;
using TombolaDevTechTest.Repositories;

namespace TombolaDevTechTest.CoffeeBeans.Commands.UpdateBeanOfTheDay;

internal sealed class UpdateBeanOfTheDayHandler(
    ICoffeeBeanRepository coffeeBeanRepository,
    ILogger<UpdateBeanOfTheDayHandler> logger) : IRequestHandler<UpdateBeanOfTheDayCommand, bool>
{
    private readonly ICoffeeBeanRepository _coffeeBeanRepository = coffeeBeanRepository;
    private readonly ILogger<UpdateBeanOfTheDayHandler> _logger = logger;
    private readonly Random _random = new();

    public async Task<bool> Handle(UpdateBeanOfTheDayCommand request, CancellationToken cancellationToken)
    {
        try
        {
            CoffeeBean? currentBeanOfTheDay = await _coffeeBeanRepository.GetCurrentBeanOfTheDayAsync();
            if (currentBeanOfTheDay != null)
            {
                _logger.LogInformation("Bean of the Day already exists for today: {BeanName}", currentBeanOfTheDay.Name);
                return false; // No update needed
            }

            var allBeans = await _coffeeBeanRepository.GetAllBeansAsync();
            var yesterdayBean = await _coffeeBeanRepository.GetLastBeanOfTheDayHistoryAsync(cancellationToken);

            // Handle the case where there are no beans in the database
            if (allBeans.Count == 0)
            {
                _logger.LogWarning("No beans available to set as Bean of the Day.");
                return false;
            }

            // Handle the case where there is only one bean in the database
            if (allBeans.Count == 1)
            {
                var bean = allBeans.First();
                if (yesterdayBean != null && bean.BeanId == yesterdayBean.BeanId)
                {
                    _logger.LogWarning("Only one bean exists, and it was the Bean of the Day yesterday.");
                    return false;
                }
                var beanOfTheDaySingular = new BeanOfTheDayHistory
                {
                    BeanId = bean.BeanId,
                    DateStamp = DateTime.UtcNow
                };
                return await AddBeanOfTheDayToContextAndSave(beanOfTheDaySingular, cancellationToken);
            }

            // Exclude yesterday's BeanOfTheDay
            var eligibleBeans = allBeans.Where(b => b.BeanId != yesterdayBean?.BeanId).ToList();
            // Step 3: Select a random bean
            var newBeanOfTheDay = eligibleBeans[_random.Next(eligibleBeans.Count)];

            return await AddBeanOfTheDayToContextAndSave(CreateBeanOfTheDayHistory(newBeanOfTheDay), cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the Bean of the Day.");
            return false;
        }
    }

    private static BeanOfTheDayHistory CreateBeanOfTheDayHistory(CoffeeBean newBeanOfTheDay)
    {
        return new BeanOfTheDayHistory
        {
            BeanId = newBeanOfTheDay.BeanId,
            DateStamp = DateTime.UtcNow
        };
    }

    private async Task<bool> AddBeanOfTheDayToContextAndSave(BeanOfTheDayHistory beanOfTheDayHistory, CancellationToken cancellationToken)
    {
        await _coffeeBeanRepository.AddBeanOfTheDayHistoryAsync(beanOfTheDayHistory, cancellationToken);
        await _coffeeBeanRepository.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Updated Bean of the Day to: {Id}", beanOfTheDayHistory.Id);
        return true;
    }
}
