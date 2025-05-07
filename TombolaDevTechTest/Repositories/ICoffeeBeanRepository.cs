using TombolaDevTechTest.Models;

namespace TombolaDevTechTest.Repositories;

public interface ICoffeeBeanRepository
{
    //Create
    Task<CoffeeBean?> AddCoffeeBeanAsync(CoffeeBean coffeeBean, CancellationToken cancellationToken);
    Task AddBeanOfTheDayHistoryAsync(BeanOfTheDayHistory beanOfTheDayHistory, CancellationToken cancellationToken);

    // Read
    Task<List<CoffeeBean>> GetAllBeansAsync();
    Task<CoffeeBean?> GetBeanByIdAsync(string beanId);
    Task<IList<CoffeeBean>?> GetCoffeeBeansByNameAsync(string beanName);
    Task<IList<CoffeeBean>?> GetCoffeeBeansByCountryNameAsync(string countryName);
    Task<IList<CoffeeBean>?> GetCoffeeBeansByColourNameAsync(string colourName);
    Task<CoffeeBean?> GetCurrentBeanOfTheDayAsync();
    Task<BeanOfTheDayHistory?> GetLastBeanOfTheDayHistoryAsync(CancellationToken cancellationToken);

    // Update
    Task<CoffeeBean?> UpdateCoffeeBeanAsync(CoffeeBean coffeeBean, CancellationToken cancellationToken);

    // Delete
    Task<bool> DeleteCoffeeBeanByIdAsync(string beanId, CancellationToken cancellationToken);

    // Misc
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
