﻿namespace Exemple.Identity.Infrastructure.Contracts.Exceptions;

public class DuplicateUserException : Exception
{
    public DuplicateUserException(string message) : base(message)
    { }
}