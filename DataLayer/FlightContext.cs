using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer
{
    public class FlightContext : ICrudDb<Flight, int>
    {
        private readonly ApplicationDbContext _dbContext;

        public FlightContext(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Flight flight)
        {
            try
            {
                _dbContext.Flights.Add(flight);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Flight> ReadAsync(int id, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                IQueryable<Flight> query = _dbContext.Flights;

                if (useNavigationalProperties)
                {
                    query = query.Include(f => f.Plane);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.SingleOrDefaultAsync(f => f.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Flight>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Flight> query = _dbContext.Flights;

                if (useNavigationalProperties)
                {
                    query = query.Include(f => f.Plane);
                }

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

        public async Task UpdateAsync(Flight flight, bool useNavigationalProperties = false)
        {
            try
            {
                Flight flightFromDb = await ReadAsync(flight.Id, useNavigationalProperties, false);
                if (flightFromDb == null)
                {
                    throw new ArgumentException($"Flight with id {flight.Id} not found.");
                }

                _dbContext.Entry(flightFromDb).CurrentValues.SetValues(flight);
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
                Flight flight = await ReadAsync(id);
                if (flight == null)
                {
                    throw new ArgumentException($"Flight with id {id} does not exist in the database.");
                }

                _dbContext.Flights.Remove(flight);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Flight>> GetAsync(int skip, int take, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                IQueryable<Flight> query = _dbContext.Flights;

                if (useNavigationalProperties)
                {
                    query = query.Include(f => f.Plane);
                }

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
