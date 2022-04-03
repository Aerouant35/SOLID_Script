public class PauseFactory
{
    public static GameCommand GetPauseCommand()
    {
        var uiManager =  UiManager.inst;
        
        return uiManager.goWin.activeSelf || uiManager.goLoose.activeSelf ? null : new PauseCommand(uiManager.goPause);
    }
}
