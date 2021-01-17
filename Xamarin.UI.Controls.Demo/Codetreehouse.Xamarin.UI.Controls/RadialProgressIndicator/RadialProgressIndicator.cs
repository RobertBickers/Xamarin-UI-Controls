using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Codetreehouse.Xamarin.UI.Controls
{
	public class RadialProgressIndicator : SKCanvasView
	{
		public class Circle
		{
			private readonly Func<SKImageInfo, SKPoint> _centrePlacementFunc;

			public Circle(Func<SKImageInfo, SKPoint> centrePlacementFunction)
			{
				_centrePlacementFunc = centrePlacementFunction;
			}

			public SKPoint Center { get; set; }

			public float Radius { get; set; }

			public SKRect Rect => new SKRect(Center.X - Radius, Center.Y - Radius, Center.X + Radius, Center.Y + Radius);


			public void CalculateCenter(SKImageInfo argsInfo, float strokeThickness)
			{

				Radius = (argsInfo.Width / 2) - strokeThickness;
				Center = _centrePlacementFunc(argsInfo);
			}
		}

		Circle _circle;

		public RadialProgressIndicator()
		{
			_circle = new Circle((info) => new SKPoint((float)info.Width / 2, (float)info.Height / 2));
		}

		protected override void OnPaintSurface(SKPaintSurfaceEventArgs paintSurfaceEventArgs)
		{
			_circle.CalculateCenter(paintSurfaceEventArgs.Info, StrokeWidth);

			paintSurfaceEventArgs.Surface.Canvas.Clear();

			DrawBackgroundCircle(paintSurfaceEventArgs.Surface.Canvas, _circle, StrokeWidth, BackgroundRingColour.ToSKColor());
			DrawProgressArc(paintSurfaceEventArgs.Surface.Canvas, _circle, Progress, StrokeWidth, ProgressColour.ToSKColor());
		}

		void DrawBackgroundCircle(SKCanvas canvas, Circle circle, float strokewidth, SKColor color)
		{
			canvas.DrawCircle(circle.Center, circle.Radius,
				new SKPaint()
				{
					StrokeWidth = strokewidth,
					Color = color,
					IsStroke = true
				});
		}

		void DrawProgressArc(SKCanvas canvas, Circle circle, int progress, float strokewidth, SKColor color)
		{
			var sweepAngle = progress * 3.6f;

			canvas.DrawArc(circle.Rect, 270, (float)sweepAngle, false,
				new SKPaint()
				{
					StrokeWidth = strokewidth,
					Color = color,
					IsStroke = true,
					StrokeCap = SKStrokeCap.Round
				});
		}

		public static readonly BindableProperty StrokeWidthProperty = BindableProperty.Create(nameof(StrokeWidth), typeof(int), typeof(RadialProgressIndicator), defaultValue: 10, propertyChanged: OnBindablePropertyChanged);

		public static readonly BindableProperty ProgressProperty = BindableProperty.Create(nameof(Progress), typeof(int), typeof(RadialProgressIndicator), propertyChanged: OnBindablePropertyChanged);

		public static readonly BindableProperty ProgressColourProperty = BindableProperty.Create(nameof(ProgressColour), typeof(Color), typeof(RadialProgressIndicator), defaultValue: Color.DarkGreen, propertyChanged: OnBindablePropertyChanged);

		public static readonly BindableProperty BackgroundRingColourProperty = BindableProperty.Create(nameof(ProgressColour), typeof(Color), typeof(RadialProgressIndicator), defaultValue: Color.LightGray, propertyChanged: OnBindablePropertyChanged);

		public int StrokeWidth
		{
			get { return (int)GetValue(StrokeWidthProperty); }
			set { SetValue(StrokeWidthProperty, value); }
		}

		public int Progress
		{
			get { return (int)GetValue(ProgressProperty); }
			set { SetValue(ProgressProperty, value); }
		}

		public Color ProgressColour
		{
			get { return (Color)GetValue(ProgressColourProperty); }
			set { SetValue(ProgressColourProperty, value); }
		}

		public Color BackgroundRingColour
		{
			get { return (Color)GetValue(BackgroundRingColourProperty); }
			set { SetValue(BackgroundRingColourProperty, value); }
		}


		static void OnBindablePropertyChanged(BindableObject bindable, object oldValue, object newValue)
			=> InvalidateDraw((RadialProgressIndicator)bindable);

		static void InvalidateDraw(RadialProgressIndicator progressCanvasView)
		{
			var context = progressCanvasView;
			context.InvalidateSurface();
		}
	}
}

