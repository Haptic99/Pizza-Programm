// /Models/OrderStatus.cs
namespace Pizza_Programm.Models
{
    public enum OrderStatus
    {
        Pending,   // Offen (wartet in der Küche)
        Completed, // Erledigt
        Cancelled  // Storniert
    }
}