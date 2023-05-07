using Domain;
using MediatR;

namespace WebApiPeriferia.Queries
{
    public class GetStatsQuery: IRequest<Stat>
    {
    }
}
