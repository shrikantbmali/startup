using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using FakeDocter.UI.Views;

namespace FakeDocter.UI.WF
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			_productInformations = new List<ProductInformation>();
		}

		private readonly List<ProductInformation> _productInformations;

		private void button1_Click(object sender, EventArgs e)
		{
			if (ValidateMedicineInformation())
			{
				AddMedicine();
				ClearProductFields();
			}
		}

		private bool ValidateMedicineInformation()
		{
			double amount;

			if (string.IsNullOrEmpty(tbxBatch.Text))
				MessageBox.Show("Enter Batch Number, Like : DN1043.");
			else if(string.IsNullOrEmpty(tbxMedicineName.Text))
				MessageBox.Show("Enter Medicine Name, Like : Combiflam.");
			else if (!double.TryParse(tbxAmount.Text, out amount))
				MessageBox.Show("Please enter valide amount, Like : 12.25.");
			else return true;

			return false;
		}

		private void AddMedicine()
		{
			var productInformation = CreateProductInformation();
			_productInformations.Add(productInformation);

			var listViewItem = CreateListViewItem(productInformation);
			lvwProductList.Items.Add(listViewItem);
		}

		private static ListViewItem CreateListViewItem(ProductInformation productInformation)
		{
			var listViewItem = new ListViewItem(productInformation.Quantity.ToString());
			listViewItem.SubItems.Add(productInformation.Name);
			listViewItem.SubItems.Add(productInformation.BatchNumber);
			listViewItem.SubItems.Add(productInformation.ExpiryDate.ToString("MM/yy"));
			listViewItem.SubItems.Add(productInformation.Amount.ToString(CultureInfo.InvariantCulture));

			listViewItem.Tag = productInformation;
			return listViewItem;
		}

		private ProductInformation CreateProductInformation()
		{
			var productInformation = new ProductInformation
			{
				Amount = double.Parse(tbxAmount.Text),
				BatchNumber = tbxBatch.Text,
				Name = tbxMedicineName.Text,
				ExpiryDate = dtpExpiryDate.Value,
				Quantity = (int) nudQuantity.Value
			};
			return productInformation;
		}

		private void ClearProductFields()
		{
			tbxAmount.Text = 0.ToString(CultureInfo.InvariantCulture);
			tbxBatch.Text = string.Empty;
			tbxMedicineName.Text = string.Empty;

			dtpExpiryDate.Value = DateTime.Today;
			nudQuantity.Value = 1;
		}

		private void btnViewFormat_Click(object sender, EventArgs e)
		{
			var previewWindow = new PreviewWindow(GetMedicalBillInformation());
			previewWindow.ShowDialog();
		}

		private MedicalBillInformation GetMedicalBillInformation()
		{
			return new MedicalBillInformation
			{
				CustomerAddress = tbxCustomerAddress.Text,
				CustomerName = tbxCustomerName.Text,
				BillNumber = tbxBillNo.Text,
				Date = dtpDate.Value.ToString("dd/MM/yy"),
				DoctorName = tbxDoctorName.Text,
				MedicalAddress = tbxMedicalAddress.Text,
				MedicalName = tbxMedicalName.Text,
				Products = _productInformations.ToArray()
			};
		}

		private void lvwProductList_SelectedIndexChanged(object sender, EventArgs e)
		{
			var istoEnable = lvwProductList.SelectedItems.Count > 0;

			btnDelete.Enabled = istoEnable;
			btnEdit.Enabled = istoEnable;
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			DeleteSelectedItem();
		}

		private void DeleteSelectedItem()
		{
			_productInformations.Remove((ProductInformation) lvwProductList.SelectedItems[0].Tag);
			lvwProductList.Items.RemoveAt(lvwProductList.SelectedIndices[0]);
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			var productInformation = (ProductInformation)lvwProductList.SelectedItems[0].Tag;
			DeleteSelectedItem();

			tbxAmount.Text = productInformation.Amount.ToString("00.00");
			tbxBatch.Text = productInformation.BatchNumber;
			tbxMedicineName.Text = productInformation.Name;

			dtpExpiryDate.Value = productInformation.ExpiryDate;
			nudQuantity.Value = productInformation.Quantity;
		}
	}
}
