using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AudioPlayer;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class MainMenu : Form
    {
        //Creates global variables for all methods.
        AudioFilePlayer idleThemePlayer = new AudioFilePlayer();
        AudioFilePlayer fightingMusicPlayer = new AudioFilePlayer();
        Timer backgroundIdleTimer;
        Timer clipTimer;
        Image[] idleImage = new Image[8];
        Image[] clipImage = new Image[168];
        Button playButton;
        Button quitButton;
        Button highScoreButton;
        Button backButton;
        TextBox highScoreTextbox;
        bool clipTimerBegan = false;
        const int buttonX = 100; 
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            //==========================================================================
            //                        Set main menu screen settings.
            this.Text = "Korra Game Remastered";
            this.Size = new Size(960, 700);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.MaximumSize;
            this.ControlBox = false;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2 - this.Height / 2);
            //plays music
            idleThemePlayer.setAudioFile(Application.StartupPath + @"\GameFiles\Music\AvatarTheme.mp3");
            fightingMusicPlayer.setAudioFile(Application.StartupPath + @"\GameFiles\Music\fightMusic.mp3");
            idleThemePlayer.playLooping();
            fightingMusicPlayer.stop();
            createButtons();

            //==========================================================================
            //                        Get Animation Images.
            for (int i = 0; i < clipImage.Length; i++)
            {
                clipImage[i] = Image.FromFile(Application.StartupPath + @"\GameFiles\ClipImages\" + (i + 1) + ".jpg");
            }

            //Sets Background Variables
            for (int i = 0; i < idleImage.Length; i++)
            {
                idleImage[i] = Image.FromFile(Application.StartupPath + @"\GameFiles\IdleImages\p" + (i + 1) + ".jpg", true);
            }
            this.BackgroundImage = idleImage[0];

            //==========================================================================
            //                     Highscore File Writing/Reading
           // while (fileReader.Peek() >= 0)
            {
                highScoreTextbox.Font = new Font(highScoreTextbox.Font.FontFamily, 16);
                highScoreTextbox.Text += "Arrow keys and space to move" + "\r\n";
                highScoreTextbox.Text += "S - Fire attack, (hold down) this is also your main attack." + "\r\n";
                highScoreTextbox.Text += "D - Water attack, this washes the enemies away and stuns them." + "\r\n";
                highScoreTextbox.Text += "A - Earth attack, create a wall to keep the enemies back." + "\r\n";
                highScoreTextbox.Text += "" + "\r\n";
                highScoreTextbox.Text += "(2015) Korra Game by: Ian Chui" + "\r\n";
                highScoreTextbox.Text += "facebook.com/peakyDicers" + "\r\n";
                highScoreTextbox.Text += "hi, this is my Korra game, from Legend of Korra. I wanted to do a lot more with this game, but I didn't have time. Anyway, best of luck in comp sci!";       
            }
            //===========================================================================
            //                      Create animation timers.
            //Setting idle main menu screen timer.
            backgroundIdleTimer = new Timer();
            backgroundIdleTimer.Tick += new EventHandler(backgroundIdleTimer_Tick);
            backgroundIdleTimer.Interval = 35;
            backgroundIdleTimer.Start();

            //setting intro clip timer.
            clipTimer = new Timer();
            clipTimer.Tick += new EventHandler(clipTimer_Tick);
            clipTimer.Interval = 30;

            //===========================================================================

            this.Invalidate(); //refreshes screen.
        }

        void clipTimer_Tick(object sender, EventArgs e)
        {
            //After the player clicks the "PLAY" button, run through a short animation.
            if (!clipTimerBegan) //sets first clip image.
            {
                this.BackgroundImage = clipImage[0];
                clipTimerBegan = true;
            }
            for (int i = 0; i < clipImage.Length; i++) //runs through all clip images once.
            {
                if (this.BackgroundImage == clipImage[i] && i != clipImage.Length - 1)
                {
                    this.BackgroundImage = clipImage[i + 1];
                    break; //switches 1 image per tick.
                }
            }
            if (this.BackgroundImage == clipImage[clipImage.Length - 1]) //at last clip, stop everything and begin game.
            {
                loadForm(new Form1()); //open game form.
                clipTimerBegan = false; //reset clip variable.
                clipTimer.Stop(); //stop clip timer.
                idleThemePlayer.stop();
                fightingMusicPlayer.playLooping();
            }
        } //good

        void backgroundIdleTimer_Tick(object sender, EventArgs e) //good
        {
            for (int i = 0; i < idleImage.Length; i++)
            //Loops through all idle images, to create an idle screen animation.
            {
                if (this.BackgroundImage == idleImage[i] && i < idleImage.Length) //goes to next idle image.
                {
                    this.BackgroundImage = idleImage[i + 1];
                    break;
                }
                if (this.BackgroundImage == idleImage[idleImage.Length - 1]) //if last image is reached, go back to beginning.
                {
                    this.BackgroundImage = idleImage[0];
                }
            }
        }


        void createButtons()
        {
            //create back button.
            backButton = new Button();
            backButton.Text = "Back";
            backButton.Font = new Font("Helvetica", 10, FontStyle.Regular);
            backButton.Location = new Point(buttonX, 450);
            backButton.Size = new Size(130, 40);
            backButton.BackColor = Color.DimGray;
            backButton.ForeColor = Color.White;
            backButton.Visible = true;
            backButton.Hide();
            backButton.Click += new EventHandler(backButton_Click);
            this.Controls.Add(backButton);

            //create play button.
            playButton = new Button();
            playButton.Text = "Play";
            playButton.Font = new Font("Helvetica", 10, FontStyle.Regular);
            playButton.Location = new Point(buttonX, 350);
            playButton.Size = new Size(130, 40);
            playButton.BackColor = Color.DimGray;
            playButton.ForeColor = Color.White;
            playButton.Show();
            this.Controls.Add(playButton);
            playButton.Click += new EventHandler(playButton_Click);

            //create highscore button.
            highScoreButton = new Button();
            highScoreButton.Text = "Controls and Credits";
            highScoreButton.Font = new Font("Helvetica", 10, FontStyle.Regular);
            highScoreButton.Location = new Point(buttonX, 400);
            highScoreButton.Size = new Size(130, 40);
            highScoreButton.BackColor = Color.DimGray;
            highScoreButton.ForeColor = Color.White;
            highScoreButton.Show();
            this.Controls.Add(highScoreButton);
            highScoreButton.Click += new EventHandler(highScoreButton_Click);

            //create quit button.
            quitButton = new Button();
            quitButton.Text = "Quit";
            quitButton.Font = new Font("Helvetica", 10, FontStyle.Regular);
            quitButton.Location = new Point(buttonX, 450);
            quitButton.Size = new Size(130, 40);
            quitButton.BackColor = Color.DimGray;
            quitButton.ForeColor = Color.White;
            quitButton.Visible = true;
            quitButton.Show();
            this.Controls.Add(quitButton);
            quitButton.Click += new EventHandler(quitButton_Click);

            //create highscore textbox.
            highScoreTextbox = new TextBox();
            highScoreTextbox.Multiline = true;
            highScoreTextbox.Text = "";
            highScoreTextbox.Font = new Font("Helvetica", 25, FontStyle.Regular);
            highScoreTextbox.Size = new Size(ClientSize.Width - 200, 300);
            highScoreTextbox.Location = new Point(100, 100);
            highScoreTextbox.BackColor = Color.Black;
            highScoreTextbox.ForeColor = Color.White;
            highScoreTextbox.Hide();
            highScoreTextbox.Enabled = false;
            this.Controls.Add(highScoreTextbox);


        } //good
        void goButton_Click(object sender, EventArgs e) //good
        {
                //show all main menu buttons.
                quitButton.Show();
                playButton.Show();
                highScoreButton.Show();
        }
        void quitButton_Click(object sender, EventArgs e)
        {
            Application.Exit(); //ends program.
        } //good
        void highScoreButton_Click(object sender, EventArgs e) //good
        {
            //show highscore & back button.
            highScoreTextbox.Show();
            backButton.Show();

            //hides main screen buttons.
            playButton.Hide();
            highScoreButton.Hide();
            quitButton.Hide();
        }

        void playButton_Click(object sender, EventArgs e)
        {
            backgroundIdleTimer.Stop(); //stops idle animation.
            clipTimer.Start();          //starts intro animation.

            //hides all buttons, for animation.
            playButton.Hide();
            highScoreButton.Hide();
            quitButton.Hide();
            backButton.Hide();
        } //good
        private void loadForm(Form frm)
        {
            frm.FormClosed += new FormClosedEventHandler(frm_FormClosed); //when game form is closed.
            frm.FormClosing += new FormClosingEventHandler(frm_FormClosing); //when game form is closing.
            this.Hide(); //hide main menu.
            frm.Show();  //show game form.
        } //good

        void frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //=============================================================================
            //                     Saves player score to txt file.
            //StreamWriter highScoreWriter = new StreamWriter(Application.StartupPath + @"\GameFiles\ScoreFiles\highscores.txt", true);
            //highScoreWriter.WriteLine(tempUsername + "\t\t" + GlobalVariablesClass.playerScore); //writes player score to txt file.
            //highScoreWriter.Close(); //close the filewriter.
            idleThemePlayer.playLooping();
            fightingMusicPlayer.stop();

            //Rewrite the highscore board.
            //fileReader = new StreamReader(Application.StartupPath + @"\GameFiles\ScoreFiles\highscores.txt");
            //highScoreTextbox.Clear(); //clears the scoreboard.
            //while (fileReader.Peek() >= 0) //rewrite with new player score.
            //{
            //   tempString = fileReader.ReadLine();
            //    highScoreTextbox.Text += tempString + "\r\n";
            //}
            //fileReader.Close(); //close the filereader.
            ///=============================================================================
        } //good

        void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show(); //show main menu.
            this.BackgroundImage = idleImage[0]; //reset background image.
            backgroundIdleTimer.Start(); //begin idle screen animation.

            //show main menu buttons.
            playButton.Show();
            quitButton.Show();
            highScoreButton.Show();
        } //good
        void backButton_Click(object sender, EventArgs e)
        {
            highScoreTextbox.Hide(); //hides highscore.

            //shows all main menu buttons.
            playButton.Show();
            highScoreButton.Show();
            quitButton.Show();
            backButton.Hide();
        } //good

    }
}
