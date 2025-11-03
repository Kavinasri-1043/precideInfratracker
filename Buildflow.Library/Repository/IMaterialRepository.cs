using Buildflow.Utility.DTO;

namespace Buildflow.Library.Repository
{
    public interface IMaterialRepository
    {
        Task<IEnumerable<MaterialDto>> GetMaterialsByProjectIdAsync(int projectId);
    }
}