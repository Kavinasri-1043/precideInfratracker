using Buildflow.Library.Repository;
using Buildflow.Library.Repository.Interfaces;
using Buildflow.Utility.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildflow.Service.Service.InventoryMaterial
{
    public interface IInventoryMaterialService
    {
        Task<IEnumerable<InventoryMaterialcsDto>> GetMaterialStatusAsync();
    }
    public class InventoryMaterialService : IInventoryMaterialService
    {
        private readonly IInventoryMaterialRepository _InventoryMaterialrepository;

        public InventoryMaterialService(IInventoryMaterialRepository inventorymaterialRepository)
        {
            _InventoryMaterialrepository = inventorymaterialRepository;
        }

        public async Task<IEnumerable<InventoryMaterialcsDto>> GetMaterialStatusAsync()
        {
            return await _InventoryMaterialrepository.GetMaterialStatusAsync();
        }
    }


}

