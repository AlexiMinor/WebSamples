﻿using MediatR;
using WebApp.Core.DTOs;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.Queries;

public class TryLoginQuery : IRequest<User>
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}