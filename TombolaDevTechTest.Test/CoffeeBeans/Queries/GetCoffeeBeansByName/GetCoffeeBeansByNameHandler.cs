using Moq;
using TombolaDevTechTest.CoffeeBeans.Queries.GetCoffeeBeansByName;
using TombolaDevTechTest.Models;
using TombolaDevTechTest.Repositories;

namespace TombolaDevTechTest.Test.CoffeeBeans.Queries.GetCoffeeBeansByName;

[TestFixture]
internal class GetCoffeeBeansByNameHandlerTests
{
    private Mock<ICoffeeBeanRepository> _mockCoffeeBeanRepository;
    private GetCoffeeBeansByNameHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockCoffeeBeanRepository = new Mock<ICoffeeBeanRepository>();
        _handler = new GetCoffeeBeansByNameHandler(_mockCoffeeBeanRepository.Object);
    }

    [Test]
    public async Task Handle_ShouldReturnCoffeeBeans_WhenBeansExist()
    {
        // Arrange
        var beanName = "Espresso";
        var expectedBeans = new List<CoffeeBean>
        {
            new() {
                BeanId = "123",
                Name = "Espresso",
                Description = "Strong coffee",
                Cost = 5.99m,
                ImageUrl = "http://example.com/espresso.jpg",
                CountryId = 1,
                ColourId = 1
            },
            new() {
                BeanId = "124",
                Name = "Espresso",
                Description = "Rich coffee",
                Cost = 6.99m,
                ImageUrl = "http://example.com/espresso2.jpg",
                CountryId = 1,
                ColourId = 2
            }
        };

        _mockCoffeeBeanRepository
            .Setup(repo => repo.GetCoffeeBeansByNameAsync(beanName))
            .ReturnsAsync(expectedBeans);

        var query = new GetCoffeeBeansByNameQuery(beanName);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(expectedBeans));
        _mockCoffeeBeanRepository.Verify(repo => repo.GetCoffeeBeansByNameAsync(beanName), Times.Once);
    }

    [Test]
    public async Task Handle_ShouldReturnEmptyList_WhenNoBeansExist()
    {
        // Arrange
        var beanName = "NonExistentBean";

        _mockCoffeeBeanRepository
            .Setup(repo => repo.GetCoffeeBeansByNameAsync(beanName))
            .ReturnsAsync(new List<CoffeeBean>());

        var query = new GetCoffeeBeansByNameQuery(beanName);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
        _mockCoffeeBeanRepository.Verify(repo => repo.GetCoffeeBeansByNameAsync(beanName), Times.Once);
    }

    [Test]
    public async Task Handle_ShouldReturnNull_WhenRepositoryReturnsNull()
    {
        // Arrange
        var beanName = "NullBean";

        _mockCoffeeBeanRepository
            .Setup(repo => repo.GetCoffeeBeansByNameAsync(beanName))
            .ReturnsAsync((IList<CoffeeBean>?)null);

        var query = new GetCoffeeBeansByNameQuery(beanName);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Null);
        _mockCoffeeBeanRepository.Verify(repo => repo.GetCoffeeBeansByNameAsync(beanName), Times.Once);
    }
}
