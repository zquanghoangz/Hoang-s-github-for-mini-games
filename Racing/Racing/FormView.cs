using System.Drawing;
using System.Windows.Forms;

namespace Racing
{
    public partial class FormView : Form
    {
        public FormView()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            _boardX = 50;
            _boardY = 50;
            _elementSize = 10;

            _row = 16;
            _col = 9;
            _matrix = new int[_row, _col];

            _carPossition = Possition.Center;
            SetNewValueForMatrix();
        }

        private void FormView_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawMatrix(g);
        }

        private void DrawMatrix(Graphics g)
        {
            for (int i = 0; i < _row; i++)
            {
                for (int j = 0; j < _col; j++)
                {
                    g.DrawRectangle(new Pen(Brushes.Blue),
                                    _boardX + j * _elementSize, _boardY + i * _elementSize,
                                    _elementSize, _elementSize);

                    if (_matrix[i, j] == 1)
                    {
                        g.FillRectangle(Brushes.Brown,
                                        _boardX + j * _elementSize, _boardY + i * _elementSize,
                                        _elementSize, _elementSize);
                    }
                    if (_matrix[i, j] == 2)
                    {
                        g.FillRectangle(Brushes.BlueViolet,
                                        _boardX + j * _elementSize, _boardY + i * _elementSize,
                                        _elementSize, _elementSize);
                    }
                }
            }
        }

        private void FormView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    switch (_carPossition)
                    {
                        case Possition.Right:
                            _carPossition = Possition.Center;
                            SetNewValueForMatrix();
                            break;
                        case Possition.Center:
                            _carPossition = Possition.Left;
                            SetNewValueForMatrix();
                            break;
                    }
                    break;
                case Keys.Right:
                    switch (_carPossition)
                    {
                        case Possition.Left:
                            _carPossition = Possition.Center;
                            SetNewValueForMatrix();
                            break;
                        case Possition.Center:
                            _carPossition = Possition.Right;
                            SetNewValueForMatrix();
                            break;
                    }
                    break;
            }
        }

        private void SetNewValueForMatrix()
        {
            ResetBoardMatrix();

            var y = (int)_carPossition;

            SetValueForDrawCar(12, y, 1);

            Invalidate(new Rectangle(_boardX, _boardY + 12 * _elementSize, 9 * _elementSize, 4 * _elementSize));
        }

        private void ResetBoardMatrix()
        {
            for (int i = 0; i < _row; i++)
                for (int j = 0; j < _col; j++)
                    _matrix[i, j] = 0;
        }

        private void SetValueForDrawCar(int x, int y, int value)
        {
            _matrix[x, 1 + y] = value;
            _matrix[x + 1, 1 + y] = value;
            _matrix[x + 2, 1 + y] = value;
            _matrix[x + 3, 1 + y] = value;
            _matrix[x + 1, y] = value;
            _matrix[x + 1, 2 + y] = value;
            _matrix[x + 3, y] = value;
            _matrix[x + 3, 2 + y] = value;
        }

        #region Nested type: Possition

        private enum Possition
        {
            Center = 3,
            Left = 0,
            Right = 6
        }

        #endregion

        #region Fields

        private int _boardX;
        private int _boardY;

        private Possition _carPossition;
        private int _col;
        private int _elementSize;
        private int[,] _matrix;
        private int _row;

        #endregion
    }
}