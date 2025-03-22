using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer
{
    public class PlaneContext : ICrudDb<Plane, int>
    {
        private readonly ApplicationDbContext _dbContext;

        public PlaneContext(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Plane plane)
        {
            try
            {
                _dbContext.Planes.Add(plane);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Plane> ReadAsync(int id, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                IQueryable<Plane> query = _dbContext.Planes;

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.SingleOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Plane>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Plane> query = _dbContext.Planes;

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Plane plane, bool useNavigationalProperties = false)
        {
            try
            {
                Plane planeFromDb = await ReadAsync(plane.Id, useNavigationalProperties, false);
                if (planeFromDb == null)
                {
                    throw new ArgumentException($"Plane with id {plane.Id} not found.");
                }

                _dbContext.Entry(planeFromDb).CurrentValues.SetValues(plane);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                Plane plane = await ReadAsync(id);
                if (plane == null)
                {
                    throw new ArgumentException($"Plane with id {id} does not exist in the database.");
                }

                _dbContext.Planes.Remove(plane);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Plane>> GetAsync(int skip, int take, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                IQueryable<Plane> query = _dbContext.Planes;

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.Skip(skip).Take(take).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
