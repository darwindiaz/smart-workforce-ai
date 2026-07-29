namespace SmartWorkforce.Domain;

public class ConciliationFactory
{
    public static Conciliation Create(
        string conciliationType,
        string bankAccountId,
        DateOnly conciliationPeriodDate,
        string createdBy,
        DateTime createdAt)
    {
        var id = Guid.NewGuid();
        var state = ConciliationStatus.Draft;

        if (string.IsNullOrWhiteSpace(conciliationType))
        {
            throw new ArgumentException("ConciliationType is required");
        }

        if (string.IsNullOrWhiteSpace(bankAccountId))
        {
            throw new ArgumentException("BankAccountId is required");
        }

        var conciliation = new Conciliation(
            id,
            conciliationType,
            bankAccountId,
            conciliationPeriodDate,
            state,
            createdBy,
            createdAt);

        return conciliation;
    }
}