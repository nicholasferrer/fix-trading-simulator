using System;
using QuickFix;
using QuickFix.Fields;
using OrderAccumulator.Domain.Entities;

namespace OrderAccumulator.Infra.Services
{
    public class FixOrderAccumulator : IApplication
    {
        private readonly IOrderAccumulatorService _orderAccumulator;
        private SessionID _sessionID;

        public FixOrderAccumulator(IOrderAccumulatorService orderAccumulator)
        {
            _orderAccumulator = orderAccumulator;
        }

        public void FromAdmin(Message message, SessionID sessionID) { }

        public void FromApp(Message message, SessionID sessionID)
        {
            if (message is QuickFix.FIX44.NewOrderSingle newOrderSingle)
            {
                ProcessNewOrderSingle(newOrderSingle);
            }
        }

        public void OnCreate(SessionID sessionID) 
        {
            _sessionID = sessionID;
        }

        public void OnLogout(SessionID sessionID) { }

        public void OnLogon(SessionID sessionID) { }

        public void ToAdmin(Message message, SessionID sessionID) { }

        public void ToApp(Message message, SessionID sessionID) { }

        private void ProcessNewOrderSingle(QuickFix.FIX44.NewOrderSingle newOrderSingle)
        {
            var order = new Order
            {
                Symbol = newOrderSingle.Symbol.Obj,
                IsBuy = newOrderSingle.Side.Obj == Side.BUY,
                Quantity = (int)newOrderSingle.OrderQty.Obj,
                Price = (decimal)newOrderSingle.Price.Obj
            };

            bool isAccepted = _orderAccumulator.ProcessOrder(order);

            PrintNewOrderSingle(newOrderSingle);

            if (isAccepted)
            {
                SendExecutionReport(newOrderSingle);
                Console.WriteLine("Order Accepted!");
                Console.WriteLine("---------------------------------------");
            }
            else
            {
                SendOrderReject(newOrderSingle);
                Console.WriteLine("Order Rejected!");
                Console.WriteLine("---------------------------------------");
            }
        }


        private void SendExecutionReport(QuickFix.FIX44.NewOrderSingle newOrderSingle)
        {
            var executionReport = new QuickFix.FIX44.ExecutionReport(
                new OrderID(Guid.NewGuid().ToString()),
                new ExecID(Guid.NewGuid().ToString()),
                new ExecType(ExecType.FILL),
                new OrdStatus(OrdStatus.FILLED),
                newOrderSingle.Symbol,
                newOrderSingle.Side,
                new LeavesQty(0),
                new CumQty(newOrderSingle.OrderQty.Obj),
                new AvgPx(newOrderSingle.Price.Obj));

            executionReport.ClOrdID = newOrderSingle.ClOrdID;
            executionReport.OrderQty = newOrderSingle.OrderQty;
            executionReport.LastQty = new LastQty(newOrderSingle.OrderQty.Obj);
            executionReport.LastPx = new LastPx(newOrderSingle.Price.Obj);

            Session.SendToTarget(executionReport, _sessionID);
        }


        private void SendOrderReject(QuickFix.FIX44.NewOrderSingle newOrderSingle)
        {
            var orderReject = new QuickFix.FIX44.OrderCancelReject(
                new OrderID(Guid.NewGuid().ToString()),
                newOrderSingle.ClOrdID,
                new OrigClOrdID(newOrderSingle.ClOrdID.getValue()),
                new OrdStatus(OrdStatus.REJECTED),
                new CxlRejResponseTo(CxlRejResponseTo.ORDER_CANCEL_REQUEST));

            orderReject.SetField(newOrderSingle.Symbol);
            orderReject.SetField(newOrderSingle.Side);
            orderReject.SetField(newOrderSingle.OrderQty);

            Session.SendToTarget(orderReject, _sessionID);
        }

        private string SideToString(Side side)
        {
            if (side.Obj == Side.BUY)
            {
                return "BUY";
            }
            else if (side.Obj == Side.SELL)
            {
                return "SELL";
            }
            else
            {
                return "UNKNOWN";
            }
        }

        private void PrintNewOrderSingle(QuickFix.FIX44.NewOrderSingle newOrderSingle)
        {
            Console.WriteLine($"NewOrderSingle details:");
            Console.WriteLine($"Symbol: {newOrderSingle.Symbol.Obj}");
            Console.WriteLine($"Side: {SideToString(newOrderSingle.Side)}");
            Console.WriteLine($"OrderQty: {newOrderSingle.OrderQty.Obj}");
            Console.WriteLine($"Price: {newOrderSingle.Price.Obj}");
            Console.WriteLine();
        }


    }
}
