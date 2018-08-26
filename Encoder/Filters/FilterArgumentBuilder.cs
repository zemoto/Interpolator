﻿using System;
using System.Linq;
using Encoder.Utils;

namespace Encoder.Filters
{
   internal static class FilterArgumentBuilder
   {
      public static string GetFilterArguments( Filter filter )
      {
         var filterString = "-filter:v ";

         var filterName = filter.ViewModel.GetType().GetAttribute<FilterAttribute>().FilterName;
         filterString += $"\"{filterName}='";

         var filterProperties = filter.ViewModel.GetType().GetProperties();
         foreach( var property in filterProperties )
         {
            var filterParamName = property.GetAttribute<FilterParameterNameAttribute>().ParameterName;

            string filterParamValue;
            var propertyValue = property.GetValue( filter.ViewModel );
            if ( propertyValue is Enum )
            {
               var enumMember = propertyValue.GetType().GetMember( propertyValue.ToString() ).First();
               filterParamValue = enumMember.GetAttribute<FilterParameterValueAttribute>().ParameterValue;
            }
            else if ( propertyValue is bool boolValue )
            {
               filterParamValue = boolValue ? "1" : "0";
            }
            else
            {
               filterParamValue = propertyValue.ToString();
            }

            filterString += $"{filterParamName}={filterParamValue}:";
         }

         filterString = filterString.TrimEnd( ':' );
         filterString += "'\"";

         return filterString;
      }
   }
}