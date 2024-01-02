using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;

namespace DovizUygulamasi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection bgl = new SqlConnection(@"Data Source=Mert;Initial Catalog=DovizUygulamasi;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {
            string bugün = "https://www.tcmb.gov.tr/kurlar/today.xml";
            var xmldosyasi = new XmlDocument();
            xmldosyasi.Load(bugün);

            string dolaralis = xmldosyasi.SelectSingleNode("Tarih_Date/Currency [@Kod='USD']/BanknoteBuying").InnerXml;
            LblDolarAlis.Text = dolaralis;

            string dolarsatis = xmldosyasi.SelectSingleNode("Tarih_Date/Currency [@Kod='USD']/BanknoteSelling").InnerXml;
            LblDolarSatis.Text = dolarsatis;

            string euroalis = xmldosyasi.SelectSingleNode("Tarih_Date/Currency [@Kod='EUR']/BanknoteBuying").InnerXml;
            LblEuroAlis.Text = euroalis;

            string eurosatis = xmldosyasi.SelectSingleNode("Tarih_Date/Currency [@Kod='EUR']/BanknoteSelling").InnerXml;
            LblEuroSatis.Text = eurosatis;

            // Türk Lirası yazdırma
            bgl.Open();
            SqlCommand lira = new SqlCommand("Select * From TBL_KASA",bgl);
            SqlDataReader dr = lira.ExecuteReader();
            while (dr.Read())
            {
                label2.Text = dr[1].ToString();
                label5.Text = dr[2].ToString();
                label13.Text = dr[3].ToString();
            }
            bgl.Close();

            
        }

        private void BtnDolarAl_Click(object sender, EventArgs e)
        {
            TxtKur.Text = LblDolarAlis.Text;
        }

        private void BtnDolarSatis_Click(object sender, EventArgs e)
        {
            TxtKur.Text = LblDolarSatis.Text;
        }

        private void BtnEuroAlis_Click(object sender, EventArgs e)
        {
            TxtKur.Text = LblEuroAlis.Text;
        }

        private void BtnEuroSatis_Click(object sender, EventArgs e)
        {
            TxtKur.Text = LblEuroSatis.Text;
        }

        private void BtnBozdur_Click(object sender, EventArgs e)
        {
             double kur, miktar, tutar;
             kur = Convert.ToDouble(TxtKur.Text);
             miktar = Convert.ToDouble(TxtMiktar.Text);
             tutar = kur * miktar;
             TxtTutar.Text = tutar.ToString();
           

        }

        private void TxtKur_TextChanged(object sender, EventArgs e)
        {
            TxtKur.Text = TxtKur.Text.Replace(".",",");
        }

        private void button1_Click(object sender, EventArgs e)
        {
             double kur = Convert.ToDouble(TxtKur.Text);
             int miktar = Convert.ToInt16(TxtMiktar.Text);
             int tutar =Convert.ToInt16(miktar/ kur);
             TxtTutar.Text = tutar.ToString();
             double kalan;
             kalan = miktar % kur;
             TxtKalan.Text = kalan.ToString();
           
        }
    }
}
