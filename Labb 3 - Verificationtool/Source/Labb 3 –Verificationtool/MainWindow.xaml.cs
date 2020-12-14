using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Labb_3__Verificationtool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileFinder myFileFinder;
     
        public MainWindow()
        {
            InitializeComponent();
            myFileFinder = new FileFinder();
        }

        private void GetDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (myFileFinder.AddFilesToListView(FileslistView))
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(FileslistView.ItemsSource);
                view.Filter = UserFilter;
            }
            
        }

        private void SizeColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            myFileFinder.Sort(FileFinder.SortType.eSize);
        }

        private void TypeColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            myFileFinder.Sort(FileFinder.SortType.eType);           
        }

        private void NameColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            myFileFinder.Sort(FileFinder.SortType.eName);
        }

        private void OpenFolderMenuItemClick(object sender, RoutedEventArgs e)
        {
            ListFile file = FileslistView.SelectedItem as ListFile;
            myFileFinder.OpenFolderThroughPath(file.FullPath);
        }

        private void OpenFildeMenuItemClick(object sender, RoutedEventArgs e)
        {
            ListFile file = FileslistView.SelectedItem as ListFile;
            myFileFinder.OpenFileThroughPath(file.FullPath+ "\\"+file.Name, file.Name);
        }

        private void TypeSortTextBoxTextChange(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(FileslistView.ItemsSource).Refresh();
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(TypeSortTextBox.Text))
                return true;
            else
                return (item as ListFile).Type.StartsWith(TypeSortTextBox.Text, StringComparison.CurrentCultureIgnoreCase) == true;
        }

    }
}
