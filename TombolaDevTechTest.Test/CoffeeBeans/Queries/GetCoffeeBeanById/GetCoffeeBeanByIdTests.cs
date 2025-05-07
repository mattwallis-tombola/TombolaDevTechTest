using Moq;
using TombolaDevTechTest.CoffeeBeans.Queries.GetBeanById;
using TombolaDevTechTest.Models;
using TombolaDevTechTest.Repositories;

namespace TombolaDevTechTest.Test.CoffeeBeans.Queries.GetCoffeeBeanById;

[TestFixture]
internal class GetCoffeeBeanByIdTests
{
    private Mock<ICoffeeBeanRepository> _mockCoffeeBeanRepository;
    private GetCoffeeBeanByIdHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockCoffeeBeanRepository = new Mock<ICoffeeBeanRepository>();
        _handler = new GetCoffeeBeanByIdHandler(_mockCoffeeBeanRepository.Object);
    }

    [Test]
    public async Task Handle_ShouldReturnCoffeeBean_WhenBeanExists()
    {
        // Arrange
        var beanId = "123";
        var expectedBean = new CoffeeBean
        {
            BeanId = beanId,
            Name = "Test Bean",
            Description = "Test Description",
            Cost = 10.99m,
            ImageUrl = "http://example.com/image.jpg",
            CountryId = 1,
            ColourId = 1
        };

        _mockCoffeeBeanRepository
            .Setup(repo => repo.GetBeanByIdAsync(beanId))
            .ReturnsAsync(expectedBean);

        var query = new GetCoffeeBeanByIdQuery(beanId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(expectedBean));
        _mockCoffeeBeanRepository.Verify(repo => repo.GetBeanByIdAsync(beanId), Times.Once);
    }

    [Test]
    public async Task Handle_ShouldReturnNull_WhenBeanDoesNotExist()
    {
        // Arrange
        var beanId = "123";

        _mockCoffeeBeanRepository
            .Setup(repo => repo.GetBeanByIdAsync(beanId))
            .ReturnsAsync((CoffeeBean?)null);

        var query = new GetCoffeeBeanByIdQuery(beanId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Null);
        _mockCoffeeBeanRepository.Verify(repo => repo.GetBeanByIdAsync(beanId), Times.Once);
    }
}
