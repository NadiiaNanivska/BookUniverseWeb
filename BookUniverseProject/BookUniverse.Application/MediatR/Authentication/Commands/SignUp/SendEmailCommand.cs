﻿using BookUniverse.Application.DTOs.UserDTOs;
using FluentResults;
using MediatR;

namespace BookUniverse.Application.MediatR.Authentication.Commands.SignUp
{
    public record SendEmailCommand(string email, string link) : IRequest<Result<Unit>>;
}
