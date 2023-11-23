using Application.Queries;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetSourceUrlHandler : IRequestHandler<GetSourceUrl, string>
    {
        public Task<string> Handle(GetSourceUrl request, CancellationToken cancellationToken)
        {
            return Task.FromResult("https:\\www.onet.pl");
        }
    }
}
