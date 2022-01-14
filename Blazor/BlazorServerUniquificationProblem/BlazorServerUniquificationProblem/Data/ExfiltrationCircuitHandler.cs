using Microsoft.AspNetCore.Components.Server.Circuits;

namespace BlazorServerUniquificationProblem.Data
{
    public class ExfiltrationCircuitHandler : CircuitHandler
    {
        private readonly ExfiltratedData _exfiltratedData;

        public ExfiltrationCircuitHandler(ExfiltratedData exfiltratedData)
        {
            _exfiltratedData = exfiltratedData;
        }

        public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _exfiltratedData.CircuitId = circuit.Id;
            return base.OnCircuitOpenedAsync(circuit, cancellationToken);
        }
    }
}
