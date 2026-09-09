
using System;
using System.Media;
using System.Windows.Forms;
using EZInput;
using System.Collections.Generic;
using System.Drawing;
namespace Game2
{
    public partial class GameForm1 : Form
    {
        PictureBox Player;
        PictureBox Enemy;
        ProgressBar PlayerHealth;
        List<PictureBox> PlayerPower = new List<PictureBox>();
        ProgressBar EnemyHealth;
        Random random;
        Label ScoreLabel;
        string EnemyDirection;
        int EnemySpeed;
        int LocationChanger;
        List<PictureBox> PlayerFire = new List<PictureBox>();
        List<PictureBox> EnemyFire = new List<PictureBox>();
        List<PictureBox> EnemyRock = new List<PictureBox>();
        int BulletCurrentTime;
        int BulletGenerationTime;

        int EnemyFireGenerationTime;
        int EnemyRockGenerationTime;
        int EnemyRockCurrentTime;
        int EnemyFireCurrentTime;
        int PlayerPowerCurrentTime;
        int PlayerPowerGenerationTime;
        int Score;
        public GameForm1()
        {
            InitializeComponent();
        }

        private void Player_Click(object sender, EventArgs e)
        {

        }

        private void GameLoop_Tick(object sender, EventArgs e)
        {
            if (Player != null)
            {
                if (Keyboard.IsKeyPressed(Key.LeftArrow) && Player.Left > 0)
                {
                    Player.Left -= LocationChanger;
                }
                if (Keyboard.IsKeyPressed(Key.RightArrow) && Player.Left + Player.Width < this.ClientSize.Width)
                {
                    Player.Left += LocationChanger;
                }
                if (Keyboard.IsKeyPressed(Key.UpArrow) && Player.Top > 0)
                {
                    Player.Top -= LocationChanger;
                }
                if (Keyboard.IsKeyPressed(Key.DownArrow) && Player.Top + Player.Height < this.ClientSize.Height)
                {
                    Player.Top += LocationChanger;
                }
            }

            if (Keyboard.IsKeyPressed(Key.Space))
            {
                if (BulletCurrentTime >= BulletGenerationTime)
                {
                    CreateBullet();
                    BulletCurrentTime = 0;
                }
            }
            MoveEnemy();
            MoveBullet();

            DetectCollosionOfPlayerFireWithEnemy();
            DetectCollosionOfEnemyFireWithPlayer();
            DetectCollosionOfPlayerFireWithRock();
            DetectCollosionOfPlayerWithcPower();
            DetectCollosionOfRockWithPlayer();
            RemoveBullet();
            BulletCurrentTime++;
            EnemyFireCurrentTime++;
            EnemyRockCurrentTime++;
            PlayerPowerCurrentTime++;
            if (EnemyFireCurrentTime >= EnemyFireGenerationTime)
            {
                CreateEnemyBullet();
                EnemyFireCurrentTime = 0;
            }
            if (EnemyRockCurrentTime >= EnemyRockGenerationTime)
            {
                CreateEnemyRock();
                EnemyRockCurrentTime = 0;
            }
           
            if (PlayerPowerCurrentTime >= PlayerPowerGenerationTime)
            {
                CreatePlayerPower();
                PlayerPowerCurrentTime = 0;
            }
            if (Player != null && PlayerHealth != null)
            {
                PlayerHealth.Left = Player.Left;
            }
            if (Player != null && PlayerHealth != null)
            {
                PlayerHealth.Top = Player.Top + Player.Height + 10;
            }
            if (Enemy != null && EnemyHealth != null)
            {
                EnemyHealth.Left = Enemy.Left;
            }
            if (Enemy != null && EnemyHealth != null)
            {
                EnemyHealth.Top = Enemy.Top + Enemy.Height + 10;
            }

            if (Enemy != null && EnemyHealth.Value == 0)
            {
                GameLoop.Enabled = false;

                Image img = Properties.Resources.background;
                GameForm3 newform = new GameForm3(img);
                DialogResult result = newform.ShowDialog();
                if (result == DialogResult.Yes)
                {
                    Start();
                }
                if (result == DialogResult.No)
                {
                    this.Close();

                }
            }

            if (PlayerHealth != null && PlayerHealth.Value == 0)
            {
                GameLoop.Enabled = false;

                Image img = Properties.Resources.background;
                GameForm3 newform = new GameForm3(img);
                DialogResult result = newform.ShowDialog();
                if (result == DialogResult.Yes)
                {
                    Start();
                }
                if (result == DialogResult.No)
                {
                    this.Close();

                }
            }



        }


        private void DetectCollosionOfEnemyFireWithPlayer()
        {
            foreach (PictureBox bullet in EnemyFire)
            {
                if (bullet != null && Player != null)
                {
                    if (bullet.Bounds.IntersectsWith(Player.Bounds))
                    {
                        if (PlayerHealth.Value >= 20)
                        {
                            PlayerHealth.Value -= 20;
                        }
                        else
                        {
                            PlayerHealth.Value = 0;
                        }

                        bullet.Visible = false;
                    }
                }
            }
        }


