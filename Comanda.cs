namespace Proiectt{

public class Comanda
{
    public string NumeObiect { get; set; }
    public double GreutateObiect { get; set; }
    //Greutatea trebuie sa fie in grame
    public string CuloareObiect { get; set; }
    public string AdresaLivrare { get; set; }
    public double CostFinal { get; set; }

    public Comanda(string numeObiect, double greutateObiect, string culoareObiect, string adresaLivrare,
        double costFinal)
    {
        NumeObiect = numeObiect;
        GreutateObiect = greutateObiect;
        CuloareObiect = culoareObiect;
        AdresaLivrare = adresaLivrare;
        CostFinal = costFinal;
    }

    public void AfisareDetaliiComanda()
    {
        Console.WriteLine("Comanda plasata:\n");
        Console.WriteLine($"Obiect: {NumeObiect}\n");
        Console.WriteLine($"Masa: {GreutateObiect}\n");
        Console.WriteLine($"Culoare:{CuloareObiect}\n");
        Console.WriteLine($"Adresa:{AdresaLivrare}\n");
        Console.WriteLine($"Cost:{CostFinal}\n");
        
    }
    
}
}
