using OrderGenerator.Domain.Entities;

namespace OrderGenerator.Domain.Interfaces
{
    public interface IOrderGeneratorService
    {
        Order GenerateOrder();
    }
}
