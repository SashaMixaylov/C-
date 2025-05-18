using System.Runtime.InteropServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using Microsoft.UI.Windowing;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Microsoft.UI;
using Windows.UI.WindowManagement;
using System.Drawing;
using System.Resources;



namespace WinFormsApp_Bongo_Cat_Forms
{
    public partial class Form1 : Form
    {
        private int actionCount = 0;  //������� ���� �������� ������������

        private int imageToggle = 0;  //������������� ����� ����������� ��� (0 � 1)

        // ����������� ����
        private Image RightPaw;  
        private Image LeftPaw;
        private Image raisedPaws;

        private Dictionary<Keys, Image> imageKeys = new Dictionary<Keys, Image>();
        private Keys lastPressedKey = Keys.None;  //��������� ������� �������

        //private DispatcherTimer timer;
        private DispatcherTimer topmostTimer;
        private const double defaultTimeoutSeconds = 0.01;
        private double timeoutSeconds = defaultTimeoutSeconds;
        private System.Timers.Timer timer;  //����������� � �������� ��������� ����� ��������
        public Form1()
        {
            InitializeComponent();

            // �������� ����������� �� ��������
            RightPaw = byteArrayToImage(Resource1.cat_right);  //����� ���������� � pictureBox ����������� ��������� ��������.
            LeftPaw = byteArrayToImage(Resource1.cat_left);    //pictureBox - ������ ������� - ������� �����������
            raisedPaws = byteArrayToImage(Resource1.cat_simple);


            // ��������� ���������� ���������
            pictureBox1.Image = raisedPaws;

            // ������������� ��������
            timer = new System.Timers.Timer(timeoutSeconds * 1000);
            timer.Elapsed += Timer_Elapsed;


            topmostTimer = new DispatcherTimer();
            topmostTimer.Interval = TimeSpan.FromSeconds(1);
        }

        public Image byteArrayToImage(byte[] byteArrayIn) //������� ��� ������ � ����������� �����������
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms, true);
            return returnImage;
        }
        private void HandleKeyPress()  //�������� ���
        {
            timer.Stop();  //������������� ���������� ������
            timer.Interval = timeoutSeconds *1000;
            timer.Start();  //������ ������ ������� ��� �������� � �������� ���������

            switch (imageToggle)         //����������� ����������� ����� ������ � ����� ������
            {
                case 0:
                    pictureBox1.Image = RightPaw;
                    imageToggle = 1;
                    break;
                case 1:
                    pictureBox1.Image = LeftPaw;
                    imageToggle = 0;
                    break;
            }
        }

        private void Timer_Elapsed(object sender, EventArgs e)
        {
            pictureBox1.Image = raisedPaws; //����������  � ������� ���������
            timer.Stop();  //������������� ������� ������
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) //����� ��������� ������
        {
            // ��������� ��������� �������
            if (keyData == Keys.Space || keyData == Keys.LWin || // ������� ��� ��������
                keyData == Keys.RWin || keyData == Keys.Tab)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            // ��������� ����������� ��� ������� �������
            if (!imageKeys.ContainsKey(keyData))
            {
                string resourceName = $"lap_{keyData}";
                try
                {
                    Image image = Resource1.ResourceManager.GetObject(resourceName) as Image;//�������� ���������������� �����������
                    if (image != null)
                    {
                        imageKeys[keyData] = image;    
                    }
                }
                catch
                {
                    imageKeys[keyData] = byteArrayToImage(new byte[0]); //������ ����������� � ������ ������
                }
            }

            if (pictureBox1.Focused)
            {
                pictureBox1.Image = imageKeys[keyData];//������ �������� ����������� �� ��������������� ������� �������
                lastPressedKey = keyData;  //���������� ��������� �������
            }

            UpdateCounter();  //����������� ������� ��������
            System.Media.SystemSounds.Beep.Play();  //���� ��� ������� ������

            return base.ProcessCmdKey(ref msg, keyData);  //�������� ���������� �������� ������ ��� ����������� ��������� �������
        }

        private void UpdateCounter() //������� ����������� ��������
        {
            actionCount++;  //����������� ������� �� +1
            countLabel.Text = $"��������: {actionCount}";  //���������� ����������
        }

        private void BongoCatForm_MouseDown(object sender, MouseEventArgs e) //��������� ��� ������� ����� � �� ���������
        {
            UpdateCounter();
            System.Media.SystemSounds.Beep.Play();
        }

        private void BongoCatForm_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateCounter();
            System.Media.SystemSounds.Beep.Play();
        }

        private void BongoCatForm_KeyDown(object sender, KeyEventArgs e) //��������� �������
        {
            UpdateCounter(); //���������� ��������
            System.Media.SystemSounds.Beep.Play();//���� �������

            Graphics g = this.CreateGraphics(); //������� ������ ��� ���������
            g.FillEllipse(System.Drawing.Brushes.Red,  //�������� �������� ����� � ������������
                         this.PointToClient(Cursor.Position).X - 10,
                         this.PointToClient(Cursor.Position).Y - 10,
                         20, 20);
            g.Dispose();  //������������ ��������
        }

        private void Form1_Load(object sender, EventArgs e)//�������� �������� ����������� ��� �������
        {
            if (RightPaw == null || LeftPaw == null || raisedPaws == null) //���  ����������� �����������
            {
                MessageBox.Show("������ �������� �����������!"); //� ������ ������
                Close();
            }
        }
        protected override void Dispose(bool disposing)  //������������ ������
        {
            base.Dispose(disposing);
            
            // ������������ �������� �����������
            if (disposing)
            {
                RightPaw?.Dispose();
                LeftPaw?.Dispose();
                raisedPaws?.Dispose();

                foreach (var image in imageKeys.Values)
                {
                    image.Dispose();
                }
            }
        }

    }
}
