using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TCPPR.Data.DatabaseContext;

using TCPPR.Data.Entities;
using TCPPR.Services.Interfaces;

namespace TCPPR.Services.Services
{
    /// <summary>
    /// Сервіс для управління обладнанням: додавання, оновлення, отримання інформації.
    /// </summary>
    public class EquipmentService : IEquipmentService
    {
        private readonly AppDbContext _dbContext;

        public EquipmentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Equipment>> GetAllEquipmentAsync()
        {
            return await _dbContext.Equipments.ToListAsync();
        }

        public async Task<Equipment> GetEquipmentByIdAsync(int equipmentId)
        {
            return await _dbContext.Equipments.FindAsync(equipmentId);
        }

        public async Task<bool> AddEquipmentAsync(Equipment equipment)
        {
            await _dbContext.Equipments.AddAsync(equipment);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateEquipmentAsync(Equipment equipment)
        {
            _dbContext.Equipments.Update(equipment);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEquipmentAsync(int equipmentId)
        {
            var equipment = await _dbContext.Equipments.FindAsync(equipmentId);
            if (equipment == null)
                return false;

            _dbContext.Equipments.Remove(equipment);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}