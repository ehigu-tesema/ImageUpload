using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace ImageUploading
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Jo\Documents\Image.mdf;Integrated Security=True;Connect Timeout=30");
        ////string imgLocation = "";
        //SqlCommand cmd;
        string imageLocation = "";// the path of image to obtained later
        SqlCommand cmd;//this is a sql command to be used later to insert image into the database.
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {/*
         
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = " Png files(*.png)|*.png|Jpg files(*.jpg)|All files(*.*)";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialog.FileName.ToString();
                pictureBox1.ImageLocation = imgLocation;
            }   
         */
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Png Files(*.png)|*.png|Jpg Files(*.jpg)|*.jpg|All Files(*.*)|*.* ";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imageLocation = dialog.FileName.ToString();
                pictureBox1.ImageLocation = imageLocation;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            byte[] images = null;// empty variable for immage
            FileStream stream = new FileStream(imageLocation, FileMode.Open, FileAccess.Read);// file stream wii open an image by image location variable
            BinaryReader br = new BinaryReader(stream);//conert it to stram
            images = br.ReadBytes((int)stream.Length);//assigns it to image variable
            con.Open();
            //SqlCommand cmd = new SqlCommand("insert into images(Id,Name,Image)values ('" + txtId.Text + "','" + txtName.Text + "',@images)",con);
            string query = "insert into image(Id,Name,Image)values ('" + txtId.Text + "','" + txtName.Text + "',@images)";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.Add(new SqlParameter("@images", images));
             cmd.ExecuteNonQuery();
            MessageBox.Show("Data saved Successfully");
            con.Close();
        }
    }
}
