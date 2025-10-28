// /Models/OrderStatus.cs

// CORRECTED: Namespace added
namespace Pizza_Programm.Models
{
    public enum OrderStatus
    {
        Pending,   // Pending (waiting in the kitchen)
        Completed, // Completed
        Cancelled  // Cancelled
    }
}