using Xunit;
using OrderAccumulator;
using OrderAccumulator.Domain.Entities;
using Moq;

namespace OrderAccumulatorTests
{
    public class OrderAccumulatorTests
    {
        [Fact]
        public void ProcessOrder_OrderWithinExposureLimit_ReturnsTrue()
        {
            var mockOrderAccumulatorService = new Mock<IOrderAccumulatorService>();
            var order = new Order
            {
                Symbol = "PETR4",
                IsBuy = true,
                Quantity = 100,
                Price = 10m
            };
            mockOrderAccumulatorService.Setup(o => o.ProcessOrder(order)).Returns(true);

            bool isAccepted = mockOrderAccumulatorService.Object.ProcessOrder(order);

            Assert.True(isAccepted);
        }

        [Fact]
        public void ProcessOrder_OrderExceedingExposureLimit_ReturnsFalse()
        {
            var mockOrderAccumulatorService = new Mock<IOrderAccumulatorService>();
            var order = new Order
            {
                Symbol = "PETR4",
                IsBuy = true,
                Quantity = 200000,
                Price = 10m
            };
            mockOrderAccumulatorService.Setup(o => o.ProcessOrder(order)).Returns(false);

            bool isAccepted = mockOrderAccumulatorService.Object.ProcessOrder(order);

            Assert.False(isAccepted);
        }

        [Fact]
        public void GetExposure_ValidSymbol_ReturnsCorrectExposure()
        {
            var mockOrderAccumulatorService = new Mock<IOrderAccumulatorService>();
            var symbol = "PETR4";
            var exposure = new Exposure { Symbol = symbol, Value = 100000m };
            mockOrderAccumulatorService.Setup(o => o.GetExposure(symbol)).Returns(exposure);

            Exposure result = mockOrderAccumulatorService.Object.GetExposure(symbol);

            Assert.NotNull(result);
            Assert.Equal(symbol, result.Symbol);
            Assert.Equal(100000m, result.Value);
        }
    }
}
