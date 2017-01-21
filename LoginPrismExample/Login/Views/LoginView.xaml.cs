using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
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
using LoginPrismExample.Login.ViewModels;

namespace LoginPrismExample.Login.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    [Export(typeof(LoginView))]
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        [Import]
        [SuppressMessage("Microsoft.Design", "CA1044:PropertiesShouldNotBeWriteOnly",
    Justification = "Needs to be a property to be composed by MEF")]
        private LoginViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}
