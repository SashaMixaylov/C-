using System.Resources;
using Timer = System.Threading.Timer;

namespace WinFormsApp1_18._04._2025
{
    public partial class Form1 : Form
    {

        public PictureBox[] pictureBoxes;
        public int[] cardValues;
        public int firstChoice = -1;
        private int secondChoice = -1;
        private bool isProcessing = false;
        private ResourceManager resourceManager;
        private Timer closeTimer;
        public Form1()
        {
            InitializeComponent();

            resourceManager = new ResourceManager(typeof(Resource1));
            pictureBoxes = new PictureBox[12];
            cardValues = new int[12];
            closeTimer = new Timer(CloseCardsCallback);

            int[] values = { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6 };
            Random rand = new Random();
            for (int i = 0; i < 12; i++)
            {
                int index = rand.Next(values.Length);
                cardValues[i] = values[index];
                values = RemoveAt(values, index);
            }

            for (int i = 0; i < 12; i++)
            {
                pictureBoxes[i] = new PictureBox
                {
                    Width = 80,
                    Height = 100,
                    Left = (i % 4) * 90 + 150,
                    Top = (i / 4) * 110 + 50,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = System.Drawing.Color.LightGray
                };
                pictureBoxes[i].Click += pictureBox1_Click;
                Controls.Add(pictureBoxes[i]);
            }
           
            MenuStrip menuStrip = new MenuStrip();

            ToolStripMenuItem gameMenu = new ToolStripMenuItem("Игра");

            ToolStripMenuItem newGameItem = new ToolStripMenuItem("Новая игра");
            newGameItem.Click += NewGameToolStripMenuItem_Click;

            ToolStripMenuItem exitItem = new ToolStripMenuItem("Выход");
            exitItem.Click += ExitToolStripMenuItem_Click;

            gameMenu.DropDownItems.AddRange(new ToolStripMenuItem[] { newGameItem, exitItem });
            menuStrip.Items.Add(gameMenu);
            Controls.Add(menuStrip);


            
        }


        private void GamesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (isProcessing) return;

            PictureBox clickedBox = (PictureBox)sender;
            int index = Array.IndexOf(pictureBoxes, clickedBox);

            if (clickedBox.Image == null)
            {
                byte[] imageData = (byte[])resourceManager.GetObject($"card{cardValues[index]}");

                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    clickedBox.Image = new Bitmap(ms);
                }

                if (firstChoice == -1)
                {
                    firstChoice = index;
                }
                else
                {
                    secondChoice = index;
                    isProcessing = true;

                    if (cardValues[firstChoice] == cardValues[secondChoice])
                    {
                        // Карточки совпали - оставляем их открытыми
                        firstChoice = -1;
                        secondChoice = -1;
                        isProcessing = false;

                        // Проверяем, все ли пары найдены
                        bool allMatched = true;
                        foreach (PictureBox pb in pictureBoxes)
                        {
                            if (pb.Image == null)
                            {
                                allMatched = false;
                                break;
                            }
                        }

                        if (allMatched)
                        {
                            MessageBox.Show("Поздравляем! Вы нашли все пары!");
                        }
                    }
                    else
                    {
                        // Карточки не совпали - закрываем их через секунду
                        closeTimer.Change(1000, Timeout.Infinite);
                        /*timer1.Interval = 1000;
                        timer1.Tick += (tSender, args) =>
                        {
                            if (firstChoice >= 0 && firstChoice < pictureBoxes.Length)
                            {
                                clickedBox.Image = null;
                                pictureBoxes[firstChoice].Image = null;
                            }
                            timer1.Stop();
                            firstChoice = -1;
                            secondChoice = -1;
                            isProcessing = false;
                        };
                        timer1.Start()*/
                    }
                }
            }
        }

        private void CloseCardsCallback(object state)
        {
            if (closeTimer != null)
            {
                closeTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }

            if (firstChoice >= 0 && firstChoice < pictureBoxes.Length &&
                secondChoice >= 0 && secondChoice < pictureBoxes.Length)
            {
                pictureBoxes[firstChoice].Image = null;
                pictureBoxes[secondChoice].Image = null;
            }

            firstChoice = -1;
            secondChoice = -1;
            isProcessing = false;
        }
        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)   //Новая игра
        {
            foreach (PictureBox pb in pictureBoxes)
            {
                pb.Image = null;
            }

            int[] values = { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6 };
            Random rand = new Random();
            for (int i = 0; i < 12; i++)
            {
                int index = rand.Next(values.Length);
                cardValues[i] = values[index];
                values = RemoveAt(values, index);
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)  //Выход
        {
            Application.Exit();
        }

        private static T[] RemoveAt<T>(T[] source, int index)
        {
            T[] dest = new T[source.Length - 1];
            Array.Copy(source, 0, dest, 0, index);
            Array.Copy(source, index + 1, dest, index, source.Length - index - 1);
            return dest;
        }


    }

}
