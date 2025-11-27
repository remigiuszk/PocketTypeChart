using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Application.Shared;
using Domain.PokeTypes;

namespace Application.PokeTypes.GetAllTypes
{
    public sealed record GetAllTypesQuery : IQuery<List<PokeType>>;

    internal sealed class GetAllTypesQueryHandler(IPokeTypeRepository repository) : IQueryHandler<GetAllTypesQuery, List<PokeType>>
    {
        private readonly IPokeTypeRepository _repository = repository;

        public async Task<Result<List<PokeType>>> Handle(GetAllTypesQuery query, CancellationToken cancellationToken)
        {
            var pokeTypes = await _repository.GetPokeTypes(cancellationToken);
            return Result.Success(pokeTypes);
        }
    }
}
