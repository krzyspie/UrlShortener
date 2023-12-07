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
        public async Task<string> Handle(GetSourceUrl request, CancellationToken cancellationToken)
        {
            var url = await _urlRepository.GetShortUrl(request.ShortUrl);

            return url ?? string.Empty;
        }
    }
}
