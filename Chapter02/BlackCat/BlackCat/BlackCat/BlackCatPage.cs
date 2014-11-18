﻿using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace BlackCat
{
    class BlackCatPage : ContentPage
    {
        public BlackCatPage()
        {
            StackLayout mainStack = new StackLayout();
            StackLayout textStack = new StackLayout
            {
                Padding = new Thickness(5),
                Spacing = 10
            };

            // Get access to the text resource.
            Assembly assembly = this.GetType().GetTypeInfo().Assembly;
            string resource = "BlackCat.Texts.TheBlackCat.txt";

            using (Stream stream = 
                        assembly.GetManifestResourceStream (resource)) 
            {
                using (StreamReader reader = new StreamReader (stream)) 
                {
                    bool gotTitle = false;
                    string line;

                    // Read in a line (which is actually a paragraph).
                    while (null != (line = reader.ReadLine())) 
                    {
                        Label label = new Label 
                        {
                            Text = line,

                            // Black text for ebooks!
                            TextColor = Color.Black
                        };

                        if (!gotTitle)
                        {
                            // Add first label (the title to mainStack.
                            label.HorizontalOptions = LayoutOptions.Center;
                            label.Font = Font.SystemFontOfSize(NamedSize.Medium, 
                                                                FontAttributes.Bold);
                            mainStack.Children.Add(label);
                            gotTitle = true;
                        }
                        else
                        {
                            // Add subsequent labels to textStack.
                            textStack.Children.Add(label);
                        }
                    }
                }
            }

            // Put the textStack in a ScrollView with FillAndExpand.
            ScrollView scrollView = new ScrollView
            {
                Content = textStack,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(5, 0),

                // Required for Android in Xamarin.Forms 1.2.3
                IsClippedToBounds = true
            };

            // Add the ScrollView as a second child of mainStack.
            mainStack.Children.Add(scrollView);

            // Set page content to mainStack.
            this.Content = mainStack;

            // White background for ebooks!
            this.BackgroundColor = Color.White;
    
            // Add some iOS padding for the page
            this.Padding = 
                new Thickness (0, Device.OnPlatform (20, 0, 0), 0, 0);
        }
    }
}

