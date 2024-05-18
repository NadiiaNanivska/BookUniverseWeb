using BookUniverse.Infrastructure.Services.EmailSender;
using FluentResults;
using MediatR;
using System.Text.Encodings.Web;

namespace BookUniverse.Application.MediatR.Authentication.Commands.SignUp
{
    public class SendEmailHandler : IRequestHandler<SendEmailCommand, Result<Unit>>
    {
        private readonly IEmailSender _emailSender;

        public SendEmailHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task<Result<Unit>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            bool isResultSuccess = await _emailSender.SendEmailAsync(request.email, request.subject, request.content);
            if (isResultSuccess)
            {
                return Result.Ok(Unit.Value);
            }
            else
            {
                return Result.Fail(new Error("FailedToSendEmailMessage"));
            }
        }
    }
}
