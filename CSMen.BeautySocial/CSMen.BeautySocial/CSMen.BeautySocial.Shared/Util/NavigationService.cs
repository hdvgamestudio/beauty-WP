using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace CSMen.BeautySocial.Util
{
    public class NavigationService
    {
        private static NavigationService m_Current;
        public static NavigationService Current
        {
            get
            {
                if (m_Current == null)
                    m_Current = new NavigationService();

                return m_Current;
            }
        }

        private NavigationService()
        {
        }

        public void Navigate(Type sourcePageType, object parameter, NavigationTransitionInfo infoOverride)
        {
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(sourcePageType, parameter, infoOverride);
        }

        public void Navigate(Type sourcePageType, object parameter)
        {
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(sourcePageType, parameter);
        }

        public void Navigate(Type sourcePageType)
        {
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(sourcePageType);
        }

        public void GoBack()
        {
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.GoBack();
        }
    }
}
