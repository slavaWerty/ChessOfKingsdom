using MainMenu;

namespace GamePlay
{
    public class CastleExitParams
    {
        public MenuEnterParams MainMenuEnterParams { get; }

        public CastleExitParams(MenuEnterParams mainMenuEnterParams)
        {
            MainMenuEnterParams = mainMenuEnterParams;
        }
    }
}
