using System;
using System.Drawing;
using System.Windows.Forms;

namespace CatchFrogs
{
    public partial class Form1 : Form
    {
        private bool _isPlaying;
        private int count;

        public Form1()
        {
            InitializeComponent();
            _isPlaying = false;
        }

        private void picFrog_Click(object sender, EventArgs e)
        {
            if (_isPlaying)
            {
                RandomLocationOfFrog();
                GameWin();
            }
        }

        private void RandomLocationOfFrog()
        {
            var random = new Random();

            int newX = random.Next(0, pnlPool.Size.Width - picFrog.Size.Width);
            int newY = random.Next(0, pnlPool.Size.Height - picFrog.Size.Height);
            picFrog.Location = new Point(newX, newY);
        }

        private bool CheckWinGame()
        {
            return (picFrog.Location.X >= picBasket.Location.X &&
                    picFrog.Location.X <= picBasket.Location.X + picBasket.Size.Width &&
                    picFrog.Location.Y >= picBasket.Location.Y &&
                    picFrog.Location.Y <= picBasket.Location.Y + picBasket.Size.Height) ||
                   (picFrog.Location.X + picFrog.Size.Width >= picBasket.Location.X &&
                    picFrog.Location.X + picFrog.Size.Width <= picBasket.Location.X + picBasket.Size.Width &&
                    picFrog.Location.Y + picFrog.Size.Height >= picBasket.Location.Y &&
                    picFrog.Location.Y + picFrog.Size.Height <= picBasket.Location.Y + picBasket.Size.Height);
        }

        private void GameWin()
        {
            if (CheckWinGame())
            {
                _isPlaying = false;

                //Stop timer
                tmr.Enabled = false;
                //Show button start
                btnStart.Enabled = true;

                MessageBox.Show("You win!!!");
            }
        }

        private void GameLose()
        {
            if (count >= 600000) //equal a minute
            {
                _isPlaying = false;

                //Stop timer
                tmr.Enabled = false;
                //Show button start
                btnStart.Enabled = true;

                MessageBox.Show("You lose!!!");
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _isPlaying = true;
            // Hide button start
            btnStart.Enabled = false;
            // Run timer
            tmr.Enabled = true;
            // Initialize frog
            while (CheckWinGame())
            {
                RandomLocationOfFrog();
            }
            //Reset count
            count = 0;
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            count += 100;
            var dateTime = new DateTime(count*1000);
            lblClock.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", dateTime.Minute,
                                          dateTime.Second,
                                          dateTime.Millisecond/10);
            // Game lose
            GameLose();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}