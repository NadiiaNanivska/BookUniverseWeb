using BookUniverse.Application.Extensions;
using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Services.EmailSender;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.Ocsp;

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
            bool isResultSuccess = await _emailSender.SendEmailConfirmationAsync(request.email, request.link);
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
