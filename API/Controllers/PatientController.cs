using BookingSystem.API;
using BookingSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using BookingSystem.Application.DTO;
using BookingSystem.Application.Service.Interface;
using BookingSystem.Infrastructure.IRepository;

namespace BookingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _repository;
        private readonly IPatientService _service;

        public PatientsController(IPatientRepository repository, IPatientService service    )
        {
            _repository = repository;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAll()
        {
            var patients = await _repository.GetAllAsync();

            return Ok(patients.Select(p => new PatientDto
            {
                PatientId = p.PatientId,
                FullName = p.FullName,
                SocialSecurityNumber = p.SocialSecurityNumber
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetById(int id)
        {
            var patient = await _repository.GetByIdAsync(id);
            if (patient == null) return NotFound();

            return Ok(new PatientDto
            {
                PatientId = patient.PatientId,
                FullName = patient.FullName,
                SocialSecurityNumber = patient.SocialSecurityNumber
            });
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreatePatientDto dto)
        {
            var patient = new Patient
            {
                FullName = dto.FullName,
                SocialSecurityNumber = dto.SocialSecurityNumber
            };

            await _repository.AddAsync(patient);
            return CreatedAtAction(nameof(GetById), new { id = patient.PatientId }, null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CreatePatientDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.FullName = dto.FullName;
            existing.SocialSecurityNumber = dto.SocialSecurityNumber;

            await _repository.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _repository.DeleteAsync(existing);
            return NoContent();
        }
    }
}