        private void DetectCollosionOfPlayerFireWithEnemy()
        {
            foreach (PictureBox bullet in PlayerFire)
            {
                if (bullet != null && Enemy != null)
                {
                    if (bullet.Visible == true && bullet.Bounds.IntersectsWith(Enemy.Bounds))

                    {
                        Score += 5;
                        ScoreLabel.Text = "Score: " + Score;

                        bullet.Visible = false;

                        if (EnemyHealth.Value >= 20)
                        {
                            EnemyHealth.Value -= 20;
                        }
                        else
                        {
                            EnemyHealth.Value = 0;
                        }
                        break;

                    }
                }
            }
        }
        private void DetectCollosionOfPlayerFireWithRock()
        {
            foreach (PictureBox bullet in PlayerFire)
            {
                foreach (PictureBox rock in EnemyRock)
                {
                    if (bullet.Visible == true && rock.Visible == true)
                    {
                        if (bullet.Bounds.IntersectsWith(rock.Bounds))
                        {
                            Score += 5;
                            ScoreLabel.Text = "Score: " + Score;

                            rock.Visible = false;
                            bullet.Visible = false;

                            break;
                        }
                    }
                }
            }
        }
        private void DetectCollosionOfPlayerWithcPower()
        {
            foreach (PictureBox power in PlayerPower)
            {

                if (power.Visible == true && Player.Visible == true)
                {
                    if (Player.Bounds.IntersectsWith(power.Bounds))
                    {
                        if (PlayerHealth.Value <= 80)
                        {
                            PlayerHealth.Value += 20;
                        }
                        else
                        {
                            PlayerHealth.Value = 100;
                        }
                        power.Visible = false;

                        break;
                    }
                }
            }
        }
    


        private void DetectCollosionOfRockWithPlayer()
        {
            foreach (PictureBox rock in EnemyRock)
            {
                if (rock != null && Player != null)
                {
                    if (rock.Bounds.IntersectsWith(Player.Bounds))
                    {
                        PlayerHealth.Value -= 20;
                        rock.Visible = false;
                    }
                }
            }
        }


        private void CreateEnemyBullet()
        {
            PictureBox Bullet = new PictureBox();
            Image img = Properties.Resources.enemy_laser;
            Bullet.Image = img;
            Bullet.Height = img.Height;
            Bullet.Width = img.Width;
            Bullet.BackColor = Color.Transparent;
            if(Enemy!=null)
            {
                Bullet.Left = Enemy.Left + 45;
                Bullet.Top = Enemy.Top + 105;
            }
           
            EnemyFire.Add(Bullet);
            this.Controls.Add(Bullet);

        }
        private void CreateEnemyRock()
        {
            PictureBox Rock = new PictureBox();
            Image img = Properties.Resources.rock1;
            Rock.Image = img;
            Rock.Height = img.Height;
            Rock.Width = img.Width;
            Rock.BackColor = Color.Transparent;
            if (Enemy != null && Rock != null)
            {
                Rock.Left = this.random.Next(0, this.Width - Rock.Width);
            }
            
            Rock.Top = 0 ;
            EnemyRock.Add(Rock);
            this.Controls.Add(Rock);

        }
        private void CreatePlayerPower()
        {
            PictureBox Power = new PictureBox();
            Image img = Properties.Resources.powerup1;
            Power.Image = img;
            Power.Height = img.Height;
            Power.Width = img.Width;
            Power.BackColor = Color.Transparent;
            if (Player != null && Power != null)
            {
                Power.Left = this.random.Next(0, this.Width - Power.Width);
            }

            Power.Top = 0;
            PlayerPower.Add(Power);
            this.Controls.Add(Power);

        }
        private void RemoveBullet()
        {
           for(int idx =0;idx<PlayerFire.Count;idx++)
            {
                if(PlayerFire[idx].Bottom <= 0)
                {
                    this.Controls.Remove(PlayerFire[idx]);
                    PlayerFire[idx].Dispose();
                    PlayerFire.RemoveAt(idx);
                    idx--;
                }
            }
            for (int idx = 0; idx < EnemyFire.Count; idx++)
            {
                if (EnemyFire[idx].Top >= this.Height||EnemyFire[idx].Visible==false)
                {
                    this.Controls.Remove(EnemyFire[idx]);
                    EnemyFire[idx].Dispose();
                    EnemyFire.RemoveAt(idx);
                    idx--;
                }
            }
            for (int idx = 0; idx < EnemyRock.Count; idx++)
            {
                if (EnemyRock[idx].Top >= this.Height || EnemyRock[idx].Visible == false)
                {
                    this.Controls.Remove(EnemyRock[idx]);
                    EnemyRock[idx].Dispose();
                    EnemyRock.RemoveAt(idx);
                    idx--;
                }
            }
            for (int idx = 0; idx < PlayerPower.Count; idx++)
            {
                if (PlayerPower[idx].Top >= this.Height || PlayerPower[idx].Visible == false)
                {
                    this.Controls.Remove(PlayerPower[idx]);
                    PlayerPower[idx].Dispose();
                    PlayerPower.RemoveAt(idx);
                    idx--;
                }
            }
        }

