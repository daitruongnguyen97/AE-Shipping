namespace Shipping.Application.Extensions;

public class ArgumentRequiredException : ApplicationException
{
    public ArgumentRequiredException()
        : base("required_column")
    {
        ArgRequired = new List<string>();
        ArgMessage = string.Empty;
    }

    public ArgumentRequiredException(string message, List<string> columns)
        : this()
    {
        ArgRequired = columns;
        ArgMessage = message;
    }

    public List<string> ArgRequired { get; set; }
    private string ArgMessage { get; set; }

    public override string Message
    {
        get
        {
            return ArgMessage;
        }
    }
}
