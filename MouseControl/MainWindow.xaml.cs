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
            InitializeComponent();
            WebServer = new HTTPServer("C:\\Users\\James\\Documents\\GitHub\\MouseControl\\MouseControl\\html\\");
            Websocket ws = new Websocket();
            AddTaskbarIcon();
        }

        // Adds the notification icon
        public void AddTaskbarIcon()
        {
            // The display text of the icon
            string DisplayText = "MouseControl running at " + WebServer.runningIp;
            systemIcon = new TaskbarIcon();
            systemIcon.ToolTipText = DisplayText;
            systemIcon.ShowBalloonTip("MouseControl", DisplayText, BalloonIcon.Info);

            // Remove the text after a certain amount of time
            Thread.Sleep(6000);
            systemIcon.HideBalloonTip();
        }
    }
}
