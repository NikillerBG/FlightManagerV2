using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer
{
    public class PassengerContext : ICrudDb<Passenger, int>
    {
        private readonly ApplicationDbContext _dbContext;

        public PassengerContext(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Passenger passenger)
        {
            try
            {
                _dbContext.Passengers.Add(passenger);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Passenger> ReadAsync(int id, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                IQueryable<Passenger> query = _dbContext.Passengers;

                if (useNavigationalProperties)
                {
                    query = query.Include(p => p.Reservation);
                }

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

        public async Task<List<Passenger>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Passenger> query = _dbContext.Passengers;

                if (useNavigationalProperties)
                {
                    query = query.Include(p => p.Reservation);
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

        public async Task UpdateAsync(Passenger passenger, bool useNavigationalProperties = false)
        {
            try
            {
                Passenger passengerFromDb = await ReadAsync(passenger.Id, useNavigationalProperties, false);
                if (passengerFromDb == null)
                {
                    throw new ArgumentException($"Passenger with id {passenger.Id} not found.");
                }

                _dbContext.Entry(passengerFromDb).CurrentValues.SetValues(passenger);
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
                Passenger passenger = await ReadAsync(id);
                if (passenger == null)
                {
                    throw new ArgumentException($"Passenger with id {id} does not exist in the database.");
                }

                _dbContext.Passengers.Remove(passenger);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Passenger>> GetAsync(int skip, int take, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                IQueryable<Passenger> query = _dbContext.Passengers;

                if (useNavigationalProperties)
                {
                    query = query.Include(p => p.Reservation);
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
