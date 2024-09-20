﻿using Flower_Management.Data;
using Flower_Management.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flower_Management.Repositories
{
    public class FlowerRepository : IFlowerRepository
    {
        private readonly FlowerDbContext _context;

        public FlowerRepository(FlowerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Flower>> GetAllFlowersAsync()
        {
            return await _context.Flowers.ToListAsync();
        }

        public async Task<Flower?> GetFlowerByIdAsync(int id)
        {
            return await _context.Flowers.FindAsync(id);
        }

        public async Task AddFlowerAsync(Flower flower)
        {
            _context.Flowers.Add(flower);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFlowerAsync(Flower flower)
        {
            _context.Entry(flower).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFlowerAsync(int id)
        {
            var flower = await _context.Flowers.FindAsync(id);
            if (flower != null)
            {
                _context.Flowers.Remove(flower);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> FlowerExistsAsync(int id)
        {
            return await _context.Flowers.AnyAsync(f => f.FlowerId == id);
        }
    }
}