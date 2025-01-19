using States.Military;
using UnityEngine;

namespace Fight.View.Militaries
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SquadBinder : MonoBehaviour
    {
        [SerializeField] private GameObject _circleTestPrefap;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public SquadType Type { get; private set; }
        public float Speed { get; private set; }

        private void OnValidate() 
            => _spriteRenderer ??= GetComponent<SpriteRenderer>();

        public void Bind(SquadViewModel viewModel)
        {
            Type = viewModel.SquadType;
            Speed = viewModel.Speed;

            _spriteRenderer.sprite = _circleTestPrefap.
                GetComponent<SpriteRenderer>().sprite;

            transform.localScale = _circleTestPrefap.transform.localScale 
                * viewModel.Amount.CurrentValue;
        }
    }
}
