using BlogManagementLibrary.Data;
using BlogManagementLibrary.Model;
using BlogManagementLibrary.Model.Dto;
using BlogManagementLibrary.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BlogManagementLibrary.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDBContext _dbContext;
        internal DbSet<Admin> _Admins;
        private string secretKey;
        public AdminRepository(ApplicationDBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _Admins = _dbContext.Admins;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public Task CreateAsync(Admin Entity)
        {
            _dbContext.AddAsync(Entity);
            return _dbContext.SaveChangesAsync();
        }

        public async Task<List<Admin>> GetAllAsync(Expression<Func<Admin, bool>>? filter)
        {
            IQueryable<Admin> query = _Admins;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Admin>> GetAllAsync()
        {
            IQueryable<Admin> query = _Admins;
            return await query.ToListAsync();
        }

        public async Task<Admin> GetAsync(Expression<Func<Admin, bool>>? filter)
        {
            IQueryable<Admin> query = _Admins;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Admin> GetAsync(int id)
        {
            Admin ad = new();
            if (id != 0)
            {
                 ad = await _Admins.Where(u => u.AdminId == id).FirstAsync()!;
            }
            return ad;
        }

        public Task RemoveAsync(Admin Entity)
        {
            _dbContext.Remove(Entity);
            return SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Admin> UpdateAsync(Admin Entity)
        {
            _dbContext.Update(Entity);
            await _dbContext.SaveChangesAsync();
            return Entity;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _dbContext.Admins.FirstOrDefault(u => u.AdminName.ToLower() == loginRequestDTO.Username.ToLower() && u.AdminPassword == loginRequestDTO.Password);
            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }

            // If user was found then we need to generate a JWT.

            var tokenHandler = new JwtSecurityTokenHandler();

            // Encoding of a key=> we need it in bytes and it is in string.
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.AdminName),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };
            return loginResponseDTO;
        }

       
    }
}
