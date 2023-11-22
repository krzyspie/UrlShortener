using MediatR;

namespace Application.Commands
{
    public class CreateShortUrl : IRequest<string>
    {
        public string Url { get; set; }
    }
}
