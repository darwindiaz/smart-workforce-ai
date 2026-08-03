using SmartWorkforce.Application.Conciliations;
using SmartWorkforce.Application.Conciliations.CreateConciliation;
using SmartWorkforce.Domain;

public class CreateConciliationHandler
{
    private readonly IConciliationRepository _conciliationRepository;

    public CreateConciliationHandler(IConciliationRepository conciliationRepository)
    {
        _conciliationRepository = conciliationRepository;
    }

    public async Task<CreateConciliationResult> Handle(CreateConciliationCommand command)
    {

        var conciliation = ConciliationFactory.Create(
            command.ConciliationType,
            command.BankAccountId,
            command.ConciliationPeriodDate,
            command.CreatedBy,
            DateTime.UtcNow
        );

        await _conciliationRepository.AddSync(conciliation, CancellationToken.None);

        return new CreateConciliationResult(
            conciliation.ConciliationId,
            conciliation.Status
            );
    }


}