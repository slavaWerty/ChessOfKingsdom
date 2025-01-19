using GamePlay;

public class FightExitParams 
{
    public CastleEnterParams CastleEnterParams { get; }

    public FightExitParams(CastleEnterParams mainMenuEnterParams)
    {
        CastleEnterParams = mainMenuEnterParams;
    }
}
