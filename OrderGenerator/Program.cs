using QuickFix;
using QuickFix.Transport;

var settings = new SessionSettings("OrderGenerator.cfg");
var orderGenerator = new OrderGeneratorService();
var app = new FixOrderGenerator(orderGenerator);
var storeFactory = new FileStoreFactory(settings);
var logFactory = new FileLogFactory(settings);
var initiator = new SocketInitiator(app, storeFactory, settings, logFactory);

initiator.Start();

while (true)
{
    var order = orderGenerator.GenerateOrder();
    app.SendOrder(order);
    Console.WriteLine("Sending Order...");
    Thread.Sleep(1000);
}

initiator.Stop();