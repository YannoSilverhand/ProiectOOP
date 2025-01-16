using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp37
{
    public class Administrator : Utilizator
    {
        private List<Imprimante> imprimante;
        private List<Comanda> comenzi;
        private Dictionary<string, double> stocFilament;

        public Administrator(string numeUtilizator, string parola) : base(numeUtilizator, parola)
        {
            imprimante = new List<Imprimante>();
            comenzi = new List<Comanda>();
            stocFilament = new Dictionary<string, double>();
        }

        public void AdaugaImprimanta(Imprimante imprimanta)
        {
            imprimante.Add(imprimanta);
            Console.WriteLine($"Imprimanta {imprimanta.ModelImprimanta} adăugată.");
        }

        public void VizualizeazaImprimantele()
        {
            foreach (var a in imprimante)
            {
                Console.WriteLine($"Model: {a.ModelImprimanta}, Producător: {a.ProducatorImprimanta}, Cost: {a.CostImprimanta} RON");
            }
        }

        public void VizualizeazaDetaliiImprimanta(string model)
        {
            var imprimanta = imprimante.FirstOrDefault(i => i.ModelImprimanta == model);
            if (imprimanta != null)
            {
                Console.WriteLine($"Detalii imprimanta {imprimanta.ModelImprimanta}: Producător {imprimanta.ProducatorImprimanta}, Cost: {imprimanta.CostImprimanta}");
            }
            else
            {
                Console.WriteLine("Imprimanta nu a fost găsită.");
            }
        }

        public void VizualizeazaComenzi()
        {
            foreach (var comanda in comenzi)
            {
                comanda.AfisareDetaliiComanda();
            }
        }
        public void AdaugaComanda(Comanda comanda)
        {
            comenzi.Add(comanda);
            Console.WriteLine($"Comanda pentru {comanda.NumeObiect} a fost adăugată.");
        }
        public void ReumplereImprimantaCuRasina(string model, double cantitate)
        {
            foreach (var a in imprimante)
            {
                if (a is ImprimantaCuRasina imprimantaCuRasina && a.ModelImprimanta == model)
                {
                    imprimantaCuRasina.ReumplereRasina(cantitate);
                    return;
                }
            }
            Console.WriteLine("Imprimanta cu rasina nu a fost gasita.Momentan nu este in stoc ceea ce dumeneavoastra doriti.");
        }
        public void AdaugareFilamentInStoc(string Tip, double cantitate)
        {
            if (stocFilament.ContainsKey(Tip))
            {
                stocFilament[Tip] += cantitate;
            }
            else
            {
                stocFilament[Tip] = cantitate;
            }
            Console.WriteLine($"Au fost adaugati {cantitate} metri de filament tip {Tip}");
        }
        public void SchimbaFilament(string model, double cantitate, string culoare)
        {
            foreach (var a in imprimante)
            {
                if (a is ImprimantaCuFilament imprimantaCuFilament && a.ModelImprimanta == model)
                {
                    imprimantaCuFilament.SchimbareFilament(cantitate, culoare);
                    return;
                }
            }
            Console.WriteLine("Imprimanta cu filament nu a fost gasita.Momentan nu este in stoc ceea ce dumneavoastra doriti.");
        }
        public void VizualizeazaFilament()
        {
            Console.WriteLine("Stocul de filament:");
            foreach (var a in stocFilament)
            {
                Console.WriteLine($"Tip:{a.Key}, Canitate:{a.Value} metri");
            }
        }
        
        public void ProcesareComenzi()
        {
            Console.WriteLine("Comenzile au fost procesate cu succes!");
            
        }
        public void VizualizeazaToateComenzile()
        {
            Console.WriteLine("Lista comenzilor procesate:");
            foreach (var comanda in comenzi)
            {
                comanda.AfisareDetaliiComanda();
            }
        
        }
    }
}
