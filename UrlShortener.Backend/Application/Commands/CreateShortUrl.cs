using MediatR;

namespace Application.Commands
{
    public class CreateShortUrl : IRequest<string>
    {
        public required string OriginUrl { get; set; }
    }
}
