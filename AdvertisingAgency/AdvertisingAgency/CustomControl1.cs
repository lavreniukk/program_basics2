using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AdvertisingAgency
{
    public class Menu : Control
    {
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen",
            typeof(bool), typeof(Menu), new PropertyMetadata(false, OnIsOpenPropertyChanged));

        private static void OnIsOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Menu menu)
            {
                menu.OnIsOpenPropertyChanged();
            }
        }

        private void OnIsOpenPropertyChanged()
        {
            if(IsOpen)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }
        }

        private void OpenMenu()
        {
            DoubleAnimation openMenu = new DoubleAnimation(225, new TimeSpan(0, 0, 0, 0, 300));
            BeginAnimation(WidthProperty, openMenu);
        }
         
        private void CloseMenu()
        {
            DoubleAnimation closeMenu = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 300));
            BeginAnimation(WidthProperty, closeMenu);
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content",
            typeof(object), typeof(Menu));

        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        static Menu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Menu), new FrameworkPropertyMetadata(typeof(Menu)));
        }

        public Menu()
        {
            Width = 0;
        }

    }
}
