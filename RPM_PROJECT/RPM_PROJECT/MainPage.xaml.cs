﻿using RPM_PROJECT.api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using RPM_PROJECT.api;

namespace RPM_PROJECT
{
    public partial class MainPage : ContentPage
    {
        protected override async void OnAppearing()
        {
            if (Preferences.Get("isLogin", false))
            {
                var user = await API.GetUser();
                if (user != null)
                {
                    ava.IsVisible = false;
                    var path = await API.GetImageProfile(user.ImageUrl);
                    avaImage.IsVisible = true;
                    avaImage.Source = ImageSource.FromStream(() => path);
                    avaImage.Aspect = Aspect.AspectFill;
                    ProfileName.Text = user.Name;
                    ProfileEmail.Text = user.Email;
                    var path1 = await API.GetImageProfile(user.ImageUrl);
                    ProfileAva.Source = ImageSource.FromStream(() => path1);
                    ProfileAva.Aspect = Aspect.AspectFill;
                }
                else
                {
                    ava.IsVisible = true;
                }
            }
            StackLayout stackLayout = new StackLayout();
            var tapGestureRecognizer = new TapGestureRecognizer();
            var result = await API.GetAllMovie();
            foreach (var movie in result)// Третий скрол фильмы
            {
                var path = await API.GetImageProfile(movie.ImagePath);
                stackLayout = new StackLayout() { WidthRequest = 250, HeightRequest = 150 };
                tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += GoToPlayer;
                stackLayout.GestureRecognizers.Add(tapGestureRecognizer);
                stackLayout.Children.Add(new Image { Source = ImageSource.FromStream(() => path), Aspect = Aspect.AspectFill });
                stackLayout.Children.Add(new Label {  Text = movie.Id.ToString(), IsVisible=false });
                stackLayout.ClassId = movie.Id.ToString();
                movieContent.Children.Add(stackLayout);
            }
            var result1 = await API.GetAllAnime();
            foreach (var movie in result1)//Четвёртый скрол фильмы
            {
                var path = await API.GetImageProfile(movie.ImagePath);
                stackLayout = new StackLayout() { WidthRequest = 250, HeightRequest = 150 };
                tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += GoToPlayer;
                stackLayout.GestureRecognizers.Add(tapGestureRecognizer);
                stackLayout.Children.Add(new Image { Source = ImageSource.FromStream(() => path), Aspect = Aspect.AspectFill });
                stackLayout.Children.Add(new Label { Text = movie.Id.ToString(), IsVisible = false });
                stackLayout.ClassId = movie.Id.ToString();
                animeContent.Children.Add(stackLayout);
            }
            var result2 = await API.GetAllMovie();
            foreach (var movie in result2)
            {
                var path = await API.GetImageProfile(movie.ImagePath);
                stackLayout = new StackLayout() { WidthRequest = 250, HeightRequest = 150 };
                tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += GoToPlayer;
                stackLayout.GestureRecognizers.Add(tapGestureRecognizer);
                stackLayout.Children.Add(new Image { Source = ImageSource.FromStream(() => path), Aspect = Aspect.AspectFill });
                stackLayout.ClassId = movie.Id.ToString(); 
                historyContent.Children.Add(stackLayout);
            }
        }

        public MainPage()
        {
            InitializeComponent();
            BurgerSlider.TranslateTo(-300, 0, 0);
            ProfileSlider.TranslateTo(600, 0, 0);
        }
        private void ClosePanel(object sender, EventArgs e)
        {
            if (BurgerSlider.TranslationX != -300)
            {
                BurgerSlider.TranslateTo(-300, 0, 450, Easing.SinOut);
                
            }
            if (ProfileSlider.TranslationX != 600)
            {
               ProfileSlider.TranslateTo(600, 0, 450, Easing.SinOut);
            }
        }
        private void OpenBurger(object sender, EventArgs e)
        {
            if(BurgerSlider.TranslationX == - 300)
            {
                BurgerSlider.TranslateTo(0, 0, 450, Easing.CubicInOut);
            }
        }

        private void Profile(object sender, EventArgs e)
        {
            if (!Preferences.Get("isLogin", false)) // Вошёл ли в аккаунт пользователь
            {
                Navigation.PushAsync(new RegPage());
            }
            else
            {
                ProfileSlider.TranslateTo(0, 0, 450, Easing.CubicInOut);              
                movie.IsVisible = true;
                anime.IsVisible = true;
                history.IsVisible = true;
                
            }
        }

        private void GoToPlayer(object sender, EventArgs e)
        {
            
            Navigation.PushAsync(new WatchPage(int.Parse((sender as StackLayout).ClassId)));
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
            //
        }

        private void Exit(object sender, EventArgs e)
        {
            Preferences.Remove("isLogin");
            Preferences.Remove("token");
            ava.IsVisible = true;
            avaImage.IsVisible = false;
            ProfileSlider.TranslateTo(300, 0, 0);
        }
    }
}
