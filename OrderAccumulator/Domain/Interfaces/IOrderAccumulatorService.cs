using OrderAccumulator.Domain.Entities;

public interface IOrderAccumulatorService
{
    Exposure GetExposure(string symbol);
    bool ProcessOrder(Order order);
}
