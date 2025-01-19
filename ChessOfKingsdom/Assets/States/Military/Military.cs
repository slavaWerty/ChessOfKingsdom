using ObservableCollections;
using R3;
using System.Linq;

namespace States.Military
{
    public class Military
    {
        public readonly MilitaryData Origin;
        public ObservableList<Squad> SquadeDates { get; } = new();
        public int Id => Origin.Id;

        public Military(MilitaryData data)
        {
            Origin = data;

            Origin.SquadEntityes.ForEach(b => SquadeDates.Add(new Squad(b)));

            SquadeDates.ObserveAdd().Subscribe(e =>
            {
                var addedSquadeEntity = e.Value;

                Origin.SquadEntityes.Add(addedSquadeEntity.Origin);
            });

            SquadeDates.ObserveRemove().Subscribe(e =>
            {
                var removeSquadentityProxy = e.Value;
                var removesSquadeEntity = Origin.SquadEntityes.FirstOrDefault(b => b.Id == removeSquadentityProxy.Origin.Id);
                Origin.SquadEntityes.Remove(removesSquadeEntity);
            });
        }
    }
}
