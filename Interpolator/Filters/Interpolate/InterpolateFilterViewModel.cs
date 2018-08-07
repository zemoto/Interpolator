﻿namespace Interpolator.Filters.Interpolate
{
   internal sealed class InterpolateFilterViewModel : FilterViewModel
   {
      public double _targetFrameRate = 60;
      public double TargetFrameRate
      {
         get => _targetFrameRate;
         set => SetProperty( ref _targetFrameRate, value );
      }

      private InterpolationMode _interpolationMode = InterpolationMode.MotionCompensated;
      public InterpolationMode InterpolationMode
      {
         get => _interpolationMode;
         set => SetProperty( ref _interpolationMode, value );
      }

      private MotionCompensationMode _motionCompensationMode = MotionCompensationMode.Adaptive;
      public MotionCompensationMode MotionCompensationMode
      {
         get => _motionCompensationMode;
         set => SetProperty( ref _motionCompensationMode, value );
      }

      private MotionEstimationMode _motionEstimationMode = MotionEstimationMode.Bidirectional;
      public MotionEstimationMode MotionEstimationMode
      {
         get => _motionEstimationMode;
         set => SetProperty( ref _motionEstimationMode, value );
      }

      private MotionEstimationAlgorithm _motionEstimationAlgorithm = MotionEstimationAlgorithm.Predictive;
      public MotionEstimationAlgorithm MotionEstimationAlgorithm
      {
         get => _motionEstimationAlgorithm;
         set => SetProperty( ref _motionEstimationAlgorithm, value );
      }

      private int _macroblockSize = 16;
      public int MacroblockSize
      {
         get => _macroblockSize;
         set => SetProperty( ref _macroblockSize, value );
      }

      private int _searchParameter = 32;
      public int SearchParameter
      {
         get => _searchParameter;
         set => SetProperty( ref _searchParameter, value );
      }

      private bool _variableSizeCompensation = false;
      public bool VariableSizeCompensation
      {
         get => _variableSizeCompensation;
         set => SetProperty( ref _variableSizeCompensation, value );
      }

      private SceneChangeDetectionAlgorithm _sceneChangeDetectionAlgorithm = SceneChangeDetectionAlgorithm.FrameDifference;
      public SceneChangeDetectionAlgorithm SceneChangeDetectionAlgorithm
      {
         get => _sceneChangeDetectionAlgorithm;
         set => SetProperty( ref _sceneChangeDetectionAlgorithm, value );
      }

      private float _sceneChangeThreshold = 5.0f;
      public float SceneChangeThreshold
      {
         get => _sceneChangeThreshold;
         set => SetProperty( ref _sceneChangeThreshold, value );
      }
   }
}
