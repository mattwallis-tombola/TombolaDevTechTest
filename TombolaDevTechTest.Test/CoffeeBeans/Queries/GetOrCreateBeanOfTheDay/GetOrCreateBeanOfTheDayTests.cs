using MediatR;
using Moq;
using TombolaDevTechTest.CoffeeBeans.Commands.UpdateBeanOfTheDay;
using TombolaDevTechTest.CoffeeBeans.Queries.GetCurrentBeanOfTheDay;
using TombolaDevTechTest.CoffeeBeans.Queries.GetOrCreateBeanOfTheDay;
using TombolaDevTechTest.Models;

namespace TombolaDevTechTest.Test.CoffeeBeans.Queries.GetOrCreateBeanOfTheDay
{
    [TestFixture]
    internal class GetOrCreateBeanOfTheDayTests
    {
        private Mock<IMediator> _mediatorMock;
        private GetOrCreateBeanOfTheDayHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mediatorMock = new Mock<IMediator>();
            _handler = new GetOrCreateBeanOfTheDayHandler(_mediatorMock.Object);
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

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetCurrentBeanOfTheDayQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedBean);

            var query = new GetOrCreateBeanOfTheDayQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedBean));
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetCurrentBeanOfTheDayQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<UpdateBeanOfTheDayCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public async Task Handle_ShouldUpdateAndReturnBeanOfTheDay_WhenNoBeanExistsInitially()
        {
            // Arrange
            var updatedBean = new CoffeeBean
            {
                BeanId = "456",
                Name = "Updated Bean of the Day",
                Description = "Freshly updated coffee bean",
                Cost = 15.99m,
                ImageUrl = "http://example.com/updatedbean.jpg",
                CountryId = 2,
                ColourId = 2
            };

            _mediatorMock
                .SetupSequence(m => m.Send(It.IsAny<GetCurrentBeanOfTheDayQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((CoffeeBean?)null) // First call returns null
                .ReturnsAsync(updatedBean);    // Second call returns the updated bean

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateBeanOfTheDayCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var query = new GetOrCreateBeanOfTheDayQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(updatedBean));
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetCurrentBeanOfTheDayQuery>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
            _mediatorMock.Verify(m => m.Send(It.IsAny<UpdateBeanOfTheDayCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldReturnNull_WhenUpdateFails()
        {
            // Arrange
            _mediatorMock
                .SetupSequence(m => m.Send(It.IsAny<GetCurrentBeanOfTheDayQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((CoffeeBean?)null); // First call returns null

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateBeanOfTheDayCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false); // Update fails

            var query = new GetOrCreateBeanOfTheDayQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetCurrentBeanOfTheDayQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<UpdateBeanOfTheDayCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
