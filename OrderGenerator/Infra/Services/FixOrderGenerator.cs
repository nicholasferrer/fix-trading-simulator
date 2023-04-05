using QuickFix;
using QuickFix.Fields;
using OrderGenerator.Domain.Entities;
using OrderGenerator.Domain.Interfaces;

public class FixOrderGenerator : IApplication
{
    private readonly IOrderGeneratorService _orderGenerator;
    private SessionID _sessionID;

    public FixOrderGenerator(IOrderGeneratorService orderGenerator)
    {
        _orderGenerator = orderGenerator;
    }

    public void FromAdmin(Message message, SessionID sessionID) { }

    public void FromApp(Message message, SessionID sessionID) { }

    public void OnCreate(SessionID sessionID)
    {
        _sessionID = sessionID;
    }

    public void OnLogout(SessionID sessionID) { }

    public void OnLogon(SessionID sessionID) { }

    public void ToAdmin(Message message, SessionID sessionID) { }

    public void ToApp(Message message, SessionID sessionID) { }

    public void SendOrder(Order order)
    {
        var newOrderSingle = new QuickFix.FIX44.NewOrderSingle(
            new ClOrdID(Guid.NewGuid().ToString()),
            new Symbol(order.Symbol),
            new Side(order.IsBuy ? Side.BUY : Side.SELL),
            new TransactTime(DateTime.UtcNow.ToUniversalTime()),
            new OrdType(OrdType.LIMIT));

        newOrderSingle.OrderQty = new OrderQty(order.Quantity);
        newOrderSingle.Price = new Price(order.Price);

        Session.SendToTarget(newOrderSingle, _sessionID);
    }
}
