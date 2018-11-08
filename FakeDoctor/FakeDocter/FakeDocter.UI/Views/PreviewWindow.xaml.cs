using FakeDocter.PrinteService;
using System.Globalization;
using System.Printing;
using System.Windows;
using System.Windows.Controls;

namespace FakeDocter.UI.Views
{
	/// <summary>
	/// Interaction logic for PreviewWindow.xaml
	/// </summary>
	public partial class PreviewWindow
	{
		public PreviewWindow(MedicalBillInformation medicalBillInformation)
		{
			InitializeComponent();

			MedicalName.Text = medicalBillInformation.MedicalName;
			MedicalName2.Text = medicalBillInformation.MedicalName;
			MedicalAddress.Text = medicalBillInformation.MedicalAddress;
			Date.Text = medicalBillInformation.Date;
			CustomerName.Text = medicalBillInformation.CustomerName;
			CustomerAddress.Text = medicalBillInformation.CustomerAddress;
			DoctorName.Text = medicalBillInformation.DoctorName;
			NetAmount.Text = medicalBillInformation.NetAmount.ToString("00.00");
			BillNo.Text = medicalBillInformation.BillNumber;

			var productCount = 0;
			foreach (var product in medicalBillInformation.Products)
			{
				var qty = new TextBlock
				{
					Text = product.Quantity.ToString(CultureInfo.InvariantCulture),
					Margin = GetMargin(Qty.Margin, productCount),
					Width = Qty.Width,
					FontFamily = Qty.FontFamily,
					VerticalAlignment = Qty.VerticalAlignment,
					HorizontalAlignment = Qty.HorizontalAlignment
				};

				var productName = new TextBlock
				{
					Text = product.Name.ToString(CultureInfo.InvariantCulture),
					Margin = GetMargin(ProductName.Margin, productCount),
					Width = ProductName.Width,
					FontFamily = ProductName.FontFamily,
					VerticalAlignment = ProductName.VerticalAlignment,
					HorizontalAlignment = ProductName.HorizontalAlignment
				};

				var batch = new TextBlock
				{
					Text = product.BatchNumber.ToString(CultureInfo.InvariantCulture),
					Margin = GetMargin(Batch.Margin, productCount),
					Width = Batch.Width,
					FontFamily = Batch.FontFamily,
					VerticalAlignment = Batch.VerticalAlignment,
					HorizontalAlignment = Batch.HorizontalAlignment
				};

				var expiry = new TextBlock
				{
					Text = product.ExpiryDate.ToString("MM/yy"),
					Margin = GetMargin(Expiry.Margin, productCount),
					Width = Expiry.Width,
					FontFamily = Expiry.FontFamily,
					VerticalAlignment = Expiry.VerticalAlignment,
					HorizontalAlignment = Expiry.HorizontalAlignment
				};

				var amount = new TextBlock
				{
					Text = product.Amount.ToString("00.00"),
					Margin = GetMargin(Amount.Margin, productCount),
					Width = Amount.Width,
					FontFamily = Amount.FontFamily,
					VerticalAlignment = Amount.VerticalAlignment,
					HorizontalAlignment = Amount.HorizontalAlignment
				};

				PrintTarget.Children.Add(qty);
				PrintTarget.Children.Add(productName);
				PrintTarget.Children.Add(batch);
				PrintTarget.Children.Add(expiry);
				PrintTarget.Children.Add(amount);

				productCount++;
			}
		}

		private static Thickness GetMargin(Thickness getMargin, int i)
		{
			return new Thickness(getMargin.Left, getMargin.Top + (20 * i), getMargin.Right, getMargin.Bottom);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var printDialog = new PrintDialog
			{
				UserPageRangeEnabled = true,
				PrintTicket = { PageOrientation = PageOrientation.Portrait }
			};

			printDialog.PrintTicket = printDialog.PrintQueue.UserPrintTicket;
			printDialog.PrintQueue = LocalPrintServer.GetDefaultPrintQueue();

			if (printDialog.ShowDialog().Value)
			{
				var xpsVisualPrinter = PrintServiceFactory.CreateXpsVisualPrinter();
				xpsVisualPrinter.Print(printDialog.PrintQueue, PrintTarget);
			}
		}
	}
}