using Moq;
using TombolaDevTechTest.CoffeeBeans.Commands.UpdateCoffeeBean;
using TombolaDevTechTest.Models;
using TombolaDevTechTest.Repositories;

namespace TombolaDevTechTest.Test.CoffeeBeans.Commands.UpdateCoffeeBean;

[TestFixture]
public class UpdateCoffeeBeanHandlerTests
{
    private Mock<ICoffeeBeanRepository> _mockCoffeeBeanRepository;
    private UpdateCoffeeBeanHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockCoffeeBeanRepository = new Mock<ICoffeeBeanRepository>();
        _handler = new UpdateCoffeeBeanHandler(_mockCoffeeBeanRepository.Object);
    }

    [Test]
    public async Task Handle_ShouldReturnUpdatedCoffeeBean_WhenUpdateIsSuccessful()
    {
        // Arrange
        var coffeeBean = new CoffeeBean
        {
            BeanId = "123",
            Cost = 10.99m,
            ImageUrl = "http://example.com/image.jpg",
            Name = "Test Bean",
            Description = "Test Description",
            CountryId = 1,
            ColourId = 1
        };

        var command = new UpdateCoffeeBeanCommand(coffeeBean);
        var cancellationToken = CancellationToken.None;

        _mockCoffeeBeanRepository
            .Setup(repo => repo.UpdateCoffeeBeanAsync(coffeeBean, cancellationToken))
            .ReturnsAsync(coffeeBean);

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(coffeeBean));
        _mockCoffeeBeanRepository.Verify(repo => repo.UpdateCoffeeBeanAsync(coffeeBean, cancellationToken), Times.Once);
    }

    [Test]
    public async Task Handle_ShouldReturnNull_WhenUpdateFails()
    {
        // Arrange
        var coffeeBean = new CoffeeBean
        {
            BeanId = "123",
            Cost = 10.99m,
            ImageUrl = "http://example.com/image.jpg",
            Name = "Test Bean",
            Description = "Test Description",
            CountryId = 1,
            ColourId = 1
        };

        var command = new UpdateCoffeeBeanCommand(coffeeBean);
        var cancellationToken = CancellationToken.None;

        _mockCoffeeBeanRepository
            .Setup(repo => repo.UpdateCoffeeBeanAsync(coffeeBean, cancellationToken))
            .ReturnsAsync((CoffeeBean?)null);

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.IsNull(result);
        _mockCoffeeBeanRepository.Verify(repo => repo.UpdateCoffeeBeanAsync(coffeeBean, cancellationToken), Times.Once);
    }
}
