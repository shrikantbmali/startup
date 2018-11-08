using System.Linq;

namespace FakeDocter.UI
{
	public struct MedicalBillInformation
	{
		public string MedicalName { get; set; }

		public string MedicalAddress { get; set; }

		public string Date { get; set; }

		public string CustomerName { get; set; }

		public string DoctorName { get; set; }

		public string CustomerAddress { get; set; }

		public string BillNumber { get; set; }

		public ProductInformation[] Products { get; set; }

		public double NetAmount
		{
			get { return Products.Sum(product => product.Amount); }
		}
	}
}