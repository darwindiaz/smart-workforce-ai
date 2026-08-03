namespace SmartWorkforce.Api.Conciliations.CreateConciliation;

public record CreateConciliationRequest(
    string ConciliationType,
    string BankAccountId,
    DateOnly ConciliationPeriodDate,
    string CreatedBy
);