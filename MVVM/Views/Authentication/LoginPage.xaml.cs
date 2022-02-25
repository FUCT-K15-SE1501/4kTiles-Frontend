using _4kTiles_Frontend.MVVM.Views.Authentication;
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
  /// Interaction logic for Login_Page.xaml
  /// </summary>
  public partial class LoginPage : Window
  {


    public LoginPage()
    {
      InitializeComponent();
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {

    }

    private void ForgotPasswordBtn(object sender, RoutedEventArgs e)
    {

    }

    private void LoginBtn_Click(object sender, RoutedEventArgs e)
    {
      Trace.TraceError("Error!");
      Trace.TraceInformation("sadad");
      Trace.TraceWarning("Warning!");
    }

    private void Close_Window(object sender, MouseButtonEventArgs e)
    {
      this.Close();
    }

    private void Minimize_Window(object sender, MouseButtonEventArgs e)
    {
      this.WindowState = WindowState.Minimized;
    }

    private void SignUp_Click(object sender, RoutedEventArgs e)
    {
      RegisterPage register = new RegisterPage();
      register.Show();
      this.Close();
    }


    private void LoginUsername_GotFocus(object sender, RoutedEventArgs e)
    {
      if (usernameBox.Text == "Username")
      {
        usernameBox.Clear();
      }
    }

    private void LoginPwd_GotFocus(object sender, RoutedEventArgs e)
    {
      if(pwdBox.Password == "Password")
      {
        pwdBox.Clear();
      }
    }
  }
}
