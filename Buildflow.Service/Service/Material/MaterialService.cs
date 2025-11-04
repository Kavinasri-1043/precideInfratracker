using Buildflow.Library.Repository;
using Buildflow.Library.Services.Interfaces;
using Buildflow.Utility.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Buildflow.Library.Services.Interfaces
{


   
        public interface IMaterialService
        {
            Task<IEnumerable<MaterialDto>> GetMaterialsByProjectAsync(int projectId);
            Task<IEnumerable<MaterialDto>> GetLowStockAlertsAsync(int projectId);
        // Add Create, Update, Delete methods later if needed
    }
}
  public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;

        public MaterialService(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public async Task<IEnumerable<MaterialDto>> GetMaterialsByProjectAsync(int projectId)
        {
            // Simply call the repository and return data
            var materials = await _materialRepository.GetMaterialsByProjectAsync(projectId);
            return materials;
        }
    public async Task<IEnumerable<MaterialDto>> GetLowStockAlertsAsync(int projectId)
    {
        var materials = await _materialRepository.GetLowStockAlertsAsync(projectId);
        return materials.Select(m =>
        {
            m.Level = "Urgent";
            return m;
        });
    }
}


