using Buildings;
using ObservableCollections;
using R3;
using System.Linq;
using States.GameResources;
using GamePlay.Settings;
using States.Military;

namespace StateRoot
{
    public class GameStateProxy
    {
        private readonly GameState _gameState;
        public ObservableList<BuildingEntityProxy> Buildings { get; } = new();
        public ObservableList<Military> Militaries { get; } = new();
        public ObservableList<Resource> Resources { get; } = new();

        public BuildingsSettings BuildSettings => _gameState.BuildingsSettings;

        public GameStateProxy(GameState gameState)
        {
            _gameState = gameState;

            InitBuildings(_gameState);
            InitResources(_gameState);
            InitMilitary(_gameState);
        }

        public int GreateEntityId()
        {
            return _gameState.CreateEntityId();
        }

        private void InitMilitary(GameState gameState)
        {
            gameState.Militaries.ForEach(b => Militaries.Add(new Military(b)));

            Militaries.ObserveAdd().Subscribe(e =>
            {
                var addedMilitaryentity = e.Value;

                gameState.Militaries.Add(addedMilitaryentity.Origin);
            });

            Militaries.ObserveRemove().Subscribe(e =>
            {
                var removeMilitaryentityProxy = e.Value;
                var removesBuildingEntity = gameState.Militaries.FirstOrDefault(b => b.Id == removeMilitaryentityProxy.Id);
                gameState.Militaries.Remove(removesBuildingEntity);
            });
        }

        private void InitBuildings(GameState gameState)
        {
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

        private void InitResources(GameState gameState)
        {
            gameState.Resource.ForEach(b => Resources.Add(new Resource(b)));

            Resources.ObserveAdd().Subscribe(e =>
            {
                var addedBuildingentity = e.Value;

                gameState.Resource.Add(addedBuildingentity.Origin);
            });

            Resources.ObserveRemove().Subscribe(e =>
            {
                var removeBuildingentityProxy = e.Value;
                var removesBuildingEntity = gameState.Resource.FirstOrDefault(b => b.ResourceType == removeBuildingentityProxy.ResourceType);
                gameState.Resource.Remove(removesBuildingEntity);
            });
        }
    }
}
