using GameRoot;
using Utils;

public class FightEnterParams : SceneEnterParams
{
    public int TestNumber { get; }

    public FightEnterParams(int TestNumber) : base(Scenes.Fight)
    {
        this.TestNumber = TestNumber;
    }
}
