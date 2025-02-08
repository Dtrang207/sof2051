using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BUS;

namespace GUI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        BUS_NhanVien busNhanVien = new BUS.BUS_NhanVien();

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            DTO_NhanVien nv = new DTO_NhanVien();
            if (txtEmail.Text != null && txtMatKhau.Text != null)
            {
                nv.EmailNV = txtEmail.Text;
                nv.MatKhau = encryption(txtMatKhau.Text);
                if (busNhanVien.NVDangNhap(nv))
                {
                    frmMain.email = nv.EmailNV;
                    DataTable dt = busNhanVien.VaiTroNV(nv.EmailNV);
                    frmMain.vaitro = Convert.ToInt16(dt.Rows[0][0]);
                    frmMain.session = 1;
                    MessageBox.Show("Đăng nhập thành công");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đăng nhập không thành công vui lòng kiểm tra lại email hoặc mk");
                    MessageBox.Show(busNhanVien.NVDangNhap(nv).ToString());
                    txtEmail.Text = "";
                    txtMatKhau.Text = "";
                    txtEmail.Focus();
                }

            }
        }
        public static string encryption(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            //encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            //Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuenmk_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text != "")
            {
                if (busNhanVien.QuenMK(txtEmail.Text))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(RandomString(3, true));
                    sb.Append(RandomNumber(12345, 54321));
                    string matkhau = encryption(sb.ToString());
                    busNhanVien.TaoMatKhau(txtEmail.Text, matkhau);
                    SendMail(txtEmail.Text, matkhau);
                }
            }
        }
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        //Tao so ngau nhien
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public void SendMail(string email, string matkhau)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com")
                {
                    Port = 25,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("admin@gmail.com", "123")
                };

                MailMessage Msg = new MailMessage()
                {
                    From = new MailAddress("admin@gmail.com"),
                    Subject = "Mật khẩu mới",
                    Body = "Mật khẩu mới của bạn là: " + matkhau
                };

                Msg.To.Add(email);

                client.Send(Msg);// Send our email.

                MessageBox.Show("Mot Email phục hồi mat khau da duoc goi toi ban!");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
