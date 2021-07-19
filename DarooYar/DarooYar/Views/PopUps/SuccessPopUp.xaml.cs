using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using DarooYar.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DarooYar.Views.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuccessPopUp : ContentView
    {
        public SuccessPopUp()
        {
            InitializeComponent();
            Timer timer = new Timer(3000);
            timer.Start();
            timer.Elapsed += (async (sender, e) =>
            {
                await PopUpUtilities.Instance.PopAsync();
                timer.Close();
            });
        }
        public SuccessPopUp(string message)
        {
            InitializeComponent();
            messageLabel.Text = message;
            Timer timer = new Timer(3000);
            timer.Start();
            timer.Elapsed += (async (sender, e) =>
            {
                await PopUpUtilities.Instance.PopAsync();
                timer.Close();
            });
        }
    }
}