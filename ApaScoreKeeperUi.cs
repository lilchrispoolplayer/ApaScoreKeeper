using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ApaScoreKeeper
{
    public partial class ApaScoreKeeperUi : Form
    {
        private List<PictureBox> poolBalls = new List<PictureBox>();
        private bool loadingFiles;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ApaScoreKeeperUi()
        {
            InitializeComponent();

            rad8Ball.Tag = Constants.EIGHT_BALL;
            rad9Ball.Tag = Constants.NINE_BALL;
            txtPlayer1Name.Tag = Constants.PLAYER_1_NAME;
            cbxPlayer1SkillLevel.Tag = Constants.PLAYER_1_SKILL_LEVEL;
            numUpDwnPlayer1Score.Tag = Constants.PLAYER_1_SCORE;
            txtPlayer2Name.Tag = Constants.PLAYER_2_NAME;
            cbxPlayer2SkillLevel.Tag = Constants.PLAYER_2_SKILL_LEVEL;
            numUpDwnPlayer2Score.Tag = Constants.PLAYER_2_SCORE;
            txtRace.Tag = Constants.RACE;
            numUpDwnInnings.Tag = Constants.INNINGS;
            numUpDwnDeadBalls.Tag = Constants.DEAD_BALLS;            
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
        /// Sets the possible skill levels to select for a player in 8-ball
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rad8BallCheckedChanged(object sender, EventArgs e)
        {
            if (!rad8Ball.Checked)
                return;

            AdjustPlayerSkillLevels(Constants.EIGHT_BALL_SKILL_LEVELS);
            SaveGameType(rad8Ball);
        }

        /// <summary>
        /// Sets the possible skill levels to select for a player in 9-ball
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rad9BallCheckedChanged(object sender, EventArgs e)
        {
            if (!rad9Ball.Checked)
                return;

            AdjustPlayerSkillLevels(Constants.NINE_BALL_SKILL_LEVELS);
            SaveGameType(rad9Ball);
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
        /// Sets the race of the match based on the players' skill levels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxPlayerSkillLevelSelectedIndexChanged(object sender, EventArgs e)
        {
            SavePlayerSkillLevelToFile(sender as ComboBox);
            if (rad8Ball.Checked)
            {
                if ((cbxPlayer1SkillLevel.SelectedIndex < 0 || 5 < cbxPlayer1SkillLevel.SelectedIndex) ||
                    (cbxPlayer2SkillLevel.SelectedIndex < 0 || 5 < cbxPlayer2SkillLevel.SelectedIndex))
                    return;

                string race = Constants.EIGHT_BALL_GAMES[cbxPlayer1SkillLevel.SelectedIndex, cbxPlayer2SkillLevel.SelectedIndex];
                string[] playerGames = race.Split('/');
                txtRace.Text = string.Format("{0} - Race - {1}", playerGames[0], playerGames[1]);
            }
            else
            {
                if ((cbxPlayer1SkillLevel.SelectedIndex < 0 || Constants.NINE_BALL_GAMES.Length < cbxPlayer1SkillLevel.SelectedIndex) ||
                    (cbxPlayer2SkillLevel.SelectedIndex < 0 || Constants.NINE_BALL_GAMES.Length < cbxPlayer2SkillLevel.SelectedIndex))
                    return;

                txtRace.Text = string.Format("{0} - Race - {1}", Constants.NINE_BALL_GAMES[cbxPlayer1SkillLevel.SelectedIndex], Constants.NINE_BALL_GAMES[cbxPlayer2SkillLevel.SelectedIndex]);
            }
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
            if (!File.Exists(Constants.GAME_TYPE)) File.Create(Constants.GAME_TYPE).Close();
            if (!File.Exists(Constants.PLAYER_1_NAME)) File.Create(Constants.PLAYER_1_NAME).Close();
            if (!File.Exists(Constants.PLAYER_1_SKILL_LEVEL)) File.Create(Constants.PLAYER_1_SKILL_LEVEL).Close();
            if (!File.Exists(Constants.PLAYER_1_SCORE)) File.Create(Constants.PLAYER_1_SCORE).Close();
            if (!File.Exists(Constants.PLAYER_2_NAME)) File.Create(Constants.PLAYER_2_NAME).Close();
            if (!File.Exists(Constants.PLAYER_2_SKILL_LEVEL)) File.Create(Constants.PLAYER_2_SKILL_LEVEL).Close();
            if (!File.Exists(Constants.PLAYER_2_SCORE)) File.Create(Constants.PLAYER_2_SCORE).Close();
            if (!File.Exists(Constants.RACE)) File.Create(Constants.RACE).Close();
            if (!File.Exists(Constants.INNINGS)) File.Create(Constants.INNINGS).Close();
            if (!File.Exists(Constants.DEAD_BALLS)) File.Create(Constants.DEAD_BALLS).Close();
        }

        /// <summary>
        /// Loads textboxes with data from files
        /// </summary>
        private void LoadFiles()
        {
            loadingFiles = true;
            if (File.Exists(Constants.GAME_TYPE))
            {
                String gameType = "";
                using (StreamReader reader = new StreamReader(Constants.GAME_TYPE))
                    gameType = reader.ReadLine();

                if (gameType == rad8Ball.Tag.ToString() || gameType == null)
                    rad8Ball.Checked = true;
                else rad9Ball.Checked = true;
            }

            if (File.Exists(Constants.PLAYER_1_NAME))
            {
                using (StreamReader reader = new StreamReader(Constants.PLAYER_1_NAME))
                    txtPlayer1Name.Text = reader.ReadLine();
            }

            if (File.Exists(Constants.PLAYER_1_SKILL_LEVEL))
            {
                using (StreamReader reader = new StreamReader(Constants.PLAYER_1_SKILL_LEVEL))
                {
                    int skillLevel = Convert.ToInt32(reader.ReadLine());
                    cbxPlayer1SkillLevel.SelectedIndex = cbxPlayer1SkillLevel.Items.IndexOf(skillLevel);
                }
            }

            if (File.Exists(Constants.PLAYER_1_SCORE))
            {
                using (StreamReader reader = new StreamReader(Constants.PLAYER_1_SCORE))
                    numUpDwnPlayer1Score.Value = Convert.ToInt32(reader.ReadLine());
            }

            if (File.Exists(Constants.PLAYER_2_NAME))
            {
                using (StreamReader reader = new StreamReader(Constants.PLAYER_2_NAME))
                    txtPlayer2Name.Text = reader.ReadLine();
            }

            if (File.Exists(Constants.PLAYER_2_SKILL_LEVEL))
            {
                using (StreamReader reader = new StreamReader(Constants.PLAYER_2_SKILL_LEVEL))
                {
                    int skillLevel = Convert.ToInt32(reader.ReadLine());
                    cbxPlayer2SkillLevel.SelectedIndex = cbxPlayer2SkillLevel.Items.IndexOf(skillLevel);
                }
            }

            if (File.Exists(Constants.PLAYER_2_SCORE))
            {
                using (StreamReader reader = new StreamReader(Constants.PLAYER_2_SCORE))
                    numUpDwnPlayer2Score.Value = Convert.ToInt32(reader.ReadLine());
            }

            if (File.Exists(Constants.RACE))
            {
                using (StreamReader reader = new StreamReader(Constants.RACE))
                    txtRace.Text = reader.ReadLine();
            }

            if (File.Exists(Constants.INNINGS))
            {
                using (StreamReader reader = new StreamReader(Constants.INNINGS))
                    numUpDwnInnings.Value = Convert.ToInt32(reader.ReadLine());
            }

            if (File.Exists(Constants.DEAD_BALLS))
            {
                using (StreamReader reader = new StreamReader(Constants.DEAD_BALLS))
                    numUpDwnDeadBalls.Value = Convert.ToInt32(reader.ReadLine());
            }
            loadingFiles = false;
        }

        /// <summary>
        /// Automatically adjusts the skill levels from 9-ball to 8-ball
        /// </summary>
        /// <param name="skillLevels"></param>
        private void AdjustPlayerSkillLevels(int[] skillLevels)
        {
            int player1SkillLevel = 0;
            if (cbxPlayer1SkillLevel.SelectedIndex != -1)
                player1SkillLevel = Convert.ToInt32(cbxPlayer1SkillLevel.Items[cbxPlayer1SkillLevel.SelectedIndex]);
            cbxPlayer1SkillLevel.Items.Clear();
            foreach (int skillLevel in skillLevels)
                cbxPlayer1SkillLevel.Items.Add(skillLevel);
            if (player1SkillLevel < skillLevels[0])
                cbxPlayer1SkillLevel.SelectedIndex = 0;
            else if (player1SkillLevel > skillLevels[skillLevels.Length - 1])
                cbxPlayer1SkillLevel.SelectedIndex = skillLevels.Length - 1;
            else cbxPlayer1SkillLevel.SelectedIndex = cbxPlayer1SkillLevel.Items.IndexOf(player1SkillLevel);
            
            int player2SkillLevel = 0;
            if (cbxPlayer2SkillLevel.SelectedIndex != -1)
                player2SkillLevel = Convert.ToInt32(cbxPlayer2SkillLevel.Items[cbxPlayer2SkillLevel.SelectedIndex]);
            cbxPlayer2SkillLevel.Items.Clear();
            foreach (int skillLevel in skillLevels)
                cbxPlayer2SkillLevel.Items.Add(skillLevel);
            if (player2SkillLevel < skillLevels[0])
                cbxPlayer2SkillLevel.SelectedIndex = 0;
            else if (player2SkillLevel > skillLevels[skillLevels.Length - 1])
                cbxPlayer2SkillLevel.SelectedIndex = skillLevels.Length - 1;
            else cbxPlayer2SkillLevel.SelectedIndex = cbxPlayer2SkillLevel.Items.IndexOf(player2SkillLevel);
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
        /// Saves the game type that is selected
        /// </summary>
        /// <param name="radioButton"></param>
        private void SaveGameType(RadioButton radioButton)
        {
            if (loadingFiles)
                return;

            using (StreamWriter writer = new StreamWriter(Constants.GAME_TYPE))
            {
                writer.Write(radioButton.Tag.ToString());
            }
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
        /// Saves the selected index of the player's skill level to a file
        /// </summary>
        /// <param name="comboBox"></param>
        private void SavePlayerSkillLevelToFile(ComboBox comboBox)
        {
            if (loadingFiles)
                return;

            using (StreamWriter writer = new StreamWriter(comboBox.Tag.ToString()))
            {
                writer.Write(comboBox.Items[comboBox.SelectedIndex]);
            }
        }

        /// <summary>
        /// Save a NumericUpDown's value to file
        /// </summary>
        /// <param name="numUpDwn"></param>
        private void SaveNumUpDwnValueToFile(NumericUpDown numUpDwn)
        {
            if (loadingFiles)
                return;

            using (StreamWriter writer = new StreamWriter(numUpDwn.Tag.ToString()))
            {
                writer.Write(numUpDwn.Value);
            }
        }
    }
}
