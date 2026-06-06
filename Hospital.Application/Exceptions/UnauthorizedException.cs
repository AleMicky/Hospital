namespace Hospital.Application.Exceptions;

public class UnauthorizedException(string message) : Exception(message);