using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Controllers
{
    public class Messages
    {
        static Messages msg = null;
        static bool flag = false;


        private Messages()
        {
           
        }

        public static Messages GetInstance()
        {
            msg = new Messages();
            return msg;
        }

        public void SystemErrorMessage()
        {
            MessageBox.Show("Operation Terminated\nPlease Contact your admistrator for details","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        public void UserErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void SystemNotificationMessage()
        {
            MessageBox.Show("Operation Succeed.", "Notifcation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public DialogResult YesNoMessage(string msgs)
        {
            return MessageBox.Show(msgs, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
