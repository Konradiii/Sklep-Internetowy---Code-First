using SklepGrovly.Utils;

namespace SklepGrovly.Tests;

public class CenyHelperTests
{
    [Fact]
    public void ObliczCenePoZnizce_Znizka20procent_ZwracaNizszaCene()
    {
        decimal cena = 100m;
        decimal znizka = 20m;
        
        var wynik = CenyHelper.ObliczCenePoZnizce(cena, znizka);
        
        Assert.Equal(80m, wynik);
    }

    [Fact]
    public void ObliczCenePoZnizce_ZnizkaRownaNull_ZwracaTakaSamaBazowaCene()
    {
        decimal cena = 100m;
        decimal? znizka = null;
        
        var wynik = CenyHelper.ObliczCenePoZnizce(cena, znizka);
        
        Assert.Equal(100m, wynik);
    }

    [Fact]
    public void ObliczCenePoZnizce_ZnizkaDajacaPrzyblizeniePowyzejDwochMiejsc_ZwracaCeneLadnieZaokraglonaDoDwochMiejsc()
    {
        decimal cena = 55.99m;
        decimal? znizka = 10;
        
        var wynik = CenyHelper.ObliczCenePoZnizce(cena, znizka);
        
        Assert.Equal(50.39m, wynik);
    }
    
    
}