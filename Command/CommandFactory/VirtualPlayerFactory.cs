public class VirtualPlayerFactory
{
    public static GameCommand GetVirtualPlayerCommand(string virtualPlayer)
    {
        return string.IsNullOrEmpty(virtualPlayer) ? null : new VirtualPlayerCommand(virtualPlayer);
    }
}
