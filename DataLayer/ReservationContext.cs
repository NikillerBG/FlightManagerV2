using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ReservationContext : ICrudDb<Reservation, int>
    {
        private readonly ApplicationDbContext _dbContext;

        public ReservationContext(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Reservation reservation)
        {
            try
            {
                _dbContext.Reservations.Add(reservation);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Reservation> ReadAsync(int id, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                IQueryable<Reservation> query = _dbContext.Reservations;

                if (useNavigationalProperties)
                {
                    // Включваме навигационните свойства за Flight и Passenger
                    query = query.Include(r => r.Flight);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.SingleOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Reservation>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Reservation> query = _dbContext.Reservations;

                if (useNavigationalProperties)
                {
                    query = query.Include(r => r.Flight);
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

        public async Task UpdateAsync(Reservation reservation, bool useNavigationalProperties = false)
        {
            try
            {
                Reservation reservationFromDb = await ReadAsync(reservation.Id, useNavigationalProperties, false);
                if (reservationFromDb == null)
                {
                    throw new ArgumentException($"Reservation with id {reservation.Id} not found.");
                }

                _dbContext.Entry(reservationFromDb).CurrentValues.SetValues(reservation);
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
                Reservation reservation = await ReadAsync(id);
                if (reservation == null)
                {
                    throw new ArgumentException($"Reservation with id {id} does not exist in the database.");
                }

                _dbContext.Reservations.Remove(reservation);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Reservation>> GetAsync(int skip, int take, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                IQueryable<Reservation> query = _dbContext.Reservations;

                if (useNavigationalProperties)
                {
                    query = query.Include(r => r.Flight);
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
