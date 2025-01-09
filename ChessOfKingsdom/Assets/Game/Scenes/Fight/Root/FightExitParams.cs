using GamePlay;

public class FightExitParams 
{
    public CastleEnterParams MainMenuEnterParams { get; }

    public FightExitParams(CastleEnterParams mainMenuEnterParams)
    {
        MainMenuEnterParams = mainMenuEnterParams;
    }
}
