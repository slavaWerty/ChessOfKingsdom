using GamePlay.View.Buildings;
using UnityEngine;

namespace View.Buildings
{
    public class BuildingsBinder : MonoBehaviour
    {
        public void Bind(BuildingsViewModel viewModel)
        {
            transform.position = new Vector3(viewModel.Position.CurrentValue.x, viewModel.Position.CurrentValue.y, 0);
        }
    }
}
