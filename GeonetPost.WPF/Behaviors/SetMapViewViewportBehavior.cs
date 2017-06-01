using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI.Controls;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interactivity;

namespace GeonetPost.WPF.Behaviors
{
  public class SetMapViewViewportBehavior : Behavior<MapView>
  {
    /// <summary>
    /// 
    /// </summary>
    public static readonly DependencyProperty ViewpointProperty =
      DependencyProperty.Register(nameof(Viewpoint), typeof(Viewpoint), typeof(SetMapViewViewportBehavior));

    /// <summary>
    /// 
    /// </summary>
    public Viewpoint Viewpoint
    {
      get { return (Viewpoint)GetValue(ViewpointProperty); }
      set { SetValue(ViewpointProperty, value); }
    }

    public SetMapViewViewportBehavior()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if(e.Property.Name == nameof(Viewpoint))
      {
        SetViewpoint(this.Viewpoint);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vp"></param>
    private async void SetViewpoint(Viewpoint vp)
    {
      if (vp != null)
      {
        var actualVp = this.AssociatedObject.GetCurrentViewpoint(ViewpointType.BoundingGeometry);
        if (actualVp == null || (actualVp != null && !actualVp.Equals(vp)))
        {
          Debug.WriteLine("SetViewpoint");
          await this.AssociatedObject.SetViewpointAsync(vp);
        }
      }
    }

  }
}
