﻿namespace cw5.Models;

public class Country_Trip
{
    public int IdCountry { get; set; }
    public Country Country { get; set; }

    public int IdTrip { get; set; }
    public Trip Trip { get; set; }
}