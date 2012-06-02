using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using GMap.NET.WindowsPresentation;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Demo.WindowsPresentation.CustomMarkers
{
   /// <summary>
   /// Interaction logic for Test.xaml
   /// </summary>
    public partial class Test : UserControl
   {
       Popup Popup;
       Label labelPopup;
       GMapMarker Marker;
       matsps.Wpf.MainWindow MainWindow;

       public Test(matsps.Wpf.MainWindow window, GMapMarker marker, string title)
      {
         InitializeComponent();

         text.Text = title;

         this.MainWindow = window;
         this.Marker = marker;

         Popup = new Popup();
         labelPopup = new Label();

         this.Loaded += new RoutedEventHandler(CustomMarkerDemo_Loaded);
         this.SizeChanged += new SizeChangedEventHandler(CustomMarkerDemo_SizeChanged);
         this.MouseEnter += new MouseEventHandler(MarkerControl_MouseEnter);
         this.MouseLeave += new MouseEventHandler(MarkerControl_MouseLeave);
         this.MouseMove += new MouseEventHandler(CustomMarkerDemo_MouseMove);
         this.MouseLeftButtonUp += new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonUp);
         this.MouseLeftButtonDown += new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonDown);

         Popup.Placement = PlacementMode.Mouse;
         {
             labelPopup.Background =  Brushes.WhiteSmoke;
             labelPopup.Foreground = Brushes.Gray;
             labelPopup.BorderBrush = Brushes.WhiteSmoke;
             labelPopup.BorderThickness = new Thickness(1);
             labelPopup.Padding = new Thickness(5);
             labelPopup.FontSize = 15;
             labelPopup.Content = title;
         }
         Popup.Child = labelPopup;
      }

      void CustomMarkerDemo_Loaded(object sender, RoutedEventArgs e)
      {
          //if (icon.Source.CanFreeze)
          //{
           //   icon.Source.Freeze();
          //}
      }

      void CustomMarkerDemo_SizeChanged(object sender, SizeChangedEventArgs e)
      {
          //Marker.Offset = new Point(-e.NewSize.Width, -e.NewSize.Height);
      }

      void CustomMarkerDemo_MouseMove(object sender, MouseEventArgs e)
      {
          if (e.LeftButton == MouseButtonState.Pressed && IsMouseCaptured)
          {
              Point p = e.GetPosition(MainWindow.MainMap);
              Marker.Position = MainWindow.MainMap.FromLocalToLatLng((int)p.X, (int)p.Y);
          }
      }

      void CustomMarkerDemo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
          if (!IsMouseCaptured)
          {
              Mouse.Capture(this);
          }
      }

      void CustomMarkerDemo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
      {
          if (IsMouseCaptured)
          {
              Mouse.Capture(null);
          }
      }

      void MarkerControl_MouseLeave(object sender, MouseEventArgs e)
      {
          Marker.ZIndex -= 10000;
          Popup.IsOpen = false;
      }

      void MarkerControl_MouseEnter(object sender, MouseEventArgs e)
      {
          Marker.ZIndex += 10000;
          Popup.IsOpen = true;
      }

   }
}
