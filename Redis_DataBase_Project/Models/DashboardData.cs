#nullable disable
namespace Redis_DataBase_Project.Models
{
	[Serializable]
	public class Dashboard
	{
		public int TotalCustomerCount { get; set; }
		public int TotalRevenue { get; set; }
		public string TopSellingProductName { get; set; }
		public string TopSellingCountryName { get; set; }
	}
}
