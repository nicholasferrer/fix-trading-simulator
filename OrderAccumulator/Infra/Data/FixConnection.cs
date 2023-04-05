using QuickFix;

public class FixConnection
{
    public ThreadedSocketAcceptor Acceptor { get; private set; }

    public FixConnection(IApplication application, SessionSettings settings)
    {
        var storeFactory = new FileStoreFactory(settings);
        var logFactory = new FileLogFactory(settings);
        Acceptor = new ThreadedSocketAcceptor(application, storeFactory, settings, logFactory);
    }
}
