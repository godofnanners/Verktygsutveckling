using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Data;
using System.ComponentModel;

namespace Labb_3__Verificationtool
{
    class FileFinder
    {
        public enum SortType
        {
            eName,
            eSize,
            eType
        }

        List<ListFile> myFiles = new List<ListFile>();
        public FileFinder()
        {

        }

        public void Sort(SortType aSortingType)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(myFiles);
            switch (aSortingType)
            {
                case SortType.eName:
                    if (view.SortDescriptions.Count == 0 || view.SortDescriptions[0].PropertyName == "Name" && view.SortDescriptions[0].Direction == ListSortDirection.Descending)
                    {
                        view.SortDescriptions.Clear();
                        view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                    }
                    else
                    {
                        view.SortDescriptions.Clear();
                        view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
                    }
                    break;
                case SortType.eSize:

                    if (view.SortDescriptions.Count == 0 || view.SortDescriptions[0].PropertyName == "Size" && view.SortDescriptions[0].Direction == ListSortDirection.Descending)
                    {
                        view.SortDescriptions.Clear();
                        view.SortDescriptions.Add(new SortDescription("Size", ListSortDirection.Ascending));
                    }
                    else
                    {
                        view.SortDescriptions.Clear();
                        view.SortDescriptions.Add(new SortDescription("Size", ListSortDirection.Descending));
                    }
                    break;
                case SortType.eType:
                    if (view.SortDescriptions.Count == 0 || view.SortDescriptions[0].PropertyName == "Type" && view.SortDescriptions[0].Direction == ListSortDirection.Descending)
                    {
                        view.SortDescriptions.Clear();
                        view.SortDescriptions.Add(new SortDescription("Type", ListSortDirection.Ascending));
                    }
                    else
                    {
                        view.SortDescriptions.Clear();
                        view.SortDescriptions.Add(new SortDescription("Type", ListSortDirection.Descending));
                    }
                    break;
                default:
                    break;
            }
        }

        public bool AddFilesToListView(ListView aListview)
        {
            myFiles.Clear();
            CommonOpenFileDialog openFileDlg = new CommonOpenFileDialog();
            openFileDlg.InitialDirectory = "C:\\";
            openFileDlg.IsFolderPicker = true;
            openFileDlg.Multiselect = true;
            // Launch OpenFileDialog by calling ShowDialog method

            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock

            if (openFileDlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                FileInfo[] info = GetFilesAsFileInfo(openFileDlg.FileName);

                foreach (var item in info)
                {
                    ListFile listFile = new ListFile();
                    listFile.Name = item.Name;
                    listFile.Type = item.Extension.Trim('.');
                    listFile.Size = item.Length;
                    listFile.FullPath = item.DirectoryName;
                    myFiles.Add(listFile);
                }
                aListview.ItemsSource = myFiles;
                return true;
            }
            return false;
        }

        public void OpenFolderThroughPath(string aPath)
        {
            Process.Start(aPath);
        }

        public void OpenFileThroughPath(string aPath, string aFileName)
        {
            Process.Start(@aPath);
        }

        public FileInfo[] GetFilesAsFileInfo(string aStringPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(aStringPath);
            return dirInfo.GetFiles(".", SearchOption.AllDirectories);
        }

        public void FilterFiles(string aSubString)
        {

        }


    }

    public class ListFile
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public long Size { get; set; }
        public string FullPath { get; set; }
    }
}