        private void MoveBullet()
        {
         foreach(PictureBox bullet in PlayerFire)
            {
                bullet.Top -= 10;
            }
            foreach (PictureBox bullet in EnemyFire)
            {
                bullet.Top += 10;
            }
            foreach(PictureBox rock in EnemyRock)
            {
                rock.Top += 15;
            }
            foreach(PictureBox playerpower in PlayerPower)
            {
                playerpower.Top += 10;
            }
        }

        private void MoveEnemy()
        {
            if (Enemy != null && Enemy.Left <= 0)
            {
                EnemyDirection = "right";
            }
            if (Enemy != null && (Enemy.Left + Enemy.Width) + 10 >= this.Width)
            {
                EnemyDirection = "left";
            }
           
            if (EnemyDirection == "left")
            {
                Enemy.Left = Enemy.Left - EnemySpeed;
            }
            if (EnemyDirection == "right")
            {
                Enemy.Left = Enemy.Left + EnemySpeed;
            }
        }
        private void CreateBullet()
        {
           PictureBox Bullet = new PictureBox();
            Image img = Properties.Resources.player_laser;
            Bullet.Image = img;
            Bullet.Height = img.Height;
            Bullet.Width = img.Width;
            Bullet.BackColor = Color.Transparent;
            if (Player != null && Bullet != null)
            {
                Bullet.Left = Player.Left + 40;
                Bullet.Top = Player.Top - 50;
            }

            PlayerFire.Add(Bullet);
            this.Controls.Add(Bullet);
        }

        private void GameForm1_Load(object sender, EventArgs e)
        {
            Start();
            
        }

        private void Start()
        { 
            this.Controls.Clear();

            PlayerFire.Clear();
            EnemyFire.Clear();
            EnemyRock.Clear();
            PlayerPower.Clear();
           
            GameLoop.Enabled = true;
            random = new Random();
            EnemySpeed = 7;
            BulletCurrentTime = 0;
            BulletGenerationTime = 10;
            EnemyFireGenerationTime = 20;
            EnemyRockGenerationTime = 300;
            PlayerPowerGenerationTime = 500;
            EnemyFireCurrentTime = 0;
            EnemyRockCurrentTime = 0;
            PlayerPowerCurrentTime = 0;
            Score = 0;
            CreatePlayer();
            CreateEnemy();
            AddScoreBar();
            
            LocationChanger = 10;
        }
        private void AddScoreBar()
        {
            
            ScoreLabel = new Label();
            ScoreLabel.Text = "Score: 0";
            ScoreLabel.Top = 10;
            ScoreLabel.Left = 10;
            ScoreLabel.AutoSize = true;
            ScoreLabel.Font = new Font("Georgia", 14);
            
            ScoreLabel.ForeColor = Color.White;
            ScoreLabel.BackColor = Color.Transparent;
              this.Controls.Add(ScoreLabel);
            ScoreLabel.BringToFront();

        }
        private void CreatePlayer()
        {
            Player = new PictureBox();
            Image img = Properties.Resources.player_ship;
            Player.Image = img;
            Player.Height = img.Height;
            Player.Width = img.Width;
            Player.BackColor = Color.Transparent;
            Player.Left = (this.Width / 2) - 75;
            Player.Top = this.Height - 150;
            this.Controls.Add(Player);
            PlayerHealth = new ProgressBar();
            PlayerHealth.Value = 100;
            PlayerHealth.Top = Player.Top+Player.Height+10;
            PlayerHealth.Left = Player.Left;
            this.Controls.Add(PlayerHealth);

        }
        private void CreateEnemy()
        {
            Enemy = new PictureBox();
            Image img = Properties.Resources.enemy_1;
            Enemy.Image = img;
            Enemy.Height = img.Height;
            Enemy.Width = img.Width;
            Enemy.BackColor = Color.Transparent;
            Enemy.Left = random.Next(0, this.Width - img.Width);
            Enemy.Top = random.Next(0);
            this.Controls.Add(Enemy);
            EnemyDirection = "left";
            EnemyHealth = new ProgressBar();
            EnemyHealth.Value = 100;
            EnemyHealth.Top = Enemy.Bottom + Enemy.Height + 10;
            EnemyHealth.Left = Enemy.Left;
            this.Controls.Add(EnemyHealth);


        }
        public int GetPlayerHealth()
        {
            if (PlayerHealth != null)
            {
                return PlayerHealth.Value;
            }

            return 0;
        }

    }
}