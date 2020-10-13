using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Collections;

namespace lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string left;
        string right;
        List<DirectoryInfo> directories_left;
        List<FileInfo> files_left;
        List<DirectoryInfo> directories_right;
        List<FileInfo> files_right;

        public MainWindow()
        {
            InitializeComponent();
            Drives_left.ItemsSource = new List<DriveInfo>(DriveInfo.GetDrives());
            Drives_right.ItemsSource = new List<DriveInfo>(DriveInfo.GetDrives());
            if (!Drives_left.Items.IsEmpty) Drives_left.SelectedItem = Drives_left.Items[0];
            if (!Drives_right.Items.IsEmpty) Drives_right.SelectedItem = Drives_right.Items[0];
            left = Drives_left.Items.CurrentItem.ToString();
            right = Drives_right.Items.CurrentItem.ToString();
            Update();
        }

        private void InitializeDirectory()
        {
            directories_left = new List<DirectoryInfo>(new DirectoryInfo(left).GetDirectories());
            files_left = new List<FileInfo>(new DirectoryInfo(left).GetFiles());
            directories_right = new List<DirectoryInfo>(new DirectoryInfo(right).GetDirectories());
            files_right = new List<FileInfo>(new DirectoryInfo(right).GetFiles());
        }

        private void Update()
        {
            List_left.Items.Clear();
            List_right.Items.Clear();
            InitializeDirectory();
            foreach (DirectoryInfo dir in directories_left) List_left.Items.Add(dir);
            foreach (FileInfo file in files_left) List_left.Items.Add(file);
            foreach (DirectoryInfo dir in directories_right) List_right.Items.Add(dir);
            foreach (FileInfo file in files_right) List_right.Items.Add(file);
        }

        private void List_left_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (List_left.SelectedItem is DirectoryInfo)
            {
                DirectoryInfo info = (DirectoryInfo)List_left.SelectedItem;
                left = info.FullName;
            }
            if (List_left.SelectedItem is FileInfo)
            {
                FileInfo info = (FileInfo)List_left.SelectedItem;
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = info.FullName;
                p.Start();
            }
            Update();
        }

        private void List_right_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (List_right.SelectedItem is DirectoryInfo)
            {
                DirectoryInfo info = (DirectoryInfo)List_right.SelectedItem;
                right = info.FullName;
            }
            if (List_right.SelectedItem is FileInfo)
            {
                FileInfo info = (FileInfo)List_right.SelectedItem;
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = info.FullName;
                p.Start();
            }
            Update();
        }

        private void Left_button_Click(object sender, RoutedEventArgs e)
        {
            List<string> lst = new List<string>(left.Split('\\'));
            left = left.TrimEnd((lst[lst.Capacity - 1]).ToCharArray());
            bool Contains = false;
            foreach(DriveInfo drive in Drives_left.ItemsSource)
            {
                if (drive.Name.Contains(left))
                {
                    Contains = true;
                    break;
                }
            }
            if (!Contains) left = left.TrimEnd('\\');
            Update();
        }

        private void Right_button_Click(object sender, RoutedEventArgs e)
        {
            List<string> lst = new List<string>(right.Split('\\'));
            right = right.TrimEnd((lst[lst.Capacity - 1]).ToCharArray());
            bool Contains = false;
            foreach (DriveInfo drive in Drives_right.ItemsSource)
            {
                if (drive.Name.Contains(right))
                {
                    Contains = true;
                    break;
                }
            }
            if (!Contains) right = right.TrimEnd('\\');
            Update();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if(List_left.SelectedItem != null)
            {
                if (List_left.SelectedItem is DirectoryInfo)
                {
                    try
                    {
                        DirectoryInfo info = (DirectoryInfo)List_left.SelectedItem;
                        Directory.Delete(info.FullName, true);
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}\nFolder is not empty", "Error", MessageBoxButton.OK);
                    }
                }
                if (List_left.SelectedItem is FileInfo)
                {
                    FileInfo info = (FileInfo)List_left.SelectedItem;
                    File.Delete(info.FullName);
                }
            }
            if(List_right.SelectedItem != null)
            {
                if (List_right.SelectedItem is DirectoryInfo)
                {
                    try
                    {
                        DirectoryInfo info = (DirectoryInfo)List_right.SelectedItem;
                        Directory.Delete(info.FullName, true);
                    }
                    catch(System.Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}\nFolder is not empty", "Error", MessageBoxButton.OK);
                    }
                }
                if (List_right.SelectedItem is FileInfo)
                {
                    FileInfo info = (FileInfo)List_right.SelectedItem;
                    File.Delete(info.FullName);
                }
            }
            Update();
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            if (List_left.SelectedItem != null)
            {
                if (List_left.SelectedItem is DirectoryInfo)
                {
                    DirectoryInfo info = (DirectoryInfo)List_left.SelectedItem;
                    MessageBox.Show("Can't copy the folder", "Error", MessageBoxButton.OK);
                }
                if (List_left.SelectedItem is FileInfo)
                {
                    FileInfo info = (FileInfo)List_left.SelectedItem;
                    string path = right + '\\' + info.Name.ToString();
                    if (!File.Exists(right))
                    {
                        var fl = File.Create(path);
                        fl.Close();
                    }
                    info.CopyTo(path, true);
                }
            }
            if (List_right.SelectedItem != null)
            {
                if (List_right.SelectedItem is DirectoryInfo)
                {
                    DirectoryInfo info = (DirectoryInfo)List_right.SelectedItem;
                    MessageBox.Show("Can't copy the folder", "Error", MessageBoxButton.OK);
                }
                if (List_right.SelectedItem is FileInfo)
                {
                    FileInfo info = (FileInfo)List_right.SelectedItem;
                    string path = left + '\\' + info.Name.ToString();
                    if (!File.Exists(left))
                    {
                        var fl = File.Create(path);
                        fl.Close();
                    }
                    info.CopyTo(path, true);
                }
            }
            Update();
        }

        private void Replace_Click(object sender, RoutedEventArgs e)
        {
            if (List_left.SelectedItem != null)
            {
                if (List_left.SelectedItem is DirectoryInfo)
                {
                    DirectoryInfo info = (DirectoryInfo)List_left.SelectedItem;
                    if (!Directory.Exists(right + '\\' + info.Name.ToString()))
                    {
                        info.MoveTo(right + '\\' + info.Name.ToString());
                    }
                }
                if (List_left.SelectedItem is FileInfo)
                {
                    FileInfo info = (FileInfo)List_left.SelectedItem;
                    if (!File.Exists(right + '\\' + info.Name.ToString()))
                    {
                        info.MoveTo(right + '\\' + info.Name.ToString());
                    }
                }
            }
            if (List_right.SelectedItem != null)
            {
                if (List_right.SelectedItem is DirectoryInfo)
                {
                    DirectoryInfo info = (DirectoryInfo)List_right.SelectedItem;
                    if (!Directory.Exists(left + '\\' + info.Name.ToString()))
                    {
                        info.MoveTo(left + '\\' + info.Name.ToString());
                    }
                }
                if (List_right.SelectedItem is FileInfo)
                {
                    FileInfo info = (FileInfo)List_right.SelectedItem;
                    if (!File.Exists(left + '\\' + info.Name.ToString()))
                    {
                        info.MoveTo(left + '\\' + info.Name.ToString());
                    }
                }
            }
            Update();
        }

        private void List_left_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            List_right.SelectedItem = null;
        }

        private void List_right_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            List_left.SelectedItem = null;
        }

        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            if (List_left.SelectedItem != null)
            {
                if (List_left.SelectedItem is DirectoryInfo)
                {
                    InputBox input = new InputBox();
                    input.Owner = this;
                    input.NewName.Text = List_left.SelectedItem.ToString();
                    input.ShowDialog();
                    if (input.DialogResult == true)
                    {
                        string path = left + '\\' + input.NewName.Text;
                        if (!Directory.Exists(path))
                        {
                            ((DirectoryInfo)List_left.SelectedItem).MoveTo(path);
                        }
                    }
                }
                if (List_left.SelectedItem is FileInfo)
                {
                    InputBox input = new InputBox();
                    input.Owner = this;
                    input.NewName.Text = ((FileInfo)List_left.SelectedItem).Name.TrimEnd(((FileInfo)List_left.SelectedItem).Extension.ToCharArray());
                    input.ShowDialog();
                    if (input.DialogResult == true)
                    {
                        string path = left + '\\' + input.NewName.Text + ((FileInfo)List_left.SelectedItem).Extension;
                        if (!File.Exists(path))
                        {
                            ((FileInfo)List_left.SelectedItem).MoveTo(path);
                        }
                    }
                }
            }
            if (List_right.SelectedItem != null)
            {
                if (List_right.SelectedItem is DirectoryInfo)
                {
                    InputBox input = new InputBox();
                    input.Owner = this;
                    input.NewName.Text = List_right.SelectedItem.ToString();
                    input.ShowDialog();
                    if (input.DialogResult == true)
                    {
                        string path = right + '\\' + input.NewName.Text;
                        if (!Directory.Exists(path))
                        {
                            ((DirectoryInfo)List_right.SelectedItem).MoveTo(path);
                        }
                    }
                }
                if (List_right.SelectedItem is FileInfo)
                {
                    InputBox input = new InputBox();
                    input.Owner = this;
                    input.NewName.Text = ((FileInfo)List_right.SelectedItem).Name.TrimEnd(((FileInfo)List_right.SelectedItem).Extension.ToCharArray());
                    input.ShowDialog();
                    if (input.DialogResult == true)
                    {
                        string path = right + '\\' + input.NewName.Text + ((FileInfo)List_right.SelectedItem).Extension;
                        if (!File.Exists(path))
                        {
                            ((FileInfo)List_right.SelectedItem).MoveTo(path);
                        }
                    }
                }
            }
            Update();
        }

        private void ReadFromFile_Click(object sender, RoutedEventArgs e)
        {
            if (List_left.SelectedItem != null)
            {
                if (List_left.SelectedItem is DirectoryInfo)
                {
                    MessageBox.Show(this, "Can't read from a folder", "Error", MessageBoxButton.OK);
                }
                if (List_left.SelectedItem is FileInfo)
                {
                    FileInfo info = (FileInfo)List_left.SelectedItem;
                    if(File.Exists(info.FullName))
                    {
                        List<Human> lst = new List<Human>();
                        bool extValid = false;
                        if (info.Extension ==".txt")
                        {
                            StreamReader stream = new StreamReader(info.FullName);
                            string str;
                            for (int i = 0; (str = stream.ReadLine()) != null; i++)
                            {
                                string[] data = str.Split(' ');
                                lst[i] = new Human(data[0], data[1], System.Convert.ToInt32(data[2]));
                            }
                            extValid = true;
                        }
                        else if(info.Extension == ".bin")
                        {
                            BinaryReader binary = new BinaryReader(File.Open(info.FullName, FileMode.Open));
                            for (int i = 0; binary.PeekChar()>-1; i++)
                            {
                                lst[i] = new Human(binary.ReadString(), binary.ReadString(), binary.ReadInt32());
                            }
                            extValid = true;
                        }
                        else
                        {
                            MessageBox.Show(this, $"Can't read from a file with extension {info.Extension}", "Error", MessageBoxButton.OK);
                        }
                        if(extValid && lst.Count != 0)
                        {
                            InformationBox box = new InformationBox();
                            box.Surname.Text = lst[0].Surname;
                            box.Name.Text = lst[0].Name;
                            box.Age.Text = lst[0].Age.ToString();
                            box.ShowDialog();
                        }
                        else MessageBox.Show(this, $"Something gone wrong. Probably file is empty", "Error", MessageBoxButton.OK);
                    }
                }
            }
            if (List_right.SelectedItem != null)
            {
                if (List_right.SelectedItem is DirectoryInfo)
                {
                    MessageBox.Show("Can't read from a folder", "Error", MessageBoxButton.OK);
                }
                if (List_right.SelectedItem is FileInfo)
                {
                    FileInfo info = (FileInfo)List_right.SelectedItem;
                    if (File.Exists(info.FullName))
                    {
                        List<Human> lst = new List<Human>();
                        bool extValid = false;

                        if (info.Extension == ".txt")
                        {
                            StreamReader stream = new StreamReader(info.FullName);
                            string str;
                            for (int i = 0; (str = stream.ReadLine()) != null; i++)
                            {
                                string[] data = str.Split(' ');
                                lst.Add(new Human(data[0], data[1], System.Convert.ToInt32(data[2])));
                            }
                            extValid = true;
                        }
                        else if (info.Extension == ".bin")
                        {
                            BinaryReader binary = new BinaryReader(File.Open(info.FullName, FileMode.Open));
                            for (int i = 0; binary.PeekChar() > -1; i++)
                            {
                                lst.Add(new Human(binary.ReadString(), binary.ReadString(), binary.ReadInt32()));
                            }
                            extValid = true;
                        }
                        else
                        {
                            MessageBox.Show(this, $"Can't read from a file with extension {info.Extension}", "Error", MessageBoxButton.OK);
                        }
                        if (extValid && lst.Count != 0)
                        {
                            InformationBox box = new InformationBox();
                            box.Surname.Text = lst[0].Surname;
                            box.Name.Text = lst[0].Name;
                            box.Age.Text = lst[0].Age.ToString();
                            box.ShowDialog();
                        } 
                        else MessageBox.Show(this, $"Something gone wrong. Probably file is empty", "Error", MessageBoxButton.OK);
                    }
                }
            }
            Update();
        }

        private void WriteInFile_Click(object sender, RoutedEventArgs e)
        {
            if (List_left.SelectedItem != null)
            {
                if (List_left.SelectedItem is DirectoryInfo)
                {
                    MessageBox.Show(this, "Can't write in a folder", "Error", MessageBoxButton.OK);
                }
                if (List_left.SelectedItem is FileInfo)
                {
                    FileInfo info = (FileInfo)List_left.SelectedItem;
                    if (File.Exists(info.FullName))
                    {
                        string surname;
                        string name;
                        int age;

                        InformationBox box = new InformationBox();
                        box.Owner = this;
                        box.ShowDialog();
                        if (box.DialogResult == true)
                        {
                            surname = box.Surname.Text;
                            name = box.Name.Text;
                            age = System.Convert.ToInt32(box.Age.Text);
                            if (info.Extension == ".txt")
                            {
                                StreamWriter stream = new StreamWriter(info.FullName);
                                Human human = new Human(surname, name, age);
                                stream.WriteLine(human.Surname + " " + human.Name + " " + human.Age.ToString());
                                stream.Close();
                            }
                            else if (info.Extension == ".bin")
                            {
                                BinaryWriter binary = new BinaryWriter(File.Open(info.FullName, FileMode.Open));
                                Human human = new Human(surname, name, age);
                                binary.Write(human.Surname);
                                binary.Write(human.Name);
                                binary.Write(human.Age);
                            }
                            else
                            {
                                MessageBox.Show(this, $"Can't write in a file with extension {info.Extension}", "Error", MessageBoxButton.OK);
                            }
                        }
                    }
                }
            }
            if (List_right.SelectedItem != null)
            {
                if (List_right.SelectedItem is DirectoryInfo)
                {
                    MessageBox.Show("Can't write in a folder", "Error", MessageBoxButton.OK);
                }
                if (List_right.SelectedItem is FileInfo)
                {
                    FileInfo info = (FileInfo)List_right.SelectedItem;
                    if (File.Exists(info.FullName))
                    {
                        string surname;
                        string name;
                        int age;

                        InformationBox box = new InformationBox();
                        box.Owner = this;
                        box.ShowDialog();
                        if (box.DialogResult == true)
                        {
                            surname = box.Surname.Text;
                            name = box.Name.Text;
                            age = System.Convert.ToInt32(box.Age.Text);
                            if (info.Extension == ".txt")
                            {
                                StreamWriter stream = new StreamWriter(info.FullName);
                                Human human = new Human(surname, name, age);
                                stream.WriteLine(human.Surname + " " + human.Name + " " + human.Age.ToString());
                                stream.Close();
                            }
                            else if (info.Extension == ".bin")
                            {
                                BinaryWriter binary = new BinaryWriter(File.Open(info.FullName, FileMode.Open));
                                Human human = new Human(surname, name, age);
                                binary.Write(human.Surname);
                                binary.Write(human.Name);
                                binary.Write(human.Age);
                            }
                            else
                            {
                                MessageBox.Show(this, $"Can't write in a file with extension {info.Extension}", "Error", MessageBoxButton.OK);
                            }
                        }
                    }
                }
            }
            Update();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (List_left.SelectedItem != null)
            {
                if (List_left.SelectedItem is DirectoryInfo || List_left.SelectedItem is FileInfo)
                {
                    InputBox input = new InputBox();
                    input.Owner = this;
                    input.ShowDialog();
                    if (input.DialogResult == true)
                    {
                        string path = left + '\\' + input.NewName.Text;
                        if (!File.Exists(path))
                        {
                            File.Create(path);
                        }
                        else
                        {
                            MessageBox.Show(this, $"File with name {path} already exists", "Error");
                        }
                    }
                }
            }
            if (List_right.SelectedItem != null)
            {
                if (List_right.SelectedItem is DirectoryInfo || List_right.SelectedItem is FileInfo)
                {
                    InputBox input = new InputBox();
                    input.Owner = this;
                    input.ShowDialog();
                    if (input.DialogResult == true)
                    {
                        string path = right + '\\' + input.NewName.Text;
                        if (!File.Exists(path))
                        {
                            File.Create(path);
                        }
                        else
                        {
                            MessageBox.Show(this, $"File with name {path} already exists", "Error");
                        }
                    }
                }
            }
            Update();
        }
    }


}
