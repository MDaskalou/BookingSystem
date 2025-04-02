using BookingSystem.Application.DTO;
using BookingSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TreatmentTypeController : ControllerBase
    {
        private readonly ITreatmentTypeService _treatmentTypeService;

        public TreatmentTypeController(ITreatmentTypeService treatmentTypeService)
        {
            _treatmentTypeService = treatmentTypeService;
        }

        // POST: api/treatmenttype
        [HttpPost]
        public async Task<ActionResult> CreateTreatmentType(CreateTreatmentTypeDto dto)
        {
            var treatmentType = await _treatmentTypeService.CreateTreatmentTypeAsync(dto);
            return CreatedAtAction(nameof(GetTreatmentTypeById), new { id = treatmentType.TreatmentTypeId }, treatmentType);
        }

        // GET: api/treatmenttype/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TreatmentTypeDto>> GetTreatmentTypeById(int id)
        {
            var treatmentType = await _treatmentTypeService.GetTreatmentTypeByIdAsync(id);
            if (treatmentType == null) return NotFound();
            return Ok(treatmentType);
        }

        // GET: api/treatmenttype
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TreatmentTypeDto>>> GetAllTreatmentTypes()
        {
            var treatmentTypes = await _treatmentTypeService.GetAllTreatmentTypesAsync();
            return Ok(treatmentTypes);
        }

        // PUT: api/treatmenttype/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTreatmentType(int id, CreateTreatmentTypeDto dto)
        {
            var result = await _treatmentTypeService.UpdateTreatmentTypeAsync(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        // DELETE: api/treatmenttype/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTreatmentType(int id)
        {
            var result = await _treatmentTypeService.DeleteTreatmentTypeAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
