﻿using System;
using System.IO;
using System.Threading;
using System.Windows;
using Encoder.Filters;
using Encoder.Utils;

namespace Encoder.Encoding
{
   internal sealed class EncodingTaskViewModel : ViewModelBase, IDisposable
   {
      private readonly Filter _filter;
      private double _sourceFrameRate;
      private TimeSpan _sourceDuration;
      public CancellationTokenSource CancelToken { get; }

      public EncodingTaskViewModel( string sourceFile, Filter filter )
      {
         SourceFile = sourceFile;
         _filter = filter;

         CancelToken = new CancellationTokenSource();
      }

      public void Dispose()
      {
         CancelToken?.Dispose();
      }

      private bool ShouldApplyFilter() => _filter != null && _filter.ShouldApplyFilter();

      public string GetEncodingArguments() => ShouldApplyFilter() ? FilterArgumentBuilder.GetFilterArguments( _filter ) : null;

      public bool Initialize()
      {
         bool success = VideoMetadataReader.GetVideoInfo( SourceFile, out var sourceFrameRate, out var sourceDuration );
         if ( !success )
         {
            MessageBox.Show( $"Could not read video file: {SourceFile}" );
            return false;
         }

         _sourceFrameRate = sourceFrameRate;
         _sourceDuration = sourceDuration;
         TargetFile = Path.Combine( Path.GetDirectoryName( SourceFile ), Path.GetFileNameWithoutExtension( SourceFile ) + $"_done.mp4" );

         _filter?.Initialize( sourceFrameRate, sourceDuration );
         
         OnPropertyChanged( null );

         return true;
      }

      public string FilterName => ShouldApplyFilter() ? _filter.FilterName : "None";
      public string SourceFile { get; }
      public string FileName => Path.GetFileName( SourceFile );
      public bool HasNoDurationData => _sourceDuration == TimeSpan.Zero && !Finished;

      public string TargetFile { get; private set; }

      public int TargetTotalFrames => ShouldApplyFilter() ? _filter.GetTargetFrameCount() : (int)( _sourceDuration.TotalSeconds * _sourceFrameRate );

      public int CpuUsage { get; set; }

      private int _framesDone;
      public int FramesDone
      {
         get => _framesDone;
         set
         {
            if ( SetProperty( ref _framesDone, value ) )
            {
               if ( TargetTotalFrames != 0 )
               {
                  Progress = Math.Round( value / (double)TargetTotalFrames * 100, 2 );
               }
            }
         }
      }

      private double _progress;
      public double Progress
      {
         get => _progress;
         private set => SetProperty( ref _progress, value );
      }

      private bool _finished;
      public bool Finished
      {
         get => _finished;
         set
         {
            if ( SetProperty( ref _finished, value ) )
            {
               OnPropertyChanged( nameof( HasNoDurationData ) );
               if ( value )
               {
                  FramesDone = TargetTotalFrames;
                  Progress = 100;
               }
            }
         }
      }

      private bool _started;
      public bool Started
      {
         get => _started;
         set => SetProperty( ref _started, value );
      }
   }
}