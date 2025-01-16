using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp37;

public abstract class Imprimante
{
    public string ModelImprimanta { get; set; }
    public string ProducatorImprimanta { get; set; }
    
    public double CostImprimanta { get; set; }

    public Imprimante(string modelImprimanta, string producatorImprimanta, double costImprimanta)
    {
        ModelImprimanta = modelImprimanta;
        ProducatorImprimanta = producatorImprimanta;
        CostImprimanta=costImprimanta;
    }

    public void Pornire()
    {
        Console.WriteLine($"{ModelImprimanta} de la {ProducatorImprimanta} este pornit si gata de lucru");
    }

    // Metoda abstracta
    public abstract void Imprimare();

    public double CalcularePret(double cost)
    {
        return cost * CostImprimanta;
    }
}

public class ImprimantaCuRasina : Imprimante
{
    public double NivelulRasinii { get; set; }

    public ImprimantaCuRasina(string modelImprimanta, string producatorImprimanta,double costImprimanta, double nivelulRasinii) 
        : base(modelImprimanta, producatorImprimanta, costImprimanta)
    {
        NivelulRasinii = nivelulRasinii;
    }

    public void ReumplereRasina(double cantitate)
    {
        NivelulRasinii += cantitate;
        Console.WriteLine($"Baia de rasina a fost reumpluta. Nivelul actual: {NivelulRasinii} ml.");
    }

    public override void Imprimare()
    {
        if (NivelulRasinii <= 0)
        {
            Console.WriteLine("Eroare: Nivelul rasinii este prea scazut pentru imprimare!");
        }
        else
        {
            Console.WriteLine($"Imprimarea cu rasina folosind modelul {ModelImprimanta} este in curs de desfasurare!");
            NivelulRasinii -= 10; // Consuma 10 ml de rasina / utilizare
        }
    }
}

public class ImprimantaCuFilament : Imprimante
{
    public double LungimeFilament { get; set; }
    public string Culoare { get; set; }

    public ImprimantaCuFilament(string modelImprimanta, string producatorImprimanta,double costImprimanta,double lungimeFilament) 
        : base(modelImprimanta, producatorImprimanta,costImprimanta)
    {
        LungimeFilament = lungimeFilament;
    }

    public void SchimbareFilament(double cantitate,string culoare)
    {
        LungimeFilament = cantitate;
        Culoare=culoare;
        Console.WriteLine($"Filamentul a fost schimbat. Lungimea actuala a filamentului este de {LungimeFilament} metri.");
    }

    public override void Imprimare()
    {
        if (LungimeFilament <= 0)
        {
            Console.WriteLine("Eroare: Lungimea filamentului este prea mica pentru imprimare!");
        }
        else
        {
            Console.WriteLine($"Imprimarea cu filament folosind modelul {ModelImprimanta} este in curs de desfasurare!");
            LungimeFilament -= 1; // Consuma 1 metru de filament / utilizare
        }
    }
}
