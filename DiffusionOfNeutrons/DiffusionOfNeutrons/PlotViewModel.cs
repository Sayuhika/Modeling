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

namespace DiffusionOfNeutrons
{
    internal class PlotViewModel : INotifyPropertyChanged
    {
		public PlotModel Model { get; protected set; } = new();

		protected double max_X = double.NaN;
		public double Max_X
		{
			get => max_X;
			set { max_X = value; PropChanged(nameof(Max_X)); }
		}

		protected double min_X = double.NaN;
		public double Min_X
		{
			get => min_X;
			set { min_X = value; PropChanged(nameof(Min_X)); }
		}

		protected double max_Y = double.NaN;
		public double Max_Y
		{
			get => max_Y;
			set { max_Y = value; PropChanged(nameof(Max_Y)); }
		}

		protected double min_Y = double.NaN;
		public double Min_Y
		{
			get => min_Y;
			set { min_Y = value; PropChanged(nameof(Min_Y)); }
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
				case nameof(max_X):
                    Model.Axes[1].Maximum = max_X;
					return;
				case nameof(min_X):
					Model.Axes[1].Minimum = min_X;
					return;
				case nameof(max_Y):
					Model.Axes[0].Maximum = max_Y;
					return;
				case nameof(min_Y):
					Model.Axes[0].Minimum = min_Y;
					return;
				default:
					return;
			}

			points.CollectionChanged += OnCollectionChanged;

            Model = new PlotModel();
            Model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = min_Y, Maximum = max_Y });
            Model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = min_X, Maximum = max_X });

            foreach (var pts in points) {
                var s = new LineSeries {
                    LineStyle = LineStyle.Solid,
                    Color = OxyColors.Aqua
                };
                s.Points.AddRange(pts);
                Model.Series.Add(s);
            }

			if (Model.Series.Count > 0) (Model.Series[0] as LineSeries).Color = OxyColors.Green;
			if (Model.Series.Count > 1) (Model.Series[1] as LineSeries).Color = OxyColors.Red;
			if (Model.Series.Count > 2) (Model.Series[2] as LineSeries).Color = OxyColors.Blue;
			if (Model.Series.Count > 3) (Model.Series[3] as LineSeries).Color = OxyColors.Cyan;

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
}
