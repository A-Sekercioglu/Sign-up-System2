using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace kayıt
{
    public partial class Form1 : Form
    {
       

        int sayac = 1;
       public  string dataway = @"kayıt.txt";
        private void button1_Click(object sender, EventArgs e)
        {
            string password = textBox1.Text;
            string name = txtbox1.Text;
            string veri ="- İsim : " + name + " Şifre : " + password;
           
            
           
            // isimde boşluk var mı yok mu diye kontrol eder
            if (name.Contains(" ") || password.Contains(" "))
            {
                ShowMessageAndClear("Please don't use space in your name or password");
                return;
            }

            // isim ve şifre boş mu diye kontrol eder
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                ShowMessageAndClear("Please fill all the empty boxes.");
                return;
            }

            // şifre uzunluk kontrol
            if (password.Length < 6)
            {
                ShowMessageAndClear("Please use a longer password.");
                return;
            }

            // şifre veya ismin başka kullanıcıda olup olmadığını kontrol eder
            foreach (var item in lstbox1.Items)
            {
                if (item.ToString().Contains(" Şifre : " + password) && item.ToString().Contains("- İsim : " + name))
                {
                    MessageBox.Show("This user and password is already used.");
                    return; // şifre kontrol
                }

            }
            FileStream fs = new FileStream(dataway, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(veri);
           
           
            sw.Close();
            //listeye yeni öge ekler
            lstbox1.Items.Add(sayac + "- İsim : " + name + " Şifre : " + password);

            sayac++; // sayacı arttırır
        }
        public Form1()
        {
            InitializeComponent();
            
        }

        void FileRead()
        {
            if (File.Exists(dataway))
            {

                FileStream fs = new FileStream(dataway, FileMode.Open, FileAccess.Read);

                StreamReader sr = new StreamReader(fs);
               var icerik= sr.ReadToEnd();
                
                var items=icerik.Split('\n');
                foreach (var item in items)
                {
                    lstbox1.Items.Add(item);
                }
                sr.Close();
            }
            else
            {
             
            }
        }

        private void ShowMessageAndClear(string message)
        {
            MessageBox.Show(message);
            txtbox1.Clear();
            textBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // kutuları boşaltır
            txtbox1.Clear();
            textBox1.Clear();
        }

     
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtbox1_TextChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void lstbox1_SelectedIndexChanged(object sender, EventArgs e) { }

        private void Form1_Load(object sender, EventArgs e)
        {
            FileRead();

        }
    }
}
