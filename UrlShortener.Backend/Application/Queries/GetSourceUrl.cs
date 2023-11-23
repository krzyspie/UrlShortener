using MediatR;

namespace Application.Queries
{
    public class GetSourceUrl : IRequest<string>
    {
        public string ShortUrl { get; set; }
    }
}
