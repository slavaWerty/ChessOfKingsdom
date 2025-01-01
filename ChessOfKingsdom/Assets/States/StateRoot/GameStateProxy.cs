using Buildings;
using ObservableCollections;
using R3;
using System.Linq;

namespace StateRoot
{
    public class GameStateProxy
    {
        private readonly GameState _gameState;
        public ObservableList<BuildingEntityProxy> Buildings { get; } = new();

        public GameStateProxy(GameState gameState)
        {
            _gameState = gameState;

            gameState.Buildings.ForEach(b => Buildings.Add(new BuildingEntityProxy(b)));

            Buildings.ObserveAdd().Subscribe(e =>
            {
                var addedBuildingentity = e.Value;

                gameState.Buildings.Add(addedBuildingentity.Origin);
            });

            Buildings.ObserveRemove().Subscribe(e =>
            {
                var removeBuildingentityProxy = e.Value;
                var removesBuildingEntity = gameState.Buildings.FirstOrDefault(b => b.Id == removeBuildingentityProxy.Id);
                gameState.Buildings.Remove(removesBuildingEntity);
            });
        }

        public int GetEntityId()
        {
            return _gameState.CreateEntityId();
        }
    }
}
