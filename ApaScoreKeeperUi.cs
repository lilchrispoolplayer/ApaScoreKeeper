using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ApaScoreKeeper
{
    public partial class ApaScoreKeeperUi : Form
    {
        private const string PLAYER_1_NAME = "Player1Name.txt";
        private const string PLAYER_1_SCORE = "Player1Score.txt";
        private const string PLAYER_2_NAME = "Player2Name.txt";
        private const string PLAYER_2_SCORE = "Player2Score.txt";
        private const string RACE = "Race.txt";
        private const string INNINGS = "Innings.txt";
        private const string DEAD_BALLS = "DeadBalls.txt";

        private List<PictureBox> poolBalls = new List<PictureBox>();
        private bool loadingFiles;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ApaScoreKeeperUi()
        {
            InitializeComponent();

            txtPlayer1Name.Tag = PLAYER_1_NAME;
            numUpDwnPlayer1Score.Tag = PLAYER_1_SCORE;
            txtPlayer2Name.Tag = PLAYER_2_NAME;
            numUpDwnPlayer2Score.Tag = PLAYER_2_SCORE;
            txtRace.Tag = RACE;
            numUpDwnInnings.Tag = INNINGS;
            numUpDwnDeadBalls.Tag = DEAD_BALLS;            
        }

        /// <summary>
        /// Initializes the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApaScoreKeeperUiLoad(object sender, EventArgs e)
        {
            InitFiles();
            LoadFiles();
            ResetScores();

            poolBalls.Add(pbx1Ball);
            poolBalls.Add(pbx2Ball);
            poolBalls.Add(pbx3Ball);
            poolBalls.Add(pbx4Ball);
            poolBalls.Add(pbx5Ball);
            poolBalls.Add(pbx6Ball);
            poolBalls.Add(pbx7Ball);
            poolBalls.Add(pbx8Ball);
            poolBalls.Add(pbx9Ball);
            poolBalls.Add(pbx10Ball);
            poolBalls.Add(pbx11Ball);
            poolBalls.Add(pbx12Ball);
            poolBalls.Add(pbx13Ball);
            poolBalls.Add(pbx14Ball);
            poolBalls.Add(pbx15Ball);
        }

        /// <summary>
        /// Saves the changed text to its appropriate file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxTextChanged(object sender, EventArgs e)
        {
            if (!(sender is TextBox textBox))
                return;

            SaveTextBoxTextToFile(textBox);
        }

        /// <summary>
        /// Writes player's score to file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumUpDwnValueChanged(object sender, EventArgs e)
        {
            if (!(sender is NumericUpDown numUpDwn))
                return;

            SaveNumUpDwnValueToFile(numUpDwn);
        }

        /// <summary>
        /// Toggles pool ball visibility
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BallCheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is CheckBox checkBox))
                return;

            poolBalls[Convert.ToInt32(checkBox.Tag) - 1].Visible = checkBox.Checked;
        }

        /// <summary>
        /// Resets the visibility of all pool balls to true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnResetVisibilityClick(object sender, EventArgs e)
        {
            chk1Ball.Checked = true;
            chk2Ball.Checked = true;
            chk3Ball.Checked = true;
            chk4Ball.Checked = true;
            chk5Ball.Checked = true;
            chk6Ball.Checked = true;
            chk7Ball.Checked = true;
            chk8Ball.Checked = true;
            chk9Ball.Checked = true;
            chk10Ball.Checked = true;
            chk11Ball.Checked = true;
            chk12Ball.Checked = true;
            chk13Ball.Checked = true;
            chk14Ball.Checked = true;
            chk15Ball.Checked = true;
        }

        /// <summary>
        /// Resets the scores on the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnResetScoresClick(object sender, EventArgs e)
        {
            using (new CenterWinDialog(this))
            {
                if (MessageBox.Show("Are you sure you want to reset scores?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                ResetScores();
            }
        }   

        /// <summary>
        /// Creates programs files if they don't exist
        /// </summary>
        private void InitFiles()
        {
            if (!File.Exists(PLAYER_1_NAME)) File.Create(PLAYER_1_NAME).Close();
            if (!File.Exists(PLAYER_1_SCORE)) File.Create(PLAYER_1_SCORE).Close();
            if (!File.Exists(PLAYER_2_NAME)) File.Create(PLAYER_2_NAME).Close();
            if (!File.Exists(PLAYER_2_SCORE)) File.Create(PLAYER_2_SCORE).Close();
            if (!File.Exists(RACE)) File.Create(RACE).Close();
            if (!File.Exists(INNINGS)) File.Create(INNINGS).Close();
            if (!File.Exists(DEAD_BALLS)) File.Create(DEAD_BALLS).Close();
        }

        /// <summary>
        /// Loads textboxes with data from files
        /// </summary>
        private void LoadFiles()
        {
            loadingFiles = true;
            if (File.Exists(PLAYER_1_NAME))
            {
                using (StreamReader reader = new StreamReader(PLAYER_1_NAME))
                    txtPlayer1Name.Text = reader.ReadLine();
            }

            if (File.Exists(PLAYER_2_NAME))
            {
                using (StreamReader reader = new StreamReader(PLAYER_2_NAME))
                    txtPlayer2Name.Text = reader.ReadLine();
            }

            if (File.Exists(RACE))
            {
                using (StreamReader reader = new StreamReader(RACE))
                    txtRace.Text = reader.ReadLine();
            }
            loadingFiles = false;
        }

        /// <summary>
        /// Resets the scores
        /// </summary>
        private void ResetScores()
        {
            numUpDwnPlayer1Score.Value = 0;
            SaveNumUpDwnValueToFile(numUpDwnPlayer1Score);

            numUpDwnPlayer2Score.Value = 0;
            SaveNumUpDwnValueToFile(numUpDwnPlayer2Score);

            numUpDwnInnings.Value = 0;
            SaveNumUpDwnValueToFile(numUpDwnInnings);

            numUpDwnDeadBalls.Value = 0;
            SaveNumUpDwnValueToFile(numUpDwnDeadBalls);
        }

        /// <summary>
        /// Saves a textbox's text to file
        /// </summary>
        /// <param name="textBox"></param>
        private void SaveTextBoxTextToFile(TextBox textBox)
        {
            if (loadingFiles)
                return;

            using (StreamWriter writer = new StreamWriter(textBox.Tag.ToString()))
            {
                writer.Write(textBox.Text);
            }
        }

        /// <summary>
        /// Save a NumericUpDown's value to file
        /// </summary>
        /// <param name="numUpDwn"></param>
        private void SaveNumUpDwnValueToFile(NumericUpDown numUpDwn)
        {
            using (StreamWriter writer = new StreamWriter(numUpDwn.Tag.ToString()))
            {
                writer.Write(numUpDwn.Value);
            }
        }
    }
}
