using GameRoot;
using Utils;

namespace GamePlay
{
    public class CastleEnterParams : SceneEnterParams
    {
        public string SaveFileName { get; }
        public int LevelNumber { get; }

        public CastleEnterParams(string saveFileName, int levelNumber) : base(Scenes.GASTLE)
        {
            SaveFileName = saveFileName;
            LevelNumber = levelNumber;
        }
    }
}
