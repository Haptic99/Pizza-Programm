// /Models/OrderStatus.cs
namespace DeinPizzaShopProjekt.Models
{
    public enum OrderStatus
    {
        Pending,   // Offen (wartet in der Küche)
        Completed, // Erledigt
        Cancelled  // Storniert
    }
}