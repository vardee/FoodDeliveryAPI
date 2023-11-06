using System.Net;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }
}
public class InternalServerErrorException : Exception
{
    public InternalServerErrorException(string message) : base(message)
    {
    }
}
public class Unauthorized : Exception
{
    public Unauthorized(string message) : base(message)
    {
    }
}

public class Forbidden : Exception
{
    public Forbidden(string message) : base(message)
    {
    }
}
public class NotFound : Exception
{
    public NotFound(string message) : base(message)
    {
    }
}
