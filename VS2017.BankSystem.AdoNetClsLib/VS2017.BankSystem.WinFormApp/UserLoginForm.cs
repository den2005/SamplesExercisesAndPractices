using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VS2017.BankSystem.WinFormApp
{
    public partial class UserLoginForm : Form
    {
        public UserLoginForm()
        {
            InitializeComponent();
        }

        private void UserLoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void LoginUser()
        {

        }

        private void ClearFields()
        {
            lblLoginResult.Text = " ";
            txtLoginName.Text = "";
            txtPassword.Text = "";
        }
    }
}
