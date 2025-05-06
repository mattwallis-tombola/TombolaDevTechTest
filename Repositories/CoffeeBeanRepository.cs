using Microsoft.EntityFrameworkCore;
using TombolaDevTechTest.Models;

namespace TombolaDevTechTest.Repositories;

internal sealed class CoffeeBeanRepository(ILogger<CoffeeBeanRepository> logger, CoffeeDbContext context) : ICoffeeBeanRepository
{
    private readonly ILogger<CoffeeBeanRepository> _logger = logger;
    private readonly CoffeeDbContext _context = context;

    public async Task<List<CoffeeBean>> GetAllBeansAsync() => await _context.CoffeeBeans.ToListAsync();

    public async Task<CoffeeBean?> GetBeanByIdAsync(string beanId)
    {
        ArgumentNullException.ThrowIfNull(beanId);
        return await _context.CoffeeBeans.FirstOrDefaultAsync(b => b.BeanId == beanId);
    }

    public async Task<IList<CoffeeBean>?> GetCoffeeBeansByNameAsync(string beanName)
    {
        ArgumentNullException.ThrowIfNull(beanName);
        return await _context.CoffeeBeans.Where(b => b.Name == beanName).ToListAsync();
    }

    public async Task<IList<CoffeeBean>?> GetCoffeeBeansByCountryNameAsync(string countryName)
    {
        ArgumentNullException.ThrowIfNull(countryName);

        var country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == countryName);
        if (country == null)
        {
            _logger.LogWarning("Country with name {CountryName} not found.", countryName);
            return null;
        }

        return await _context.CoffeeBeans.Where(b => b.CountryId == country.Id).ToListAsync();
    }

    public async Task<IList<CoffeeBean>?> GetCoffeeBeansByColourNameAsync(string colourName)
    {
        ArgumentNullException.ThrowIfNull(colourName);

        var colour = await _context.Colours.FirstOrDefaultAsync(c => c.Name == colourName);
        if (colour == null)
        {
            _logger.LogWarning("Colour with name {ColourName} not found.", colourName);
            return null;
        }
        return await _context.CoffeeBeans.Where(b => b.ColourId == colour.Id).ToListAsync();
    }

    public async Task<CoffeeBean?> AddCoffeeBeanAsync(CoffeeBean coffeeBean, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(coffeeBean);

        var insertedBean = await _context.CoffeeBeans.AddAsync(coffeeBean, cancellationToken);
        await SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Added new coffee bean with ID: {BeanId}", coffeeBean.BeanId);

        return insertedBean.Entity;
    }

    public async Task<CoffeeBean?> GetCurrentBeanOfTheDayAsync()
    {
        var beanOfTheDayHistory = await _context.BeanOfTheDayHistory
           .Where(b => b.DateStamp == DateTime.UtcNow.Date)
           .Select(b => b.BeanId)
           .FirstOrDefaultAsync();

        return beanOfTheDayHistory == null ? null : await _context.CoffeeBeans.FirstOrDefaultAsync(b => b.BeanId == beanOfTheDayHistory);
    }

    public async Task AddBeanOfTheDayHistoryAsync(BeanOfTheDayHistory beanOfTheDayHistory, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(beanOfTheDayHistory);

        try
        {
            await _context.BeanOfTheDayHistory.AddAsync(beanOfTheDayHistory, cancellationToken);
            await SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Failed to insert BeanOfTheDayHistory with ID: {BeanId}", beanOfTheDayHistory.BeanId);
            throw;
        }
    }

    public async Task<BeanOfTheDayHistory?> GetLastBeanOfTheDayHistoryAsync(CancellationToken cancellationToken)
    {
        return await _context.BeanOfTheDayHistory.OrderByDescending(b => b.DateStamp).FirstOrDefaultAsync(cancellationToken);
    }


    public async Task<bool> DeleteCoffeeBeanByIdAsync(string beanId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(beanId);

        // Find the coffee bean by ID  
        var coffeeBean = await _context.CoffeeBeans.FirstOrDefaultAsync(b => b.BeanId == beanId, cancellationToken);

        if (coffeeBean == null)
        {
            return false;
        }

        // Remove entries from BeanOfTheDayHistory where the BeanId matches  
        var beanOfTheDayEntries = _context.BeanOfTheDayHistory.Where(b => b.BeanId == beanId);
        _context.BeanOfTheDayHistory.RemoveRange(beanOfTheDayEntries);

        // Remove the coffee bean  
        _context.CoffeeBeans.Remove(coffeeBean);
        await SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted CoffeeBean with ID: {BeanId} and rows containing this ID from BeanOfTheDayHistory.", beanId);
        return true;
    }

    public async Task<CoffeeBean?> UpdateCoffeeBeanAsync(CoffeeBean coffeeBean, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(coffeeBean);

        var bean = await _context.CoffeeBeans.FirstOrDefaultAsync(b => b.BeanId == coffeeBean.BeanId, cancellationToken);

        if (bean == null)
        {
            _logger.LogWarning("Coffee bean with ID: {BeanId} not found.", coffeeBean.BeanId);
            return null;
        }

        bean.Name = coffeeBean.Name;
        bean.Description = coffeeBean.Description;
        bean.Cost = coffeeBean.Cost;
        bean.ImageUrl = coffeeBean.ImageUrl;
        bean.CountryId = coffeeBean.CountryId;
        bean.ColourId = coffeeBean.ColourId;

        try
        {
            _context.CoffeeBeans.Update(bean);
            await SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Updated coffee bean with ID: {BeanId}", coffeeBean.BeanId);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Failed to update coffee bean with ID: {BeanId}", coffeeBean.BeanId);
            throw;
        }

        return bean;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database update failed.");
            throw;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("Operation was canceled.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");
            throw;
        }
    }
}
