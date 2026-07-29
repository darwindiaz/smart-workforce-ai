namespace SmartWorkforce.Domain;

public class Conciliation
{
    private readonly Guid _conciliationId;
    private string _conciliationType;
    private readonly string _bankAccountId;
    private readonly DateOnly _conciliationPeriodDate;
    private ConciliationStatus _status;
    private readonly string _createdBy;
    private readonly DateTime _createdAt;

    public Conciliation(
        Guid conciliationId,
        string conciliationType,
        string bankAccountId,
        DateOnly conciliationPeriodDate,
        ConciliationStatus status,
        string createdBy,
        DateTime createdAt)
    {
        _conciliationId = conciliationId;
        _conciliationType = conciliationType;
        _bankAccountId = bankAccountId;
        _conciliationPeriodDate = conciliationPeriodDate;
        _status = status;
        _createdBy = createdBy;
        _createdAt = createdAt;
    }

    public void LoadBankMovements()
    {
        // Pendiente de implementar
    }

    public void LoadAccountingMovements()
    {
        // Pendiente de implementar
    }

    public void ExecuteAutomaticConciliation()
    {
        // Pendiente de implementar
    }

    public void RegisterAccountingAdjustment()
    {
        // Pendiente de implementar
    }

    public void JustifyConciliationEntry()
    {
        // Pendiente de implementar
    }

    public void SendForApproval()
    {
        // Pendiente de implementar
    }

    public void ApproveConciliation()
    {
        if (_status != ConciliationStatus.PendingReview)
        {
            throw new InvalidOperationException(
                "La conciliación no está en estado de revisión.");
        }

        _status = ConciliationStatus.Approved;
    }
}