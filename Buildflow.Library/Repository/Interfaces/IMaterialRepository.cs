
using System;
using System.Collections.Generic;
using Buildflow.Utility.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buildflow.Library.Services.Interfaces
{
    public interface IMaterialService
    {
        Task<IEnumerable<MaterialDto>> GetMaterialsByProjectAsync(int projectId);
    }
}
