using OrderAccumulator.Domain.Entities;

public class OrderAccumulatorService : IOrderAccumulatorService
{
    private readonly Dictionary<string, Exposure> _exposures;
    private const decimal ExposureLimit = 1000000m;

    public OrderAccumulatorService()
    {
        _exposures = new Dictionary<string, Exposure>
        {
            { "PETR4", new Exposure { Symbol = "PETR4", Value = 0m } },
            { "VALE3", new Exposure { Symbol = "VALE3", Value = 0m } },
            { "VIIA4", new Exposure { Symbol = "VIIA4", Value = 0m } }
        };
    }

    public Exposure GetExposure(string symbol)
    {
        return _exposures[symbol];
    }

    public bool ProcessOrder(Order order)
    {
        decimal orderValue = order.Price * order.Quantity;
        Exposure exposure = _exposures[order.Symbol];

        if (order.IsBuy)
        {
            if (exposure.Value + orderValue <= ExposureLimit)
            {
                exposure.Value += orderValue;
                return true;
            }
        }
        else
        {
            if (exposure.Value - orderValue >= -ExposureLimit)
            {
                exposure.Value -= orderValue;
                return true;
            }
        }

        return false;
    }

}
