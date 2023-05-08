
using Infraestructure.Dtos.Response;

namespace Infraestructure.Interfaces
{
    public interface IStatsRepository
    {
        public Task<bool> InsertStat(string dnaType);
        public Task<GetStatsQueryResponse> GetStats();
    }
}