using System.Drawing.Drawing2D;

namespace GameConway
{
    public partial class MainForm : Form
    {
        private Game game;
        private bool isRunning;
        private Thread? gameThread;

        private const int WIDTH = 1024;
        private const int HEIGHT = 1024;
        private const int CELL_SIZE = 8;

        public MainForm()
        {
            InitializeComponent();

            game = new Game(64, 64);
            isRunning = false;

            RenderGame();
        }

        private void ThreadLoop()
        {
            while (isRunning)
            {
                UpdateGame();
                Thread.Sleep(10);
            }
        }

        private void UpdateGame()
        {
            game.Update();
            RenderGame();
        }

        private void RenderGame()
        {
            pbBackground.Image = game.DrawBitmap(WIDTH, HEIGHT, CELL_SIZE);
        }

        private void pbBackground_MouseClick(object sender, MouseEventArgs e)
        {
            if (isRunning)
                return;

            int x = e.X / CELL_SIZE;
            int y = e.Y / CELL_SIZE;

            if (game.IsValid(x, y))
            {
                bool state = game.GetCellState(x, y);
                game.SetCellState(x, y, !state);
                RenderGame();
            }
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                isRunning = true;
                gameThread = new Thread(() => ThreadLoop());
                gameThread.Start();
                btnStartStop.Text = "Stop";
            }
            else
            {
                isRunning = false;
                btnStartStop.Text = "Start";
            }
        }

        private void pbBackground_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X / CELL_SIZE;
            int y = e.Y / CELL_SIZE;

            if (game.IsValid(x, y))
            {
                this.Text = $"Game [X: {x}, Y: {y}]";
            }
        }
    }
}