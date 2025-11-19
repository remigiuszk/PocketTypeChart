using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Application.Shared;
using Domain.PokeTypes;

namespace Application.PokeTypes.GetAllTypes
{
    public sealed record GetAllTypesQuery : IQuery<ICollection<PokeType>>;

    internal sealed class GetAllTypesQueryHandler(IPokeTypeRepository repository) : IQueryHandler<GetAllTypesQuery, ICollection<PokeType>>
    {
        private readonly IPokeTypeRepository _repository = repository;

        public async Task<Result<ICollection<PokeType>>> Handle(GetAllTypesQuery query, CancellationToken cancellationToken)
        {
            var pokeTypes = await _repository.GetPokeTypes(cancellationToken);
            return Result.Success(pokeTypes);
        }
    }
}
