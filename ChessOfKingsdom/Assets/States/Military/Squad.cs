using R3;

namespace States.Military
{
    public class Squad
    {
        public readonly SquadEntity Origin;

        public readonly ReactiveProperty<int> Amount;
        public readonly ReactiveProperty<float> Speed;

        public SquadType SquadType => Origin.Type;

        public Squad(SquadEntity data)
        {
            Origin = data;
            Amount = new ReactiveProperty<int>(data.Amount);
            Speed = new ReactiveProperty<float>(data.Speed);

            Amount.Subscribe(newValue => data.Amount = newValue);
            Speed.Subscribe(newValue => data.Speed = newValue);
        }
    }
}
