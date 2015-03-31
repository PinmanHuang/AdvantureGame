/*
 * Visual C# Game Programming for Teens
 * Copyright 2011 by Jonathan S. Harbour
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using RPG;
using System.IO;

namespace Sprite_Demo
{
    public partial class Form1 : Form
    {
        private bool p_gameOver = false;
        private int p_startTime = 0;
        private int p_currentTime = 0;
        public Game game;
        public Bitmap dragonImage;
        public Sprite dragonSprite;
        public Bitmap grass;
        public Bitmap bg;
        public Sprite building1;
        public Sprite building2;
        public Sprite building3;
        public int frameCount = 0;
        public int frameTimer = 0;
        public float frameRate = 0;
        public PointF velocity;
        public int direction = 2;
        public int speed = 5;
        string initString = "用鍵盤的上下左右方向鍵移動人物，進行闖關";
        public string guessNumGame="";
        public string compareGame = "";
        public string snakeGame = "";
        public string game1Path = "1.txt";
        public string game2Path = "2.txt";
        public string game3Path = "3.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            Main();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Game_KeyPressed(e.KeyCode);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Shutdown();
        }

        public bool Game_Init()
        {
            this.Text = "通訊工程系 計算機概論 期末專案 黃品嫚";
            grass = game.LoadBitmap("grass.bmp");
            bg = game.LoadBitmap("bg.png");
            dragonImage = game.LoadBitmap("farmerwalk.png");
            dragonSprite = new Sprite(ref game);
            dragonSprite.Image = dragonImage;
            dragonSprite.Width = 96;
            dragonSprite.Height = 96;
            dragonSprite.Columns = 8;
            dragonSprite.TotalFrames = 64;
            dragonSprite.AnimationRate = 20;
            dragonSprite.X = 10;
            dragonSprite.Y = 160;

            building1 = new Sprite(ref game);
            building1.Image = game.LoadBitmap("building1.png");
            building1.Size = new Size(128, 128);
            building1.TotalFrames = 1;
            building1.Velocity = new PointF(0, 0);
            building1.X = 550;
            building1.Y = 5;

            building2 = new Sprite(ref game);
            building2.Image = game.LoadBitmap("building2.png");
            building2.Size = new Size(128, 128);
            building2.TotalFrames = 1;
            building2.Velocity = new PointF(0, 0);
            building2.X = 550;
            building2.Y = 140;


            building3 = new Sprite(ref game);
            building3.Image = game.LoadBitmap("building3.png");
            building3.Size = new Size(128, 128);
            building3.TotalFrames = 1;
            building3.Velocity = new PointF(0, 0);
            building3.X = 550;
            building3.Y = 280;
            
            return true;
        }

        public void Game_Update(int time)         //開啟另一個視窗，進入小遊戲
        {
            if (dragonSprite.IsColliding(ref building1))      //IsColliding判斷人物座標有有無落入建築物座標中(有無碰撞)
            {
                velocity = new Point(0, 0);
                dragonSprite.X = 500;
                Form2 form2 = new Form2();
                form2.FormBorderStyle = FormBorderStyle.FixedSingle;
                form2.form1 = this;
                form2.MaximizeBox = false;
                form2.ShowDialog();
                 
            }
            if (dragonSprite.IsColliding(ref building2))
            {
                velocity = new Point(0, 0);
                dragonSprite.X = 500;
                Form3 form3 = new Form3();
                form3.FormBorderStyle = FormBorderStyle.FixedSingle;
                form3.form1 = this;
                form3.MaximizeBox = false;
                form3.ShowDialog();
            }
            if (dragonSprite.IsColliding(ref building3))
            {
                velocity = new Point(0, 0);
                dragonSprite.X = 500;
                Form4 form4 = new Form4();
                form4.FormBorderStyle = FormBorderStyle.FixedSingle;
                form4.form1 = this;
                form4.MaximizeBox = false;
                form4.ShowDialog();
            }
        }

        public void Game_Draw()
        {
            //draw background
            game.DrawBitmap(ref grass, 0, 0, 690, 500);
            building1.Draw();
            building2.Draw();
            building3.Draw();
            dragonSprite.X += velocity.X;
            dragonSprite.Y += velocity.Y;
            

            //animate and draw dragon sprite
            dragonSprite.Animate(direction * 8 + 1, direction * 8 + 7);
            dragonSprite.Draw();
            game.Print(0, 0, initString);
            if (!guessNumGame.Equals(""))
            {
            }

            if (File.Exists(game1Path))
            {
                using (StreamReader sr = new StreamReader(game1Path))
                {
                    string str="       猜數字遊戲\n目前最佳時間:"+sr.ReadLine()+"秒";
                    if(!guessNumGame.Equals(""))
                        str = str + "\n    您花的時間:" + guessNumGame + "秒";
                    game.Print(400,2,str,new SolidBrush(Color.White));
                }
            }

            if (File.Exists(game2Path))
            {
                using (StreamReader sr = new StreamReader(game2Path))
                {
                    string str="       比大小遊戲\n目前最佳戰績:"+sr.ReadLine()+"勝";
                    if(!compareGame.Equals(""))
                        str = str + "\n 您" + compareGame + "勝";
                    game.Print(400, 140,str, new SolidBrush(Color.White));
                }
            }

            if (File.Exists(game3Path))
            { 

                using (StreamReader sr = new StreamReader(game3Path))
                {
                    string str = "       拾硬幣遊戲\n目前最佳拾取" + sr.ReadLine() + "枚硬幣";
                    if (!snakeGame.Equals(""))
                        str = str + "\n 您拾取" + snakeGame + "枚硬幣";
                    game.Print(400, 290, str, new SolidBrush(Color.White));
                }
            }
        }

        public void Game_End()
        {
            dragonImage = null;
            dragonSprite = null;
            grass = null;
        }

        public void Game_KeyPressed(System.Windows.Forms.Keys key)  //偵測鍵盤按鍵有無被按下
        {
            switch (key)
            {
                case Keys.Escape: Shutdown(); break;
                case Keys.Up: direction = 0; break;
                case Keys.Right: direction = 2; break;
                case Keys.Down: direction = 4; break;
                case Keys.Left: direction = 6; break;
            }
            //move the dragon sprite
            switch (direction)
            {
                case 0: velocity = new Point(0, -speed); break;
                case 2: velocity = new Point(speed, 0); break;
                case 4: velocity = new Point(0, speed); break;
                case 6: velocity = new Point(-speed, 0); break;
            }
        }

        public void Shutdown()
        {
            p_gameOver = true;
        }

        //*******************************************
        //real time game loop
        //*******************************************
        public void Main()
        {
            Form form = (Form)this;
            game = new Game(ref form, 690, 420);

            //load and initialize game assets
            Game_Init();

            while (!p_gameOver)
            {
                //update timer
                p_currentTime = Environment.TickCount;

                //let gameplay code update 
                Game_Update(p_currentTime - p_startTime);

                //refresh at 60 FPS
                if (p_currentTime > p_startTime + 16)
                {
                    //update timing 
                    p_startTime = p_currentTime;

                    //let gameplay code draw
                    Game_Draw();

                    //give the form some cycles 
                    Application.DoEvents();

                    //let the game object update
                    game.Update();
                }

                frameCount += 1;
                if (p_currentTime > frameTimer + 1000)
                {
                    frameTimer = p_currentTime;
                    frameRate = frameCount;
                    frameCount = 0;
                }
            }

            //free memory and shut down
            Game_End();
            Application.Exit();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            velocity = new Point(0, 0);
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            //initString = "111111";
        }

    }

}
