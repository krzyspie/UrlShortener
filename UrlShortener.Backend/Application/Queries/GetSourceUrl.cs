using MediatR;

namespace Application.Queries
{
    public class GetSourceUrl : IRequest<string>
    {
        public required string ShortUrl { get; set; }
    }
}
