using QuickFix;
using QuickFix.Transport;
public class FixConnection
{
    public SocketInitiator Initiator { get; private set; }

    public FixConnection(IApplication application, SessionSettings settings)
    {
        var storeFactory = new FileStoreFactory(settings);
        var logFactory = new FileLogFactory(settings);
        Initiator = new SocketInitiator(application, storeFactory, settings, logFactory);
    }
}
