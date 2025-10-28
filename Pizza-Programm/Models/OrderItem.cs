// /Models/OrderItem.cs
using CommunityToolkit.Mvvm.ComponentModel;

// KORRIGIERT: Namespace hinzugefügt
namespace Pizza_Programm.Models
{
    // Dies ist ein Schnappschuss einer Pizza zum Zeitpunkt der Bestellung.
    public partial class OrderItem : ObservableObject
    {
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private int _orderId;

        [ObservableProperty]
        private Order _order;

        [ObservableProperty]
        private string _pizzaName; // Name als Kopie

        [ObservableProperty]
        private decimal _priceAtOrder; // Preis als Kopie

        // Hier speichern wir Kundenwünsche, z.B. "Ohne Oliven, Extra Käse"
        [ObservableProperty]
        private string _customizations;
    }
}