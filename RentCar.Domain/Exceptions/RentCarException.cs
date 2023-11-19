namespace RentCar.Domain.Exceptions;

public class RentCarException : Exception
{
    public RentCarException(string message) : base(message) { }
    public static void When(bool hasError, string error)
    {
        if (hasError)
        {
            throw new RentCarException(error);
        }
    }
}
