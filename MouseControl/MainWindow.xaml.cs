using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hardcodet.Wpf.TaskbarNotification;
using System.Threading;
using System.Drawing;

namespace MouseControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public TaskbarIcon systemIcon;
        private HTTPServer WebServer;

        public MainWindow()
        {
            string htmlDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\html\\";
            InitializeComponent();
            WebServer = new HTTPServer(htmlDir);
            //"C:\\Users\\James\\Documents\\GitHub\\MouseControl\\MouseControl\\html\\"
            Websocket ws = new Websocket();
            AddTaskbarIcon();
        }

        // Adds the notification icon
        public void AddTaskbarIcon()
        {
            // The display text of the icon
            string DisplayText = "MouseControl running at " + WebServer.runningIp;
            systemIcon = new TaskbarIcon();
            systemIcon.Icon = new System.Drawing.Icon(typeof(App), "MouseControlIcon.ico"); ;
            systemIcon.ToolTipText = DisplayText;
            systemIcon.ShowBalloonTip("MouseControl", DisplayText, BalloonIcon.Info);

            // Remove the text after a certain amount of time
            Thread.Sleep(6000);
            systemIcon.HideBalloonTip();

            // Add context menu
            ContextMenu cMenu = new ContextMenu();

            MenuItem about = new MenuItem();
            about.Header = "About";
            about.Click += aboutClick;

            MenuItem exit = new MenuItem();
            exit.Header = "Exit";
            exit.Click += exitClick;

            cMenu.Items.Add(about);
            cMenu.Items.Add(exit);

            systemIcon.ContextMenu = cMenu;

        }

        // About button in the context menu clicked
        public void aboutClick(object sender, System.EventArgs e)
        {
            this.Show();
        }

        // Exit button clicked
        public void exitClick(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
