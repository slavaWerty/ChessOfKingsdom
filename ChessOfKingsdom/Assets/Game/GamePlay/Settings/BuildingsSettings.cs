using UnityEngine;

namespace GamePlay.Settings
{
    [CreateAssetMenu(fileName = "BuildingsSettings", menuName = "Game NewSettings/Buildings/New Buildings Settings")]
    public class BuildingsSettings : ScriptableObject
    {
        public int Id;
        public BuildingsInitzialSettings InitzialSettings;
    }
}
