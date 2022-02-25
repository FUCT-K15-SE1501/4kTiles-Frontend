using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace _4kTiles_Frontend.MVVM.Views.Authentication
{
  /// <summary>
  /// Interaction logic for Register_Page.xaml
  /// </summary>
  public partial class RegisterPage : Window
  {
    public RegisterPage()
    {
      InitializeComponent();
    }

    private void Minimize_Window(object sender, MouseButtonEventArgs e)
    {
      this.WindowState = WindowState.Minimized;
    }

    private void Close_Window(object sender, MouseButtonEventArgs e)
    {
      this.Close();
    }

    private void RegisBtn_Click(object sender, RoutedEventArgs e)
    {
      Trace.WriteLine("Registered!");
    }

    private void LoginPage_Click(object sender, RoutedEventArgs e)
    {
      LoginPage loginPage = new LoginPage();
      loginPage.Show();
      this.Close();
    }

    private void ConfirmPwd_GotFocus(object sender, RoutedEventArgs e)
    {
      if(confirmPwd.Password == "Password")
      {
        confirmPwd.Clear();
      } else if(confirmPwd.Password != pwdBox.Password)
      {
        Trace.WriteLine("Please confirm your password");
      }
    }

    private void Username_GotFocus(object sender, RoutedEventArgs e)
    {
      if(usernameBox.Text == "Username")
      {
        usernameBox.Clear();
      }
    }

    private void Email_GotFocus(object sender, RoutedEventArgs e)
    {
      if (emailBox.Text == "Email")
      {
        emailBox.Clear();
      }
    }

    private void Password_GotFocus(object sender, RoutedEventArgs e)
    {
      if (pwdBox.Password == "Password")
      {
        pwdBox.Clear();
      }
    }
  }
}
