using System.Collections.Generic;
using System.Threading.Tasks;
using TCPPR.Data.Entities;

namespace TCPPR.Services.Interfaces
{
    /// <summary>
    /// Інтерфейс для управління запасами: отримання, додавання, оновлення, списання.
    /// </summary>
    public interface IInventoryService
    {
        /// <summary>
        /// Отримує всі записи запасних частин.
        /// </summary>
        /// <returns>Колекція всіх запасних частин.</returns>
        Task<IEnumerable<SparePart>> GetAllSparePartsAsync();

        /// <summary>
        /// Отримує інформацію про запасну частину за її ідентифікатором.
        /// </summary>
        /// <param name="sparePartId">Ідентифікатор запасної частини.</param>
        /// <returns>Об’єкт запасної частини або null, якщо частину не знайдено.</returns>
        Task<SparePart> GetSparePartByIdAsync(int sparePartId);

        /// <summary>
        /// Додає нову запасну частину до інвентарю.
        /// </summary>
        /// <param name="sparePart">Об’єкт запасної частини, який потрібно додати.</param>
        /// <returns>True, якщо частина додана успішно, інакше — False.</returns>
        Task<bool> AddSparePartAsync(SparePart sparePart);

        /// <summary>
        /// Оновлює інформацію про наявну запасну частину.
        /// </summary>
        /// <param name="sparePart">Об’єкт запасної частини з оновленою інформацією.</param>
        /// <returns>True, якщо оновлення пройшло успішно, інакше — False.</returns>
        Task<bool> UpdateSparePartAsync(SparePart sparePart);

        /// <summary>
        /// Видаляє запасну частину з інвентарю за її ідентифікатором.
        /// </summary>
        /// <param name="sparePartId">Ідентифікатор запасної частини.</param>
        /// <returns>True, якщо частина видалена успішно, інакше — False.</returns>
        Task<bool> DeleteSparePartAsync(int sparePartId);

        /// <summary>
        /// Зменшує кількість вказаної запасної частини.
        /// </summary>
        /// <param name="sparePartId">Ідентифікатор запасної частини.</param>
        /// <param name="quantity">Кількість для списання.</param>
        /// <returns>True, якщо списання пройшло успішно, інакше — False.</returns>
        Task<bool> DeductSparePartAsync(int sparePartId, int quantity);

        /// <summary>
        /// Оновлює кількість вказаної запасної частини на нове значення.
        /// </summary>
        /// <param name="sparePartId">Ідентифікатор запасної частини.</param>
        /// <param name="newQuantity">Нова кількість запасної частини.</param>
        /// <returns>True, якщо оновлення кількості пройшло успішно, інакше — False.</returns>
        Task<bool> UpdateSparePartQuantityAsync(int sparePartId, int newQuantity);
    }
}
