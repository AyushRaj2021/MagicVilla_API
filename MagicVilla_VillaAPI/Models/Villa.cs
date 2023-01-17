namespace MagicVilla_VillaAPI.Models
{
	public class Villa
	{
		//contains all field whether it is sent to endpoint or not unlike dto class
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}