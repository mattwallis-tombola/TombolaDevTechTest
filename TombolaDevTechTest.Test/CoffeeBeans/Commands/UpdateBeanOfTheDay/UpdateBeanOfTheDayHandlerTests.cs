using Microsoft.Extensions.Logging;
using Moq;
using TombolaDevTechTest.CoffeeBeans.Commands.UpdateBeanOfTheDay;
using TombolaDevTechTest.Models;
using TombolaDevTechTest.Repositories;

namespace TombolaDevTechTest.Test.CoffeeBeans.Commands.UpdateBeanOfTheDay;

[TestFixture]
internal class UpdateBeanOfTheDayHandlerTests
{
    private Mock<ICoffeeBeanRepository> _mockCoffeeBeanRepository;
    private Mock<ILogger<UpdateBeanOfTheDayHandler>> _mockLogger;
    private UpdateBeanOfTheDayHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockCoffeeBeanRepository = new Mock<ICoffeeBeanRepository>();
        _mockLogger = new Mock<ILogger<UpdateBeanOfTheDayHandler>>();
        _handler = new UpdateBeanOfTheDayHandler(_mockCoffeeBeanRepository.Object, _mockLogger.Object);
    }

    [Test]
    public async Task Handle_ShouldReturnFalse_WhenBeanOfTheDayAlreadyExists()
    {
        // Arrange
        var existingBean = new CoffeeBean { BeanId = "123", Name = "Test Bean" };
        _mockCoffeeBeanRepository
            .Setup(repo => repo.GetCurrentBeanOfTheDayAsync())
            .ReturnsAsync(existingBean);

        var command = new UpdateBeanOfTheDayCommand();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.False);
        _mockLogger.Verify(
           logger => logger.Log(
               It.IsAny<LogLevel>(),
               It.IsAny<EventId>(),
               It.IsAny<It.IsAnyType>(),
               It.IsAny<Exception?>(),
               It.IsAny<Func<It.IsAnyType, Exception?, string>>()
           ),
           Times.Once
        );
    }

    [Test]
    public async Task Handle_ShouldReturnFalse_WhenNoBeansExist()
    {
        // Arrange
        _mockCoffeeBeanRepository
            .Setup(repo => repo.GetAllBeansAsync())
            .ReturnsAsync(new List<CoffeeBean>());

        var command = new UpdateBeanOfTheDayCommand();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.False);
        _mockLogger.Verify(
           logger => logger.Log(
               It.IsAny<LogLevel>(),
               It.IsAny<EventId>(),
               It.IsAny<It.IsAnyType>(),
               It.IsAny<Exception?>(),
               It.IsAny<Func<It.IsAnyType, Exception?, string>>()
           ),
           Times.Once);
    }

    [Test]
    public async Task Handle_ShouldReturnTrue_WhenBeanOfTheDayIsUpdatedSuccessfully()
    {
        // Arrange
        var allBeans = new List<CoffeeBean>
        {
            new() { BeanId = "123", Name = "Bean 1" },
            new() { BeanId = "456", Name = "Bean 2" }
        };
        var yesterdayBean = new BeanOfTheDayHistory { BeanId = "123" };

        _mockCoffeeBeanRepository
            .Setup(repo => repo.GetCurrentBeanOfTheDayAsync())
            .ReturnsAsync((CoffeeBean?)null);
        _mockCoffeeBeanRepository
            .Setup(repo => repo.GetAllBeansAsync())
            .ReturnsAsync(allBeans);
        _mockCoffeeBeanRepository
            .Setup(repo => repo.GetLastBeanOfTheDayHistoryAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(yesterdayBean);

        _mockCoffeeBeanRepository
            .Setup(repo => repo.AddBeanOfTheDayHistoryAsync(It.IsAny<BeanOfTheDayHistory>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _mockCoffeeBeanRepository
            .Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var command = new UpdateBeanOfTheDayCommand();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
        _mockCoffeeBeanRepository.Verify(
            repo => repo.AddBeanOfTheDayHistoryAsync(It.IsAny<BeanOfTheDayHistory>(), It.IsAny<CancellationToken>()),
            Times.Once);
        _mockCoffeeBeanRepository.Verify(
            repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
        _mockLogger.Verify(
           logger => logger.Log(
               It.IsAny<LogLevel>(),
               It.IsAny<EventId>(),
               It.IsAny<It.IsAnyType>(),
               It.IsAny<Exception?>(),
               It.IsAny<Func<It.IsAnyType, Exception?, string>>()
           ),
           Times.Once);
    }

    [Test]
    public async Task Handle_ShouldReturnFalse_WhenOnlyOneBeanExistsAndItWasYesterday()
    {
        // Arrange
        var singleBean = new CoffeeBean { BeanId = "123", Name = "Bean 1" };
        var yesterdayBean = new BeanOfTheDayHistory { BeanId = "123" };

        _mockCoffeeBeanRepository
            .Setup(repo => repo.GetCurrentBeanOfTheDayAsync())
            .ReturnsAsync((CoffeeBean?)null);
        _mockCoffeeBeanRepository
            .Setup(repo => repo.GetAllBeansAsync())
            .ReturnsAsync(new List<CoffeeBean> { singleBean });
        _mockCoffeeBeanRepository
            .Setup(repo => repo.GetLastBeanOfTheDayHistoryAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(yesterdayBean);

        var command = new UpdateBeanOfTheDayCommand();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.False);
        _mockLogger.Verify(
           logger => logger.Log(
               It.IsAny<LogLevel>(),
               It.IsAny<EventId>(),
               It.IsAny<It.IsAnyType>(),
               It.IsAny<Exception?>(),
               It.IsAny<Func<It.IsAnyType, Exception?, string>>()
           ),
           Times.Once);
    }
}
