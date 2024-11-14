using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TCPPR.Data.DatabaseContext;
using TCPPR.Data.Entities;
using TCPPR.Services.Interfaces;

namespace TCPPR.Services.Services
{
    /// <summary>
    /// Сервіс для управління запасами: додавання, оновлення, отримання, списання.
    /// </summary>
    public class InventoryService : IInventoryService
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Конструктор, який приймає контекст бази даних для доступу до запасних частин.
        /// </summary>
        /// <param name="dbContext">Контекст бази даних</param>
        public InventoryService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Отримання всіх записів запасних частин з бази даних.
        /// </summary>
        /// <returns>Список всіх запасних частин</returns>
        public async Task<IEnumerable<SparePart>> GetAllSparePartsAsync()
        {
            return await _dbContext.SpareParts.ToListAsync();
        }

        /// <summary>
        /// Отримання запасної частини за її ідентифікатором.
        /// </summary>
        /// <param name="sparePartId">Ідентифікатор запасної частини</param>
        /// <returns>Запасна частина, або null, якщо її не знайдено</returns>
        public async Task<SparePart> GetSparePartByIdAsync(int sparePartId)
        {
            return await _dbContext.SpareParts.FindAsync(sparePartId);
        }

        /// <summary>
        /// Додає нову запасну частину до бази даних.
        /// </summary>
        /// <param name="sparePart">Об’єкт запасної частини, який потрібно додати</param>
        /// <returns>True, якщо додавання пройшло успішно, інакше — False</returns>
        public async Task<bool> AddSparePartAsync(SparePart sparePart)
        {
            await _dbContext.SpareParts.AddAsync(sparePart);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Оновлює існуючу запасну частину.
        /// </summary>
        /// <param name="sparePart">Об’єкт запасної частини з оновленою інформацією</param>
        /// <returns>True, якщо оновлення пройшло успішно, інакше — False</returns>
        public async Task<bool> UpdateSparePartAsync(SparePart sparePart)
        {
            _dbContext.SpareParts.Update(sparePart);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Видаляє запасну частину з бази даних за її ідентифікатором.
        /// </summary>
        /// <param name="sparePartId">Ідентифікатор запасної частини</param>
        /// <returns>True, якщо видалення пройшло успішно, інакше — False</returns>
        public async Task<bool> DeleteSparePartAsync(int sparePartId)
        {
            var sparePart = await _dbContext.SpareParts.FindAsync(sparePartId);
            if (sparePart == null)
                return false;

            _dbContext.SpareParts.Remove(sparePart);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Зменшує кількість певної запасної частини (списання).
        /// </summary>
        /// <param name="sparePartId">Ідентифікатор запасної частини</param>
        /// <param name="quantity">Кількість для списання</param>
        /// <returns>True, якщо списання пройшло успішно, інакше — False</returns>
        public async Task<bool> DeductSparePartAsync(int sparePartId, int quantity)
        {
            var sparePart = await _dbContext.SpareParts.FindAsync(sparePartId);
            if (sparePart == null || sparePart.Quantity < quantity)
                return false;

            sparePart.Quantity -= quantity;
            _dbContext.SpareParts.Update(sparePart);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Оновлює кількість певної запасної частини на нове значення.
        /// </summary>
        /// <param name="sparePartId">Ідентифікатор запасної частини</param>
        /// <param name="newQuantity">Нова кількість запасної частини</param>
        /// <returns>True, якщо оновлення пройшло успішно, інакше — False</returns>
        public async Task<bool> UpdateSparePartQuantityAsync(int sparePartId, int newQuantity)
        {
            var sparePart = await _dbContext.SpareParts.FindAsync(sparePartId);
            if (sparePart == null)
                return false;

            sparePart.Quantity = newQuantity;
            _dbContext.SpareParts.Update(sparePart);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
