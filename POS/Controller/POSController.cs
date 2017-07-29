using POS.Controllers;
using POS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Controller
{
    class POSController
    {
        #region Variables
        DatabaseHelper db;
        Messages msg;

        #endregion

        #region Constructor
        public POSController()
        {
            msg = Messages.GetInstance();
            db = new DatabaseHelper();
        }
        #endregion

        #region Client
        public string[] LoadClientFullNameData()
        {
            string[] arr = null;
            string query = "select cFName + ' ' + cLName as FullName from Client";
            try
            {
                DataTable dt = db.DataNavigationOperations(query);
                return arr = ExtractStringArrayFromDataTable(dt);
            }
            catch (Exception ex)
            {
                msg.SystemErrorMessage();
            }
            return arr;
        }

        private string[] ExtractStringArrayFromDataTable(DataTable dt)
        {
            string[] arr = new string[dt.Rows.Count];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = dt.Rows[i][0].ToString();
            }
            return arr;
        }


        public string LoadClientIDAgainstFullName(Client c)
        {
            try
            {
                string query = "select cID from Client where cFName + ' ' + cLName = '" + c.cFName + "'";
                DataTable dt = db.DataNavigationOperations(query);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                msg.SystemErrorMessage();
            }
            return "0";
        }

        #endregion

        #region Product
        public string[] LoadProductFullNameData()
        {
            string[] arr = null;
            string query = "select pName from Product";
            try
            {
                DataTable dt = db.DataNavigationOperations(query);
                return arr = ExtractStringArrayFromDataTable(dt);
            }
            catch (Exception ex)
            {
                msg.SystemErrorMessage();
            }
            return arr;
        }

        public string LoadProductIDAgainstFullName(Product product)
        {
            try
            {
                string query = "select pID from Product where pName = '" + product.pName + "'";
                DataTable dt = db.DataNavigationOperations(query);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                msg.SystemErrorMessage();
            }
            return "0";
        }

        public string LoadProductPriceAgainstProductName(Product pro)
        {
            try
            {
                string query = "select pPrice from Product where pName = '" + pro.pName + "'";
                DataTable dt = db.DataNavigationOperations(query);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return "0";
        }
        #endregion

        #region Sales Person
        public string[] LoadSalesPersonFullNameData()
        {
            string[] arr = null;
            string query = "select spFName + ' ' + spLName as FullName from SalesPerson";
            try
            {
                DataTable dt = db.DataNavigationOperations(query);
                return arr = ExtractStringArrayFromDataTable(dt);
            }
            catch (Exception ex)
            {
                msg.SystemErrorMessage();
            }
            return arr;
        }

        public string LoadSalesPersonIDAgainstFullName(SalesPerson sp)
        {
            try
            {
                string query = "select spID from SalesPerson where spFName + ' ' + spLName = '" + sp.spFName + "'";
                DataTable dt = db.DataNavigationOperations(query);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                msg.SystemErrorMessage();
            }
            return "0";
        }
        #endregion

        #region POS
        public void InsertPOS(POSClass pos)
        {
            try
            {
                string query = "insert into POS (posID,posOrderDate,posDesc,posTotAmount,posTotQty,cID,spID) values ('"+pos.posID+"','" + pos.posOrderDate.ToLongDateString() + "','" + pos.posDesc + "'," + pos.posTotAmount + "," + pos.posTotQty + ",'" + pos.cID + "', '" + pos.spID + "')";
                db.DataManupulationOperation(query);
                
            }
            catch (Exception ex)
            {
                msg.SystemErrorMessage();
            }
        }
        #endregion

        #region POS Details
        public void InsertPOSDetails(POSDetail posd)
        {
            try
            {
                string query = "insert into POSDetails (posdID,posID,posdDesc,posdAmount,posdQty,pID) values ('"+posd.posdID+"','" + posd.posID + "','" + posd.posdDesc + "'," + posd.posdAmount + "," + posd.posdQty + ", '" + posd.pID + "')";
                db.DataManupulationOperation(query);
                msg.SystemNotificationMessage();
            }
            catch (Exception ex)
            {
                msg.SystemErrorMessage();
            }
        }
        #endregion
    }
}
