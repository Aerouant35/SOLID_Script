public class VirtualPlayerCommand : GameCommand
{
    private string m_sVirtualPlayerName;
    
    public VirtualPlayerCommand(string virtualPlayerName)
    {
        m_sVirtualPlayerName = virtualPlayerName;
    }
    
    public override void Execute()
    {
        var currentPlayer = PlayerSpawnManager.instance.currentPlayer;
        var playerDic = PlayerSpawnManager.instance.playerDic;
        
        if (!playerDic.TryGetValue(m_sVirtualPlayerName, out var Name))
        {
            PlayerSpawnManager.instance.PlayerJoin(m_sVirtualPlayerName);
        }
            
        //disable old players selector
        if (currentPlayer != null)
        {
            currentPlayer.gameObject.GetComponent<InputComponent>().SetActive(false);
        }
            
        //Set new player
        currentPlayer = playerDic[m_sVirtualPlayerName];
            
        //Enbale new player selector
        if (currentPlayer != null)
        {
            currentPlayer.gameObject.GetComponent<InputComponent>().SetActive(true);
        }

        PlayerSpawnManager.instance.currentPlayer = currentPlayer;
    }
}
