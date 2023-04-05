using OrderGenerator.Domain.Entities;
using OrderGenerator.Domain.Interfaces;

public class OrderGeneratorService : IOrderGeneratorService
{
    private readonly Random _random = new Random();
    private readonly string[] _symbols = { "PETR4", "VALE3", "VIIA4" };

    public Order GenerateOrder()
    {
        return new Order
        {
            Symbol = _symbols[_random.Next(_symbols.Length)],
            IsBuy = _random.Next(2) == 0,
            Quantity = _random.Next(1, 100000),
            Price = Math.Round(_random.Next(1, 100000) * 0.01m, 2)
        };
    }
}
