using Microsoft.EntityFrameworkCore;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Services
{
    public class VesselService : IVesselService
    {
        private BurakDbContext _context;

        public VesselService(BurakDbContext context)
        {
            this._context = context;
        }

        public void CreateVessel(Vessel vessel)
        {

            if(vessel.Name == null || vessel.Name == "")
            {
                throw new Exception("Name cannot be empty");
            }

            if(_context.Vessels.Any(v => v.Name == vessel.Name))
            {
                throw new Exception("Name is already existed");
            }

            if(vessel.CompanyId == 0 && vessel.Company == null)
            {
                throw new Exception("Please select company");
            }

            _context.Vessels.Add(vessel);
            _context.SaveChanges();

        }

        public bool DeleteVessel(int id)
        {
            var vessel = _context.Vessels.Where(v => v.Id == id).Include(v => v.Invoices).FirstOrDefault();
            if (vessel == null)
            {
                throw new Exception("Vessel is not existed");
            }

            if(vessel.Invoices != null && vessel.Invoices.Count > 0)
            {
                throw new Exception("Vessel cant be deleted because it has invoices, you have to delete its invoices first");
            }

            _context.Vessels.Remove(vessel);
            _context.SaveChanges();
            return true;
        }

        public List<Vessel> GetAllVessels()
        {
            return _context.Vessels.AsNoTracking().OrderBy(v => v.Name).Include(v => v.Company).ThenInclude(c => c.Bank).ToList();
        }

        public List<Vessel> SearchForVesselByName(string name)
        {
            if(name == null || string.Empty == name)
            {
                return _context.Vessels.ToList();
            }
            string tmp = name.ToLower();
            return _context.Vessels.AsNoTracking().Where(v => v.Name.ToLower().Contains(tmp)).ToList();
        }

        public void UpdateVessel(Vessel vessel)
        {
            if(!_context.Vessels.Any(v => v.Id == vessel.Id))
            {
                throw new Exception("Vessel not found");
            }

            if(vessel.Name == null || vessel.Name == "")
            {
                throw new Exception("Name cannot be empty");
            }

            if(_context.Vessels.Any(v => v.Name == vessel.Name && v.Id != vessel.Id))
            {
                throw new Exception("Name is already existed");
            }

            if(vessel.CompanyId == 0 && vessel.Company == null)
            {
                throw new Exception("Please select company");
            }

            _context.Vessels.Update(vessel);
            _context.SaveChanges();
        }
    }
}
