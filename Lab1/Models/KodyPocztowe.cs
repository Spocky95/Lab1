using System;
using System.Collections.Generic;

namespace Lab1.Models;

public partial class KodyPocztowe
{
    public string KodPocztowy { get; set; } = null!;

    public string Adres { get; set; } = null!;

    public string Miejscowosc { get; set; } = null!;

    public string Wojewodztwo { get; set; } = null!;

    public string Powiat { get; set; } = null!;
}
