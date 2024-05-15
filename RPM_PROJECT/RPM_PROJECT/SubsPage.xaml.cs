﻿using RPM_PROJECT.api.HttpEntitie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPM_PROJECT.api;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RPM_PROJECT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SubsPage : ContentPage
	{
        StackLayout stackLayout = new StackLayout();
        StackLayout stackLayout2 = new StackLayout();
        StackLayout RealStack = new StackLayout();
        RelativeLayout relativeLayout = new RelativeLayout();
        protected override async void OnAppearing()
        {
            var subsribes = await API.GetAllSubscribe();
            foreach (Subsribe sub in subsribes)
            {
                stackLayout = new StackLayout
                {
                    Margin = new Thickness(50, 10),
                };
                RealStack = new StackLayout()
                {
                    BackgroundColor = Color.FromHex("#1263DE"),
                    Children = {
                        new Label
                    {
                        Margin = new Thickness(10, 40, 10, 70),
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        Text = sub.Name,
                        FontSize = 40
                    }
                }
                };

                relativeLayout = new RelativeLayout { HorizontalOptions = LayoutOptions.CenterAndExpand };

                relativeLayout.Children.Add(new Image()
                {
                    Source = "rectangularforsub.png",
                    Margin = new Thickness(35, 0),
                    IsVisible = false
                }, Constraint.Constant(0));
                relativeLayout.Children.Add(new Label()
                {
                    Text = sub.CountMonth.ToString(),
                    FontSize = 70,
                    TextColor = Color.White,
                    Margin = new Thickness(48, 5)
                }, Constraint.Constant(0));
                relativeLayout.Children.Add(new Label()
                {
                    Text = "Месяцев",
                    Margin = new Thickness(49, 95, 0, 50),
                    TextColor = Color.White,
                    FontSize = 60,
                    BackgroundColor = Color.FromHex("#1263DE"),
                    HorizontalTextAlignment = TextAlignment.Center,
                    WidthRequest = 80
                }, Constraint.Constant(1));

                RealStack.Children.Add(relativeLayout);
                RealStack.Children.Add(new Label
                {
                    Margin = new Thickness(10, 40, 10, 30),
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    TextColor = Color.White,
                    Text = sub.Title,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
                });

                stackLayout.Children.Add(RealStack);

                stackLayout2 = new StackLayout()
                {
                    Children =
                {

                    new Label
                        {
                            Text = sub.Description,  FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
                        },
                    new Button
                        {
                            Text = "Оформить подписку",
                            CornerRadius = 20,
                            HorizontalOptions = LayoutOptions.StartAndExpand,
                            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                            Margin = new Thickness(0, 10, 0, 0),
                            HeightRequest = 45,
                            TextColor= Color.White,
                            BackgroundColor= Color.FromHex("#1263DE")
                        }
                }
                };
                stackLayout.Children.Add(stackLayout2);

                scroll.Children.Add(stackLayout);
            }
            StackLayout stackLayout1 = new StackLayout
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                Children =
                {
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.Center,
                        Spacing = 20,
                        Children =
                        {
                            new Image { Source = "vk.png" },
                            new Image { Source = "tg.png" },
                            new Image { Source = "chviter.png" }
                        }
                    },
                    new Label
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        Text = "Мы всегда готовы вам помочь"
                    }
                }
            };
            scroll.Children.Add(stackLayout1);
            base.OnAppearing();
        }


        public SubsPage ()
		{
			InitializeComponent ();
            BurgerSlider.TranslateTo(-300, 0, 0);
            ProfileSlider.TranslateTo(300, 0, 0);


            // Отрисовка подписок



        }
        private void ClosePanel(object sender, EventArgs e)
        {
            if (BurgerSlider.TranslationX != -300)
            {
                BurgerSlider.TranslateTo(-300, 0, 450, Easing.SinOut);

            }
            if (ProfileSlider.TranslationX != 300)
            {
                ProfileSlider.TranslateTo(300, 0, 450, Easing.SinOut);
            }
        }
        private void OpenBurger(object sender, EventArgs e)
        {
            if (BurgerSlider.TranslationX == -300)
            {
                BurgerSlider.TranslateTo(0, 0, 450, Easing.CubicInOut);
            }
        }

        private void Profile(object sender, EventArgs e)
        {
            if (1 == 2) // Вошёл ли в аккаунт пользователь
            {
                Navigation.PushAsync(new RegPage());
            }
            else
            {
                ProfileSlider.TranslateTo(0, 0, 450, Easing.CubicInOut);
            }
        }
        private void Anime(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MoviesPage(2));
        }
        private void Movies(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MoviesPage(0));
        }
        private void Serials(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MoviesPage(1));
        }
        private void My(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MylistsPage());
        }
        private void Sub(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SubsPage());
        }
        private void Settings(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage());
        }

        private void ToMainPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void Exit(object sender, EventArgs e)
        {

        }
    }
}
