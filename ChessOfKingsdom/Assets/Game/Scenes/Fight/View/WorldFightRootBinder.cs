using Fight.Services;
using Fight.View.Militaries;
using States.Military;
using System.Collections.Generic;
using UnityEngine;

namespace Fight.View
{
    public class WorldFightRootBinder : MonoBehaviour
    {
        private MilitaryService _militaryService;

        private readonly Dictionary<int, MilitaryBinder> _createMilitaryMap = new();

        public void Bind(WorldFightRootViewModel viewModel)
        {
            _militaryService = viewModel.MilitaryService;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            { 
                _militaryService.AddSquads(1, new List<Squad>()
                {
                    new Squad(new SquadEntity()
                    {
                        Amount = 1,
                        Type = SquadType.Rook
                    }),
                    new Squad(new SquadEntity()
                    {
                        Amount = 1,
                        Type = SquadType.Pawn
                    }),
                    new Squad(new SquadEntity()
                    {
                        Amount = 1,
                        Type = SquadType.Queen
                    })
                });

                Debug.Log(_militaryService._militariesMap[0]);
            }
        }

        public MilitaryBinder CreateMilitaryBinder(MilitaryViewModel viewModel)
        {
            var militaryId = viewModel.Id;
            var prefapMilitaryLevelPath = $"Prefabs/Fight/Militaries/Military_{militaryId}";
            var militaryPrefap = Resources.Load<MilitaryBinder>(prefapMilitaryLevelPath);

            var createdMilitary = Instantiate(militaryPrefap);

            createdMilitary.transform.position = new Vector3(0, 0, 0);

            foreach (var squadViewModel in viewModel.SquadViewModels)
            {
                var squadBinder = CreateSquadBinder(squadViewModel);

                createdMilitary.SquadBindersMap[(int)squadViewModel.SquadType] = squadBinder;

                squadBinder.transform.SetParent(createdMilitary.transform);
                squadBinder.transform.position = new Vector3(0, 0, 0);
            }

            createdMilitary.Bind(viewModel);
            _createMilitaryMap[viewModel.Id] = createdMilitary;

            return createdMilitary;
        }

        public SquadBinder CreateSquadBinder(SquadViewModel viewModel)
        {
            var squadType = viewModel.SquadType;
            var prefapSquadLevelPath = $"Prefabs/Fight/Squads/Squad_{(int)squadType}";
            var squadPrefap = Resources.Load<SquadBinder>(prefapSquadLevelPath);

            var createdSquad = Instantiate(squadPrefap);
            createdSquad.Bind(viewModel);

            return createdSquad;
        }
    }
}
