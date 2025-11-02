using Microsoft.Maui.Controls;
using System;
using System.Globalization;
using System.IO;

namespace Examen1_PM_II.Converters
{
    public class Base64ToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string base64String && !string.IsNullOrWhiteSpace(base64String))
            {
                try
                {
                    // Convierte la cadena Base64 a un array de bytes
                    byte[] imageBytes = System.Convert.FromBase64String(base64String);

                    // Crea un ImageSource a partir del stream de memoria
                    return ImageSource.FromStream(() => new MemoryStream(imageBytes));
                }
                catch
                {
                    // Retorna null o una imagen por defecto en caso de error
                    return null;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}