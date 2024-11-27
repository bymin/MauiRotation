using Microsoft.Maui.Graphics;

namespace TrapezoidDemo
{
    public class TrapezoidDrawable : IDrawable
    {
        private readonly MainViewModel viewModel;

        public TrapezoidDrawable(MainViewModel vm)
        {
            viewModel = vm;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // Clear the canvas
            canvas.FillColor = Colors.Transparent;
            canvas.FillRectangle(dirtyRect);

            // Save the canvas state
            canvas.SaveState();

            // Move the origin of the coordinate system to the center of the container
            canvas.Translate(dirtyRect.Width / 2, dirtyRect.Height / 2);

            // Scaling factor to fit the screen size
            var scaleX = dirtyRect.Width / viewModel.Width;
            var scaleY = dirtyRect.Height / viewModel.Height;
            var scale = Math.Min(scaleX, scaleY);

            // Apply scaling
            canvas.Scale((float)scale, (float)scale);

            // Draw the line segment
            if (viewModel.RotatedEdgePoints != null && viewModel.RotatedEdgePoints.Length == 2)
            {
                canvas.StrokeColor = Colors.Blue;
                canvas.StrokeSize = 4 / (float)scale;

                canvas.DrawLine(
                    (float)viewModel.RotatedEdgePoints[0].X,
                    (float)viewModel.RotatedEdgePoints[0].Y,
                    (float)viewModel.RotatedEdgePoints[1].X,
                    (float)viewModel.RotatedEdgePoints[1].Y);
            }

            // Restore the canvas state
            canvas.RestoreState();
        }
    }
}