
using Timer = System.Windows.Forms.Timer;

namespace TimeBreak;

public class BlackScreenForm : Form
{
    private const string BreakMessage = "It is time for a short break";

    private int secondsToClose = 0;
    private Timer countdownTimer;
    private Button btnClose;
    private Label lblText;

    public BlackScreenForm()
    {
        this.FormBorderStyle = FormBorderStyle.None;
        this.WindowState = FormWindowState.Maximized;
        this.BackColor = Color.Black;
        this.Opacity = 0.8;
        this.TopMost = true;
        this.ShowInTaskbar = false;
        this.Shown += Form_Shown;

        countdownTimer = new Timer();
        countdownTimer.Tick += CountdownTimer_Tick;

        btnClose = new Button();
        btnClose.Top = 10;
        btnClose.Left = 10;
        btnClose.Width = 100;
        btnClose.Height = 25;
        btnClose.Text = "Click to Close";
        btnClose.BackColor = Color.WhiteSmoke;
        btnClose.Click += btnClose_Click;
        this.Controls.Add(btnClose);

        lblText = new Label();
        btnClose.Top = 10;
        btnClose.Left = 10;
        btnClose.Width = 150;
        lblText.Text = BreakMessage;
        lblText.ForeColor = Color.White;
        lblText.TextAlign = ContentAlignment.TopCenter;
        lblText.Font = new Font(SystemFonts.CaptionFont.FontFamily, 14, FontStyle.Bold);
        this.Controls.Add(lblText);
    }

    public void ShowForDuration(int minutes)
    {
        secondsToClose = minutes.MinutesToSeconds();

        lblText.Text = $"{BreakMessage}: {TimeSpan.FromSeconds(secondsToClose)}";
        countdownTimer.Interval = 1000;
        countdownTimer.Start();

        if (!this.Visible)
        {
            this.Show();
            this.BringToFront();
        }
    }

    private void Form_Shown(object sender, EventArgs e)
    {
        btnClose.Left = (this.Width - btnClose.Width) / 2;
        btnClose.Top = (this.Height - btnClose.Height) / 2;

        lblText.Width = this.Width - 20;
        lblText.Top = btnClose.Top - lblText.Height - 10;
        lblText.Left = (this.Width - lblText.Width) / 2;
    }

    private void CountdownTimer_Tick(object sender, EventArgs e)
    {
        lblText.Text = $"{BreakMessage}: {TimeSpan.FromSeconds(secondsToClose)}";

        secondsToClose--;
        if (secondsToClose <= 0)
        {
            countdownTimer.Stop();
            this.Close();
        }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        secondsToClose = 0;
        countdownTimer.Stop();
        this.Close();
    }
}
