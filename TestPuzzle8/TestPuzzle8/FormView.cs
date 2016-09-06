using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TestPuzzle8
{
    public partial class FormView : Form
    {
        const int PEICE_SIZE = 100;
        const int LOCATION = 50;
        PuzzleGame _puzzleGame;
        int _timeCount = 0;

        public FormView()
        {
            InitializeComponent();
            _puzzleGame = new PuzzleGame { Name = "OriginalGame" };
            _puzzleGame.CreateMatrix(PEICE_SIZE, LOCATION, LOCATION);

            UpdatePictureBoxLocation();
            SetTagForPicturesbox();
        }

        private void UpdatePictureBoxLocation()
        {
            for (int i = 0; i < 9; i++)
            {

                switch (_puzzleGame.Matrix[i].Value)
                {
                    case 0:
                        {
                            pictureBox0.Location = _puzzleGame.Matrix[i].GetPoint();
                            break;
                        }
                    case 1:
                        {
                            pictureBox1.Location = _puzzleGame.Matrix[i].GetPoint();
                            break;
                        }
                    case 2:
                        {
                            pictureBox2.Location = _puzzleGame.Matrix[i].GetPoint();
                            break;
                        }
                    case 3:
                        {
                            pictureBox3.Location = _puzzleGame.Matrix[i].GetPoint();
                            break;
                        }
                    case 4:
                        {
                            pictureBox4.Location = _puzzleGame.Matrix[i].GetPoint();
                            break;
                        }
                    case 5:
                        {
                            pictureBox5.Location = _puzzleGame.Matrix[i].GetPoint();
                            break;
                        }
                    case 6:
                        {
                            pictureBox6.Location = _puzzleGame.Matrix[i].GetPoint();
                            break;
                        }
                    case 7:
                        {
                            pictureBox7.Location = _puzzleGame.Matrix[i].GetPoint();
                            break;
                        }
                    case 8:
                        {
                            pictureBox8.Location = _puzzleGame.Matrix[i].GetPoint();
                            break;
                        }
                }

            }
        }

        private void SetTagForPicturesbox()
        {
            pictureBox0.Tag = 0;
            pictureBox1.Tag = 1;
            pictureBox2.Tag = 2;
            pictureBox3.Tag = 3;
            pictureBox4.Tag = 4;
            pictureBox5.Tag = 5;
            pictureBox6.Tag = 6;
            pictureBox7.Tag = 7;
            pictureBox8.Tag = 8;
        }

        private void PictureBoxClick(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            int idx = (int)pic.Tag;

            //if it near zero, 
            if (IsNearZeroPeice(idx))
            {
                //it will swap location with zero
                _puzzleGame.Matrix[idx].SwapLocation(_puzzleGame.Matrix[0]);

                //Update change
                UpdatePictureBoxLocation();

                //Set start time
                if (!timer.Enabled)
                {
                    timer.Enabled = true;
                    _timeCount = 0;
                }

                //Check to win
                if (_puzzleGame.IsWin(_puzzleGame.CustomMatrix(PEICE_SIZE, LOCATION, LOCATION)))
                {
                    MessageBox.Show("You win!");
                    timer.Enabled = false;
                    _puzzleGame.CreateMatrix(PEICE_SIZE, LOCATION, LOCATION);
                }
            }
        }

        private bool IsNearZeroPeice(int idx)
        {
            return (_puzzleGame.Matrix[idx].X + PEICE_SIZE == _puzzleGame.Matrix[0].X && _puzzleGame.Matrix[idx].Y == _puzzleGame.Matrix[0].Y) ||
                (_puzzleGame.Matrix[idx].X - PEICE_SIZE == _puzzleGame.Matrix[0].X && _puzzleGame.Matrix[idx].Y == _puzzleGame.Matrix[0].Y) ||
                (_puzzleGame.Matrix[idx].Y + PEICE_SIZE == _puzzleGame.Matrix[0].Y && _puzzleGame.Matrix[idx].X == _puzzleGame.Matrix[0].X) ||
                (_puzzleGame.Matrix[idx].Y - PEICE_SIZE == _puzzleGame.Matrix[0].Y && _puzzleGame.Matrix[idx].X == _puzzleGame.Matrix[0].X);
        }

        private void btnSaveState_Click(object sender, EventArgs e)
        {
            PuzzleGame curGame = new PuzzleGame { Name = "SaveData.xml",Matrix = new PuzzlePeice[9] };


            curGame.Matrix[0] = new PuzzlePeice { X = pictureBox0.Location.X, Y = pictureBox0.Location.Y, Value = (int)pictureBox0.Tag };
            curGame.Matrix[1] = new PuzzlePeice { X = pictureBox1.Location.X, Y = pictureBox1.Location.Y, Value = (int)pictureBox1.Tag };
            curGame.Matrix[2] = new PuzzlePeice { X = pictureBox2.Location.X, Y = pictureBox2.Location.Y, Value = (int)pictureBox2.Tag };
            curGame.Matrix[3] = new PuzzlePeice { X = pictureBox3.Location.X, Y = pictureBox3.Location.Y, Value = (int)pictureBox3.Tag };
            curGame.Matrix[4] = new PuzzlePeice { X = pictureBox4.Location.X, Y = pictureBox4.Location.Y, Value = (int)pictureBox4.Tag };
            curGame.Matrix[5] = new PuzzlePeice { X = pictureBox5.Location.X, Y = pictureBox5.Location.Y, Value = (int)pictureBox5.Tag };
            curGame.Matrix[6] = new PuzzlePeice { X = pictureBox6.Location.X, Y = pictureBox6.Location.Y, Value = (int)pictureBox6.Tag };
            curGame.Matrix[7] = new PuzzlePeice { X = pictureBox7.Location.X, Y = pictureBox7.Location.Y, Value = (int)pictureBox7.Tag };
            curGame.Matrix[8] = new PuzzlePeice { X = pictureBox8.Location.X, Y = pictureBox8.Location.Y, Value = (int)pictureBox8.Tag };

            string fileContent = Serializer.ToXmlString(curGame);
            File.WriteAllText(Path.Combine( Environment.CurrentDirectory, curGame.Name), fileContent);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            _timeCount++;
            lblTime.Text = TimeSpan.FromSeconds(_timeCount).ToString();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            _puzzleGame.CreateMatrix(PEICE_SIZE, LOCATION, LOCATION);
            UpdatePictureBoxLocation();
            lblTime.Text = "00:00:00";
        }

        private void btnLoadSaveGame_Click(object sender, EventArgs e)
        {
            //TBC
        }


    }
}
