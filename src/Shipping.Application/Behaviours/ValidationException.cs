using FluentValidation.Results;

namespace Shipping.Exceptions;

public class ValidationException : ApplicationException
{
    public ValidationException()
        : base()
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }

    public override string Message
    {
        get
        {
            return string.Join(", " + System.Environment.NewLine, 
                Errors.Values.Select(v => string.Join(", " + System.Environment.NewLine, v)));
        }
    }
}
