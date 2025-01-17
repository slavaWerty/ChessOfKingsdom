using GameRoot;
using ObservableCollections;
using States.GameResources;

public class FightEnterParams : SceneEnterParams
{
    public ObservableList<Resource> Resources { get; }

    public FightEnterParams(ObservableList<Resource> resources) : base(Utils.Scenes.Fight)
    {
        Resources = resources;
    }
}
