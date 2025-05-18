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
        private int actionCount = 0;  //счетчик всех действий пользователя

        private int imageToggle = 0;  //переключатель между состояниями лап (0 и 1)

        // Изображения кота
        private Image RightPaw;  
        private Image LeftPaw;
        private Image raisedPaws;

        private Dictionary<Keys, Image> imageKeys = new Dictionary<Keys, Image>();
        private Keys lastPressedKey = Keys.None;  //последняя нажатая клавиша

        //private DispatcherTimer timer;
        private DispatcherTimer topmostTimer;
        private const double defaultTimeoutSeconds = 0.01;
        private double timeoutSeconds = defaultTimeoutSeconds;
        private System.Timers.Timer timer;  //возвращение к базовому состоянию после действия
        public Form1()
        {
            InitializeComponent();

            // Загрузка изображений из ресурсов
            RightPaw = byteArrayToImage(Resource1.cat_right);  //после добавления в pictureBox изображений появилась проблема.
            LeftPaw = byteArrayToImage(Resource1.cat_left);    //pictureBox - правая клавиша - выбрать изображение
            raisedPaws = byteArrayToImage(Resource1.cat_simple);


            // Настройка начального состояния
            pictureBox1.Image = raisedPaws;

            // Инициализация таймеров
            timer = new System.Timers.Timer(timeoutSeconds * 1000);
            timer.Elapsed += Timer_Elapsed;


            topmostTimer = new DispatcherTimer();
            topmostTimer.Interval = TimeSpan.FromSeconds(1);
        }

        public Image byteArrayToImage(byte[] byteArrayIn) //функция для работы с добавлением изображений
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms, true);
            return returnImage;
        }
        private void HandleKeyPress()  //анимация лап
        {
            timer.Stop();  //Останавливает предыдущий таймер
            timer.Interval = timeoutSeconds *1000;
            timer.Start();  //Запуск нового таймера для возврата в исходное состояние

            switch (imageToggle)         //Переключает изображение между правой и левой лапами
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
            pictureBox1.Image = raisedPaws; //Возвращает  в базовое состояние
            timer.Stop();  //остановливает текущий таймер
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) //мелод обработки клавиш
        {
            // Исключаем системные клавиши
            if (keyData == Keys.Space || keyData == Keys.LWin || // клавиши для пропуска
                keyData == Keys.RWin || keyData == Keys.Tab)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            // Загружаем изображение для нажатой клавиши
            if (!imageKeys.ContainsKey(keyData))
            {
                string resourceName = $"lap_{keyData}";
                try
                {
                    Image image = Resource1.ResourceManager.GetObject(resourceName) as Image;//загрузка соответствующего изображения
                    if (image != null)
                    {
                        imageKeys[keyData] = image;    
                    }
                }
                catch
                {
                    imageKeys[keyData] = byteArrayToImage(new byte[0]); //пустое изображение в случае ошибки
                }
            }

            if (pictureBox1.Focused)
            {
                pictureBox1.Image = imageKeys[keyData];//замена текущего изображения на соответствующий клавише нажатой
                lastPressedKey = keyData;  //запоминает последнюю клавишу
            }

            UpdateCounter();  //увеличивает счетчик действий
            System.Media.SystemSounds.Beep.Play();  //звук при нажатии клавиш

            return base.ProcessCmdKey(ref msg, keyData);  //Передача управления базовому классу для стандартной обработки клавиши
        }

        private void UpdateCounter() //счетчик совершенных действий
        {
            actionCount++;  //увеличивает счетчик на +1
            countLabel.Text = $"Действий: {actionCount}";  //обновление результата
        }

        private void BongoCatForm_MouseDown(object sender, MouseEventArgs e) //обработка для нажатия мышки и ее обработка
        {
            UpdateCounter();
            System.Media.SystemSounds.Beep.Play();
        }

        private void BongoCatForm_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateCounter();
            System.Media.SystemSounds.Beep.Play();
        }

        private void BongoCatForm_KeyDown(object sender, KeyEventArgs e) //обработка клавиши
        {
            UpdateCounter(); //увеличение счетчика
            System.Media.SystemSounds.Beep.Play();//звук нажатия

            Graphics g = this.CreateGraphics(); //создает объект для рисования
            g.FillEllipse(System.Drawing.Brushes.Red,  //создание красного круга с координатами
                         this.PointToClient(Cursor.Position).X - 10,
                         this.PointToClient(Cursor.Position).Y - 10,
                         20, 20);
            g.Dispose();  //освобождение ресурсов
        }

        private void Form1_Load(object sender, EventArgs e)//проверка загрузки изображения при запуске
        {
            if (RightPaw == null || LeftPaw == null || raisedPaws == null) //три  необходимых изображения
            {
                MessageBox.Show("Ошибка загрузки изображений!"); //в случае ошибки
                Close();
            }
        }
        protected override void Dispose(bool disposing)  //Освобождение памяти
        {
            base.Dispose(disposing);
            
            // Освобождение ресурсов изображений
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
