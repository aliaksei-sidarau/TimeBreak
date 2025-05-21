using Timer = System.Windows.Forms.Timer;

namespace TimeBreak;

static class Program
{
    static DateTime lastStart;
    static Timer breakTimer;
    static NotifyIcon trayIcon = new NotifyIcon();
    static BlackScreenForm blackScreen = new BlackScreenForm();
    
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Setup timer
        breakTimer = new Timer();
        breakTimer.Tick += (object sender, EventArgs e) =>
            StopTimerAndShowBlackScreen(SettingsData.Default.BreakMinuts);
        breakTimer.Interval = SettingsData.Default.GetBreakInterval();
        breakTimer.Start();
        lastStart = DateTime.Now;

        // Setup context menu
        var timeItem = new ToolStripMenuItem();
        timeItem.Enabled = false;

        var settingsItem = new ToolStripMenuItem("Settings");
        settingsItem.Click += (object sender, EventArgs e) => StopTimerAndShowBlackScreen(1);

        var exitItem = new ToolStripMenuItem("Exit");
        exitItem.Click += (object sender, EventArgs e) =>
        {
            breakTimer.Stop();
            blackScreen.Close();
            trayIcon.Visible = false;

            Application.Exit();
        };
        
        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add(timeItem);
        contextMenu.Items.Add(settingsItem);
        contextMenu.Items.Add(exitItem);
        contextMenu.Opening += (sender, e) =>
        {
            timeItem.Text = $"Last: {lastStart.ToString("HH:mm:ss")}";
        };

        // Setup tray icon
        trayIcon.Icon = SystemIcons.Warning;
        trayIcon.Text = "TimeBreak Screen App";
        trayIcon.ContextMenuStrip = contextMenu;
        trayIcon.Visible = true;

        Application.Run();
    }

    private static void StopTimerAndShowBlackScreen(int minutes)
    {
        breakTimer.Stop();

        if (blackScreen is null || blackScreen.IsDisposed || !blackScreen.Visible)
        {
            blackScreen = new BlackScreenForm();
            blackScreen.FormClosed += (object sender, FormClosedEventArgs e) =>
            {
                breakTimer.Interval = SettingsData.Default.GetBreakInterval();
                breakTimer.Start();
                lastStart = DateTime.Now;
            };
            blackScreen.ShowForDuration(minutes);
        }
    }
}