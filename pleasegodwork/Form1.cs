using System.Drawing.Drawing2D;
using Timer = System.Windows.Forms.Timer;

namespace pleasegodwork
{
    public partial class Form1 : Form
    {
        private Timer _timer;
        private DateTime _startTime = DateTime.MinValue;
        private TimeSpan _currentElapsedTime = TimeSpan.Zero;
        private TimeSpan _totalElapsedTime = TimeSpan.Zero;

        private bool _timerRunning = false;
        List<Panel> l = new List<Panel>();
        public Form1()
        {
            InitializeComponent();
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += new EventHandler(_timer_Tick);
        }
        void _timer_Tick(object sender, EventArgs e)
        {
            var timeSinceStartTime = DateTime.Now - _startTime;
            timeSinceStartTime = new TimeSpan(timeSinceStartTime.Hours,
                                              timeSinceStartTime.Minutes,
                                              timeSinceStartTime.Seconds);
            _currentElapsedTime = timeSinceStartTime + _totalElapsedTime;
            lblsec.Text = _currentElapsedTime.ToString();
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                       Color.Coral,
                       Color.IndianRed,
                       45F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            l.Add(panel1);
            l.Add(Grateful);
            l.Add(panel3);
            panel1.BackgroundImage = Gradient2D(panel1.ClientRectangle,
               Color.FromArgb(0,255,255,0), Color.LightGoldenrodYellow, Color.IndianRed, Color.OrangeRed);
            Grateful.BackgroundImage = Gradient2D(Grateful.ClientRectangle,
               Color.FromArgb(189, 103, 24), Color.FromArgb(63, 188, 132, 49), Color.FromArgb(47, 174, 36, 36), Color.FromArgb(10, 81, 9, 137));
            panel3.BackgroundImage = Gradient2D(panel3.ClientRectangle,
               Color.DarkCyan, Color.BlueViolet, Color.Violet, Color.MediumVioletRed);
            label3.Text = DateTime.Now.ToString("D");
            l[0].BringToFront();
            l[0].Visible = true;
            l[1].Visible = false;
            l[2].Visible = false;

        }
        Bitmap Gradient2D(Rectangle r, Color c1, Color c2, Color c3, Color c4)
        {
            Bitmap bmp = new Bitmap(r.Width, r.Height);

            float delta12R = 1f * (c2.R - c1.R) / r.Height;
            float delta12G = 1f * (c2.G - c1.G) / r.Height;
            float delta12B = 1f * (c2.B - c1.B) / r.Height;
            float delta34R = 1f * (c4.R - c3.R) / r.Height;
            float delta34G = 1f * (c4.G - c3.G) / r.Height;
            float delta34B = 1f * (c4.B - c3.B) / r.Height;
            using (Graphics G = Graphics.FromImage(bmp))
                for (int y = 0; y < r.Height; y++)
                {
                    Color c12 = Color.FromArgb(255, c1.R + (int)(y * delta12R),
                          c1.G + (int)(y * delta12G), c1.B + (int)(y * delta12B));
                    Color c34 = Color.FromArgb(255, c3.R + (int)(y * delta34R),
                          c3.G + (int)(y * delta34G), c3.B + (int)(y * delta34B));
                    using (LinearGradientBrush lgBrush = new LinearGradientBrush(
                          new Rectangle(0, y, r.Width, 1), c12, c34, 0f))
                    { G.FillRectangle(lgBrush, 0, y, r.Width, 1); }
                }
            return bmp;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Title = "Save";
            SFD.Filter = "Text Document(*.txt)|*.txt|All Files(*.*)|(*.*)";
            if (SFD.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(SFD.FileName, RichTextBoxStreamType.PlainText);
                Text = SFD.FileName;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Title = "Open";
            OFD.Filter = "Text Document(*.txt)|*.txt|All Files(*.*)|(*.*)";
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.LoadFile(OFD.FileName, RichTextBoxStreamType.PlainText);
                Text = OFD.FileName;
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            l[1].BringToFront();
            l[1].Visible = true;
            l[2].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonEllipse1_Click_1(object sender, EventArgs e)
        {
            if (!_timerRunning)
            {
                _startTime = DateTime.Now;

                _totalElapsedTime = _currentElapsedTime;

                _timer.Start();
                _timerRunning = true;
            }
            else 
            {
                _timer.Stop();
                _timerRunning = false;
                
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            l[0].BringToFront();
            l[0].Visible = true;
            l[1].Visible = false;
            l[2].Visible = false;
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            l[2].BringToFront();
            l[2].Visible = true;
            
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void headerPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void headerPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}