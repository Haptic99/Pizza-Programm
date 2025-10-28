using System.Configuration;
using System.Data;
using System.Windows;
using System.Globalization; // HINZUGEFÜGT
using System.Threading;     // HINZUGEFÜGT

namespace Pizza_Programm
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // HINZUGEFÜGT: OnStartup-Methode
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Setzt die Standardkultur für die gesamte Anwendung auf Thai (Thailand)
            var cultureInfo = new CultureInfo("th-TH");

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            // Stellt sicher, dass WPF-Bindungen die neue Kultur verwenden
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    System.Windows.Markup.XmlLanguage.GetLanguage(cultureInfo.Name)));
        }
    }
}