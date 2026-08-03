using SmartWorkforce.Application.Conciliations;
using SmartWorkforce.Domain;

namespace SmartWorkforce.Infrastructure.Conciliations;

public class ConciliationRepository : IConciliationRepository
{
    public Task AddSync(Conciliation conciliation, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}