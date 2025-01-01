using R3;
using StateRoot;

namespace States
{
    public interface IGameStateProvider
    {
        public GameStateProxy GameState { get; }
        // public GameSettingsStateProxy Settings {get;} и добавить для этого значения методы загруски сохранения ресетирования

        public Observable<GameStateProxy> LoadGameState();

        public Observable<bool> SaveGameState();
        public Observable<bool> ResetGameState();
    }
}
