using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public partial class FormView : Form
    {
        private enum Direction
        {
            Left, Right, Up, Down
        }

        private readonly int _elementSize;
        private Point _boardSize;
        private Point _boardStart;

        private List<Point> _snake;
        private Direction _moveDirection;

        public FormView()
        {
            InitializeComponent();
            _boardStart = new Point(50, 30);
            _boardSize = new Point(20, 30);
            _elementSize = 16;
            _moveDirection = Direction.Right;

            InitializeSnake();
        }

        private void InitializeSnake()
        {
            _snake = new List<Point>
                         {
                             new Point(0, 0),
                             new Point(0, 1),
                             new Point(0, 2),
                             new Point(0, 3),
                             new Point(0, 4)
                         };
        }

        private void FormView_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            DrawBoard(g);
            DrawSnake(g);
        }

        private void DrawSnake(Graphics g)
        {
            foreach (Point point in _snake)
            {
                g.DrawImage(new Bitmap("soccer.png"),
                            _boardStart.X + point.Y * _elementSize,
                            _boardStart.Y + point.X * _elementSize, _elementSize, _elementSize);
            }
        }

        private void DrawBoard(Graphics g)
        {
            for (int i = 0; i < _boardSize.X; i++)
            {
                for (int j = 0; j < _boardSize.Y; j++)
                {
                    g.DrawRectangle(new Pen(Brushes.BlueViolet),
                                    _boardStart.X + j * _elementSize, _boardStart.Y + i * _elementSize, _elementSize,
                                    _elementSize);
                }
            }
        }

        private void tmrSnake_Tick(object sender, System.EventArgs e)
        {
            for (int i = 0; i < _snake.Count - 1; i++)
            {
                _snake[i] = _snake[i + 1];
            }

            if (_moveDirection == Direction.Left)
            {
                _snake[_snake.Count - 1] = new Point(_snake[_snake.Count - 1].X, _snake[_snake.Count - 1].Y - 1);
            }
            else if (_moveDirection == Direction.Right)
            {
                _snake[_snake.Count - 1] = new Point(_snake[_snake.Count - 1].X, _snake[_snake.Count - 1].Y + 1);
            }
            else if (_moveDirection == Direction.Up)
            {
                _snake[_snake.Count - 1] = new Point(_snake[_snake.Count - 1].X - 1, _snake[_snake.Count - 1].Y);
            }
            else if (_moveDirection == Direction.Down)
            {
                _snake[_snake.Count - 1] = new Point(_snake[_snake.Count - 1].X + 1, _snake[_snake.Count - 1].Y);
            }

            Invalidate();
        }

        private void FormView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    _moveDirection = Direction.Left;
                    break;
                case Keys.Right:
                    _moveDirection = Direction.Right;
                    break;
                case Keys.Up:
                    _moveDirection = Direction.Up;
                    break;
                case Keys.Down:
                    _moveDirection = Direction.Down;
                    break;
            }
        }
    }
}