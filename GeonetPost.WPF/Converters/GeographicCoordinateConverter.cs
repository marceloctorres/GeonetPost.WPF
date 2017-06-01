using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GeonetPost.WPF.Converters
{
  public class GeographicCoordinateConverter : IValueConverter
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value.GetType() == typeof(double) && targetType == typeof(string))
      {
        return ToDMS((double)value, (string)parameter);
      }
      return value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="valor"></param>
    /// <param name="eje"></param>
    /// <returns></returns>
    private string ToDMS(double valor, string eje)
    {
      var cSigno = string.Empty;
      var signo = Math.Sign(valor);
      valor = Math.Abs(valor);

      if (!string.IsNullOrEmpty(eje))
      {
        cSigno = eje.ToUpper() == "X" ? signo > 0 ? "E" : "W" : signo > 0 ? "N" : "S";
        signo = 1;
      }

      var grados = Math.Floor(valor);
      var minutos = Math.Floor((valor - grados) * 60);
      var segundos = ((valor - grados) * 60 - minutos) * 60;

      return string.Format("{0:0}°{1:00}'{2:00.000}\" {3}", signo * grados, minutos, segundos, cSigno);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
