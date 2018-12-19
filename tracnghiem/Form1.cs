using System;
using System.Drawing;
using FireSharp.Config;
using System.Windows.Forms;
using FireSharp.Interfaces;
using FireSharp;

namespace tracnghiem
{
    public partial class mainForm : Form
    {
        IFirebaseClient client()
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "R2olJJpLRPZZmHGvIRcGk8urf7ajdCP9yWcVWOwy",
                BasePath = "https://tracnghiem-c8cd5.firebaseio.com"
            };
            IFirebaseClient client = new FirebaseClient(config);

            return client;
        }


        int numberQuestion;
        Button submit = new Button();
        RadioButton[,] rb = new RadioButton[1000,5];
        PHTextBox user = new PHTextBox("Mã Sinh Viên");
        PHTextBox password = new PHTextBox("Mật Khẩu");
        Button loginButton = new Button();
        public mainForm()
        {
            InitializeComponent();
        }

        void login()
        {
            loginButton.Text = "Đăng nhập";
            loginButton.Location = new Point(400, 0);
            password.UseSystemPasswordChar = true;
            user.Location = new Point(100, 2);
            user.MaxLength = 8;
            password.Location = new Point(250, 2);
            this.Controls.Add(loginButton);
            this.Controls.Add(user);
            this.Controls.Add(password);

            loginButton.Click += LoginButton_Click;
            this.AcceptButton = loginButton;

        }


        void start()
        {

            numberQuestion = Int32.Parse(client().Get("nq").Body);
            WebBrowser test = new WebBrowser();
            test.Location = new Point(350, 50);

     
            test.Size = new Size(1500,numberQuestion*50);
            test.Navigate(new Uri(client().Get("dethi").Body.Replace("\"", "")));
        


            for (int i = 1; i <= numberQuestion; i++)
            {
                Label lb = new Label();
                lb.Size = new Size(45, 15);
                lb.Location = new Point(50, 50 * i);
                lb.Text = "Câu " + i;
                this.Controls.Add(lb);
                GroupBox grb = new GroupBox();
                grb.Location = new Point(100, 50 * i - 20);
                grb.Size = new Size(205, 50);
                this.Controls.Add(grb);



                for (int j = 1; j <= 4; j++)
                {
                    rb[i, j] = new RadioButton();
                    String t = "";
                    switch (j)
                    {
                        case 1:
                            t = "A";
                            break;
                        case 2:
                            t = "B";
                            break;
                        case 3:
                            t = "C";
                            break;
                        case 4:
                            t = "D";
                            break;
                    }
                    rb[i, j].Text = t;
                    rb[i, j].Enabled = false;
                    rb[i, j].Size = new Size(40, 25);
                    rb[i, j].Location = new Point(10 + 50 * (j - 1), 15);
                    grb.Controls.Add(rb[i, j]);


                }


            }

           
            submit.Location = new Point(140, 50 * numberQuestion+50);
            submit.Size = new Size(100,50);
            submit.Enabled = false;
            submit.Click += Submit_Click;
            submit.Text = "Nộp Bài";
            this.Controls.Add(submit);
            this.Controls.Add(test);

        }
        void activeRadioButton()
        {

            for (int i = 1; i <= numberQuestion; i++)
                for (int j = 1; j <= 4; j++)
                    rb[i, j].Enabled = true;
            submit.Enabled = true;
        }



        private void Submit_Click(object sender, EventArgs e)
        {
           
        }

     

        public void LoginButton_Click(object sender, EventArgs e)
        {
            
            string s = client().Get("sinhvien/" + user.Text).Body;
            s =s.Replace("\"","");

            
             if (password.Text==s)
            {
                user.Enabled = false;
                password.Enabled = false;
                loginButton.Enabled = false;
                MessageBox.Show("Đăng nhập thành công");
                activeRadioButton();
     
            }

            else MessageBox.Show("Lỗi đăng nhập");
           

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            login();
            start();

        }
    }
}
