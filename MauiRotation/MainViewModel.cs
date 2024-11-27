using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TrapezoidDemo
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private double width = 500;
        private double height = 500;
        private double tiltAngle = 45;
        private double cameraDistance = 1000;
        private double _anchorYValue = 0.5;

        // Stores the three points of the rotated edge
        private Point[] rotatedEdgePoints;
        public Point[] RotatedEdgePoints
        {
            get => rotatedEdgePoints;
            set
            {
                rotatedEdgePoints = value;
                OnPropertyChanged();
            }
        }

        public double Width
        {
            get => width;
            set
            {
                if (SetProperty(ref width, value))
                {
                    OnPropertyChanged(nameof(Width));
                }
            }
        }

        public double Height
        {
            get => height;
            set
            {
                if (SetProperty(ref height, value))
                {
                    OnPropertyChanged(nameof(Height));
                }
            }
        }

        public double TiltAngle
        {
            get => tiltAngle;
            set
            {
                if (SetProperty(ref tiltAngle, value))
                {
                    OnPropertyChanged(nameof(TiltAngle));
                }
            }
        }

        public double CameraDistance
        {
            get => cameraDistance;
            set
            {
                if (SetProperty(ref cameraDistance, value))
                {
                    OnPropertyChanged(nameof(CameraDistance));
                }
            }
        }

        public double AnchorYValue
        {
            get => _anchorYValue;
            set
            {
                if (_anchorYValue != value)
                {
                    _anchorYValue = value;
                    OnPropertyChanged(nameof(AnchorYValue));
                }
            }
        }


        public TrapezoidDrawable TrapezoidDrawable { get; }

        public MainViewModel()
        {
            TrapezoidDrawable = new TrapezoidDrawable(this);

            if (RuntimeInformation.OSDescription.ToUpper().Contains("ANDROID"))
            {
                cameraDistance = 220;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
