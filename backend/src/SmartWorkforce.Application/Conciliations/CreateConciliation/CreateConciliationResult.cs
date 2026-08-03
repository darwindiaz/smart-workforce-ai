namespace SmartWorkforce.Application.Conciliations.CreateConciliation;

public class CreateConciliationResult
{
    public CreateConciliationResult(Guid conciliationId, ConciliationStatus status)
    {
        ConciliationId = conciliationId;
        Status = status;
    }

    public Guid ConciliationId { get; }
    public ConciliationStatus Status { get; }
}