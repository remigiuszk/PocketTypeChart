using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Domain.Models;

namespace Application.PokeTypes.GetAllTypes
{
    public sealed record GetAllTypesQuery : IQuery<ICollection<PokeType>>;

    internal sealed class GetAllTypesQueryHandler(IPokeTypeRepository repository) : IQueryHandler<GetAllTypesQuery, ICollection<PokeType>>
    {
        private readonly IPokeTypeRepository _repository = repository;

        public async Task<ICollection<PokeType>> Handle(GetAllTypesQuery query, CancellationToken cancellationToken)
        {
            return await _repository.GetPokeTypes(cancellationToken);
        }
    }
}
