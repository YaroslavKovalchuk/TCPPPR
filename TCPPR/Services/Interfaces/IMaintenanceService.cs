using System.Collections.Generic;
using System.Threading.Tasks;
using TCPPR.Data.Entities;

namespace TCPPR.Services.Interfaces
{
    /// <summary>
    /// Інтерфейс для управління заявками на ремонт: отримання, додавання, оновлення, видалення.
    /// </summary>
    public interface IMaintenanceService
    {
        Task<IEnumerable<MaintenanceRequest>> GetAllMaintenanceRequestsAsync(); // Отримання всіх заявок на ремонт

        Task<MaintenanceRequest> GetMaintenanceRequestByIdAsync(int requestId); // Отримання заявки на ремонт за ідентифікатором

        Task<bool> AddMaintenanceRequestAsync(MaintenanceRequest request); // Додавання нової заявки на ремонт

        Task<bool> UpdateMaintenanceRequestAsync(MaintenanceRequest request); // Оновлення інформації про заявку на ремонт

        Task<bool> DeleteMaintenanceRequestAsync(int requestId); // Видалення заявки на ремонт
    }
}