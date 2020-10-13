using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

namespace lab1
{
    /// <summary>
    /// Interaction logic for InformationBox.xaml
    /// </summary>
    public partial class InformationBox : Window
    {
        public InformationBox()
        {
            InitializeComponent();
        }

        Regex rx = new Regex(@"^[1-9]\d{0,2}$");

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Surname.Text == "")
            {
                MessageBox.Show("Enter surname", "Error", MessageBoxButton.OK);
                return;
            }

            if (this.Name.Text == "")
            {
                MessageBox.Show("Enter name", "Error", MessageBoxButton.OK);
                return;
            }

            if (this.Age.Text == "")
            {
                //write only numbers, use regex
                MessageBox.Show("Enter age", "Error", MessageBoxButton.OK);
                return;
            }

            if(!rx.IsMatch(this.Age.Text))
            {
                MessageBox.Show("Age must contain only numbers", "Error", MessageBoxButton.OK);
                return;
            }
            DialogResult = true;
        }
    }
}
