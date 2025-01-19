using R3;
using System.Collections.Generic;
using System;
using ObservableCollections;
using Fight.View.Militaries;
using States.Military;
using Fight.Commands;
using System.Linq;
using UnityEngine;

namespace Fight.Services
{
    public class MilitaryService
    {
        public readonly ObservableList<MilitaryViewModel> Militaries = new();

        public readonly Dictionary<int, MilitaryViewModel> _militariesMap = new();
        private readonly ICommandProcessor _cmd;

        public MilitaryService(ObservableList<Military> militaries, ICommandProcessor cmd)
        {
            _cmd = cmd;

            militaries.ForEach(CreateMilitaryViewModel);

            militaries.ObserveAdd().Subscribe(e => CreateMilitaryViewModel(e.Value));
            militaries.ObserveRemove().Subscribe(e => RemoveMilitaryViewModel(e.Value));
        }

        public bool AddSquads(int id, List<Squad> squads)
        {
            var command = new CmdSquadAdd(id, squads);

            return _cmd.Process(command);
        }

        public bool TrySpendSquads(int id, List<Squad> squads)
        {
            var command = new CmdSquadSpend(id, squads);

            return _cmd.Process(command);
        }

        public ObservableList<Squad> ObserveMilitary(int id)
        {
            if (_militariesMap.TryGetValue(id, out var militaryViewModel))
            {
                return militaryViewModel.SquadEntityes;
            }

            throw new Exception($"Resource of type {id} doesn't exist");
        }

        private void CreateMilitaryViewModel(Military military)
        {
            var militaryViewModel = new MilitaryViewModel(military);

            for (int i = 0; i < military.SquadeDates.Count; i++)
            {
                var squad = military.SquadeDates
                    .FirstOrDefault(s => s.SquadType == military.SquadeDates[i].SquadType);

                militaryViewModel.SquadViewModels.Add(CreateSquadViewModel(squad));
            }

            _militariesMap[military.Id] = militaryViewModel;

            Militaries.Add(militaryViewModel);
        }

        private void RemoveMilitaryViewModel(Military military)
        {
            if (_militariesMap.TryGetValue(military.Id, out var militaryViewModel))
            {
                Militaries.Remove(militaryViewModel);
                _militariesMap.Remove(military.Id);
            }
        }

        private SquadViewModel CreateSquadViewModel(Squad squad)
        {
            var squadViewModel = new SquadViewModel(squad);

            return squadViewModel;
        }

        public void RemoveSquadViewModel(Squad squad, int id)
        {
            if (_militariesMap.TryGetValue(id, out var militaryViewModel))
            {
                var squadViewModel = militaryViewModel.SquadViewModels
                    .FirstOrDefault(s => s.SquadType == squad.SquadType);

                if(squadViewModel == null)
                    Debug.LogError("Error");

                militaryViewModel.SquadViewModels.Remove(squadViewModel);
            }
        }
    }
}
