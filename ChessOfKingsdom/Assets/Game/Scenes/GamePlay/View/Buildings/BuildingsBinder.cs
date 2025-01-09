using GamePlay.View.Buildings;
using UnityEngine;

namespace View.Buildings
{
    public class BuildingsBinder : MonoBehaviour
    {
        [SerializeField] private string _typeId;

        public string TypeId => _typeId;

        public SpriteRenderer MainRenderer;
        public Vector2Int Size = Vector2Int.one;

        public Vector2Int Position => new Vector2Int((int)transform.position.x,
            (int)transform.position.y);

        public void Bind(BuildingsViewModel viewModel)
        {
            transform.position = new Vector3(viewModel.Position.CurrentValue.x, viewModel.Position.CurrentValue.y, 0);
            viewModel.StartExucute();
        }

        public void SetTransparent(bool available)
        {
            if (available)
            {
                MainRenderer.color = Color.green;
            }
            else
            {
                MainRenderer.color = Color.red;
            }
        }

        public void SetNormal()
        {
            MainRenderer.color = Color.white;
        }

        private void OnDrawGizmos()
        {
            for (int x = 0; x < Size.x; x++)
            {
                for (int y = 0; y < Size.y; y++)
                {
                    if ((x + y) % 2 == 0) Gizmos.color = new Color(0.88f, 0f, 1f, 0.3f);
                    else Gizmos.color = new Color(1f, 0.68f, 0f, 0.3f);

                    Gizmos.DrawCube(transform.position + new Vector3(x, y, 0), new Vector3(1f, 1f, 0));
                }
            }
        }
    }
}
