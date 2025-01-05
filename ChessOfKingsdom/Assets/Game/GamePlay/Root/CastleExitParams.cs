using MainMenu;

namespace GamePlay
{
    public class CastleExitParams
    {
        public MenuEnterParams MainMenuEnterParams { get; }
        public FightEnterParams FightEnterParams { get; }

        public CastleExitParams(MenuEnterParams mainMenuEnterParams, FightEnterParams fightEnterParams)
        {
            MainMenuEnterParams = mainMenuEnterParams;
            FightEnterParams = fightEnterParams;
        }
    }
}
