using AssetManagement.Data;
using AssetManagement.Models;
using AssetManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Services.Implementations
{
    public class EmployeeAssetAllocationService : IEmployeeAssetAllocationService
    {
        private readonly EFCoreDbContext _context;

        public EmployeeAssetAllocationService(EFCoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeAssetAllocation>> GetAllAsync()
        {
            return await _context.EmployeeAssetAllocations
                .Include(a => a.Employee)
                .Include(a => a.Asset)
                .ToListAsync();
        }

        public async Task<EmployeeAssetAllocation?> GetByIdAsync(int id)
        {
            return await _context.EmployeeAssetAllocations
                .Include(a => a.Employee)
                .Include(a => a.Asset)
                .FirstOrDefaultAsync(a => a.AllocationID == id);
        }

        public async Task<EmployeeAssetAllocation> CreateAsync(EmployeeAssetAllocation allocation)
        {
            _context.EmployeeAssetAllocations.Add(allocation);
            await _context.SaveChangesAsync();
            return allocation;
        }

        public async Task<EmployeeAssetAllocation?> UpdateAsync(int id, EmployeeAssetAllocation allocation)
        {
            var existing = await _context.EmployeeAssetAllocations.FindAsync(id);
            if (existing == null) return null;

            existing.EmployeeID = allocation.EmployeeID;
            existing.AssetID = allocation.AssetID;
            existing.AllocationDate = allocation.AllocationDate;
            existing.ReturnDate = allocation.ReturnDate;
            existing.Status = allocation.Status;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var record = await _context.EmployeeAssetAllocations.FindAsync(id);
            if (record == null) return false;

            _context.EmployeeAssetAllocations.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
