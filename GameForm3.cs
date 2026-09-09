using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game2
{
    public partial class GameForm3 : Form
    {
        public GameForm3(Image img)
        {

            InitializeComponent();
            this.BackgroundImage = img;
            Label label = new Label();
            GameForm1 form = new GameForm1();
            label.Text = "Game End!";
            label.Size= new Size(250, 50);
            label.Top = this.Height / 2 - label.Height / 2;
            label.Left = this.Width / 2 - label.Width / 2;
            label.BackColor = Color.White;
            label.Font = new Font("Georgia", 24, FontStyle.Bold);
            this.Controls.Add(label);
           
        }

        private void GameForm3_Load(object sender, EventArgs e)
        {

        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}
