﻿using System.Diagnostics;
using Microsoft.Maui.Controls;
using System.Numerics;

namespace TrapezoidDemo
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel ViewModel => BindingContext as MainViewModel;

        public MainPage()
        {
            InitializeComponent();

            ViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(ViewModel.TiltAngle) ||
                    e.PropertyName == nameof(ViewModel.Width) ||
                    e.PropertyName == nameof(ViewModel.Height) ||
                    e.PropertyName == nameof(ViewModel.CameraDistance))
                {
                    // Update calculations
                    UpdateRotatedEdge();
                    // Redraw
                    TrapezoidView.Invalidate();
                }
            };

            // Initial calculation
            UpdateRotatedEdge();
        }

        private void UpdateRotatedEdge()
        {
            double width = ViewModel.Width;
            double height = ViewModel.Height;
            double tiltAngle = ViewModel.TiltAngle;
            double cameraDistance = ViewModel.CameraDistance;

            // Convert angles from degrees to radians
            double theta = tiltAngle * Math.PI / 180.0;

            // Use MAUI's default camera distance
            double d = cameraDistance; // Adjust as needed

            // Original vertices (relative to the center)
            Vector3 leftTop = new Vector3((float)(-width / 2), (float)(-height / 2), 0);
            Vector3 rightTop = new Vector3((float)(width / 2), (float)(-height / 2), 0);

            // Rotation matrix around X-axis
            Matrix4x4 rotationMatrix = Matrix4x4.CreateRotationX((float)theta);

            // Apply rotation
            Vector3 leftTopRotated = Vector3.Transform(leftTop, rotationMatrix);
            Vector3 rightTopRotated = Vector3.Transform(rightTop, rotationMatrix);

            // Apply perspective projection
            float sLeft = (float)(d / (d - leftTopRotated.Z));
            float sRight = (float)(d / (d - rightTopRotated.Z));

            Vector2 leftTopProjected = new Vector2(leftTopRotated.X * sLeft, leftTopRotated.Y * sLeft);
            Vector2 rightTopProjected = new Vector2(rightTopRotated.X * sRight, rightTopRotated.Y * sRight);

            // Update the ViewModel
            ViewModel.RotatedEdgePoints = new Microsoft.Maui.Graphics.Point[]
            {
                new Microsoft.Maui.Graphics.Point(leftTopProjected.X, leftTopProjected.Y),
                new Microsoft.Maui.Graphics.Point(rightTopProjected.X, rightTopProjected.Y)
            };
        }
    }
}
