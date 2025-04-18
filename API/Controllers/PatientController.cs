using BookingSystem.Application.DTO;
using BookingSystem.Application.Service.Interface;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _repository;
        private readonly IPatientService _patientService;

        public PatientsController(IPatientRepository repository, IPatientService patientService )
        {
            _repository = repository;
            _patientService = patientService;
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
        public async Task<ActionResult<PatientDto>> GetpatientById(int id)
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
        public async Task<ActionResult> CreatePatient(CreatePatientDto dto)
        {
            var patient = new Patient
            {
                FullName = dto.FullName,
                SocialSecurityNumber = dto.SocialSecurityNumber
            };

            await _repository.AddAsync(patient);
            return CreatedAtAction(nameof(GetpatientById), new { id = patient.PatientId }, null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePatient(int id,[FromBody] UpdatePatientDto dto)
        {
           var result  = await _patientService.UpdatePatientAsync(id, dto);
           if(!result) return BadRequest();
           return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatient(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _repository.DeleteAsync(existing);
            return NoContent();
        }
    }
}
