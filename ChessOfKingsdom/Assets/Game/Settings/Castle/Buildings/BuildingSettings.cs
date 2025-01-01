using System.Collections.Generic;
using UnityEngine;

namespace Settings.Castle
{
    [CreateAssetMenu(fileName = "BuildingsSettings", menuName = "Game Settings/Buildings/New Building Settings"        )]
    public class BuildingSettings : ScriptableObject
    {
        public string TypeId;
        public string TitleId;
        public string DescriptionID;
        public List<BuildingsLevelSettings> BuildingsLevelSettings;
    }
}
