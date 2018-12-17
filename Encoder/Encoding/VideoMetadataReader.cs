﻿using System;
using System.Diagnostics;
using Encoder.Utils;

namespace Encoder.Encoding
{
   internal static class VideoMetadataReader
   {
      private static readonly string FfprobeExeLocation;

      private static string VideoInfoArgs( string fileName ) => $"-v error -select_streams v:0 -of default=noprint_wrappers=1:nokey=1 -show_entries stream=r_frame_rate,duration \"{fileName}\"";

      static VideoMetadataReader()
      {
         FfprobeExeLocation = EmbeddedFfmpegManager.GetFfprobeExecutableFilePath();
      }

      public static bool GetVideoInfo( string file, out double frameRate, out TimeSpan duration )
      {
         duration = new TimeSpan();
         frameRate = 0;

         var process = new Process
         {
            StartInfo = new ProcessStartInfo
            {
               FileName = FfprobeExeLocation,
               Arguments = VideoInfoArgs( file ),
               UseShellExecute = false,
               RedirectStandardOutput = true,
               CreateNoWindow = true,
               WindowStyle = ProcessWindowStyle.Hidden
            }
         };

         process.StartAsChildProcess();
         process.WaitForExit();

         var output = process.StandardOutput.ReadToEnd();
         try
         {
            var values = output.Split( new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries );

            var fpsFraction = values[0].Split( '/' );
            frameRate = Math.Round( double.Parse( fpsFraction[0] ) / double.Parse( fpsFraction[1] ), 2 );

            if ( double.TryParse( values[1], out double secondsDuration ) )
            {
               duration = TimeSpan.FromSeconds( secondsDuration );
            }
            return true;
         }
         catch
         {
            return false;
         }
         finally
         {
            process.Dispose();
         }
      }
   }
}
