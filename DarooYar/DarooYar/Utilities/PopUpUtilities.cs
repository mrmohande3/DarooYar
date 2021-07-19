using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DarooYar.Models;
using DarooYar.Views.PopUps;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace DarooYar.Utilities
{
    public class PopUpUtilities
    {
        private static PopUpUtilities _instance;

        public static PopUpUtilities Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PopUpUtilities();
                return _instance;
            }
        }

        public async Task PopAsync()
        {
            if (PopupNavigation.Instance.PopupStack.Count > 0)
                await PopupNavigation.Instance.PopAllAsync();
        }

        public async Task PushAsync(View content, bool closeWithClickBack = true)
        {
            await PopAsync();
            await PopupNavigation.Instance.PushAsync(new PopupPage
            {
                Content = content,
                CloseWhenBackgroundIsClicked = closeWithClickBack
            });
        }

        public async void PushIndicator()
        {
            await PopAsync();
            var indicator = new Frame
            {
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                CornerRadius = 100,
                Content = new ActivityIndicator
                {
                    Color = (Color)Application.Current.Resources["PrimaryColor"],
                    WidthRequest = 70,
                    HeightRequest = 70,
                    IsRunning = true,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                }
            };
            await PushAsync(indicator, false);
        }
        public async void PushSuccess(string message = null)
        {
            await PopAsync();
            if (message == null)
                await PushAsync(new SuccessPopUp());
            else
                await PushAsync(new SuccessPopUp(message));
        }
        public async void PushError(string message)
        {
            await PopAsync();
            await PushAsync(new ErrorPopUp(message));
        }

        public void LockPage(ContentPage page, string message = null)
        {
            Grid mainGrid = new Grid();
            mainGrid.Children.Add(page.Content);

            Frame mainFrame = new Frame
            {
                BackgroundColor = Color.FromHex("#60444444"),
                Content = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new Label
                        {
                            Text = "اجازه دسترسی به این بخش را ندارید",
                            HorizontalOptions = LayoutOptions.Center,
                        },
                        new Label
                        {
                            Text = message,
                            HorizontalOptions = LayoutOptions.Center
                        }
                    }
                }
            };
            page.Content = mainGrid;

        }
        public void LockPage(ContentView view, string message = null)
        {
            Grid mainGrid = new Grid();
            mainGrid.Children.Add(view.Content);

            Frame mainFrame = new Frame
            {
                BackgroundColor = Color.FromHex("#99444444"),
                CornerRadius = 5,
                Content = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new Label
                        {
                            Text = FaIconFont.DoorClosed,
                            FontSize = 60,
                            TextColor = Color.OrangeRed,
                            FontFamily = "fa-regular-400.ttf#Font Awesome 5 Pro Reqular",
                            HorizontalOptions = LayoutOptions.Center,
                        },
                        new Label
                        {
                            Text = "اجازه دسترسی به این بخش را ندارید",
                            FontSize = 18,
                            TextColor = Color.White,
                            HorizontalOptions = LayoutOptions.Center,
                        },
                        new Label
                        {
                            Text = message,
                            TextColor = Color.White,
                            HorizontalTextAlignment = TextAlignment.Center,
                            HorizontalOptions = LayoutOptions.Center
                        }
                    }
                }
            };
            mainGrid.Children.Add(mainFrame);
            view.Content = mainGrid;

        }
    }
}
