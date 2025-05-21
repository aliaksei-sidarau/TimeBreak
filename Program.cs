using Timer = System.Windows.Forms.Timer;

namespace TimeBreak;

static class Program
{
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
        breakTimer.Tick += breakTimer_Tick;
        breakTimer.Interval = SettingsData.Default.GetBreakInterval();
        breakTimer.Start();

        // Setup context menu
        var settingsItem = new ToolStripMenuItem("Settings");
        settingsItem.Click += SettingsItem_Click;

        var exitItem = new ToolStripMenuItem("Exit");
        exitItem.Click += ExitItem_Click;
        
        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add(settingsItem);
        contextMenu.Items.Add(exitItem);

        // Setup tray icon
        trayIcon.Icon = SystemIcons.Application;
        trayIcon.Text = "Black Screen App";
        trayIcon.ContextMenuStrip = contextMenu;
        trayIcon.Visible = true;

        Application.Run();
    }

    private static void breakTimer_Tick(object sender, EventArgs e)
    {
        breakTimer.Stop();

        blackScreen = new BlackScreenForm();
        blackScreen.FormClosed += BlackScreen_FormClosed;
        blackScreen.ShowForDuration(SettingsData.Default.BreakMinuts);
    }

    private static void SettingsItem_Click(object sender, EventArgs e)
    {
        breakTimer.Stop();
        blackScreen.ShowForDuration(1);
    }

    private static void BlackScreen_FormClosed(object sender, EventArgs e)
    {
        breakTimer.Interval = SettingsData.Default.GetBreakInterval();
        breakTimer.Start();
    }

    private static void ExitItem_Click(object sender, EventArgs e)
    {
        blackScreen.Close();

        trayIcon.Visible = false;
        breakTimer.Stop();

        Application.Exit();
    }
}