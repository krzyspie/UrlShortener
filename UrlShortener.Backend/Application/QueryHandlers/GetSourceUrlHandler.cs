using Application.Queries;
using Infrastructure.Repository;
using MediatR;

namespace Application.QueryHandlers
{
    internal class GetSourceUrlHandler : IRequestHandler<GetSourceUrl, string>
    {
        private readonly IUrlRepository _urlRepository;

        public GetSourceUrlHandler(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }
        public Task<string> Handle(GetSourceUrl request, CancellationToken cancellationToken)
        {
            var url = _urlRepository.GetShortUrl(request.ShortUrl);

            return Task.FromResult(url);
        }
    }
}
