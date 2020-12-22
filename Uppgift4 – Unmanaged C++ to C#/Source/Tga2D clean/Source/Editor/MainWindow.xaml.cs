using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EngineBridge myEngineBridge;
        IntPtr myHwndToPanel;
        Thread myEngineThread;
        private bool initialized;
        public MainWindow()
        {
            InitializeComponent();
            myHwndToPanel = myEngineRenderPanel.Handle;
            myEngineRenderPanel.Left = 0 ;
            myEngineRenderPanel.Top = 0 ;
            myEngineThread = new Thread(LoadGfxEngine);
            myEngineRenderPanel.Update();
            myEngineThread.Start();
            initialized = true;
        }

        private void LoadGfxEngine()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    lock (myEngineRenderPanel)
                    {
                        myEngineRenderPanel.Anchor = (System.Windows.Forms.AnchorStyles.None);
                        myEngineBridge = new EngineBridge();
                        myEngineBridge.Init(myHwndToPanel, 1280, 720);
                    }
                });
        }
        //public MainWindow()
        //{
        //    InitializeComponent();
        //
        //}
        //
        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    //HwndSource hwndSource = (HwndSource)HwndSource.FromVisual(myCanvas);
        //    //myHWND = hwndSource.Handle;
        //    //myEngineThread = new Thread(() => { RunGfxEngine(); });
        //    //myEngineThread.Start();
        //   
        //}

        //private void RunGfxEngine()
        //{
        //    lock (myCanvas)
        //    {
        //        myEngineBridge = new EngineBridge();
        //        myEngineBridge.Init(myHWND, 1280, 720);
        //    }
        //}

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            myEngineBridge.ShutDown();
            myEngineThread.Join();
        }

        private void myScaleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (myEngineBridge != null)
            {
                myEngineBridge.TextureScale((float)myScaleSlider.Value);
            }
        }
    }
}
