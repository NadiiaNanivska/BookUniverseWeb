using FluentResults;

namespace BookUniverse.Application.MediatR.ResultVariations
{
    public class NullResult<T> : Result<T>
    {
        public NullResult()
            : base()
        {
        }
    }
}
