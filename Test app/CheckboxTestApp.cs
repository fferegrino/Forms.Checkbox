﻿using System;

using Xamarin.Forms;
using Messier16.Forms.Controls;
using System.Linq;

namespace CheckboxTestApp
{
    public class App : Application
    {
        public App()
        {

			var cb1 = new Checkbox() { HeightRequest = 23, WidthRequest = 23, StyleId = "check1" } ;
            var cb2 = new Checkbox() { HeightRequest = 23, WidthRequest = 23, IsEnabled = false, StyleId = "check2"};
            var cb3 = new Checkbox() { HeightRequest = 23, WidthRequest = 23, Checked = true , StyleId = "check3"};
			var cb4 = new Checkbox() { HeightRequest = 23, WidthRequest = 23 };

            cb1.CheckedChanged += 
                (sender, e) =>
            { 
                    cb2.IsEnabled = e.IsChecked;
                    cb3.Checked = !cb1.Checked;
            };

            cb2.CheckedChanged +=
                (sender, e) => cb4.IsVisible = e.IsChecked;

            var items = Enumerable.Range(0, 30)
                .Select(i => new { 
                    Text = "Item " + i,
                    Selectable = i % 2 == 0,
                    Visible = i % 2 == 0
                });
            
            var selectableList = new ListView(){
                ItemsSource = items
            };

            selectableList.ItemTemplate = new DataTemplate(typeof(SelectableCell));
            selectableList.ItemTemplate.SetBinding(SelectableCell.TextProperty, "Text");
            selectableList.ItemTemplate.SetBinding(SelectableCell.CheckboxEnabledProperty, "Selectable");
            selectableList.ItemTemplate.SetBinding(SelectableCell.CheckboxVisibleProperty, "Visible");
#if DEBUG
            selectableList.ItemTemplate.SetBinding(SelectableCell.TestProperty, "Text");
#endif

            var checkboxes = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                    Orientation = StackOrientation.Horizontal,
                Children =
                {
                    cb1,
                    cb2,
                    cb3,
                    cb4
                }
            };

            // The root page of your application
            MainPage = new NavigationPage( new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        checkboxes,
                        selectableList
                    }
                }
                });
        }

    }
}

