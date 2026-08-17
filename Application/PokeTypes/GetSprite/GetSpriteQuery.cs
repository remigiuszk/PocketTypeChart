using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Application.Shared;

namespace Application.PokeTypes.GetSprite
{
    public sealed record GetSpriteQuery(int Id) : IQuery<byte[]>;

    internal sealed class GetSpriteQueryHandler(IPokeTypeRepository repository) : IQueryHandler<GetSpriteQuery, byte[]>
    {
        private readonly IPokeTypeRepository _repository = repository;

        public async Task<Result<byte[]>> Handle(GetSpriteQuery query, CancellationToken cancellationToken)
        {
            var sprite = await _repository.GetSpriteImageAsync(query.Id, cancellationToken);
            return sprite;
        }
    }
}
