﻿namespace CvAnalysisSystem.Common.Exceptions;

public class TooManyRequestException : Exception
{
    public TooManyRequestException(string? message) : base(message)
    {
    }
}
