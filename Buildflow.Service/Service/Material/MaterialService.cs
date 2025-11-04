using Buildflow.Library.Repository;
using Buildflow.Library.Services.Interfaces;
using Buildflow.Utility.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Buildflow.Service.Service.Material
{


   
        public interface IMaterialService
        {
            Task<IEnumerable<MaterialDto>> GetMaterialsByProjectAsync(int projectId);
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
            var materials = await _materialRepository.GetMaterialsByProjectIdAsync(projectId);
            return materials;
        }
    }


