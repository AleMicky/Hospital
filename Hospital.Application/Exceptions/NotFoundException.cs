namespace Hospital.Application.Exceptions;

public class NotFoundException(string message)
    : Exception(message)
{
}