﻿using MagicVilla_VillaAPI.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.Dto
{
	public class VillaNumberDTO
	{
		[Required]
		public int VillaNo { get; set; }

		public string SpecialDetails { get; set; }
		[Required]
		public int VillaID { get; set; }
		public VillaDTO Villa { get; set; }

	}
}