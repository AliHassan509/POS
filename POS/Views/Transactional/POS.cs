using POS.Controller;
using POS.Controllers;
using POS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextIDGenerator;

namespace POS.Views.Transactional
{
    public partial class POS : Form
    {
        POSController Controller;
        Messages msg;
        public POS()
        {
            msg = Messages.GetInstance();
            Controller = new POSController();
            InitializeComponent();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            if (msg.YesNoMessage("Do you really want to close this form?") == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            POSClass pos = new POSClass();
            pos.spID = (spID.Text);
            pos.cID = (cID.Text);
            pos.posDesc = posDesc.Text;
            pos.posOrderDate = DateTime.Parse(posOrderDate.Text);
            pos.posTotAmount = decimal.Parse(posTotAmount.Text);
            pos.posTotQty = int.Parse(posTotQty.Text);
            pos.posID = posID.Text;
            Controller.InsertPOS(pos);

            
            POSDetail posd = new POSDetail();
            Product pro = new Product();
            for (int i = 0; i < productGrid.Rows.Count - 1; i++)
            {
                GenerateIDForTable generateID = new GenerateIDForTable("POS");
                var id = generateID.ReturnTextID("POSDetails", 20, 10, 10);
                posd.posdID = id;
                pro.pName = productGrid.Rows[i].Cells["productName"].Value.ToString();
                posd.posID = (posID.Text);
                posd.pID = (Controller.LoadProductIDAgainstFullName(pro));
                posd.posdDesc = productGrid.Rows[i].Cells["productDesc"].Value.ToString();
                posd.posdQty = int.Parse(productGrid.Rows[i].Cells["productQty"].Value.ToString());
                posd.posdAmount = decimal.Parse(productGrid.Rows[i].Cells["productAmount"].Value.ToString());
                Controller.InsertPOSDetails(posd);
            }
        }

        private void POS_Load(object sender, EventArgs e)
        {
            string[] arr = Controller.LoadClientFullNameData();
            LoadDataToComboBox(cName, arr);
            arr = Controller.LoadProductFullNameData();
            LoadDataToComboBox(productName, arr);
            SalesPerson sp = new SalesPerson {spFName = spName.Text};
            string spIdVar = Controller.LoadSalesPersonIDAgainstFullName(sp);
            spID.Text = spIdVar;
            RefreshData();
        }

        private void LoadDataToComboBox(DataGridViewComboBoxColumn combo, string[] arr)
        {
            try
            {
                combo.Items.Clear();
                for (int i = 0; i < arr.Length; i++)
                {
                    combo.Items.Add(arr[i]);
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        private void LoadDataToComboBox(ComboBox combo, string[] arr)
        {
            try
            {
                combo.Items.Clear();
                for (int i = 0; i < arr.Length; i++)
                {
                    combo.Items.Add(arr[i]);
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        private void cName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Client client = new Client();
            try
            {
                if (cName.Text != "")
                {
                    client.cFName = cName.Text;
                    string id = Controller.LoadClientIDAgainstFullName(client);
                    cID.Text = id;
                }
                
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        private void cancleBtn_Click(object sender, EventArgs e)
        {
            if (msg.YesNoMessage("Do you really want to close this form?") == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void RefreshData()
        {
            GenerateIDForTable generateID = new GenerateIDForTable("POS");
            var id = generateID.ReturnTextID("POS", 20, 3, 17);
            posID.Text = id;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string proName = productGrid.Rows[e.RowIndex].Cells["productName"].Value.ToString();
                Product pro = new Product();
                pro.pName = proName;
                string id = Controller.LoadProductPriceAgainstProductName(pro);
                pro.pPrice = decimal.Parse(id);
                productGrid.Rows[e.RowIndex].Cells["productPrice"].Value = pro.pPrice.ToString();
            }
            catch (Exception ex)
            {
                msg.UserErrorMessage("Please Select Product First.");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            double totAmount = 0;
            int pTotQty=0;
            for (int i = 0; i < productGrid.Rows.Count -1; i++)
            {
                var pPrice = double.Parse(productGrid.Rows[i].Cells["productPrice"].Value.ToString());
                var pQty = int.Parse(productGrid.Rows[i].Cells["productQty"].Value.ToString());
                var pAmount = pQty * pPrice;
                pTotQty += pQty;
                totAmount += pAmount;
                productGrid.Rows[i].Cells["productAmount"].Value = pAmount;
            }
            posTotAmount.Text = totAmount + "";
            posTotQty.Text = pTotQty.ToString();

        }

        private void newBtn_Click(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
