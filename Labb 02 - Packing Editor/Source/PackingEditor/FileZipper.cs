using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Threading;

namespace PackingEditor
{
    class FileZipper
    {
        private BackgroundWorker myWorker;
        List<string> myFilePaths;
        double myFilesDoneValue = 0;
        double myFileWorthValue = 0;
        public FileZipper(BackgroundWorker aBackgroundWorker)
        {
            myFilePaths = new List<string>();
            myWorker = aBackgroundWorker;
        }

        public double FilesDoneValue
        {
            get { return myFilesDoneValue; }
        }

        public int myNumberOfFilePaths
        {
            get { return myFilePaths.Count; }
        }

        public void HandleFileDrop(DragEventArgs e, ListBox aListBox)
        {
            myFilePaths.Clear();
            aListBox.Items.Clear();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                if (file.IndexOf(".zip") != -1)
                {
                    myFilePaths.Clear();
                    ReadZipFile(files[0], aListBox);
                    return;
                }
                else
                {
                    myFilePaths.Add(file);
                }
            }

            foreach (string file in files)
            {
                aListBox.Items.Add(file);
            }
        }

        public void ZipFiles(string fileName)
        {
            var zip = ZipFile.Open(fileName, ZipArchiveMode.Create);
            myFileWorthValue = 100 / myFilePaths.Count;


            foreach (string file in myFilePaths)
            {
                zip.CreateEntryFromFile(file, Path.GetFileName(file),CompressionLevel.Optimal);
                myFilesDoneValue += myFileWorthValue;
                myWorker.ReportProgress((int)myFilesDoneValue);
                Thread.Sleep(100);
            }
            myWorker.ReportProgress(100); //this is because report progress only takes int and myFilesDoneValue is a double so informatio may have been lost
            zip.Dispose();
        }

        public void ReadZipFile(string aZipPath, ListBox aListBox)
        {
            FileStream zipFileToOpen = new FileStream(aZipPath, FileMode.Open);
            ZipArchive archive = new ZipArchive(zipFileToOpen, ZipArchiveMode.Read);

            foreach (var zipArchiveEntry in archive.Entries)
            {
                aListBox.Items.Add(zipArchiveEntry.FullName);
            }
        }

        public void ClearZipList(ListBox aListBox, ProgressBar aProgressBar)
        {
            aListBox.Items.Clear();
            aProgressBar.Value = 0;
        }



        
    }
}

