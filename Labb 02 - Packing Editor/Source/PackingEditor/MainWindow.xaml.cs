using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.IO;


namespace PackingEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileZipper myFileZipper;
        private BackgroundWorker myWorker = new BackgroundWorker();
        string myOutputFile;
        public MainWindow()
        {
            InitializeComponent();
            myFileZipper = new FileZipper(myWorker);
            myWorker.WorkerReportsProgress = true;
            myWorker.DoWork += myWorker_DoWork;
            myWorker.ProgressChanged += myWorker_ProgressChanged;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            myOutputFile = ZipNameTextBox.Text + ".zip";
            if (File.Exists(myOutputFile))
            {
                MessageBox.Show("File already Exist, name it something else plz");
                return;
            }
            else if (myFileZipper.myNumberOfFilePaths <= 0)
            {
                MessageBox.Show("No valid files to zip, (did you perhaps try to zip a zip file?)");
                return;
            }
            
            myWorker.RunWorkerAsync();
        }

        private void FilesStackPanel_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                myFileZipper.HandleFileDrop(e, FileListBox);
            }
        }

        private void CleanButton_Click(object sender, RoutedEventArgs e)
        {
            myFileZipper.ClearZipList(FileListBox, ZippingProgressBar);
        }

        private void myWorker_DoWork(object sender, DoWorkEventArgs e)
        {
                myFileZipper.ZipFiles(myOutputFile);
        }

        private void myWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ZippingProgressBar.Value = e.ProgressPercentage;
        }
    }
}
