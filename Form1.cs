using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        //declares global variables for all methods.
        Rectangle playerRect;
        Rectangle[] darkSpiritRect = new Rectangle[30];
        Rectangle earthRect;
        Rectangle floorRect;

        Image playerRightImage = Image.FromFile(Application.StartupPath + @"\GameFiles\EntityImages\playerFaceRight.png", true);
        Image playerLeftImage = Image.FromFile(Application.StartupPath + @"\GameFiles\EntityImages\playerFaceLeft.png", true);
        Image waterSymbolImage = Image.FromFile(Application.StartupPath + @"\GameFiles\HudFiles\waterSymbol.png", true);
        Image earthSymbolImage = Image.FromFile(Application.StartupPath + @"\GameFiles\HudFiles\earthSymbol.png", true);
        Image bloodSymbolImage = Image.FromFile(Application.StartupPath + @"\GameFiles\HudFiles\blood.png", true);
        Image floorImage = Image.FromFile(Application.StartupPath + @"\GameFiles\EntityImages\floor.jpg", true);
        Random randomNumber = new Random();
        Random randomDiLeeMaker = new Random();

        //creates timers.
        Timer playerMovementTimer;
        Timer playerJumpTimer;
        Timer darkSpiritMovementTimer;
        Timer darkSpiritHealthTimer;
        Timer diLeeMovementTimer;
        Timer diLeeHealthTimer;
        Timer earthAttackTimer;
        Timer fireAttackTimer;
        Timer waterAttackTimer;
        Timer levelOneTimer;
        Timer levelTwoTimer;
        Timer levelChangerTimer;
        Timer playerHealthTimer;
        Timer waterAttackCooldownTimer;
        Timer earthAttackCooldownTimer;
        Timer diLeeAttackTimer;
        Timer timeElapsedTimer;

        //Dark Spirit Variables.
        Image darkSpiritImage = Image.FromFile(Application.StartupPath + @"\GameFiles\EntityImages\darkSpirit.png", true);
        int darkSpiritNum = 0;
        const int darkSpiritGravity = 10;
        const int darkSpiritHeight = 90;
        const int darkSpiritWidth = 90;
        int[] darkSpiritDx;
        int[] darkSpiritHealth;
        int[] enemyUnpauseCount;
        bool[] darkSpiritPaused;
        bool[] darkSpiritSpawned;
        bool[] darkSpiritAlive;

        //Player Variables. 
        int playerDx;
        int gravity = 0;
        int upper = 0;
        bool playerJumping = false;
        bool playerFacingRight = false;
        const int playerHeight = 110;
        const int playerWidth = 100;

        //Earth Attack Variables
        Image earthRightImage = Image.FromFile(Application.StartupPath + @"\GameFiles\EntityImages\earthR.png", true);
        Image earthLeftImage = Image.FromFile(Application.StartupPath + @"\GameFiles\EntityImages\earthL.png", true);
        int earthGravity = 0;
        int earthUpper = 0;
        int earthDx = 0;
        int earthFriction = 0;
        int earthDespawnCounter = 0;
        bool usingEarth = false;
        bool earthSpawned = false;
        bool earthGrounded = false;
        bool earthPunch = false;
        bool earthFaceRight = false;

        //Fire Attack Variables. 
        Image fireRightImage = Image.FromFile(Application.StartupPath + @"\GameFiles\EntityImages\fireR.png", true);
        Image fireLeftImage = Image.FromFile(Application.StartupPath + @"\GameFiles\EntityImages\fireL.png", true);
        Rectangle[] fireRect = new Rectangle[5];
        bool usingFire = false;
        bool fireOnce = false;
        bool fireLetGo = false;
        bool[] fireFaceRight;
        bool[] fireSpawned;
        int[] fireDX;
        int[] fireTimeCounter;

        //Water Attack Variables. 
        Image waterRightImage = Image.FromFile(Application.StartupPath + @"\GameFiles\EntityImages\waveR.png", true);
        Image waterLeftImage = Image.FromFile(Application.StartupPath + @"\GameFiles\EntityImages\waveL.png", true);
        Image iceRightImage = Image.FromFile(Application.StartupPath + @"\GameFiles\EntityImages\spikesR.png", true);
        Image iceLeftImage = Image.FromFile(Application.StartupPath + @"\GameFiles\EntityImages\spikesL.png", true);
        Rectangle waterRightRect;
        Rectangle waterLeftRect;
        Rectangle iceRightRect;
        Rectangle iceLeftRect;
        bool usingWater = false;
        bool waterSpawned = false;
        bool iceSpawned = false;
        int waterGravity = 0;
        int waterUpper = 0;
        int waterRightDx = 0;
        int waterLeftDx = 0;
        int iceSpawnCounter = 0;
        int iceDespawnCounter = 0;
        int iceDy = 0;
        int waterSequence = 0;

        //Level Variables. 
        int L1EnemyCount = 0;
        int L2EnemyCount = 0;
        int enemyOnScreen = 0;
        int darkSpiritOnScreen = 0;
        int diLeeOnScreen = 0;
        int diLeeStock;
        bool levelOneComplete = false;
        bool lvl2AllDead = false;
        bool lvl2AllAlive = false;

        //Other Variables.
        bool rightArrowDown = false;
        bool leftArrowDown = false;
        int yLimit = 600;
        int numOfDarkSpirits;
        int numOfFire;
        int numOfDiLee;
        bool spawnEnemyRight = false;
        bool allDead = true;
        bool allAlive = true;
        int timeElapsed = 0;
        int score = 0;
        bool level2Start = false;
        bool level1Start = false;

        //Di Lee Variables. 
        Image diLeeRightImage = Image.FromFile(Application.StartupPath + @"\GameFiles\EntityImages\diLeeR.png", true);
        Image diLeeLeftImage = Image.FromFile(Application.StartupPath + @"\GameFiles\EntityImages\diLeeL.png", true);
        Rectangle[] diLeeRect = new Rectangle[20];
        bool[] diLeeSpawned;
        bool[] diLeeAlive;
        bool[] diLeeRandomMovement;
        bool[] diLeeRunAway;
        bool[] diLeeRightOfPlayer;
        bool[] diLeeFaceRight;
        int[] diLeeDx;
        int[] diLeeRandomCounter;
        int[] diLeeRunAwayCounter;
        int[] diLeeHealth;
        int diLeeRandomNumber = 0;
        const int diLeeWidth = 60;
        const int diLeeHeight = 120;
        const int diLeeSpeed = 7;
        int diLeeRandomAttackNum;

        //player Health Variables. 
        const int playerHpChunkWidth = 1;
        const int playerHpChunkHeight = 30;
        Rectangle[] playerHpChunkRect = new Rectangle[500];
        SolidBrush redBrush = new SolidBrush(Color.Red);
        int numOfPlayerHpChunks = 500;
        int playerHpChunkXPos = playerHpChunkWidth;

        //Attack Cooldown Variables.
        Rectangle[] waterCdChunkReloadRect = new Rectangle[250];
        Rectangle[] earthCdChunkReloadRect = new Rectangle[250];
        const int cdChunkWidth = 1;
        const int barHeight = 30;
        Rectangle waterCdBackgroundRect;
        Rectangle earthCdBackgroundRect;
        Rectangle waterSymbolRect;
        Rectangle earthSymbolRect;
        Rectangle bloodSymbolRect;
        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        SolidBrush darkBlueBrush = new SolidBrush(Color.MidnightBlue);
        SolidBrush brownBrush = new SolidBrush(Color.Peru);
        SolidBrush darkBrownBrush = new SolidBrush(Color.SaddleBrown);
        int waterCdChunkXPos;
        int earthCdChunkXpos;
        bool waterCdBegin = false;
        bool earthCdBegin = false;
        bool waterAttackOnCooldown = false;
        bool earthAttackOnCooldown = false;
        bool[] waterCdChunkReloadRectVisible;
        bool[] earthCdChunkReloadRectVisible;

        //Di Lee Attack Variables. 
        Image brickImage = Image.FromFile(Application.StartupPath + @"\GameFiles\EntityImages\brick.png", true);
        const int numOfBricks = 3;
        const int brickWidth = 40;
        const int brickHeight = 15;
        const int brickSpeed = 10;
        Rectangle[] brickRect = new Rectangle[numOfBricks];
        bool[] brickSpawned;
        int[] brickDx;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setAllVariables(); //creates all variables.
            createAllTimers(); //create all timers.
            createHud();       //create player hud.

            //===============================================================
            //                Sets Screen Settings.
            this.Text = ("Korra Game Remastered");

            //set size and prevent resizing of screen.
            this.Size = new Size(1200, 700);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.MaximumSize;

            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2 - this.Height / 2);//centers the game on screen.
            this.DoubleBuffered = true;  //keeps program smooth.      
            this.BackgroundImage = Image.FromFile(Application.StartupPath + @"\GameFiles\HudFiles\background2.jpg", true); //creates background

            //==================================================================
            //                    Loads all Rectangles. 
            playerRect = new Rectangle(500, yLimit - playerHeight, playerHeight, playerWidth); //create player rectangle.
            floorRect = new Rectangle(0, yLimit, ClientSize.Width, ClientSize.Height - yLimit); //makes floor.
            earthRect = new Rectangle(0, 0, 120, 80);    //create earth attack rectangle.
            for (int i = 0; i < numOfFire; i++)
            {
                fireRect[i] = new Rectangle(-50, -50, 70, 30); //create fireball rectangles.
            }
            for (int i = 0; i < numOfDiLee; i++)
            {
                diLeeRect[i] = new Rectangle(-50, -50, diLeeWidth, diLeeHeight); //create all di lee rectangles.
            }
            for (int i = 0; i < numOfDarkSpirits; i++)
            {
                darkSpiritRect[i] = new Rectangle(ClientSize.Width, yLimit - darkSpiritRect[0].Height, darkSpiritWidth, darkSpiritHeight);
            }//create all dark spirit rectangles.


            //===================================================================
            //                          Starts Game.
            levelOneTimer.Start(); //begins level one. 
            levelChangerTimer.Start(); //starts level changer timer.

            //===================================================================
            //              creates keydown and keyup methods. 
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.KeyUp += new KeyEventHandler(Form1_KeyUp);
            this.Paint += new PaintEventHandler(Form1_Paint);

        }

        void Form1_Paint(object sender, PaintEventArgs e)
        {
            //===============================================================
            //                         Draws hud items. 
            // Create font and brush.
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Create point for upper-left corner of drawing.
            PointF drawPoint = new PointF(950.0F, 30.0F);

            // Draw string to screen.
            e.Graphics.DrawString("Elapsed Time: " + Convert.ToString(timeElapsed), drawFont, drawBrush, drawPoint);
            //===========================================================

            //health bar
            for (int i = 0; i != numOfPlayerHpChunks; i++) //for every health bar chunk.
            {
                e.Graphics.FillRectangle(redBrush, playerHpChunkRect[i]); //paint it red.
            }
            //draw all cooldown symbols.
            e.Graphics.DrawImage(bloodSymbolImage, bloodSymbolRect); //health bar symbol.
            e.Graphics.DrawImage(waterSymbolImage, waterSymbolRect); //water attack symbol.
            e.Graphics.DrawImage(earthSymbolImage, earthSymbolRect); //earth attack symbol.

            e.Graphics.FillRectangle(darkBlueBrush, waterCdBackgroundRect);  //paints water cooldown bar background.
            e.Graphics.FillRectangle(darkBrownBrush, earthCdBackgroundRect); //paint earth cooldown bar background.

            //Water attack cooldown bar.
            for (int i = 0; i != waterCdChunkReloadRect.Length; i++) //for every water cooldown bar.
            {
                if (waterCdChunkReloadRectVisible[i])
                {
                    e.Graphics.FillRectangle(blueBrush, waterCdChunkReloadRect[i]); //paint the bar blue, if it is visible.
                }
            }
            //Earth attack cooldown bar.
            for (int i = 0; i != earthCdChunkReloadRect.Length; i++) //for every earth cooldown bar.
            {
                if (earthCdChunkReloadRectVisible[i])
                {
                    e.Graphics.FillRectangle(brownBrush, earthCdChunkReloadRect[i]); //paint it brown, if it is visible.
                }
            }
            //===============================================================
            //                 Draws Dark Spirit image.
            for (int i = 0; i < numOfDarkSpirits; i++) //checks all dark spirits.
            {
                if (darkSpiritAlive[i])
                {
                    e.Graphics.DrawImage(darkSpiritImage, darkSpiritRect[i]); //paint dark spirit, if it is alive.
                }
            }
            //===============================================================
            //                        Draws player image.
            if (playerFacingRight)
            {
                e.Graphics.DrawImage(playerRightImage, playerRect); //display right image, if player facing right.
            }
            if (!playerFacingRight)
            {
                e.Graphics.DrawImage(playerLeftImage, playerRect); //display left image, if player facing left.
            }
            //===============================================================
            //                  Draw di Lee image. 
            for (int i = 0; i < numOfDiLee; i++) //checks all di lee agents.
            {
                if (diLeeAlive[i])
                {
                    if (diLeeFaceRight[i]) //if dilee is alive and facing right.
                    {
                        e.Graphics.DrawImage(diLeeRightImage, diLeeRect[i]); //draw dilee right image to di lee rectangle.
                    }
                    else if (!diLeeFaceRight[i]) //if dilee is alive and facing left.
                    {
                        e.Graphics.DrawImage(diLeeLeftImage, diLeeRect[i]); //draw dilee left image to di lee rectangle.
                    }
                }
            }
            //===============================================================
            //              Draws proper Earth Attack image.                                

            if (usingEarth) //if player is using earth attack.
            {
                if (earthFaceRight) //if rock is facing right.
                {
                    e.Graphics.DrawImage(earthRightImage, earthRect); //display right Earth attack image.
                }
                if (!earthFaceRight) //if rock is facing left.
                {
                    e.Graphics.DrawImage(earthLeftImage, earthRect);  //display left Earth attack image.
                }
            }
            //===============================================================
            //               Draws proper Fire Attack image. 

            if (usingFire) //if player is using fire attack.
            {
                for (int i = 0; i < numOfFire; i++) //check all fireballs.
                {
                    if (fireFaceRight[i] && fireSpawned[i])
                    //if player facing right, display right fire image to fire rectangle.
                    {
                        e.Graphics.DrawImage(fireRightImage, fireRect[i]);
                    }
                    if (!fireFaceRight[i])
                    //if player facing left, display left fire image to fire rectangle.
                    {
                        e.Graphics.DrawImage(fireLeftImage, fireRect[i]);
                    }
                }
            }
            //===============================================================
            //              Draws proper Water Attack image.
            if (usingWater)
            //if player is using water attack.
            {
                //draw all water attack images to corresponding rectangle.
                e.Graphics.DrawImage(waterRightImage, waterRightRect);
                e.Graphics.DrawImage(waterLeftImage, waterLeftRect);
                e.Graphics.DrawImage(iceRightImage, iceRightRect);
                e.Graphics.DrawImage(iceLeftImage, iceLeftRect);
            }
            //di lee bricks
            for (int i = 0; i < brickRect.Length; i++) //for all bricks.
            {
                if (brickSpawned[i])
                //if the brick is spawned, paint it to the rectangle.
                {
                    e.Graphics.DrawImage(brickImage, brickRect[i]);
                }
            }
            e.Graphics.DrawImage(floorImage, floorRect); //paints floorimage to floor rectangle.
            //Paints Rectangles and refreshes screen.
        } //good
        void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //===================================================================
            //                        Movement Keys (let go)
            if (e.KeyCode == Keys.Right)        //if right arrow key is let go. 
            {
                rightArrowDown = false;         //comfirm right arrow key is NOT down. 

                //Prevents player from stuttering, if left arrow key is pressed at same time. 
                if (!leftArrowDown)
                {
                    playerDx = 0;               //removes all player horizontal velocity.
                }
            }
            if (e.KeyCode == Keys.Left)         //if left arrow key is let go of.
            {
                leftArrowDown = false;          //comfirm left arrow key is NOT down.

                //Prevents player from stuttering, if right arrow key is pressed at same time. 
                if (!rightArrowDown)
                {
                    playerDx = 0;               //removes all player horizontal velocity.
                }
            }
            //=====================================================================
            //                       Ability Keys (let go)

            if (e.KeyCode == Keys.S)
            {
                fireLetGo = true; //only allow fire once.
            }
        } //good
        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //===========================================================
            //                       Movement Keys

            if (e.KeyCode == Keys.Right)    //if right arrow key pressed. 
            {
                playerDx = 7;               //player moves to right.
                rightArrowDown = true;      //comfirm right arrow key is down. 
                playerFacingRight = true;   //comfirm player is facing to the right. 
            }
            if (e.KeyCode == Keys.Left)     //if left arrow key pressed.
            {
                playerDx = -7;              //player moves to the left.
                leftArrowDown = true;       //comfirm left arrow key is down. 
                playerFacingRight = false;  //comfirm player is facing to the left. 
            }
            if (e.KeyCode == Keys.Space)    //if spacebar pressed. 
            {
                if (!playerJumping)
                {
                    playerJumping = true; //prevents player from jumping while already jumping.
                    gravity = 3;          //set player gravity value.
                    upper = 20;           //set player upper value.
                }
            }
            //=============================================================
            //                      Ability Keys
            if (e.KeyCode == Keys.A) //if A pressed. 
            {
                if (!usingEarth && !earthAttackOnCooldown)
                //if not using earth attack, and if it's not on cooldown.
                {
                    earthAttackCooldownTimer.Start(); //start the cooldown timer.
                    usingEarth = true;                //set using Earth status to true.
                    earthGravity = 3;                 //set earth graivty to 3.
                    earthUpper = 20;                  //set earth upper to 20.
                }
            }
            if (e.KeyCode == Keys.S) //if S pressed. 
            {
                usingFire = true;   //player is now using fire attack.
                fireOnce = true;    //only fire one fireball. 
            }
            if (e.KeyCode == Keys.D) //if D pressed. 
            {
                if (!usingWater && !waterAttackOnCooldown)
                //if not suing water attack, and if it's not on cooldown.
                {
                    usingWater = true;                //prevents from using water attack, if already using it.
                    waterAttackCooldownTimer.Start(); //begin waterattack cooldown timer.
                }
            }
        } //good

        void timeElapsedTimer_Tick(object sender, EventArgs e)
        {
            timeElapsed += 1; //keeps track of time game has been running. (seconds)
        }
        void setAllVariables()
        {
            //get length of entities.
            numOfDarkSpirits = darkSpiritRect.Length;
            numOfFire = fireRect.Length;
            numOfDiLee = diLeeRect.Length;
            //===============================================
            //          create brick variables.
            brickSpawned = new bool[numOfBricks];
            brickDx = new int[numOfBricks];
            for (int i = 0; i < brickRect.Length; i++)
            {
                brickSpawned[i] = false;
                brickDx[i] = 0;
            }

            //===============================================
            //          create cooldown variables.
            waterCdChunkReloadRectVisible = new bool[waterCdChunkReloadRect.Length];
            earthCdChunkReloadRectVisible = new bool[earthCdChunkReloadRect.Length];
            for (int i = 0; i != earthCdChunkReloadRect.Length; i++)
            {
                earthCdChunkReloadRectVisible[i] = true;
            }
            for (int i = 0; i != waterCdChunkReloadRect.Length; i++)
            {
                waterCdChunkReloadRectVisible[i] = true;
            }
            //===============================================
            //          create dark spirit variables.
            darkSpiritDx = new int[numOfDarkSpirits];
            darkSpiritHealth = new int[numOfDarkSpirits];
            enemyUnpauseCount = new int[numOfDarkSpirits];
            darkSpiritPaused = new bool[numOfDarkSpirits];
            darkSpiritSpawned = new bool[numOfDarkSpirits];
            darkSpiritAlive = new bool[numOfDarkSpirits];
            for (int i = 0; i < numOfDarkSpirits; i++)
            {
                darkSpiritDx[i] = 0;
                darkSpiritHealth[i] = 0;
                enemyUnpauseCount[i] = 0;
                darkSpiritPaused[i] = false;
                darkSpiritSpawned[i] = false;
                darkSpiritAlive[i] = false;
            }
            //===============================================
            //            create fire variables.
            fireFaceRight = new bool[numOfFire];
            fireSpawned = new bool[numOfFire];
            fireDX = new int[numOfFire];
            fireTimeCounter = new int[numOfFire];
            for (int i = 0; i < numOfFire; i++)
            {
                fireFaceRight[i] = false;
                fireSpawned[i] = false;
                fireDX[i] = 0;
                fireTimeCounter[i] = 0;
            }

            //===============================================
            //          create di lee variables.
            diLeeStock = diLeeRect.Length;
            diLeeAlive = new bool[numOfDiLee];
            diLeeSpawned = new bool[numOfDiLee];
            diLeeDx = new int[numOfDiLee];
            diLeeRandomCounter = new int[numOfDiLee];
            diLeeRandomMovement = new bool[numOfDiLee];
            diLeeRunAway = new bool[numOfDiLee];
            diLeeRunAwayCounter = new int[numOfDiLee];
            diLeeRightOfPlayer = new bool[numOfDiLee];
            diLeeFaceRight = new bool[numOfDiLee];
            diLeeHealth = new int[numOfDiLee];
            for (int i = 0; i < numOfDiLee; i++)
            {
                diLeeAlive[i] = false;
                diLeeSpawned[i] = false;
                diLeeDx[i] = 0;
                diLeeRandomMovement[i] = false;
                diLeeRandomCounter[i] = 0;
                diLeeRunAwayCounter[i] = 0;
                diLeeRunAway[i] = false;
                diLeeRightOfPlayer[i] = false;
                diLeeFaceRight[i] = true;
                diLeeHealth[i] = 4;
            }
            //===============================================
        } //creates all variables.
        void createAllTimers()
        {
            //set time Elapsed timer. 
            timeElapsedTimer = new Timer();
            timeElapsedTimer.Tick += new EventHandler(timeElapsedTimer_Tick);
            timeElapsedTimer.Interval = 1000;
            timeElapsedTimer.Start();

            //set dilee attack timer. 
            diLeeAttackTimer = new Timer();
            diLeeAttackTimer.Tick += new EventHandler(diLeeAttackTimer_Tick);
            diLeeAttackTimer.Interval = 10;

            //set earth attack cooldown timer.
            earthAttackCooldownTimer = new Timer();
            earthAttackCooldownTimer.Tick += new EventHandler(earthAttackCooldownTimer_Tick);
            earthAttackCooldownTimer.Interval = 20;

            //set water attack cooldown timer.
            waterAttackCooldownTimer = new Timer();
            waterAttackCooldownTimer.Tick += new EventHandler(waterAttackCooldownTimer_Tick);
            waterAttackCooldownTimer.Interval = 20;

            //set player health timer.
            playerHealthTimer = new Timer();
            playerHealthTimer.Tick += new EventHandler(playerHealthTimer_Tick);
            playerHealthTimer.Interval = 10;
            playerHealthTimer.Start();

            //set level changer timer.
            levelChangerTimer = new Timer();
            levelChangerTimer.Tick += new EventHandler(levelChangerTimer_Tick);
            levelChangerTimer.Interval = 10;
            levelChangerTimer.Start();

            //set level one timer.
            levelOneTimer = new Timer();
            levelOneTimer.Tick += new EventHandler(levelOneTimer_Tick);
            levelOneTimer.Interval = 10;

            //sets level two timer.
            levelTwoTimer = new Timer();
            levelTwoTimer.Tick += new EventHandler(levelTwoTimer_Tick);
            levelTwoTimer.Interval = 10;

            //sets player movement timer. 
            playerMovementTimer = new Timer();
            playerMovementTimer.Tick += new EventHandler(playerMovementTimer_Tick);
            playerMovementTimer.Interval = 10;
            playerMovementTimer.Start();

            //set player jump timer. 
            playerJumpTimer = new Timer();
            playerJumpTimer.Tick += new EventHandler(playerJumpTimer_Tick);
            playerJumpTimer.Interval = 10;
            playerJumpTimer.Start();

            //sets earth attack timer. 
            earthAttackTimer = new Timer();
            earthAttackTimer.Tick += new EventHandler(earthAttackTimer_Tick);
            earthAttackTimer.Interval = 10;
            earthAttackTimer.Start();

            //sets fire attack timer.
            fireAttackTimer = new Timer();
            fireAttackTimer.Tick += new EventHandler(fireAttackTimer_Tick);
            fireAttackTimer.Interval = 10;
            fireAttackTimer.Start();

            //set water attack timer.
            waterAttackTimer = new Timer();
            waterAttackTimer.Tick += new EventHandler(waterAttackTimer_Tick);
            waterAttackTimer.Interval = 10;
            waterAttackTimer.Start();

            //set dark spirit movement timer. 
            darkSpiritMovementTimer = new Timer();
            darkSpiritMovementTimer.Tick += new EventHandler(darkSpiritMovementTimer_Tick);
            darkSpiritMovementTimer.Interval = 10;
            darkSpiritMovementTimer.Start();

            //set dark spirit health timer. 
            darkSpiritHealthTimer = new Timer();
            darkSpiritHealthTimer.Tick += new EventHandler(darkSpiritHealthTimer_Tick);
            darkSpiritHealthTimer.Interval = 10;
            darkSpiritHealthTimer.Start();

            //set di lee movment timer. 
            diLeeMovementTimer = new Timer();
            diLeeMovementTimer.Tick += new EventHandler(diLeeMovementTimer_Tick);
            diLeeMovementTimer.Interval = 10;
            
            //set di lee health timer.
            diLeeHealthTimer = new Timer();
            diLeeHealthTimer.Tick += new EventHandler(diLeeHealthTimer_Tick);     
            diLeeHealthTimer.Interval = 10;
        }

        void earthAttackCooldownTimer_Tick(object sender, EventArgs e)
        {
            //==================================================================================
            //                          Begin Earth attack cooldown.

            if (!earthCdBegin) //prevents from restarting cooldown, if already going.
            {
                earthAttackOnCooldown = true;
                earthCdBegin = true;
                for (int i = 0; i != earthCdChunkReloadRect.Length; i++) //wips earth cooldown bar once.
                {
                    earthCdChunkReloadRectVisible[i] = false;
                }
            }
            for (int i = 0; i != earthCdChunkReloadRect.Length; i++) //load a bar chunk every tick.
            {
                if (!earthCdChunkReloadRectVisible[i]) //if bar chunk is not visible, turn it visible.
                {
                    earthCdChunkReloadRectVisible[i] = true;
                    break; //only one bar per tick.
                }
            }
            //====================================================================================
            //                             Resets cooldown variables.
            if (earthCdChunkReloadRectVisible[earthCdChunkReloadRect.Length - 1]) //if the cooldown bar is complete.
            {
                earthCdBegin = false;               //can start cooldown again.
                earthAttackOnCooldown = false;      //allows player to use earth attack again.
                earthAttackCooldownTimer.Stop();    //stops cooldown timer.
            }
            
        } //good
        void waterAttackCooldownTimer_Tick(object sender, EventArgs e)
        {
            //==================================================================================
            //                          Begin Water attack cooldown.

            if (!waterCdBegin) //prevents from restarting cooldown, if already going.
            {
                waterAttackOnCooldown = true;
                waterCdBegin = true;
                for (int i = 0; i != waterCdChunkReloadRect.Length; i++) //wipes water cool down bar ONCE.
                {
                    waterCdChunkReloadRectVisible[i] = false;
                }
            }
            for (int i = 0; i != waterCdChunkReloadRect.Length; i++) //load a bar chunk every tick.
            {
                if (!waterCdChunkReloadRectVisible[i]) //if barchunk isn't visible, make it visible.
                {
                    waterCdChunkReloadRectVisible[i] = true;
                    break; //only one bar chunk per tick.
                }
            }
            //====================================================================================
            //                            Resets cooldown.
            if (waterCdChunkReloadRectVisible[waterCdChunkReloadRect.Length - 1]) //if the cooldown bar is complete.
            {
                waterCdBegin = false;               //can start water cooldown again.
                waterAttackOnCooldown = false;      //allows player to use water attack again.
                waterAttackCooldownTimer.Stop();    //stops water cooldown timer.
            }
        } //good
        


        void playerHealthTimer_Tick(object sender, EventArgs e)
        {
            //===================================================
            //           If player hits a dark spirit.

            for (int i = 0; i != numOfDarkSpirits; i++) //checks all dark spirits.
            {
                if (playerRect.IntersectsWith(darkSpiritRect[i])) //if player hits dark spirit.
                {
                    if (numOfPlayerHpChunks > 0)
                    //as long as player has health, lose 1 health.
                    {
                        numOfPlayerHpChunks -= 1;
                    }
                }
            }
            //===================================================
            //          If player hits a brick. 
            for (int i = 0; i < brickRect.Length; i++) //checks all bricks.
            {
                if (brickSpawned[i]) //if brick has spawned.
                {
                    if (brickRect[i].IntersectsWith(playerRect)) //if player hits brick.
                    {
                        if (numOfPlayerHpChunks > 5)
                        //as long as player has more than 5 health, player loses 5 health.
                        {
                            numOfPlayerHpChunks -= 5;
                        }
                    }
                }
            }
            if (numOfPlayerHpChunks <= 0) //if player has 0 or less health. 
            {
                playerHealthTimer.Stop(); //stop player health timer.
                score = 0; //score is equal to 0;
                MessageBox.Show("You died. Game Over."); //display Gameover message.
                //GlobalVariablesClass.playerScore = score.ToString(); //send score to main menu to store.
                this.Close(); //close the game window.
            }
            //===================================================
        } //good

        void levelChangerTimer_Tick(object sender, EventArgs e)
        {
            if (!level1Start) //prevents restarting level 1 every tick.
            {
                levelOneTimer.Start(); //start level 1 timer.
                level1Start = true;
            }
            if (levelOneComplete) //after level one completion.
            {
                if (!level2Start) //prevents restarting level 2 timer.
                {
                    level2Start = true;
                    this.BackgroundImage = Image.FromFile(Application.StartupPath + @"\GameFiles\HudFiles\level2background.jpg", true);

                    //stops level 1 timer.
                    levelOneTimer.Stop();

                    //starts level 2 timers.
                    levelTwoTimer.Start();
                    diLeeMovementTimer.Start();
                    diLeeAttackTimer.Start();
                    diLeeHealthTimer.Start();

                    //reset level variables.
                    L2EnemyCount = 0;
                    enemyOnScreen = 0;
                    darkSpiritOnScreen = 0;

                    //reset dark spirit variables.
                    for (int i = 0; i < numOfDarkSpirits; i++)
                    {
                        darkSpiritAlive[i] = false;
                        darkSpiritSpawned[i] = false;
                        darkSpiritHealth[i] = 5;
                    }
                }

            }
        }

        void levelOneTimer_Tick(object sender, EventArgs e)
        {
            //==============================================================================
            //                          Spawns Dark Spirit.

            if (L1EnemyCount < 30) //Only spawn 30 dark spirits this level. 
            {
                if (enemyOnScreen < 4) //if less than 4 enemies on screen, then spawn more.
                {
                    for (int i = 0; i < numOfDarkSpirits; i++) //checks all dark spirits.
                    {
                        if (!darkSpiritSpawned[i]) //if dark spirit hasn't spawned, spawn it.
                        {
                            if (spawnEnemyRight)
                            //spawns the enemy on right side of screen
                            //and set dark spirit variables.
                            {
                                darkSpiritRect[i] = new Rectangle(ClientSize.Width, yLimit - darkSpiritRect[0].Height, darkSpiritWidth, darkSpiritHeight);
                                darkSpiritSpawned[i] = true;
                                spawnEnemyRight = false;
                                darkSpiritAlive[i] = true;
                                darkSpiritDx[i] = -4;
                                darkSpiritHealth[i] = 5;
                                enemyOnScreen += 1;
                                L1EnemyCount += 1;
                            }
                            else if (!spawnEnemyRight) //spawns enemy on left side of screen.
                            //spawns the enemy on left side of screen
                            //and set dark spirit variables.
                            {
                                darkSpiritRect[i] = new Rectangle(0 - darkSpiritRect[i].Width, yLimit - darkSpiritRect[0].Height, darkSpiritWidth, darkSpiritHeight);
                                darkSpiritSpawned[i] = true;
                                spawnEnemyRight = true;
                                darkSpiritAlive[i] = true;
                                darkSpiritDx[i] = -4;
                                darkSpiritHealth[i] = 5;
                                enemyOnScreen += 1;
                                L1EnemyCount += 1;
                            }
                        }
                        if (enemyOnScreen >= 4) //if 4 or more enemies on screen, stop spawning.
                        {
                            break;
                        }
                    }
                }
            }
            //==============================================================================
            //                     Checks for Level 1 Completion.

            for (int i = 0; i < numOfDarkSpirits; i++) //checks all dark spirits.
            {
                if (darkSpiritAlive[i]) //if any dark spirit is alive, they are not all dead.
                {
                    allDead = false;
                }
                if (!darkSpiritAlive[i]) //if any dark spirit is dead, they are not all alive.
                {
                    allAlive = false;
                }
            }
            if (!allAlive && !allDead) //if only some are dead, reset variables.
            {
                allAlive = true;
                allDead = true;
            }
            if (L1EnemyCount == 30 && allDead && !allAlive)
            //if 30 dark spirits have spawned, and they are all dead, 
            //then the level 1 is complete.
            {
                levelOneComplete = true;
            }
            //==============================================================================
        } //good
        void levelTwoTimer_Tick(object sender, EventArgs e)
        {
            if (L2EnemyCount < 50) //if enemy count isn't 50.
            {
                if (enemyOnScreen < 5) //if less than 5 enemies on screen.
                {
                    //==================================================================================
                    //                           Spawns Di Lee enemies.

                    if (diLeeOnScreen < 2) //if less than two di lee agents, spawn an agent.
                    {
                        for (int i = 0; i < numOfDiLee; i++) //checks all di lee agents.
                        {
                            if (!diLeeSpawned[i]) // if di lee hasn't spawned, spawn it.
                            {
                                if (spawnEnemyRight)
                                //spawns di lee to right side of screen
                                //and set all di lee variables.
                                {
                                    diLeeRect[i].X = ClientSize.Width;
                                    diLeeRect[i].Y = yLimit - diLeeRect[i].Height;
                                    diLeeFaceRight[i] = false;
                                    diLeeDx[i] = -6;
                                    diLeeSpawned[i] = true;
                                    diLeeStock -= 1;
                                    diLeeOnScreen += 1;
                                    enemyOnScreen += 1;
                                    L2EnemyCount += 1;
                                    diLeeAlive[i] = true;
                                    spawnEnemyRight = false;
                                    diLeeHealth[i] = 4;
                                }
                                else if (!spawnEnemyRight)
                                //spawns di lee to left side of screen
                                //and set all di lee variables.
                                {
                                    diLeeRect[i].X = 0;
                                    diLeeRect[i].Y = yLimit - diLeeRect[i].Height;
                                    diLeeFaceRight[i] = true;
                                    diLeeDx[i] = 6;
                                    diLeeSpawned[i] = true;
                                    diLeeStock -= 1;
                                    diLeeOnScreen += 1;
                                    enemyOnScreen += 1;
                                    L2EnemyCount += 1;
                                    diLeeAlive[i] = true;
                                    spawnEnemyRight = true;
                                    diLeeHealth[i] = 4;
                                }
                                if (diLeeOnScreen >= 2) //if more than two di lees on screen, stop spawning.
                                {
                                    break;
                                }
                            }
                        }
                        //==================================================================================
                        //                               Spawns dark spirits.
                    }
                    if (darkSpiritOnScreen < 3) //if less than 3 dark spirits on screen.
                    {
                        for (int i = 0; i < numOfDarkSpirits; i++) //checks all dark spirits.
                        {
                            if (!darkSpiritSpawned[i]) //if dark spirit hasn't spawned, spawn it.
                            {
                                if (spawnEnemyRight)
                                //spawn dark spirit to right side of screen
                                //and set all dark spirit variables.
                                {
                                    darkSpiritRect[i].X = ClientSize.Width;
                                    darkSpiritRect[i].Y = yLimit - darkSpiritHeight;
                                    darkSpiritSpawned[i] = true;
                                    darkSpiritAlive[i] = true;
                                    spawnEnemyRight = false;
                                    darkSpiritDx[i] = -4;
                                    darkSpiritHealth[i] = 5;
                                    darkSpiritOnScreen += 1;
                                    enemyOnScreen += 1;
                                    L2EnemyCount += 1;
                                }
                                else if (!spawnEnemyRight)
                                //spawn dark spirit to left side of screen
                                //and set all dark spirit variables.
                                {
                                    darkSpiritRect[i].X = 0 - darkSpiritWidth;
                                    darkSpiritRect[i].Y = yLimit - darkSpiritHeight;
                                    darkSpiritSpawned[i] = true;
                                    darkSpiritAlive[i] = true;
                                    spawnEnemyRight = true;
                                    darkSpiritDx[i] = 4;
                                    darkSpiritHealth[i] = 5;
                                    darkSpiritOnScreen += 1;
                                    L2EnemyCount += 1;
                                    enemyOnScreen += 1;
                                }
                                if (darkSpiritOnScreen >= 3) //if 3 or more dark spirits on screen, stop spawning.
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            //===================================================================================
            //                      Checks Level 2 Completion.

            for (int i = 0; i < numOfDarkSpirits; i++) //checks all dark spirits.
            {
                if (darkSpiritAlive[i]) //if any dark spirits are alive, it means they are not all dead.
                {
                    lvl2AllDead = false;
                }
                if (!darkSpiritAlive[i])
                {
                    lvl2AllAlive = false; //if any dark spirits are dead, it means they are not all alive.
                }
            }
            for (int i = 0; i < diLeeRect.Length; i++) //checks all di lee.
            {
                if (diLeeAlive[i])
                {
                    lvl2AllDead = false; //if any di lee are alive, it means they are not all dead.
                }
                if (!diLeeAlive[i])
                {
                    lvl2AllAlive = false; //if any di lee are dead, it means they are not all alive.
                }
            }

            if (!lvl2AllAlive && !lvl2AllDead) //if only some of the enemies are dead, reset both values.
            {
                lvl2AllAlive = true;
                lvl2AllDead = true;
            }
            if (L2EnemyCount == 50 && lvl2AllDead && !lvl2AllAlive) //if 50 enemies have spawned, and they are all dead. 
            {
                levelTwoTimer.Stop(); //stop level two timer. 
                score = 1000 - timeElapsed + numOfPlayerHpChunks; //calculate player score. 
                MessageBox.Show("Congrats, you finished in " + timeElapsed + " seconds with " + numOfPlayerHpChunks + " health remaining. Your score is " + score + "."); //display score message box.
                //GlobalVariablesClass.playerScore = score.ToString(); //send score to main menu to store.
                this.Close(); //close the game window.
            }
            //===================================================================================
        } //good


        void playerMovementTimer_Tick(object sender, EventArgs e)
        {
            this.Invalidate(); //refreshes screen every tick.
            playerRect.X += playerDx; //moves player in horizontal direction.

            //======================================================
            //      Prevents Player from moving off the screen.

            if (playerRect.Right >= ClientSize.Width) //if player hits right wall, keep him there.
            {
                playerRect.X = ClientSize.Width - playerRect.Width;
            }
            if (playerRect.Left <= 0) //if player hits left wall, keep him there.
            {
                playerRect.X = 0;
            }
            //======================================================
        } //good
        void playerJumpTimer_Tick(object sender, EventArgs e)
        {
            if (playerJumping) //if the player is jumping.
            {
                playerRect.Y -= upper;   //move player up by upper value.
                playerRect.Y += gravity; //move player down by gravity value.
                upper -= 1;  //upper value decreases every tick, to simulate loss in upward velocity over time.

                if (playerRect.Bottom >= yLimit) //if player lands on ground.
                {
                    playerJumping = false; //stop jumping.
                    playerRect.Y = yLimit - playerRect.Height; //set bottom of player to ylimit.

                    //reset jumping variables.
                    gravity = 0;
                    upper = 0;
                }
            }
        } //good


        void earthAttackTimer_Tick(object sender, EventArgs e)
        {
            if (usingEarth)
            //if the player is using earth attack.
            {
                usingEarth = true;
                //=====================================================
                //           Decides where to spawn Earth Rock.
                if (playerFacingRight && !earthSpawned)
                //if player facing right, and rock hasn't spawned.
                {
                    earthFaceRight = true;                              //rock is facing right.
                    earthRect.X = playerRect.X + playerRect.Width;      //rock spawns right of player.
                    earthRect.Y = playerRect.Y + playerRect.Height;     //rock spawns underground.
                    earthDx = 25;                                       //rock has right-way velocity.
                    earthFriction = -1;                                 //rock has left-way friction.
                    earthSpawned = true;                                //set rock status to spawned.
                }
                else if (!playerFacingRight && !earthSpawned)
                //if player facing left, and rock hasn't spawned.
                {
                    earthFaceRight = false;                             //rock is facing left.
                    earthRect.X = playerRect.X - earthRect.Width;       //rock spawns left of player.
                    earthRect.Y = playerRect.Y + playerRect.Height;     //rock spawns underground.
                    earthDx = -25;                                      //rock has left-way velocity.
                    earthFriction = 1;                                  //rock has right-way friction.
                    earthSpawned = true;                                //set rock status to spawned.
                }
                //=====================================================
                //           Earth Spawns with Ground Animation.

                earthUpper -= 1; //rock loses gradually loses upward velocity.
                earthRect.Y = earthRect.Y - earthUpper + earthGravity; //rock's vertical movement, every tick.
                if (earthGravity > earthUpper) //when the rock is falling.
                    if (earthRect.Bottom >= yLimit)
                    //if rock hits ylimit, stop all gravity variables.
                    {
                        earthUpper = 0;
                        earthGravity = 0;
                        earthRect.Y = yLimit - earthRect.Height; //keep rock at ylimit.
                        earthGrounded = true; //set earthgrounded status to true.
                    }
            }
            //====================================================
            //               Earth Punch Animation
            if (earthGrounded == true)
            //if the earth is grounded.
            {
                earthPunch = true;
                //set earthPunch status to true.
                if (earthPunch == true)
                {
                    if (earthFaceRight && earthDx > 0)
                    //if rock is moving right, horizontal velocity decreases.
                    {
                        earthDx -= 1;
                    }
                    if (!earthFaceRight && earthDx < 0)
                    //if rock is moving left, horizontal velocity decreases.
                    {
                        earthDx += 1;
                    }
                    if (earthDx == 0)
                    //if horizontal velocity is 0
                    {
                        //reset all rock punch animation variables.
                        earthPunch = false;
                        earthDx = 0;
                        earthFriction = 0;

                    }
                    earthRect.X = earthRect.X + earthDx + earthFriction; //moves earth rock, in horizontal velocity.
                    //====================================================
                    //               Earth Despawn Counter

                    earthDespawnCounter += 1; //despawn counter adds 1 every tick.
                    if (earthDespawnCounter == 250)
                    //after 250 ticks, reset all earth attack variables.
                    {
                        usingEarth = false;
                        earthSpawned = false;
                        earthGrounded = false;
                        earthDespawnCounter = 0;

                        //hides earthrock outside of screen.
                        earthRect.X = 0;
                        earthRect.Y = 0;
                    }
                }
            }
        } //good
        void waterAttackTimer_Tick(object sender, EventArgs e)
        {
            if (usingWater) //prevents player from using water attack while already using water attack.
            {
                //==================================================================================
                //                              Spawning Water and Ice
                if (!waterSpawned && waterSequence != 2)
                //If water hasn't spawned, and gone up twice.
                {
                    waterRightRect = new Rectangle(playerRect.Right - 50, 550, 400, 200); //create right wave, right of player.
                    waterLeftRect = new Rectangle(playerRect.Left - waterLeftRect.Width + 50, 550, 400, 200); //create left wave, left of player.

                    waterUpper = 20;     //both waves have upper force.
                    waterGravity = 3;    //both waves have gravity.
                    waterRightDx = 3;    //right wave has right velocity.
                    waterLeftDx = -3;    //left wave  has left velocity.
                    waterSpawned = true; //water has spawned.
                    waterSequence += 1;  //water has gone up +1 times.
                }
                iceSpawnCounter += 1; //begin ice spawn counter.
                if (iceSpawnCounter == 55)
                //when ice spawn counter hits 55, spawn ice.
                {
                    if (!iceSpawned)
                    //if ice hasn't spawn, spawn it.
                    {
                        iceRightRect = new Rectangle(playerRect.Right + 300, yLimit + 50, 200, 150);//spawn right ice, right of player.
                        iceLeftRect = new Rectangle(playerRect.Left - iceLeftRect.Width - 300, yLimit + 50, 200, 150); //spawn left ice, left of player.
                        iceSpawned = true; //ice has now spawned.
                        iceDy = -27;    //set ice to shoot up very quickly.
                    }
                }
                //=====================================================================================
                //                               Water and Ice Movement.

                waterUpper -= 1; //waves gradually falls back down.

                //right waves follows right wave movement.
                waterRightRect.Y = waterRightRect.Y - waterUpper + waterGravity; //in vertical plane.
                waterRightRect.X += waterRightDx;                                //in horizontal plane.

                //left wave follows left wave movement.
                waterLeftRect.Y = waterLeftRect.Y - waterUpper + waterGravity;   //in vertical plane.
                waterLeftRect.X += waterLeftDx;                                  //in horizontal plane.

                if (iceRightRect.Y < ClientSize.Height - iceRightRect.Height)
                //stop the ice once it reaches ylimit.
                {
                    iceDy = 0;
                }

                //ice follows ice movement.
                iceRightRect.Y += iceDy;
                iceLeftRect.Y += iceDy;

                //=====================================================================================
                //                              Ice Despawn Counter + Reset.

                iceDespawnCounter += 1; //ice despawn counter, counts.
                if (waterLeftRect.Top > ClientSize.Height)
                //if water is below screen, set spawn status to false.
                {
                    waterSpawned = false;
                }
                if (iceDespawnCounter == 130)
                //once despawn counter hits 130.
                {
                    //reset all water attack variables for next attack.
                    usingWater = false;
                    waterSpawned = false;
                    iceSpawned = false;
                    iceSpawnCounter = 0;
                    iceDespawnCounter = 0;
                    waterSequence = 0;

                    //hides wave and ice rectangles, outside of screen.
                    iceRightRect.X = 0;
                    iceRightRect.Y = -500;
                    iceLeftRect.X = 0;
                    iceLeftRect.Y = -500;
                    //=====================================================================================
                }
            }   
        } //good
        void fireAttackTimer_Tick(object sender, EventArgs e)
        {
            //=================================================================================
            //                           Shoots 1 out of the 5 Fireballs.
            if (fireOnce && fireLetGo)
            //only allow to shoot fire once, with one click.
            {
                for (int i = 0; i < numOfFire; i++) //checks all fireballs.
                {
                    if (playerFacingRight && !fireSpawned[i])
                    //if player facing right, shoot fireball to the right.
                    {
                        //spawn fireball next to player.
                        fireRect[i].X = playerRect.Right;
                        fireRect[i].Y = playerRect.Y;

                        //set fireball properties.
                        fireSpawned[i] = true;
                        fireFaceRight[i] = true;
                        fireDX[i] = 20;
                        fireOnce = false;
                        break; //only shoot one fireball.
                    }
                    else if (!playerFacingRight && !fireSpawned[i])
                    //if player facing left, shoot fireball to the left.
                    {
                        //spawn fireball.
                        fireRect[i] = new Rectangle(playerRect.Left - fireRect[i].Width, playerRect.Y + 20, 70, 30);

                        //set fireball properties.
                        fireSpawned[i] = true;
                        fireFaceRight[i] = false;
                        fireDX[i] = -20;
                        fireOnce = false;
                        break; //only shoot one fireball.
                    }
                }
            }
            //======================================================================
            //                Removes fireball if doesn't hit anything.

            for (int i = 0; i < numOfFire; i++) //checks all fireballs.
            {
                if (fireSpawned[i])
                //if fireball has been spawned, begin a despawn counter.
                {
                    fireTimeCounter[i] += 1;
                }
                if (fireTimeCounter[i] == 60)
                //if fireball despawn counter hits 60.
                {
                    fireSpawned[i] = false; //set spawned status to false.
                    fireTimeCounter[i] = 0; //reset despawn coutner.
                }
            }
            //=======================================================================
            //               When a fireball hits a dark spirit.
            for (int i = 0; i < numOfDarkSpirits; i++)  //checks every dark spirit.
            {
                for (int j = 0; j < numOfFire; j++)   //for every fireball.
                {
                    if (fireRect[j].IntersectsWith(darkSpiritRect[i]) && darkSpiritAlive[i])
                    //if the fireball intersects with a dark spirit.
                    {
                        fireSpawned[j] = false; //set spawned status to false.

                        //hides fireball off screen.
                        fireRect[j].X = -500;
                        fireRect[j].Y = -500;
                    }
                }
            }
            //========================================================================
            //              When a fireball hits a di lee agent.

            for (int i = 0; i < diLeeRect.Length; i++) //checks every di lee agent.
            {
                for (int j = 0; j < fireRect.Length; j++)    //for every fireball.
                {
                    if (fireRect[j].IntersectsWith(diLeeRect[i]) && diLeeAlive[i])
                    //if the fireballe intersects with a di lee.
                    {
                        fireSpawned[j] = false; //set spawned status to false.

                        //hides fireball off screen,
                        fireRect[j].X = -500;
                        fireRect[j].Y = -500;
                    }
                }
            }
            //===============================================
            //            Fireball Movement
            for (int i = 0; i < fireRect.Length; i++) //for every fireball.
            {
                fireRect[i].X += fireDX[i]; //give it it's own horizontal velocity.
            }
        } //good


        void darkSpiritMovementTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 30; i++) //loops through all dark spirits.
            {
                //========================================================================
                //                    Makes dark spirit chase player.

                if (darkSpiritAlive[i]) //if the dark spirit is alive.
                {
                    if (darkSpiritRect[i].Right <= playerRect.Left && !darkSpiritPaused[i])
                    //if dark spirit is left of player, make it go right.
                    {
                        darkSpiritDx[i] = 4;
                    }
                    if (darkSpiritRect[i].Left >= playerRect.Right && !darkSpiritPaused[i])
                    //if dark spirit is right of player, make it go left.
                    {
                        darkSpiritDx[i] = -4;
                    }
                    //======================================================================
                    //                      Random dark spirit movement.
                    darkSpiritNum = randomNumber.Next(1500); //gets a random number (1 - 1500)

                    if (!darkSpiritPaused[i])
                    //if dark spirit is not doing "random sequence"
                    //dark spirit has a chance of doing it.
                    {
                        if (darkSpiritNum < 10)
                        {
                            darkSpiritPaused[i] = true; //dark spirit is now under "random sequence"
                            darkSpiritDx[i] = darkSpiritDx[i] * -1; //dark spirit will randomly turn around.
                        }
                    }
                    //======================================================================
                    //Stops dark spirit "random sequence"

                    if (darkSpiritPaused[i]) //if dark spirit is under "random sequence."
                    {
                        if (enemyUnpauseCount[i] < 100) //counts 100 ticks.
                        {
                            enemyUnpauseCount[i] += 1;
                        }
                        if (enemyUnpauseCount[i] == 100) //after 100 ticks, dark spirit is no longer doing "random sequence."
                        {
                            darkSpiritPaused[i] = false; //reset "random sequence" status.
                            enemyUnpauseCount[i] = 0;    //reset "random sequence" stopper counter.
                        }
                    }
                    //===========================================================================
                    //              Prevents dark spirit from walking off edge.
                    if (darkSpiritRect[i].Right >= ClientSize.Width)
                    //if dark spirit hits right wall, make it go left.
                    {
                        darkSpiritDx[i] = -4;
                    }
                    if (darkSpiritRect[i].Left <= 0)
                    //if dakr spirit hits left wall, make it go right.
                    {
                        darkSpiritDx[i] = 4;
                    }

                    //============================================================================
                    //                     If dark spirit hits earth.
                    if (earthFaceRight && earthSpawned)
                    //if earth attack hits dark spirit from the left, 
                    //dark spirit will move to the right.
                    {
                        if (darkSpiritRect[i].Left <= earthRect.Right && darkSpiritRect[i].Right > earthRect.Right + 50)
                        {
                            darkSpiritRect[i].X = earthRect.X + earthRect.Width; //dark spirit X coord, follows right side of earth rock.
                        }
                    }
                    if (!earthFaceRight && earthSpawned)
                    //if earth attack hits dark spirit from the right, 
                    //dark spirit will move to the left.
                    {
                        if (darkSpiritRect[i].Right >= earthRect.Left && darkSpiritRect[i].Left < earthRect.Left)
                        {
                            darkSpiritRect[i].X = earthRect.X - darkSpiritRect[i].Width; //dark spirit X coord, follows left side of earth rock.
                        }
                    }

                    //============================================================================
                    //                    If dark spirit hits Water and Ice. 

                    if (usingWater) //if player is using water attack, 
                    {
                        if (darkSpiritRect[i].Bottom >= waterRightRect.Top) //if dark spirit is above the wave.
                        {
                            if (darkSpiritRect[i].Right < waterRightRect.Right && darkSpiritRect[i].Left > waterRightRect.Left) //if dark spirit is between width of right wave.
                            {
                                darkSpiritRect[i].Y = waterRightRect.Y - darkSpiritRect[i].Height; //dark spirit Y coord will follow height of wave.

                                darkSpiritRect[i].X += 10; //dark spirit will be washed to right side.
                                darkSpiritDx[i] = 0;       //standard dark spirit movement is temporarily canceled.

                            }
                            if (darkSpiritRect[i].Right < waterLeftRect.Right && darkSpiritRect[i].Left > waterLeftRect.Left) //for the left wave.
                            {
                                darkSpiritRect[i].Y = waterRightRect.Y - darkSpiritRect[i].Height; //dark spirit Y coord will follow height of wave.

                                darkSpiritRect[i].X -= 10;  //dark spirit will be washed to left side.
                                darkSpiritDx[i] = 0;        //standard dark spirit movement is temporarily canceled.
                            }
                        }
                    }
                    //====================================================================================
                    //                             If dark spirit lands on Ice
                    if (iceSpawned)
                    {
                        if (darkSpiritRect[i].Right <= iceLeftRect.Right && darkSpiritRect[i].Left > iceLeftRect.Left && darkSpiritRect[i].Bottom != yLimit)
                        //if enemy is inbetween  left ice, and not touching ground.
                        {
                            darkSpiritDx[i] = 0; //temporarily stop standard dark spirit movement. 
                            darkSpiritRect[i].Y = yLimit - darkSpiritRect[i].Height - iceLeftRect.Height / 2; //dark spirit Y coord is halfway up ice.
                        }

                        if (darkSpiritRect[i].Right <= iceRightRect.Right && darkSpiritRect[i].Left > iceRightRect.Left)
                        //if enemy is inbetween right ice, and not touching ground.
                        {
                            darkSpiritDx[i] = 0; //temporarily stop standard dark spirit movement. 
                            darkSpiritRect[i].Y = yLimit - darkSpiritRect[i].Height - iceRightRect.Height / 2;  //dark spirit Y coord is halfway up ice.
                        }
                    }
                    //=============================================================================================
                    //                            Dark Spirit Physics. 

                    if (darkSpiritRect[i].Bottom < yLimit)
                    //if dark spirit is in the air, make him fall w/ gravity.
                    {
                        darkSpiritRect[i].Y += darkSpiritGravity;
                    }
                    if (darkSpiritRect[i].Bottom >= yLimit)
                    //if adkr spirit is at floor level, keep him there.
                    {
                        darkSpiritRect[i].Y = yLimit - darkSpiritRect[i].Height;
                    }

                    if (darkSpiritRect[i].IntersectsWith(playerRect))
                    //if dark spirit hits player, make it turn around.
                    {
                        darkSpiritDx[i] *= -1;      //turn dark spirit around.
                        darkSpiritPaused[i] = true; //stop normal tracking for a momment.
                    }

                    darkSpiritRect[i].X += darkSpiritDx[i]; //dark spirit moves according to it's dx velocity.
                    //=============================================================================================
                    //
                }
                else if (!darkSpiritAlive[i])
                //if dark spirit isn't alive, don't let it move.
                {
                    darkSpiritDx[i] = 0;
                }
            }
        } //goopd
        void darkSpiritHealthTimer_Tick(object sender, EventArgs e)
        {
            //=============================================================
            //              If Dark Spirit is hit by fireball.

            for (int i = 0; i < numOfDarkSpirits; i++) //checks all dark spirits
            {
                if (darkSpiritAlive[i]) //runs through alive dark spirits.
                {
                    for (int j = 0; j < numOfFire; j++) //runs through all fireballs.
                    {
                        if (fireRect[j].IntersectsWith(darkSpiritRect[i]))
                        //if fireball hits dark spirit, dark spirit loses 1 health.
                        {
                            darkSpiritHealth[i] -= 1;
                        }
                    }
                    if (darkSpiritHealth[i] <= 0)
                    //if dark spirit health is 0 or less. 
                    {
                        darkSpiritAlive[i] = false; //darkspirit is no longer alive.
                        enemyOnScreen -= 1;         //enemies on screen decreases by 1.
                        darkSpiritOnScreen -= 1;    //dark spirits on screen decreases by 1.

                        //teleports darkspirit rectangle off screen.
                        darkSpiritRect[i].X = -200;
                        darkSpiritRect[i].Y = -200;
                    }
                }
            }
            //=============================================================
        } //good

        //default state. 


        void diLeeMovementTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < numOfDiLee; i++) //checks all di lee agents.
            {
                if (diLeeAlive[i]) //only move di lee, if they are alive.
                {
                    //============================================================
                    //            Determines which way di Lee is facing.
                    if (diLeeRect[i].Right < playerRect.Left)
                    //if di lee is left of player, make him face right.
                    {
                        diLeeFaceRight[i] = true;
                    }
                    else if (diLeeRect[i].Left > playerRect.Right)
                    //if di lee is right of player, make him face left.
                    {
                        diLeeFaceRight[i] = false;
                    }
                    //=============================================================
                    //              Teleport Dilee left and right.
                    if (diLeeRect[i].Right > ClientSize.Width)
                    //if di lee hits right wall, tp to left.
                    {
                        diLeeRect[i].X = 0;
                    }
                    if (diLeeRect[i].Left < 0)
                    //if di lee hits left wall, tp to right.
                    {
                        diLeeRect[i].X = ClientSize.Width - diLeeWidth;
                    }
                    //==============================================================
                    //         Checks distance between player and di Lee.
                    if (diLeeRect[i].Right > playerRect.Left - 200 && diLeeRect[i].Right < playerRect.Left)
                    //if dilee left of player and too close.
                    {
                        diLeeRunAway[i] = true;          //begin "run away" sequence.
                        diLeeRightOfPlayer[i] = false;   //di lee is left of player.
                        // diLeeFaceRight[i] = true;        //make di lee face other way.
                    }
                    if (diLeeRect[i].Left < playerRect.Right + 200 && diLeeRect[i].Right > playerRect.Right)
                    //if dilee right of player and too close.
                    {
                        diLeeRunAway[i] = true;         //begin "run away sequence.
                        diLeeRightOfPlayer[i] = true;   //di lee is right of player.
                        //  diLeeFaceRight[i] = false;      //make di lee face other way.
                    }
                    //==============================================================
                    //           Move di Lee, if player is too close.          
                    if (diLeeRunAway[i])
                    //if dilee is under "run away" sequence.
                    {
                        if (diLeeRightOfPlayer[i])
                        //if dilee is right of player, make him face right and run right. 
                        {
                            diLeeDx[i] = 5;
                            diLeeFaceRight[i] = true;
                        }
                        if (!diLeeRightOfPlayer[i])
                        //if dilee is left of player, make him face left and run left.
                        {
                            diLeeDx[i] = -5;
                            diLeeFaceRight[i] = false;
                        }

                        diLeeRunAwayCounter[i] += 1; //count runaway counter. 

                        if (diLeeRunAwayCounter[i] == 50)
                        //if runaway counter hits 50, stop "runaway" sequence.
                        {
                            diLeeDx[i] = 0;              //stop dilee movement.
                            diLeeRunAwayCounter[i] = 0;  //reset "runaway" counter.
                            diLeeRunAway[i] = false;     //reset "runaway"  status.
                        }
                    }
                    //======================================================================
                    //                          Random Di Lee Movement.      

                    if (!diLeeRandomMovement[i])  // if dilee is not doing random sequence.
                    {
                        diLeeRandomNumber = randomDiLeeMaker.Next(100);
                        if (diLeeRandomNumber > 1 && diLeeRandomNumber < 5)
                        //dilee has chance of doing random sequence.
                        {
                            diLeeRandomMovement[i] = true; //set dilee to "random movement" status.
                            if (diLeeRect[i].X > playerRect.X)
                            //if di lee on right side of player, move to the left.
                            {
                                diLeeDx[i] = -diLeeSpeed;
                            }
                            else if (diLeeRect[i].X < playerRect.X)
                            //if di lee on left side of player, move to the right.
                            {
                                diLeeDx[i] = diLeeSpeed;
                            }
                        }
                    }
                    if (diLeeRandomMovement[i])
                    //if dilee is under "random movement" sequence, begin reset counter.
                    {
                        diLeeRandomCounter[i] += 1; //counts 1 every tick.
                    }
                    if (diLeeRandomCounter[i] == 30)
                    //if counter reaches 30, di lee is no longer under "random movement" sequence.
                    {
                        diLeeRandomCounter[i] = 0;      //reset counter.
                        diLeeRandomMovement[i] = false; //reset "random movement" status.
                    }
                    //==========================================================================
                    diLeeRect[i].X += diLeeDx[i]; //moves Dilee in horizontal direction.
                }
            }
                //########################################################################################
                //                      REMASTERED PART 2 - dilee health timer
                for (int i = 0; i < diLeeRect.Length; i++) //runs through all di lee agents.
                {
                    if (diLeeSpawned[i]) //if di lee is spawned.
                    {
                        for (int j = 0; j < fireRect.Length; j++) //runs through all fireballs.
                        {
                            if (diLeeRect[i].IntersectsWith(fireRect[j]))
                            //if di lee intersects with fireball. 
                            {
                                diLeeHealth[i] -= 1; //dilee loses 1 health.
                            }
                        }
                    }
                    if (diLeeHealth[i] <= 0)
                    //if dilee has 0 or less health. 
                    {
                        diLeeSpawned[i] = false; //dilee is no longer spawned.
                        diLeeAlive[i] = false;  //dilee is no longer alive.
                        enemyOnScreen -= 1;     //enemy on screen decreases by 1.
                        diLeeOnScreen -= 1;     //di lee on screen decreases by 1.

                        //move diLee rectangle off screen.
                        diLeeRect[i].X = -100;
                        diLeeRect[i].Y = -100;
                    }
                }
                //########################################################################################
                //                       REMASTERED PART 3 - dilee attack timer
                //=========================================================================
                //                  Spawns di lee's attack brick.
                for (int i = 0; i < diLeeRect.Length; i++)      //checks all di lees.
                {
                    if (diLeeSpawned[i] && diLeeDx[i] == 0)     //if he is spawned, and not moving.
                    {
                        diLeeRandomAttackNum = randomNumber.Next(100);
                        if (diLeeRandomAttackNum < 2)           // 1 in 100 chance that di lee will attack, per tick.
                        {
                            for (int j = 0; j < brickRect.Length; j++) //checks all bricks.
                            {
                                if (!brickSpawned[j])   //if brick hasn't spawned, spawn it.
                                {
                                    if (diLeeFaceRight[i]) //if dilee facing right, shoot rock right.
                                    {
                                        brickRect[j] = new Rectangle(diLeeRect[i].X + diLeeRect[i].Width, diLeeRect[i].Y + 50, brickWidth, brickHeight); //creates brick rectangle.
                                        brickSpawned[j] = true;
                                        brickDx[j] = brickSpeed;
                                        break; //only shoot one brick per tick.
                                    }
                                    else if (!diLeeFaceRight[i]) //if dilee facing left, shoot rock left.
                                    {
                                        brickRect[j] = new Rectangle(diLeeRect[i].X - brickWidth, diLeeRect[i].Y + 50, brickWidth, brickHeight); //creates brick rectangle.
                                        brickSpawned[j] = true;
                                        brickDx[j] = -brickSpeed;
                                        break; //only shoot one brick per tick.
                                    }
                                }
                            }
                        }
                    }
                }

                //=========================================================================
                //                    Despawns Brick, if it goes off screen.

                for (int i = 0; i < brickRect.Length; i++) //checks all bricks.
                {
                    if (brickSpawned[i]) //check if brick has already been spawned.
                    {
                        if (brickRect[i].IntersectsWith(playerRect)) //if brick hits player, despawn brick.
                        {
                            brickSpawned[i] = false;
                            brickRect[i].X = -100;
                            brickRect[i].Y = -100;

                        }
                        if (brickRect[i].Left >= ClientSize.Width || brickRect[i].Right <= 0) //if brick hits sides of screen, despawn brick.
                        {
                            brickSpawned[i] = false;
                            brickRect[i].X = -100;
                            brickRect[i].Y = -100;
                        }
                    }
                    //=========================================================================
                    brickRect[i].X += brickDx[i]; //brick moves every tick.
                }
                //#####################################################################################
                 
            }//good
        void diLeeHealthTimer_Tick(object sender, EventArgs e)
        { 
        } //good
        void diLeeAttackTimer_Tick(object sender, EventArgs e)
        {
        }  //good



        void createHud()
        {
            //Cooldown bar location is relative to it's symbol's location.
            //======================================================================
            //                          Hud Symbols
            bloodSymbolRect = new Rectangle(25, 20, 60, 45);
            waterSymbolRect = new Rectangle(30, 75, 40, 40);
            earthSymbolRect = new Rectangle(30, 125, 40, 40);

            //======================================================================
            //                     Earth Attack Cooldown.
            earthCdBackgroundRect = new Rectangle(earthSymbolRect.X + earthSymbolRect.Width, earthSymbolRect.Y + 5, waterCdChunkReloadRect.Length, barHeight); //earth cooldown background bar.

            for (int i = 0; i != waterCdChunkReloadRect.Length; i++)
            {
                earthCdChunkReloadRect[i] = new Rectangle(earthCdBackgroundRect.X + earthCdChunkXpos, earthCdBackgroundRect.Y, cdChunkWidth, barHeight);
                earthCdChunkXpos += cdChunkWidth;
            }

            //======================================================================
            //                         Health Bar 
            for (int i = 0; i != numOfPlayerHpChunks; i++)
            {
                playerHpChunkRect[i] = new Rectangle(playerHpChunkXPos + 15 + waterSymbolRect.Width, 30, playerHpChunkWidth, playerHpChunkHeight);
                playerHpChunkXPos += playerHpChunkWidth;
            }

            //======================================================================
            //                       Water Attack Cooldown.
            waterCdBackgroundRect = new Rectangle(waterSymbolRect.X + waterSymbolRect.Width, waterSymbolRect.Y + 5, waterCdChunkReloadRect.Length, barHeight); // dark blue bar.

            for (int i = 0; i != waterCdChunkReloadRect.Length; i++) //light blue reload bar.
            {
                waterCdChunkReloadRect[i] = new Rectangle(waterCdChunkXPos + waterSymbolRect.X + waterSymbolRect.Width - 1, waterCdBackgroundRect.Y, cdChunkWidth, barHeight);
                waterCdChunkXPos += cdChunkWidth;
            }
        }       //creates the 'heads up display' for  the player.
    }

}
