using Moq;
using TombolaDevTechTest.CoffeeBeans.Commands.AddCoffeeBean;
using TombolaDevTechTest.Models;
using TombolaDevTechTest.Repositories;

namespace TombolaDevTechTest.Test.CoffeeBeans.Commands.AddCoffeeBean;

[TestFixture]
public class AddCoffeeBeanHandlerTests
{
    private Mock<ICoffeeBeanRepository> _mockCoffeeBeanRepository;
    private AddCoffeeBeanHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockCoffeeBeanRepository = new Mock<ICoffeeBeanRepository>();
        _handler = new AddCoffeeBeanHandler(_mockCoffeeBeanRepository.Object);
    }

    [Test]
    public async Task Handle_ShouldReturnCoffeeBean_WhenRepositoryAddsSuccessfully()
    {
        // Arrange
        var coffeeBean = new CoffeeBean
        {
            BeanId = "123",
            Name = "Test Bean",
            Description = "Test Description",
            Cost = 10.99m,
            ImageUrl = "http://example.com/image.jpg",
            CountryId = 1,
            ColourId = 1
        };

        var command = new AddCoffeeBeanCommand(coffeeBean);

        _mockCoffeeBeanRepository
            .Setup(repo => repo.AddCoffeeBeanAsync(coffeeBean, It.IsAny<CancellationToken>()))
            .ReturnsAsync(coffeeBean);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(coffeeBean));
        _mockCoffeeBeanRepository.Verify(repo => repo.AddCoffeeBeanAsync(coffeeBean, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_ShouldReturnNull_WhenRepositoryReturnsNull()
    {
        // Arrange
        var coffeeBean = new CoffeeBean
        {
            BeanId = "123",
            Name = "Test Bean",
            Description = "Test Description",
            Cost = 10.99m,
            ImageUrl = "http://example.com/image.jpg",
            CountryId = 1,
            ColourId = 1
        };

        var command = new AddCoffeeBeanCommand(coffeeBean);

        _mockCoffeeBeanRepository
            .Setup(repo => repo.AddCoffeeBeanAsync(coffeeBean, It.IsAny<CancellationToken>()))
            .ReturnsAsync((CoffeeBean?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNull(result);
        _mockCoffeeBeanRepository.Verify(repo => repo.AddCoffeeBeanAsync(coffeeBean, It.IsAny<CancellationToken>()), Times.Once);
    }
}
