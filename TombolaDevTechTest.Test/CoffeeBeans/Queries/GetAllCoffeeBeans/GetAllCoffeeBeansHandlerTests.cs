using Moq;
using TombolaDevTechTest.CoffeeBeans.Queries.GetAllCoffeeBeans;
using TombolaDevTechTest.Models;
using TombolaDevTechTest.Repositories;

namespace TombolaDevTechTest.Test.CoffeeBeans.Queries.GetAllCoffeeBeans;

[TestFixture]
internal class GetAllCoffeeBeansHandlerTests
{
    private Mock<ICoffeeBeanRepository> _mockCoffeeBeanRepository;
    private GetAllCoffeeBeansHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockCoffeeBeanRepository = new Mock<ICoffeeBeanRepository>();
        _handler = new GetAllCoffeeBeansHandler(_mockCoffeeBeanRepository.Object);
    }

    [Test]
    public async Task Handle_ShouldReturnListOfCoffeeBeans_WhenRepositoryReturnsData()
    {
        // Arrange
        var coffeeBeans = new List<CoffeeBean>
        {
            new() { Name = "Arabica", Description = "Smooth and mild", Cost = 10.5m },
            new() { Name = "Robusta", Description = "Strong and bold", Cost = 8.0m }
        };
        _mockCoffeeBeanRepository
            .Setup(repo => repo.GetAllBeansAsync())
            .ReturnsAsync(coffeeBeans);

        var query = new GetAllCoffeeBeansQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Arabica"));
            Assert.That(result[1].Name, Is.EqualTo("Robusta"));
        });
    }

    [Test]
    public async Task Handle_ShouldReturnEmptyList_WhenRepositoryReturnsNoData()
    {
        // Arrange
        _mockCoffeeBeanRepository
            .Setup(repo => repo.GetAllBeansAsync())
            .ReturnsAsync(new List<CoffeeBean>());

        var query = new GetAllCoffeeBeansQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Handle_ShouldThrowException_WhenRepositoryThrowsException()
    {
        // Arrange
        _mockCoffeeBeanRepository
            .Setup(repo => repo.GetAllBeansAsync())
            .ThrowsAsync(new System.Exception("Database error"));

        var query = new GetAllCoffeeBeansQuery();

        // Act & Assert
        Assert.ThrowsAsync<System.Exception>(async () => await _handler.Handle(query, CancellationToken.None));
    }
}
