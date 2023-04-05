using Moq;
using Xunit;
using OrderGenerator.Domain.Entities;
using OrderGenerator.Domain.Interfaces;

namespace FixExchange.Test
{
    public class OrderGeneratorUnitTests
    {
        [Fact]
        public void GenerateOrder_ValidOrder_ReturnsOrderWithValidProperties()
        {
            var mockOrderGeneratorService = new Mock<IOrderGeneratorService>();
            mockOrderGeneratorService.Setup(o => o.GenerateOrder()).Returns(new Order
            {
                Symbol = "PETR4",
                IsBuy = true,
                Quantity = 100,
                Price = 10m
            });

            var order = mockOrderGeneratorService.Object.GenerateOrder();

            Assert.NotNull(order);
            Assert.Contains(order.Symbol, new[] { "PETR4", "VALE3", "VIIA4" });
            Assert.True(order.IsBuy || !order.IsBuy);
            Assert.InRange(order.Quantity, 1, 100000);
            Assert.True(order.Price >= 0.01m && order.Price < 1000 && (order.Price % 0.01m == 0));
        }
    }
}