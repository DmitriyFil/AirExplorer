using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace AirExplorer
{

    public partial class MainWindow : Window
    {

        TabItem leftActiveTab;
        TabItem rightActiveTab;

        public MainWindow()
        {
            InitializeComponent();
        }

        int i = 0;

        private void ExplorerButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Oops... something went wrong...");
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Oops... something went wrong...");
        }

        private void SchedulerButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Oops... something went wrong...");
        }

        private void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Oops... something went wrong...");
        }

        private void AccountsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Oops... something went wrong...");
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Oops... something went wrong...");
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Oops... something went wrong...");
        }

        private void VersionButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Oops... something went wrong...");
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Oops... something went wrong...");
        }

        private void RegistryText_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Oops... something went wrong...");
        }

        private void CloseTab(object sender, EventArgs e)
        {
            Button s = (Button)sender;
            StackPanel sp = (StackPanel)s.Parent;
            TabItem ti = (TabItem)sp.Parent;
            TabControl tc = (TabControl)ti.Parent;
            if (tc == leftTab)
            {
                leftTab.Items.Remove(ti);
            }
            else
            {
                rightTab.Items.Remove(ti);
            }
        }

        private StackPanel GetHeaderForMyComputer()
        {
            StackPanel headerPanel = new StackPanel();
            Image icon = new Image
            {
                Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/myComputer.png")),
                Margin = new Thickness(-4, 0, 5, 0),
                Height = 13
            };
            TextBlock text = new TextBlock
            {
                Text = "Этот компьютер"
            };
            text.Margin = new Thickness(0, 0, 10, 0);

            headerPanel.Orientation = Orientation.Horizontal;
            headerPanel.Children.Add(icon);
            headerPanel.Children.Add(text);
            return headerPanel;
        }

        private Button GetCloseBtn()
        {
            Button close = new Button();

            Image closeIcon = new Image
            {
                Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/plusR.png")),
                Margin = new Thickness(0, 0, 0, 0),
                Height = 13
            };
            close.Content = closeIcon;
            close.Click += new RoutedEventHandler(CloseTab);
            close.MouseEnter += new MouseEventHandler(CloseBtn_MouseEnter);
            close.MouseLeave += new MouseEventHandler(CloseBtn_MouseLeave);
            close.Style = (Style)FindResource("x_style"); // создать стиль чтобы красным горелось при наведении и убрать рамку
            return close;
        }

        private void CloseBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            Image closeIcon = new Image
            {
                Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/plusR.png")),
                Margin = new Thickness(0, 0, 0, 0),
                Height = 13
            };
            button.Content = closeIcon;
        }

        private void CloseBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            Image closeIcon = new Image
            {
                Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/plusRwhite.png")),
                Margin = new Thickness(1, 1, 1, 1),
                Height = 11
            };
            button.Content = closeIcon;
        }

        private void Tab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (leftActiveTab == null || rightActiveTab == null)
            {
                leftActiveTab = (TabItem)leftTab.SelectedItem;
                rightActiveTab = (TabItem)rightTab.SelectedItem;
                return;
            }
            if ((TabControl)sender == leftTab)
            {
                ProcessingOnTab(leftActiveTab, (TabControl)sender);
            }
            else
            {
                ProcessingOnTab(rightActiveTab, (TabControl)sender);
            }
        }

        private void ProcessingOnTab(TabItem previousItem, TabControl tc)
        {
            if (previousItem != tc.Items.GetItemAt(0))
            {
                StackPanel sp = (StackPanel)previousItem.Header;
                sp.Children.RemoveAt(2);
            }
            TabItem selectedItem = (TabItem)tc.SelectedItem;
            if (selectedItem == null)
            {
                if (tc.Items.Count == 1)
                {
                    selectedItem = (TabItem)tc.Items.GetItemAt(0);
                }
                else
                {
                    selectedItem = (TabItem)tc.Items.GetItemAt(tc.Items.Count - 1);

                }
            }
            if (selectedItem != tc.Items.GetItemAt(0))
            {
                StackPanel selectedItemHeader = (StackPanel)selectedItem.Header;
                selectedItemHeader.Children.Add(GetCloseBtn());
            }

            leftActiveTab = (TabItem)leftTab.SelectedItem;
            rightActiveTab = (TabItem)rightTab.SelectedItem;
        }

        private Grid GetExplorer()
        {
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition
            {
                Height = GridLength.Auto
            });
            grid.RowDefinitions.Add(new RowDefinition
            {
                Height = GridLength.Auto
            });
            grid.RowDefinitions.Add(new RowDefinition
            {
                Height = GridLength.Auto
            });
            grid.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(1, GridUnitType.Star)
            });
            grid.RowDefinitions.Add(new RowDefinition
            {
                Height = GridLength.Auto
            });
            UIElement firstRow = (UIElement)GetFirstRowOfExplorer();
            Grid.SetRow(firstRow, 0);
            UIElement secondRow = (UIElement)GetSecondRowOfExplorer();
            Grid.SetRow(secondRow, 1);
            UIElement lineRow = (UIElement)GetLineForExplorer();
            Grid.SetRow(lineRow, 2);
            UIElement thirdRow = (UIElement)GetThirdRow();
            Grid.SetRow(thirdRow, 3);
            //UIElement fourthRow = (UIElement)GetFirstRowOfExplorer();
            //Grid.SetRow(fourthRow, 4);

            grid.Children.Add(firstRow);
            grid.Children.Add(secondRow);
            grid.Children.Add(lineRow);
            grid.Children.Add(thirdRow);

            return grid;
        }

        private object GetThirdRow()
        {
            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(2, GridUnitType.Star)
            });
            grid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = GridLength.Auto
            });
            grid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(4, GridUnitType.Star)
            });
            ListView lv = GetListViewForExplorer();
            lv.BorderBrush = new SolidColorBrush(Colors.White);
            Grid.SetColumn(lv, 2);
            TreeView tv = GetTreeViewForExplorer();
            tv.SelectedItemChanged += TreeViewItemSelected;
            tv.BorderBrush = new SolidColorBrush(Colors.White);
            Grid.SetColumn(tv, 0);

            GridSplitter splitter = new GridSplitter
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 2,
                Background = new SolidColorBrush(Color.FromRgb(240, 240, 255))
            };
            splitter.ShowsPreview = true;
            Grid.SetColumn(splitter, 1);
            ///
            grid.Children.Add(splitter);
            grid.Children.Add(lv);
            grid.Children.Add(tv);
            return grid;
        }

        private void TreeViewItemSelected(object sender, RoutedPropertyChangedEventArgs<Object> e)
        {
            TreeView tvilight = (TreeView)sender;
            TreeViewItem item = (TreeViewItem)tvilight.SelectedItem;
            Grid grid = (Grid)tvilight.Parent;
            UIElementCollection childs = grid.Children;
            foreach (UIElement uI in childs)
            {
                if (uI is ListView pinkiePie)
                {
                    pinkiePie.Items.Clear();
                    FillListView(pinkiePie, item);
                }
            }
        }

        private void FillListView(ListView pinkiePie, TreeViewItem item)
        {
            try
            {
                DirectoryInfo info = (DirectoryInfo)item.Tag;
                DirectoryInfo[] infoDir = info.GetDirectories();
                foreach (DirectoryInfo dir in infoDir)
                {
                    ListViewModel lvm = new ListViewModel(dir.Name, null, dir.LastWriteTime.ToShortDateString() + " " + dir.LastWriteTime.ToShortTimeString(), "Папка с файлами")
                    {
                        Original = dir
                    };
                    pinkiePie.Items.Add(lvm);
                }
                FileInfo[] files = info.GetFiles();
                foreach (FileInfo file in files)
                {
                    ListViewModel lvm = new ListViewModel(file.Name, file.Length.ToString(), file.LastWriteTime.ToShortDateString() + " " + file.LastWriteTime.ToShortTimeString(), file.Extension)
                    {
                        Original = file
                    };
                    pinkiePie.Items.Add(lvm);
                }

            }
            catch { }
        }

        private ListView GetListViewForExplorer()
        {
            ListView list = new ListView();
            GridView gridView = new GridView();
            list.View = gridView;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Имя",
                DisplayMemberBinding = new Binding("Name")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Размер",
                DisplayMemberBinding = new Binding("Length")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Дата",
                DisplayMemberBinding = new Binding("CreationTime")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Тип",
                DisplayMemberBinding = new Binding("Type")
            });
            return list;
        }

        private void AddDrives(TreeViewItem root)
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady) // Проверяем готов ли диск
                {
                    DirectoryInfo newDrive = new DirectoryInfo(drive.RootDirectory.FullName);
                    TreeViewItem newItem = new TreeViewItem
                    {
                        Tag = newDrive,
                        Header = $"Диск {newDrive.Name}"
                    };
                    newItem.Expanded += TreeViewItemExpanded;
                    root.Items.Add(newItem);
                }
            }

        }

        private TreeView GetTreeViewForExplorer()
        {
            TreeView treeView = new TreeView();
            TreeViewItem root = new TreeViewItem
            {
                Header = "Этот компьютер"
            };
            TreeViewItem desktop = new TreeViewItem
            {
                Tag = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)),
                Header = "Рабочий стол"
            };
            TreeViewItem documents = new TreeViewItem
            {
                Header = "Мои документы",
                Tag = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Personal)),
            };
            documents.Expanded += TreeViewItemExpanded;
            desktop.Expanded += TreeViewItemExpanded;
            //root.Items.Add(); добавить "изображения" "документы" "мой компьютер" и "музыка"
            root.Items.Add(desktop);
            root.Items.Add(documents);
            root.Expanded += TreeViewItemExpanded;
            treeView.Items.Add(root);
            AddDrives(root);
            root.IsExpanded = true;
            root.IsSelected = true;
            return treeView;
        }

        private void BuildTree(string path, TreeViewItem node)
        {
            try
            {
                DirectoryInfo info = new DirectoryInfo(path);
                DirectoryInfo[] infoDir = info.GetDirectories();
                foreach (DirectoryInfo item in infoDir)
                {
                    TreeViewItem newItem = new TreeViewItem
                    {
                        Tag = item,
                        Header = item.Name,
                    };
                    node.Items.Add(newItem);
                    newItem.Expanded += TreeViewItemExpanded;
                }
            }
            catch { };
        }

        private void TreeViewItemExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem root = (TreeViewItem)sender;
            foreach (TreeViewItem item in root.Items)
            {
                if (item.Items.Count == 0)
                {
                    BuildTree(((DirectoryInfo)(item.Tag)).FullName, item);
                }
            }
            e.Handled = true;
        }

        private object GetLineForExplorer()
        {
            StackPanel line = new StackPanel
            {
                Height = 1,
                Background = (Brush)Application.Current.MainWindow.FindResource("SuperLightGray"),
            };
            return line;
        }

        private object GetSecondRowOfExplorer()
        {
            Grid secondRow = new Grid
            {
                Margin = new Thickness(1)
            };
            secondRow.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = GridLength.Auto,
            });
            secondRow.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = GridLength.Auto,
            });
            secondRow.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = GridLength.Auto,
            });
            secondRow.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            });
            secondRow.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = GridLength.Auto,
            });
            Button treeBtn = new Button
            {
                Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/plusR.png")),
                    Height = 16,
                    Width = 16,
                },
            };
            Grid.SetColumn(treeBtn, 0);
            secondRow.Children.Add(treeBtn);
            Button newFolderBtn = new Button
            {
                Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/plusR.png")),
                    Height = 16,
                    Width = 16,
                },
            };
            Grid.SetColumn(newFolderBtn, 1);
            secondRow.Children.Add(newFolderBtn);
            Button favoritsBtn = new Button
            {
                Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/plusR.png")),
                    Height = 16,
                    Width = 16,
                },
            };
            Grid.SetColumn(favoritsBtn, 2);
            secondRow.Children.Add(favoritsBtn);
            Button moreBtn = new Button
            {
                Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/plusR.png")),
                    Height = 16,
                    Width = 16,
                },
            };
            Grid.SetColumn(moreBtn, 4);
            secondRow.Children.Add(moreBtn);
            return secondRow;
        }

        private object GetFirstRowOfExplorer()
        {
            Grid firstRow = new Grid
            {
                Margin = new Thickness(1)
            };
            firstRow.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = GridLength.Auto,
            });
            firstRow.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = GridLength.Auto,
            });
            firstRow.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            });
            firstRow.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = GridLength.Auto,
            });
            firstRow.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = GridLength.Auto,
            });
            Button backBtn = new Button
            {
                Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/plusR.png")),
                    Height = 18,
                    Width = 18,
                },
            };
            Grid.SetColumn(backBtn, 0);
            firstRow.Children.Add(backBtn);
            Button forwardBtn = new Button
            {
                Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/plusR.png")),
                    Height = 18,
                    Width = 18,
                },
            };
            Grid.SetColumn(forwardBtn, 1);
            firstRow.Children.Add(forwardBtn);
            Button favoriteBtn = new Button
            {
                Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/plusR.png")),
                    Height = 18,
                    Width = 18,
                },
            };
            Grid.SetColumn(favoriteBtn, 3);
            firstRow.Children.Add(favoriteBtn);
            Button refreshBtn = new Button
            {
                Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/plusR.png")),
                    Height = 18,
                    Width = 18,
                },
            };
            Grid.SetColumn(refreshBtn, 4);
            firstRow.Children.Add(refreshBtn);
            return firstRow;
        }

        private void ThisComputerText_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            TabControl tabControl;
            if (btn.Name == "leftComputer")
            {
                tabControl = leftTab;
            }
            else
            {
                tabControl = rightTab;
            }
            TabItem tabItem = new TabItem();
            ///headerPanel.Children.Add(close);
            tabItem.Header = GetHeaderForMyComputer();
            tabItem.Name = $"tab{i}";
            i++;
            tabItem.Style = (Style)FindResource("tabsStyle");
            tabItem.Content = GetExplorer();
            tabControl.Items.Add(tabItem);
            tabControl.SelectedItem = tabItem;
        }

    }
}
