﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Interpolator
{
   internal sealed class EqualityToVisibilityConverter : IValueConverter
   {
      public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
      {
         return value.Equals( parameter ) ? Visibility.Visible : Visibility.Collapsed;
      }

      public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
      {
         throw new NotImplementedException();
      }
   }
}
