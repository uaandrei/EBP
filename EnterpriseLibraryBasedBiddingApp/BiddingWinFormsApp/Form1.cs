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
            var productServices = new ProductServices();
            var product = new Product();
            var valids = productServices.AddProduct(product);
            var bid = new Bid();
            var valid = productServices.AddBidForProduct(product, bid);
        }
    }
}