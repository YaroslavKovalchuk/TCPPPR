using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TCPPR.Data.DatabaseContext;

using TCPPR.Data.Entities;
using TCPPR.Services.Interfaces;

namespace TCPPR.Services.Services
{
    /// <summary>
    /// Сервіс для управління заявками на ремонт: додавання, оновлення, отримання інформації.
    /// </summary>
    public class MaintenanceService : IMaintenanceService
    {
        private readonly AppDbContext _dbContext;

        public MaintenanceService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MaintenanceRequest>> GetAllMaintenanceRequestsAsync()
        {
            return await _dbContext.MaintenanceRequests.ToListAsync();
        }

        public async Task<MaintenanceRequest> GetMaintenanceRequestByIdAsync(int requestId)
        {
            return await _dbContext.MaintenanceRequests.FindAsync(requestId);
        }

        public async Task<bool> AddMaintenanceRequestAsync(MaintenanceRequest request)
        {
            await _dbContext.MaintenanceRequests.AddAsync(request);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateMaintenanceRequestAsync(MaintenanceRequest request)
        {
            _dbContext.MaintenanceRequests.Update(request);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteMaintenanceRequestAsync(int requestId)
        {
            var request = await _dbContext.MaintenanceRequests.FindAsync(requestId);
            if (request == null)
                return false;

            _dbContext.MaintenanceRequests.Remove(request);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}