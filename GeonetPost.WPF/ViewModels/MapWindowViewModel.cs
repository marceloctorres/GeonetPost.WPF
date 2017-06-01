using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using System.Windows.Media;

namespace GeonetPost.WPF.ViewModels
{
  public class MapWindowViewModel : BindableBase
  {
    private Map _myMap;

    /// <summary>
    /// 
    /// </summary>
    public Map MyMap
    {
      get { return _myMap; }
      set { SetProperty(ref _myMap, value); }
    }

    private GraphicsOverlayCollection _grapchicsOverlays;

    /// <summary>
    /// 
    /// </summary>
    public GraphicsOverlayCollection GraphicsOverlays
    {
      get { return _grapchicsOverlays; } 
      set { SetProperty(ref _grapchicsOverlays, value); }
    }

    private Viewpoint _viewpoint;

    /// <summary>
    /// 
    /// </summary>
    public Viewpoint Viewpoint
    {
      get { return _viewpoint; }
      set { SetProperty(ref _viewpoint, value); }
    }

    private Viewpoint _updatedViewpoint;

    /// <summary>
    /// 
    /// </summary>
    public Viewpoint UpdatedViewpoint
    {
      get { return _updatedViewpoint; }
      set { SetProperty(ref _updatedViewpoint, value); }
    }

    /// <summary>
    /// 
    /// </summary>
    public ICommand ButtonClickCommand { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public ICommand ZoomCommand { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public ICommand UpdateViewpointCommand { get; private set; }

    public MapWindowViewModel()
    {
      MyMap = new Map(Basemap.CreateStreets());
      GraphicsOverlays = new GraphicsOverlayCollection();
      ButtonClickCommand = new DelegateCommand(ButtonClickAction);
      ZoomCommand = new DelegateCommand(ZoomAction);
      UpdateViewpointCommand = new DelegateCommand<Viewpoint>(UpdateViewpointAction);

      GraphicsOverlay go = new GraphicsOverlay()
      {
        Id = "MyGraphicOverlay"
      };
      GraphicsOverlays.Add(go);
    }

    /// <summary>
    /// 
    /// </summary>
    private void ButtonClickAction()
    {
      var g = new Graphic()
      {
        Geometry = new MapPoint(-74, 4, SpatialReferences.Wgs84),
        Symbol = new SimpleMarkerSymbol() {  Color=Colors.Green, Style= SimpleMarkerSymbolStyle.Circle, Size=10 }
      };
      GraphicsOverlays[0].Graphics.Clear();
      GraphicsOverlays[0].Graphics.Add(g);

    }

    /// <summary>
    /// 
    /// </summary>
    private void ZoomAction()
    {
      Viewpoint = new Viewpoint(4, -74, 5000000);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vp"></param>
    private void UpdateViewpointAction(Viewpoint vp)
    {
      var projectedVp = new Viewpoint(GeometryEngine.Project(vp.TargetGeometry, SpatialReferences.Wgs84), vp.Camera);
      if (UpdatedViewpoint == null || projectedVp.ToJson() != UpdatedViewpoint.ToJson())
      {
        UpdatedViewpoint = projectedVp;
      }
    }
  }
}
