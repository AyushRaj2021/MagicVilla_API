using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Models.DTO;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{
	//[Route("api/[controller]")]
	[Route("api/VillaAPI")]
	[ApiController]
	public class VillaAPIController : ControllerBase
	{
		private readonly IVillaRepository _dbVilla;
		private readonly IMapper _mapper;
		public VillaAPIController(IVillaRepository dbVilla, IMapper mapper)
		{
			_dbVilla = dbVilla;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
		{
			//Output type is VillaDTO type so we r mapping from villa to villaDto
			IEnumerable<Villa> villaList = await _dbVilla.GetAllAsync();
 			return Ok(_mapper.Map<List<VillaDTO>>(villaList));
		}

		[HttpGet("{id:int}", Name = "GetVilla")]
		[ProducesResponseType(200)]
		//[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		//[ProducesResponseType(404)]
		[ProducesResponseType(400)]
		public async Task<ActionResult<VillaDTO>> GetVilla(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var villa = await _dbVilla.GetAsync(u=>u.Id == id);
			if (villa == null)
			{
				return NotFound();
			}
			return Ok(_mapper.Map<VillaDTO>(villa));
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreateDTO createDTO)
		{
			//if (!ModelState.IsValid)
			//{
			//    return BadRequest(ModelState);
			//}
			if(await _dbVilla.GetAsync(u=>u.Name.ToLower() == createDTO.Name.ToLower())!=null)
			{
				ModelState.AddModelError("CustomError", "Villa already Exists!");
				return BadRequest(ModelState);
			}
			if (createDTO == null)
			{
				return BadRequest(createDTO);
			}
			//if (villaDTO.Id > 0)
			//{
			//	return StatusCode(StatusCodes.Status500InternalServerError);
			//}
			//we are converting villaDto type to villa type as efcore is of villa type

			Villa model = _mapper.Map<Villa>(createDTO);
			//Villa model = new()
			//{
			//	Amenity = villaDTO.Amenity,
			//	Details = villaDTO.Details,
			//	ImageUrl = villaDTO.ImageUrl,
			//	Name = villaDTO.Name,
			//	Occupancy = villaDTO.Occupancy,
			//	Rate = villaDTO.Rate,
			//	Sqft = villaDTO.Sqft
			//};
			await _dbVilla.CreateAsync(model);
			return CreatedAtRoute("GetVilla", new { id = model.Id }, model); 
		}

		[HttpDelete("{id:int}", Name = "DeleteVilla")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]		
		public async Task<IActionResult> DeleteVilla(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var villa = await _dbVilla.GetAsync(u => u.Id == id);
			if (villa == null)
			{
				return NotFound();
			}
			await _dbVilla.RemoveAsync(villa);
			return NoContent();
		}

		[HttpPut("{id:int}", Name = "UpdateVilla")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateDTO)
		{
			if (updateDTO == null || id != updateDTO.Id)
			{
				return BadRequest();
			}
			//var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
			//villa.Name = villaDTO.Name;
			//villa.Sqft = villaDTO.Sqft;
			//villa.Occupancy = villaDTO.Occupancy;

			//we are converting updateDTO type to villa type as ef core is of villa type
			Villa model = _mapper.Map<Villa>(updateDTO);
			//Villa model = new()
			//{
			//	Amenity = villaDTO.Amenity,
			//	Details = villaDTO.Details,
			//	Id = villaDTO.Id,
			//	ImageUrl = villaDTO.ImageUrl,
			//	Name = villaDTO.Name,
			//	Occupancy = villaDTO.Occupancy,
			//	Rate = villaDTO.Rate,
			//	Sqft = villaDTO.Sqft
			//};
			await _dbVilla.UpdateAsync(model);
			return NoContent();

		}

		[HttpPatch("{id:int}",Name = "UpdatePartialVilla")]
		[ProducesResponseType(StatusCodes.Status204NoContent) ]
		[ProducesResponseType(StatusCodes.Status400BadRequest ) ]
		public async Task<IActionResult> UpdatePartialVilla (int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
		{
			if(patchDTO == null || id == 0)
			{
				return BadRequest();
			}
			var villa = _dbVilla.GetAsync(u => u.Id == id, tracked: false);
			//we are converting villa type to villadto type as patch object is of villadto type
			VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);

			//VillaUpdateDTO villaDTO = new()
			//{
			//	Amenity = villa.Amenity,
			//	Details = villa.Details,
			//	Id = villa.Id,
			//	ImageUrl = villa.ImageUrl,
			//	Name = villa.Name,
			//	Occupancy = villa.Occupancy,
			//	Rate = villa.Rate,
			//	Sqft = villa.Sqft
			//};			
			if(villa == null)
			{
				return BadRequest();
			}
			Villa model = _mapper.Map<Villa>(villaDTO);
			//patchDTO.ApplyTo(villaDTO, ModelState);
			//Villa model = new Villa()
			//{
			//	Amenity = villaDTO.Amenity,
			//	Details = villaDTO.Details,
			//	Id = villaDTO.Id,
			//	ImageUrl = villaDTO.ImageUrl,
			//	Name = villaDTO.Name,
			//	Occupancy = villaDTO.Occupancy,
			//	Rate = villaDTO.Rate,
			//	Sqft = villaDTO.Sqft
			//};
			await _dbVilla.UpdateAsync(model); 
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return NoContent();
		}
	}
}
