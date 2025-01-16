using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ConsoleApp37
{
    class Program
    {
        static List<Utilizator> utilizatori = new List<Utilizator>
        {
            new Administrator("admin", "admin123"),
            new Utilizator("user", "user123")
        };

        static Administrator admin = (Administrator)utilizatori.First(u => u is Administrator);
        static List<Comanda> comenzi = new List<Comanda>();

        const string StareFisier = "stare_program.json";

        static void Main(string[] args)
        {
            Console.WriteLine("Bine ati venit pe aplicatia 3D Print\n");

            // Încărcăm starea programului dacă există
            IncarcaStare();

            Utilizator utilizatorAutentificat = null;

            // Autentificare
            while (utilizatorAutentificat == null)
            {
                Console.WriteLine("Introduceti numele de utilizator:");
                string nume = Console.ReadLine();
                Console.WriteLine("Introduceti parola:");
                string parola = Console.ReadLine();

                utilizatorAutentificat = AutentificaUtilizator(utilizatori, nume, parola);

                if (utilizatorAutentificat == null)
                {
                    Console.WriteLine("Autentificare esuata! Incercati din nou.");
                }
            }

            Console.WriteLine($"Bine ai venit, {utilizatorAutentificat.NumeUtilizator}!");

            // Menținem utilizatorul conectat până când decide să se deconecteze
            while (true)
            {
                if (utilizatorAutentificat is Administrator adminUser)
                {
                    // Admin se poate loga și accesa opțiunile
                    MeniuAdmin(adminUser);
                }
                else
                {
                    // Utilizator normal
                    MeniuUtilizator();
                }

                // Optiune pentru schimbarea utilizatorului
                Console.WriteLine("Doresti să te deconectezi si să te autentifici din nou? (da/nu)");
                string raspuns = Console.ReadLine();
                if (raspuns.ToLower() == "da")
                {
                    utilizatorAutentificat = null;
                    break;
                }
            }

            // Salvăm starea programului la ieșire
            SalveazaStare();
        }

        static Utilizator AutentificaUtilizator(List<Utilizator> utilizatori, string nume, string parola)
        {
            return utilizatori.FirstOrDefault(u => u.VerificareUtilizator(nume, parola));
        }

        static void MeniuAdmin(Administrator admin)
        {
            Console.WriteLine("Meniu Admin:");
            Console.WriteLine("1. Adauga imprimanta");
            Console.WriteLine("2. Vizualizeaza imprimante");
            Console.WriteLine("3. Vizualizeaza detalii imprimanta");
            Console.WriteLine("4. Reumplere imprimante cu rasina");
            Console.WriteLine("5. Adaugare filament in stoc");
            Console.WriteLine("6. Vizualizeaza filament in stoc");
            Console.WriteLine("7. Vizualizeaza Comenzi");
            Console.WriteLine("8. Procesare Comenzi");

            var optiune = Console.ReadLine();

            switch (optiune)
            {
                case "1":
                    var imprimanta = new ImprimantaCuRasina("Model1", "Producator1", 200.0, 50.0);
                    admin.AdaugaImprimanta(imprimanta);
                    break;
                case "2":
                    admin.VizualizeazaImprimantele();
                    break;
                case "3":
                    admin.VizualizeazaDetaliiImprimanta("Model1");
                    break;
                case "4":
                    admin.ReumplereImprimantaCuRasina("Model1", 400);
                    break;
                case "5":
                    admin.AdaugareFilamentInStoc("filament", 100);
                    break;
                case "6":
                    admin.VizualizeazaFilament();
                    break;
                case "7":
                    admin.VizualizeazaToateComenzile();
                    break;
                case "8":
                    admin.ProcesareComenzi();
                    break;
                default:
                    Console.WriteLine("Opțiune invalidă.");
                    break;
            }
        }

        static void MeniuUtilizator()
        {
            Console.WriteLine("Meniu Utilizator:");
            Console.WriteLine("1. Vizualizarea Costurilor");
            Console.WriteLine("2. Vizualizează comenzile");
            Console.WriteLine("3. Iesire");

            var optiune = Console.ReadLine();

            switch (optiune)
            {
                case "1":
                    Console.WriteLine("Costurile pentru imprimanta cu rasina sunt de 1 RON/G");
                    Console.WriteLine("Costurile pentru imprimanta cu filament sunt de 5 RON/CM");
                    break;
                case "2":
                    Console.WriteLine("Introduceti de la tastatura gramajul obiectului comandat/pe care il doriti sa il comandati:");
                    int x = int.Parse(Console.ReadLine());
                    Console.WriteLine($"Pretul pentru imprimanta cu rasina este de {x} lei pe {x} grame");
                    Console.WriteLine($"Pretul pentru imprimanta cu filament este de {5 * x} lei pe {x} cm");
                    break;
                case "3":
                    Console.WriteLine("Deconectare...");
                    break;
                default:
                    Console.WriteLine("Opțiune invalidă.");
                    break;
            }
        }

        static void SalveazaStare()
        {
            var stare = new
            {
                Imprimante = admin.GetImprimante(),
                Comenzi = admin.GetComenzi(),
                Filamente = admin.GetStocFilament(),
            };

            string json = JsonSerializer.Serialize(stare, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(StareFisier, json);
            Console.WriteLine("Starea programului a fost salvată.");
        }

        static void IncarcaStare()
        {
            if (File.Exists(StareFisier))
            {
                string json = File.ReadAllText(StareFisier);
                var stare = JsonSerializer.Deserialize<dynamic>(json);

                foreach (var imprimanta in stare.Imprimante)
                {
                    admin.AdaugaImprimanta(new ImprimantaCuRasina(imprimanta.ModelImprimanta, imprimanta.ProducatorImprimanta, imprimanta.CostImprimanta, imprimanta.NivelulRasinii));
                }

                foreach (var comanda in stare.Comenzi)
                {
                    admin.AdaugaComanda(new Comanda(comanda.NumeObiect, comanda.GreutateObiect, comanda.CuloareObiect, comanda.AdresaLivrare, comanda.CostFinal));
                }

                foreach (var filament in stare.Filamente)
                {
                    admin.AdaugareFilamentInStoc(filament.Key, filament.Value);
                }

                Console.WriteLine("Starea programului a fost încărcată.");
            }
        }
    }
}