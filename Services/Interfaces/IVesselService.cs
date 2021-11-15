using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IVesselService
    {
        List<Vessel> GetAllVessels();
        void CreateVessel(Vessel vessel);
        void UpdateVessel(Vessel vessel);
        bool DeleteVessel(int id);

        List<Vessel> SearchForVesselByName(string name);
    }
}
