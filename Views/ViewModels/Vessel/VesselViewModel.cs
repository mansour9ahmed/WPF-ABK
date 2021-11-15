using Utils;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ViewModels
{
    public class VesselViewModel : BindableBase,ModelViewBase
    {
        private IVesselService _service;
        public ObservableCollection<Vessel> Vessels { get; set; } = new ObservableCollection<Vessel>();

        private Vessel _selectedVessel;
        private string _searchTxt;
        public Vessel SelectedVessel
        {
            get { return _selectedVessel; }
            set { SetProperty(ref _selectedVessel, value); }
        }
        public string SearchTxt
        {
            get { return _searchTxt; }
            set
            {
                if(_searchTxt != value)
                {
                    _searchTxt = value;
                    search(_searchTxt);
                    OnPropertyChanged("SearchTxt");
                }
            }
        }

        public VesselViewModel(IVesselService service)
        {
            _service = service;

            foreach(var vessel in _service.GetAllVessels())
            {
                Vessels.Add(vessel);
            }

            GoToUpdateVesselCommand = new RelayCommand<Vessel>(goToUpdateVessel);
            GoToAddVesselCommand = new RelayCommand(goToAddVessel);
            DeleteVesselCommand = new RelayCommand<Vessel>(deleteVessel);


            _selectedVessel = null;
        }

        public RelayCommand<Vessel> GoToUpdateVesselCommand { get; set; }
        public RelayCommand GoToAddVesselCommand { get; set; }
        public RelayCommand GoTOUpdateVesslCommand { get; set; }
        public RelayCommand<Vessel> DeleteVesselCommand { get; set; }
        

        private void goToUpdateVessel(Vessel vessel)
        {
            Mediator.Notify("GoToView", "UpdateVesselView", vessel);
        }

        private void goToAddVessel()
        {
            Mediator.Notify("GoToView", "AddVesselView");
        }

        private void search(string name)
        {
           var vessels = _service.SearchForVesselByName(name);
            if(vessels != null)
            {
                this.Vessels.Clear();
                foreach(var v in vessels)
                {
                    this.Vessels.Add(v);
                }
            }
        }

        private void deleteVessel(Vessel vessel)
        {
            _service.DeleteVessel(vessel.Id);
            Vessels.Remove(vessel);
        }
    }
}
