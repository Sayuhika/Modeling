using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Argon_IdealGasModel
{
    internal class PlotViewModel : INotifyPropertyChanged
    {
		public PlotModel Model { get; protected set; } = new();

		protected double max = 1.0;
		public double Max {
			get => max;
			set { max = value; PropChanged(nameof(Max)); }
		}

		protected double min = 1.0;
		public double Min
		{
			get => min;
			set { min = value; PropChanged(nameof(Min)); }
		}

		protected ObservableCollection<List<DataPoint>> points = new();
        public IList<List<DataPoint>> Points { 
            get => points;
            set { 
                points = value as ObservableCollection<List<DataPoint>>
					?? new ObservableCollection<List<DataPoint>>(value); 
                PropChanged(nameof(Points)); 
            }
		}

		public PlotViewModel()
		{
			this.PropertyChanged += OnPropertyChanged;
		}

		public event PropertyChangedEventHandler? PropertyChanged;
        protected void PropChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        virtual protected void OnPropertyChanged(object? sender, PropertyChangedEventArgs e) 
        {
			switch (e.PropertyName) {
				case nameof(Points):
					break;
				case nameof(Max):
					foreach (var ax in Model.Axes)
						ax.Maximum = Max;
					return;
				case nameof(Min):
					foreach (var ax in Model.Axes)
						ax.Minimum = Min;
					return;
				default:
					return;
			}

			points.CollectionChanged += OnCollectionChanged;

            Model = new PlotModel();
            Model.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            Model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = 0 });

            foreach (var pts in points) {
                var s = new LineSeries {
                    LineStyle = LineStyle.Solid,
                    Color = OxyColors.Aqua
                };
                s.Points.AddRange(pts);
                Model.Series.Add(s);
            }

			if (Model.Series.Count > 0) (Model.Series[0] as LineSeries).Color = OxyColors.Blue;
			if (Model.Series.Count > 1) (Model.Series[1] as LineSeries).Color = OxyColors.Green;
			if (Model.Series.Count > 2) (Model.Series[2] as LineSeries).Color = OxyColors.Red;

			PropChanged(nameof(Model));
        }

		protected void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					if (e.NewItems?[0] is IEnumerable<DataPoint> @pa) {
						var s = new LineSeries { LineStyle = LineStyle.Solid, Color = OxyColors.Black };
						s.Points.AddRange(@pa);
						Model.Series.Add(s);
					}
					break;

				case NotifyCollectionChangedAction.Remove:
					Model.Series.RemoveAt(e.OldStartingIndex);
					break;

				case NotifyCollectionChangedAction.Replace:
					if (e.NewItems?[0] is IEnumerable<DataPoint> @pr) {
						var s = Model.Series[e.NewStartingIndex] as LineSeries 
							?? throw new InvalidCastException($"Unexpected type: {Model.Series[0]} is not LineSeries");
						s.Points.Clear();
						s.Points.AddRange(@pr);
					}
					break;

				case NotifyCollectionChangedAction.Move:
					Model.Series[e.NewStartingIndex] = Model.Series[e.OldStartingIndex];
					break;

				case NotifyCollectionChangedAction.Reset:
					Model.Series.Clear();
					break;
			}
			Model.InvalidatePlot(true);
			PropChanged(nameof(Model));
		}
    }

    class PlotViewModelPoints : PlotViewModel
    {
		private int pointSize = 0;
		public int PointSize
		{
			get => pointSize;
			set { pointSize = value; PropChanged(nameof(PointSize)); }
		}
		public PlotViewModelPoints()
		{
			this.PropertyChanged += OnPropertyChanged;

			Points = new[] { new List<DataPoint>() };
		}

		protected override void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
			switch (e.PropertyName) {
				case nameof(Points):
					break;
				case nameof(PointSize):
					foreach (var srs in Model.Series)
                        if (srs is LineSeries ls)
                        {
							ls.MarkerSize = pointSize;
                        }
					return;
				case nameof(Max):
					foreach (var ax in Model.Axes)
						ax.Maximum = Max;
					return;
				default:
					return;
			}

			points.CollectionChanged += OnCollectionChanged;

			Model = new PlotModel();
            Model.Axes.Add(new LinearAxis { Position = AxisPosition.Left,	Minimum = 0, Maximum = Max });
            Model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = 0, Maximum = Max });

            var s = new LineSeries {
                LineStyle = LineStyle.Solid,
                MarkerType = MarkerType.Circle,
                MarkerFill = OxyColors.Black,
                MarkerSize = pointSize,
                Color = OxyColors.Transparent
            };
			if (points.Count == 1 && points[0] is not null)
				s.Points.AddRange(points[0]);
            Model.Series.Add(s);

            PropChanged(nameof(Model));
        }
    }
}
