using Moq;
using TombolaDevTechTest.CoffeeBeans.Queries.GetCurrentBeanOfTheDay;
using TombolaDevTechTest.Models;
using TombolaDevTechTest.Repositories;

namespace TombolaDevTechTest.Test.CoffeeBeans.Queries.GetCurrentBeanOfTheDay
{
    [TestFixture]
    internal class GetCurrentBeanOfTheDayHandlerTests
    {
        private Mock<ICoffeeBeanRepository> _mockCoffeeBeanRepository;
        private GetCurrentBeanOfTheDayHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockCoffeeBeanRepository = new Mock<ICoffeeBeanRepository>();
            _handler = new GetCurrentBeanOfTheDayHandler(_mockCoffeeBeanRepository.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnCurrentBeanOfTheDay_WhenBeanExists()
        {
            // Arrange
            var expectedBean = new CoffeeBean
            {
                BeanId = "123",
                Name = "Bean of the Day",
                Description = "Special coffee bean",
                Cost = 12.99m,
                ImageUrl = "http://example.com/bean.jpg",
                CountryId = 1,
                ColourId = 1
            };

            _mockCoffeeBeanRepository
                .Setup(repo => repo.GetCurrentBeanOfTheDayAsync())
                .ReturnsAsync(expectedBean);

            var query = new GetCurrentBeanOfTheDayQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedBean));
            _mockCoffeeBeanRepository.Verify(repo => repo.GetCurrentBeanOfTheDayAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldReturnNull_WhenNoBeanOfTheDayExists()
        {
            // Arrange
            _mockCoffeeBeanRepository
                .Setup(repo => repo.GetCurrentBeanOfTheDayAsync())
                .ReturnsAsync((CoffeeBean?)null);

            var query = new GetCurrentBeanOfTheDayQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNull(result);
            _mockCoffeeBeanRepository.Verify(repo => repo.GetCurrentBeanOfTheDayAsync(), Times.Once);
        }
    }
}
