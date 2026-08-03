using SmartWorkforce.Domain;

namespace SmartWorkforce.Application.Conciliations;

public interface IConciliationRepository
{
    Task AddSync(Conciliation conciliation, CancellationToken cancellationToken);
}