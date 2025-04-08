namespace Challenge.Domain.Enums
{
    public enum OrderItemState
    {
        Saved = 0,
        Valid = 1,
        Invalid = 2,
        Processing = 3,
        Cancelled = 4,
        Completed = 5,
        WaintToShipp = 6,
        Shipped = 7,
        WaitSendToFactory = 8,
        SendToFactory = 9,
        WaintToRecive = 10,
        Recived = 11,
        WaintToPay = 12,
        Payed = 13,
        WaintToSendToClient = 14,
    }
}
