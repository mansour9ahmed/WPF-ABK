using Utils;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ViewModels;

namespace ViewModels
{
    public class UpdateVesselViewModel : ModelViewBase
    {
        private IVesselService _service;
        private ICompanyService _comService;

        public VesselFacade Vessel { get; set; }
        public ObservableCollection<Company> Companies { get; set; }

        public UpdateVesselViewModel(IVesselService service,ICompanyService comService,Vessel vessel)
        {
            _service = service;
            _comService = comService;

            Companies = new ObservableCollection<Company>();

            foreach(var com in _comService.GetAllCompanies())
            {
                Companies.Add(com);
            }

            Vessel = new VesselFacade(vessel);

            UpdateVesselCommand = new RelayCommand(updateVessel);
            CancelCommand = new RelayCommand(goToVesselView);
        }

        public RelayCommand UpdateVesselCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        private void updateVessel()
        {
            _service.UpdateVessel(Vessel.vessel);
            goToVesselView();
        }

        private void goToVesselView()
        {
            Mediator.Notify("GoToView", "VesselView");
        }
    }
}
