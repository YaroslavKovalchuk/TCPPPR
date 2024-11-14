using System.Collections.Generic;
using System.Threading.Tasks;
using TCPPR.Data.Entities;

namespace TCPPR.Services.Interfaces
{
    /// <summary>
    /// Інтерфейс для управління обладнанням: додавання, оновлення, отримання інформації.
    /// </summary>
    public interface IEquipmentService
    {
        Task<IEnumerable<Equipment>> GetAllEquipmentAsync(); // Отримання всіх записів обладнання

        Task<Equipment> GetEquipmentByIdAsync(int equipmentId); // Отримання обладнання за ідентифікатором

        Task<bool> AddEquipmentAsync(Equipment equipment); // Додавання нового обладнання

        Task<bool> UpdateEquipmentAsync(Equipment equipment); // Оновлення інформації про обладнання

        Task<bool> DeleteEquipmentAsync(int equipmentId); // Видалення обладнання
    }
}