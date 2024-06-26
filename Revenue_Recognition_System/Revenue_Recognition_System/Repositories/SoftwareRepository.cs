using Microsoft.EntityFrameworkCore;
using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Repositories;

public class SoftwareRepository : ISoftwareRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public SoftwareRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Software?> GetSoftwareAsync(int id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetDBContext().Softwares
            .Include(s => s.Category)
            .Include(s => s.Contracts)
            .Where(software => software.IdSoftware == id)
            .FirstOrDefaultAsync(cancellationToken);
    }
}