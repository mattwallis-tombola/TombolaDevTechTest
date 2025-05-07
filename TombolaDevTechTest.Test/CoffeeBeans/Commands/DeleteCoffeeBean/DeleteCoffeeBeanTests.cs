using Moq;
using TombolaDevTechTest.CoffeeBeans.Commands.DeleteCoffeeBean;
using TombolaDevTechTest.Repositories;

namespace TombolaDevTechTest.Test.CoffeeBeans.Commands.DeleteCoffeeBean;

[TestFixture]
internal class DeleteCoffeeBeanTests
{
    private Mock<ICoffeeBeanRepository> _mockCoffeeBeanRepository;
    private DeleteCoffeeBeanHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockCoffeeBeanRepository = new Mock<ICoffeeBeanRepository>();
        _handler = new DeleteCoffeeBeanHandler(_mockCoffeeBeanRepository.Object);
    }

    [Test]
    public async Task Handle_ShouldReturnTrue_WhenDeleteIsSuccessful()
    {
        // Arrange
        var beanId = "test-bean-id";
        var command = new DeleteCoffeeBeanCommand(beanId);
        _mockCoffeeBeanRepository
            .Setup(repo => repo.DeleteCoffeeBeanByIdAsync(beanId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
        _mockCoffeeBeanRepository.Verify(repo => repo.DeleteCoffeeBeanByIdAsync(beanId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_ShouldReturnFalse_WhenDeleteFails()
    {
        // Arrange
        var beanId = "test-bean-id";
        var command = new DeleteCoffeeBeanCommand(beanId);
        _mockCoffeeBeanRepository
            .Setup(repo => repo.DeleteCoffeeBeanByIdAsync(beanId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.False);
        _mockCoffeeBeanRepository.Verify(repo => repo.DeleteCoffeeBeanByIdAsync(beanId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void Handle_ShouldThrowException_WhenRepositoryThrowsException()
    {
        // Arrange
        var beanId = "test-bean-id";
        var command = new DeleteCoffeeBeanCommand(beanId);
        _mockCoffeeBeanRepository
            .Setup(repo => repo.DeleteCoffeeBeanByIdAsync(beanId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new System.Exception("Repository error"));

        // Act & Assert
        Assert.ThrowsAsync<System.Exception>(async () => await _handler.Handle(command, CancellationToken.None));
        _mockCoffeeBeanRepository.Verify(repo => repo.DeleteCoffeeBeanByIdAsync(beanId, It.IsAny<CancellationToken>()), Times.Once);
    }
}
