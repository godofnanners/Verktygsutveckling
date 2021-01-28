using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum eCurve
        {
            None,
            CatmullRom,
            Bezier
        }
        EngineBridge myEngineBridge;
        IntPtr myHwndToPanel;
        Thread myEngineThread;
        private bool initialized;
        eCurve myCurveType;
        Curves.BezierCurve myBezierCurve = new Curves.BezierCurve();
        List<PointF> myPoints = new List<PointF>();
        List<double> myPointsBezier = new List<double>();
        List<Line> myCurrentLines = new List<Line>();

        public MainWindow()
        {
            InitializeComponent();
            myHwndToPanel = myEngineRenderPanel.Handle;
            myEngineRenderPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            myEngineRenderPanel.Left = -(int)(myEngineRenderPanel.Width*2.9);
            myEngineRenderPanel.Top = -(int)(myEngineRenderPanel.Height *3.2);
            myEngineThread = new Thread(LoadGfxEngine);
            myEngineRenderPanel.Update();
            myEngineThread.Start();
            initialized = true;
            myCurveType = eCurve.None;
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

        private void Canvas_CurvePoints_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            System.Windows.Point p = e.GetPosition(Canvas_CurvePoints);
            System.Windows.Point p2 = new System.Windows.Point(p.X + 5, p.Y); e.GetPosition(Canvas_CurvePoints);
            System.Windows.Point p3 = new System.Windows.Point(p.X - 5, p.Y); e.GetPosition(Canvas_CurvePoints);
            System.Windows.Point p4 = new System.Windows.Point(p.X, p.Y + 5); e.GetPosition(Canvas_CurvePoints);
            System.Windows.Point p5 = new System.Windows.Point(p.X, p.Y - 5); e.GetPosition(Canvas_CurvePoints);
            Polygon polygon = new Polygon();
            polygon.Stroke = System.Windows.Media.Brushes.Red;
            polygon.Fill = System.Windows.Media.Brushes.DarkRed;
            polygon.StrokeThickness = 1;
            myPoints.Add(new PointF((float)p.X, (float)p.Y));
            myPointsBezier.Add(p.X);
            myPointsBezier.Add(p.Y);
            polygon.Points.Add(p2);
            polygon.Points.Add(p4);
            polygon.Points.Add(p3);
            polygon.Points.Add(p5);
            Canvas_CurvePoints.Children.Add(polygon);
        }

        private void Generate_button_Click(object sender, RoutedEventArgs e)
        {
            myEngineBridge.ClearSprites();
            foreach (Line line in myCurrentLines)
            {
                Canvas_CurvePoints.Children.Remove(line);
            }
            myCurrentLines.Clear();
            switch (myCurveType)
            {
                case eCurve.None:
                    break;
                case eCurve.CatmullRom:
                    List<PointF> tempPoints = new List<PointF>();
                    tempPoints = myPoints;
                    tempPoints.Insert(0, myPoints[0]);
                    tempPoints.Insert(myPoints.Count - 1, myPoints[myPoints.Count - 1]);
                    PointF[] points = Catmull_Rom_Sample.CatmullRomSpline.Generate(tempPoints.ToArray(), 60);

                    for (int i = 0; i < points.Length; i++)
                    {
                        if (i < points.Length - 1)
                        {
                            Line line = new Line();
                            line.X1 = points[i].X;
                            line.X2 = points[i + 1].X;
                            line.Y1 = points[i].Y;
                            line.Y2 = points[i + 1].Y;
                            line.Stroke = System.Windows.Media.Brushes.Red;
                            line.StrokeThickness = 1;
                            myCurrentLines.Add(line);
                            Canvas_CurvePoints.Children.Add(line);
                        }
                        
                        myEngineBridge.SetSpritePosition((float)(points[i].X / Canvas_CurvePoints.Width), (float)(points[i].Y / Canvas_CurvePoints.Height));
                    }
                    break;
                case eCurve.Bezier:
                    int cpts = 80;
                    double[] pointsBezier = new double[cpts*2];
                    myBezierCurve.Generate(myPointsBezier.ToArray(), cpts, pointsBezier);
                    for (int i = 0; i < pointsBezier.Length; i+=2)
                    {
                        if (i < pointsBezier.Length - 2)
                        {
                            Line line = new Line();
                            line.X1 = pointsBezier[i];
                            line.X2 = pointsBezier[i + 2];
                            line.Y1 = pointsBezier[i + 1];
                            line.Y2 = pointsBezier[i + 3];
                            line.Stroke = System.Windows.Media.Brushes.Red;
                            line.StrokeThickness = 1;
                            myCurrentLines.Add(line);
                            Canvas_CurvePoints.Children.Add(line);
                            myEngineBridge.SetSpritePosition((float)(pointsBezier[i]/Canvas_CurvePoints.Width), (float)(pointsBezier[i + 1]/Canvas_CurvePoints.Height));
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void Catmull_Rom_radioButton_Checked(object sender, RoutedEventArgs e)
        {
            myCurveType = eCurve.CatmullRom;
        }

        private void Bezier_radioButton_Checked(object sender, RoutedEventArgs e)
        {
            myCurveType = eCurve.Bezier;
        }

        private void Clear_button_Click(object sender, RoutedEventArgs e)
        {
            Canvas_CurvePoints.Children.Clear();
            myPoints.Clear();
            myPointsBezier.Clear();
            myCurrentLines.Clear();
            myEngineBridge.ClearSprites();
        }

        private void LoadSprite_button_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog openFileDlg = new CommonOpenFileDialog();
            openFileDlg.InitialDirectory = "C:\\";
            openFileDlg.IsFolderPicker = false;
            openFileDlg.Multiselect = false;
            CommonFileDialogFilter filter = new CommonFileDialogFilter("DDS Files",".dds");
            filter.ShowExtensions = true;
            openFileDlg.Filters.Add(filter);
            if (openFileDlg.ShowDialog()== CommonFileDialogResult.Ok)
            {
                myEngineBridge.SetSpritePath(openFileDlg.FileName);
            }
        }
    }
}
