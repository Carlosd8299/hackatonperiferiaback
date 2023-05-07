using Domain;
using Infraestructure.Interfaces;
using MediatR;
using WebApiPeriferia.Queries;

namespace WebApiPeriferia.Handlers
{
    public class GetStatsQueryHandler : IRequestHandler<GetStatsQuery, Stat>
    {
        private IStatsRepository _statsRepository;

        public GetStatsQueryHandler(IStatsRepository statsRepository)
        {
            this._statsRepository = statsRepository;
        }

        public async Task<Stat> Handle(GetStatsQuery request, CancellationToken cancellationToken)
        {
            Stat statResponse;
            var response = await _statsRepository.GetStats();

            statResponse = new Stat
            {
                CountHumanDna = response.CountHumanDna,
                CountMutantDna = response.CountMutantDna,
                Ratio = response.Ratio,
            };

            return statResponse;
        }
    }
}
