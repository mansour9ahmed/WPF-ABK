using Utils;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ViewModels
{
    public class AddVesselViewModel : ModelViewBase
    {
        private IVesselService _service;

        public ObservableCollection<Company> Companies { get; set; }

        public VesselFacade Vessel { get; set; }

        public AddVesselViewModel(IVesselService service, ICompanyService comService)
        {
            _service = service;
            Vessel = new VesselFacade();
            Vessel.ActivationDate = DateTime.Now;

            Companies = new ObservableCollection<Company>();

            comService.GetAllCompanies().ForEach(c => { Companies.Add(c); });

            SaveCommand = new RelayCommand(addNewVessel);
            CancelCommand = new RelayCommand(goTOVessel);
        }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        private void addNewVessel()
        {
            _service.CreateVessel(Vessel.vessel);
            goTOVessel();
        }

        private void goTOVessel()
        {
            Mediator.Notify("GoToView", "VesselView");
        }

    }
}
