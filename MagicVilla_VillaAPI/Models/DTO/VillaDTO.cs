using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.DTO
{
	public class VillaDTO
	{
		//contains only those field which we want to send to endpoint
		public int Id { get; set; }
		[Required]
		[MaxLength(30)]
		public string Name { get; set; }
	}
}
