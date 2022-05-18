using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyStore.Data;
using MyStore.Domain.Entities;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Services
{

    public interface ISupplierService
    {
        SupplierModel AddSupplier(SupplierModel newSupplier);
        bool Delete(int id);
        bool Exists(int id);
        IEnumerable<SupplierModel> GetAllSuppliers();
        SupplierModel GetById(int id);
        void UpdateSupplier(SupplierModel model);
    }

    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository supplierRepository;
        private readonly IMapper mapper;

        public SupplierService(ISupplierRepository supplierRepository, IMapper mapper)
        {
            this.supplierRepository = supplierRepository;
            this.mapper = mapper;
        }

        public IEnumerable<SupplierModel> GetAllSuppliers()
        {
            var allSuppliers = supplierRepository.GetAll().ToList();//List<Supplier>
                                                                    //transform domain objects from List<Supplier> -> List<SupplierModel>
            var supplierModels = mapper.Map<IEnumerable<SupplierModel>>(allSuppliers);

            return supplierModels;
        }

        public SupplierModel GetById(int id)
        {
            var supplierToGet = supplierRepository.GetById(id);
            return mapper.Map<SupplierModel>(supplierToGet);
        }
        public bool Exists(int id)
        {
            return supplierRepository.Exists(id);
        }
        public SupplierModel AddSupplier(SupplierModel newSupplier)
        {
            Supplier supplierToAdd = mapper.Map<Supplier>(newSupplier);
            var addedSupplier = supplierRepository.Add(supplierToAdd);
            newSupplier = mapper.Map<SupplierModel>(addedSupplier);
            return newSupplier;
        }
        public void UpdateSupplier(SupplierModel model)
        {
            Supplier supplierToUpdate = mapper.Map<Supplier>(model);
            supplierRepository.Update(supplierToUpdate);
        }
        public bool Delete(int id)
        {
            Supplier itemToDelete = supplierRepository.GetById(id);
            return supplierRepository.Delete(itemToDelete);
        }
    }
}
