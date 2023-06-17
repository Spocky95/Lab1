using System;
using System.Collections.Generic;

namespace Lab1.Models;

public partial class KodyPocztoweEf
{
    public string KodPocztowy { get; set; } = null!;

    public string Adres { get; set; } = null!;

    public string Miejscowosc { get; set; } = null!;

    public string Wojewodztwo { get; set; } = null!;

    public string Powiat { get; set; } = null!;

    public int Id { get; set; }
}
