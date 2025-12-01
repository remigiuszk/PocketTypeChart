using Application.Abstractions.Messaging;
using Application.Abstractions.Queries;
using Application.DamageRelations.GetDamageRelationsForSelectedTypes.ReadModel;
using Application.DamageRelations.GetTypingEffectivenessQuery.Mappers;
using Application.Shared;


namespace Application.DamageRelations.GetDamageRelationsForSelectedTypes
{
    public sealed record GetTypingEffectivenessQuery(List<int> SelectedTypesId) : IQuery<TypingEffectivenessReadModel>;

    internal class GetTypingEffectivenessQueryHandler(
        IDamageRelationQueries queries
        ) : IQueryHandler<GetTypingEffectivenessQuery, TypingEffectivenessReadModel>
    {
        private readonly IDamageRelationQueries _queries = queries;

        public async Task<Result<TypingEffectivenessReadModel>> Handle(GetTypingEffectivenessQuery request, CancellationToken cancellationToken)
        {
            var allRelations = await _queries.GetAllDamageRelationsForSelectedTypes(request.SelectedTypesId);

            var defensiveTypes = allRelations.MapDefensiveRelations(request.SelectedTypesId);

            //TODO - ofensive types musza miec jeszcze source type - bedziemy kategoryzowac w sposob ze fighting types move affect: bla bla
            var ofensiveTypes = allRelations.MapOffensiveRelations(request.SelectedTypesId);

            return new TypingEffectivenessReadModel()
            {
                DefensiveDamageRelations = defensiveTypes,
                OffensiveDamageRelations = ofensiveTypes
            };
        }
    }
}
