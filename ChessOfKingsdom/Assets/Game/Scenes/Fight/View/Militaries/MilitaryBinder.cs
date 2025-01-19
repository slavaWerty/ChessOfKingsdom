using System.Collections.Generic;
using UnityEngine;

namespace Fight.View.Militaries
{
    public class MilitaryBinder : MonoBehaviour
    {
        [field: SerializeField] private List<Transform> squadPoints = new();

        public readonly Dictionary<int, SquadBinder> SquadBindersMap = new();

        public void Bind(MilitaryViewModel viewModel)
        {
            foreach (var entity in viewModel.SquadEntityes)
            {
                if (!SquadBindersMap.ContainsKey((int)entity.SquadType))
                    continue;

                Debug.Log((int)entity.SquadType);

                SquadBindersMap[(int)entity.SquadType].transform.position
                    = squadPoints[(int)entity.SquadType].position;
            }
        }
    }
}
