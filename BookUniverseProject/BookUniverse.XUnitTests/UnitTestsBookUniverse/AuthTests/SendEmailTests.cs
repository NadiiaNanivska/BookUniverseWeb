using BookUniverse.Application.MediatR.Authentication.Commands.SignUp;
using BookUniverse.Infrastructure.Services.EmailSender;
using Moq;
using Xunit;

namespace BookUniverse.XUnitTests.UnitTestsBookUniverse.AuthTests
{
    public class SendEmailTests
    {
        private readonly Mock<IEmailSender> _mockEmailSender;
        private readonly SendEmailHandler _handler;

        public SendEmailTests()
        {
            _mockEmailSender = new Mock<IEmailSender>();
            _handler = new SendEmailHandler(_mockEmailSender.Object);
        }

        [Fact]
        public async Task ReturnSuccessResult_WhenEmailIsSentSuccessfully()
        {
            // Arrange
            var request = new SendEmailCommand("test@example.com", "Link","Subject", "Content");
            _mockEmailSender.Setup(sender => sender.SendEmailAsync(request.email, request.subject, request.content))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task ReturnFailureResult_WhenEmailIsNotSent()
        {
            // Arrange
            var request = new SendEmailCommand("test@example.com", "Link","Subject", "Content");
            _mockEmailSender.Setup(sender => sender.SendEmailAsync(request.email, request.subject, request.content))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("FailedToSendEmailMessage", result.Errors.First().Message);
        }
    }
}

