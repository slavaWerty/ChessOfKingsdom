using UnityEngine;

namespace Settings.Castle
{
    [CreateAssetMenu(fileName = "BuildingLevelSettings", menuName = "Game Settings/Buildings/New Building Level Settings")]
    public class BuildingsLevelSettings : ScriptableObject
    {
        public int Level;
        public double BaseIncome;
    }
}
