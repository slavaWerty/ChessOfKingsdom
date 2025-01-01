using System.Collections.Generic;
using UnityEngine;

namespace Settings.Castle.Buildings
{
    [CreateAssetMenu(fileName = "BuildingSettings", menuName = "Game Settings/Buildings/New Buildings Settings")]
    public class BuildingsSettings : ScriptableObject
    {
        public List<BuildingSettings> AllBuildings;
    }
}
