namespace SklepGrovly.Utils;

public static class CenyHelper
{
    public static decimal ObliczCenePoZnizce(decimal cena, decimal? znizka)
    {
        var z = znizka ?? 0;
        return z > 0 ? Math.Round(cena * (1 - z / 100m), 2) : cena;
    }
}