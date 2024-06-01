﻿namespace TaxiSystem.Models.Bookings;

public enum BookingState
{
    None = 0,
    Booked,
    Accepted,

    Denied,
    Completed,
    Cancelled
}
