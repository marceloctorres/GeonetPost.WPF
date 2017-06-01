using Esri.ArcGISRuntime.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace GeonetPost.WPF.Behaviors
{
  public class MapViewViewpointChangedBehavior : Behavior<MapView>
  {
    /// <summary>
    /// 
    /// </summary>
    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command),
      typeof(ICommand),
      typeof(MapViewViewpointChangedBehavior));

    /// <summary>
    /// 
    /// </summary>
    public ICommand Command
    {
      get { return (ICommand)GetValue(CommandProperty); }
      set { SetValue(CommandProperty, value); }
    }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnAttached()
    {
      this.AssociatedObject.ViewpointChanged += AssociatedObject_ViewpointChanged;
    }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnDetaching()
    {
      this.AssociatedObject.ViewpointChanged += AssociatedObject_ViewpointChanged;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AssociatedObject_ViewpointChanged(object sender, EventArgs e)
    {
      if (this.Command != null)
      {
        MapView mapView = (MapView)sender;
        if (mapView != null)
        {
          var viewpoint = mapView.GetCurrentViewpoint(Esri.ArcGISRuntime.Mapping.ViewpointType.BoundingGeometry);
          if (this.Command.CanExecute(viewpoint))
          {
            this.Command.Execute(viewpoint);
          }
        }
      }
    }
  }
}
