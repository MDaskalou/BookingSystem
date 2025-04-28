using BookingSystem.Application.DTO;
using BookingSystem.Application.Services;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure.IRepository;

namespace BookingSystem.Application.Service.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<RoleDto> CreateRoleAsync(CreateRoleDto dto)
        {
            var role = new Role
            {
                RoleName = dto.RoleName
            };

            await _repository.AddAsync(role);

            return new RoleDto
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName
            };
        }

        public async Task<RoleDto?> GetRoleByIdAsync(int id)
        {
            var role = await _repository.GetByIdAsync(id);
            if (role == null) return null;

            return new RoleDto
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName
            };
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            var roles = await _repository.GetAllAsync();
            return roles.Select(r => new RoleDto
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName
            });
        }

        public async Task<bool> UpdateRoleAsync(int id, CreateRoleDto dto)
        {
            var role = await _repository.GetByIdAsync(id);
            if (role == null) return false;

            role.RoleName = dto.RoleName;

            await _repository.UpdateAsync(role);
            return true;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _repository.GetByIdAsync(id);
            if (role == null) return false;

            await _repository.DeleteAsync(role);
            return true;
        }
    }
}
