using UnityEngine;

namespace GamePlay.Services.BuildingsAbilities
{
    public class TestBuildingAbility : IBuildingAbility
    {
        public void Execute()
        {
            Debug.Log("Test");
        }
    }
}
