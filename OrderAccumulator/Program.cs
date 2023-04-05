using QuickFix;
using OrderAccumulator.Infra.Services;

var settings = new SessionSettings("OrderAccumulator.cfg");
var orderAccumulator = new OrderAccumulatorService();
var app = new FixOrderAccumulator(orderAccumulator);
var storeFactory = new FileStoreFactory(settings);
var logFactory = new FileLogFactory(settings);
var acceptor = new ThreadedSocketAcceptor(app, storeFactory, settings, logFactory);

acceptor.Start();

Console.WriteLine("Pressione qualquer tecla para sair...");
Console.ReadKey();

acceptor.Stop();