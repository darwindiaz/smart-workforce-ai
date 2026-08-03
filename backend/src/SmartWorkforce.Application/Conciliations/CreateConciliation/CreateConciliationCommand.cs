namespace SmartWorkforce.Application.Conciliations.CreateConciliation;

public record CreateConciliationCommand(
    string ConciliationType,
    string BankAccountId,
    DateOnly ConciliationPeriodDate,
    string CreatedBy
    );