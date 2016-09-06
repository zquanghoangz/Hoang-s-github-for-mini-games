using System;
using System.Drawing;
using System.Windows.Forms;
using FreeFalling.Properties;

namespace FreeFalling
{
    public partial class FormView : Form
    {
        #region Fields

        private const int Ground = 310;
        private const int BallSize = 32;
        private int _ballPosition;
        private bool _isFallingDown;
        private int _theHighestPoint;

        #endregion

        #region Constructor and Initialize

        public FormView()
        {
            InitializeComponent();
            InitializeBall();
        }

        private void InitializeBall()
        {
            _theHighestPoint = 300 - BallSize;
            _ballPosition = 300 - BallSize;
            _isFallingDown = true;
            Invalidate();
        }

        #endregion

        #region Drawing

        private void FormView_Paint(object sender, PaintEventArgs e)
        {
            DrawBolder(e.Graphics);
            DrawBall(e.Graphics);
        }

        private void DrawBall(Graphics graphics)
        {
            graphics.DrawImage(Resources.ball2, 150, Ground - _ballPosition - BallSize, BallSize, BallSize);
        }

        private static void DrawBolder(Graphics graphics)
        {
            graphics.DrawRectangle(new Pen(Brushes.Chocolate), 10, 10, 300, 300);
        }

        #endregion

        #region Events

        private void TimerBallMove_Tick(object sender, EventArgs e)
        {
            CalculativeMovingSpeed();

            _ballPosition += _isFallingDown ? -1 : 1;
            tmrBallMove.Enabled = _theHighestPoint > 0;

            if (_ballPosition == 0)
            {
                CalculativeTheHighestPoint();
                _isFallingDown = false;
            }

            if (_ballPosition >= _theHighestPoint)
            {
                _isFallingDown = true;
            }

            //Re-draw form
            Invalidate(new Rectangle(150, Ground - _ballPosition - BallSize - 1, BallSize, BallSize + 2));
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            InitializeBall();
            tmrBallMove.Enabled = true;
        }

        private void FormView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Dispose();
            }
        }

        #endregion

        #region Business functions

        private void CalculativeTheHighestPoint()
        {
            _theHighestPoint /= 2;
        }

        private void CalculativeMovingSpeed()
        {
            //Change timer interval to make ball move faster or slower
            int value = _theHighestPoint/5;
            if (!_isFallingDown && value > 0 && _ballPosition/value > 0)
            {
                tmrBallMove.Interval = (_ballPosition/value)*(_ballPosition/value);
            }
            else
            {
                tmrBallMove.Interval = 1;
            }
        }

        #endregion
    }
}