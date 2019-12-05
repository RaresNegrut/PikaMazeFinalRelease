using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace PikaMaze
{
    public partial class Form1 : Form
    {
        #region Anti-Flicker, dunno how it works really
        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;
            System.Reflection.PropertyInfo aProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            aProp.SetValue(c, true, null);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        #endregion
        #region The Form Code
        public Form1()
        {
            InitializeComponent();
            MovementTimer.Start();
            PictureBox1LOC = pictureBox1.Location;
            picboundupdate();
            SetDoubleBuffered(panel1);
        }
        #endregion
        bool up = false, down = false, left = false, right = false;
        int speed=3;
        Point PictureBox1LOC;
        static Rectangle picbound_up;
        static Rectangle picbound_down;
        static Rectangle picbound_left;
        static Rectangle picbound_right;

        static int HitUp;
        static int HitDown;
        static int HitLeft;
        static int HitRight;

        private void picboundupdate()
        {
            picbound_up = new Rectangle(pictureBox1.Left, pictureBox1.Top - speed, pictureBox1.Width, speed);
            picbound_down = new Rectangle(pictureBox1.Left, pictureBox1.Bottom + speed, pictureBox1.Width, speed);
            picbound_left = new Rectangle(pictureBox1.Left - speed, pictureBox1.Top, speed, pictureBox1.Height);
            picbound_right = new Rectangle(pictureBox1.Right + speed, pictureBox1.Top, speed, pictureBox1.Height);

            HitUp = -1;
            HitDown = -1;
            HitLeft = -1;
            HitRight = -1;
        }
        #region Useless Accidentally generated junk code
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region KeyUp and KeyDown events
        public void Form1_Keydown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    up = true;
                    break;
                case Keys.A:
                case Keys.Left:
                    left = true;
                    break;
                case Keys.S:
                case Keys.Down:
                    down = true;
                    break;
                case Keys.D:
                case Keys.Right:
                    right = true;
                    break;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    up = false;
                    break;
                case Keys.S:
                case Keys.Down:
                    down = false;
                    break;
                case Keys.A:
                case Keys.Left:
                    left = false;
                    break;
                case Keys.D:
                case Keys.Right:
                    right = false;
                    break;
            }
        }
        #endregion
        public void MovementTimer_Tick(object sender, EventArgs e)
        {
            if(picbound_down.IntersectsWith(label1.Bounds))
            {
                pictureBox1.Location = PictureBox1LOC;
                picboundupdate();
                up = down = left = right = false;
                MessageBox.Show("Congratulations!", "You did it, you got to the end, good for you!");
            }
            foreach (var pan in panel1.Controls.OfType<Panel>())
            {
                if (pan.Name != "panel1")
                {
                    if (picbound_up.IntersectsWith(pan.Bounds))
                        HitUp = pictureBox1.Top - pan.Bottom;

                    if (picbound_down.IntersectsWith(pan.Bounds))
                        HitDown = pan.Top - pictureBox1.Bottom;

                    if (picbound_left.IntersectsWith(pan.Bounds))
                        HitLeft = pictureBox1.Left - pan.Right;

                    if (picbound_right.IntersectsWith(pan.Bounds))
                        HitRight = pan.Left - pictureBox1.Right;

                }

            }
            if (up == true)
                if (pictureBox1.Top >= speed)
                    if (HitUp == -1)
                        pictureBox1.Top -= speed;
                    else
                        pictureBox1.Top -= HitUp;
                else
                    pictureBox1.Top = 0;

            if (down == true)
                if (pictureBox1.Bottom <= this.ClientRectangle.Height - speed)
                    if (HitDown == -1)
                        pictureBox1.Top += speed;
                    else
                        pictureBox1.Top += HitDown;
                else
                    pictureBox1.Top = this.ClientRectangle.Bottom - pictureBox1.Height;

            //#region Attempt to fix ReadOnly errors
            //public string Bottom
            //{

            //}
            //#endregion

            if (left == true)
                if (pictureBox1.Left >= speed)
                    if (HitLeft == -1)
                        pictureBox1.Left -= speed;
                    else
                        pictureBox1.Left -= HitLeft;
                else
                    pictureBox1.Left = 0;

            if (right == true)
                if (pictureBox1.Right <= this.ClientRectangle.Width - speed)
                    if (HitRight == -1)
                        pictureBox1.Left += speed;
                    else
                        pictureBox1.Left += HitRight;
                else
                    pictureBox1.Left = this.ClientRectangle.Right - pictureBox1.Width;
            picboundupdate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
