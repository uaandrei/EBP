using DomainModel;
using ServiceLayer;
using System;
using System.Windows.Forms;

namespace BiddingWinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            User user = new User();
            user.Name = "Adi";
            user.Password = "1q2w3e";
            UserServices userServices = new UserServices();
            userServices.AddUser(user);
            var list = userServices.GetAllUsers();
            dataGridView1.DataSource = list;
            var x = list[0].Bids;
        }
    }
}