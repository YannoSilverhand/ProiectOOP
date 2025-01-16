using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp39
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bine ati venit pe aplicatia 3D Print\n");
            // Creare utilizatori (admin si user)
            List<Utilizator> utilizatori = new List<Utilizator>
            {
                new Administrator("admin", "admin123"),
                new Utilizator("user", "user123")
            };
            Comanda comanda1 = new Comanda("Figurina", 250, "Rosu", "Timisoara", 250);
            Comanda comanda2 = new Comanda("Suport casti", 650, "Negru", "Bucuresti", 650);

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
                if (utilizatorAutentificat is Administrator admin)
                {
                    // Admin se poate loga și accesa opțiunile
                    MeniuAdmin(admin);
                }
                else
                {
                    // Utilizator normal
                    MeniuUtilizator();
                }

                // Optiune pentru schimbarea utilizatorului
                Console.WriteLine("Doresti sa te deconectezi si să te autentifici din nou? (da/nu)");
                string raspuns = Console.ReadLine();
                if (raspuns.ToLower() == "da")
                {
                    utilizatorAutentificat = null;
                }
            }
        }

        static Utilizator AutentificaUtilizator(List<Utilizator> utilizatori, string nume, string parola)
        {
            return utilizatori.FirstOrDefault(u => u.VerificareUtilizator(nume, parola));
        }

        static void MeniuAdmin(Administrator admin)
        {
            Console.WriteLine("Meniu Admin:");
            Console.WriteLine("1. Adauga imprimanta:");
            Console.WriteLine("2. Vizualizeaza imprimante:");
            Console.WriteLine("3.Vizualizeaza detalii imprimanta:");
            Console.WriteLine("4. Reumplere imprimante cu rasina:");
            Console.WriteLine("5. Adaugare filament in stoc:");
            Console.WriteLine("6. Vizualizeaza filament in stoc");
            Console.WriteLine("7. Vizualizeaza Comenzi:");
            Console.WriteLine("8. Procesare Comenzi:");

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
                    admin.VizualizeazaDetaliiImprimanta("Model2");
                    break;
                case "4":admin.ReumplereImprimantaCuRasina("Model1",400);
                    break;
                case "5":admin.AdaugareFilamentInStoc("filament",100);
                    break;
                case "6":admin.VizualizeazaFilament();
                    break;
                case "7":
                    Comanda comanda1 = new Comanda("Figurina", 250, "Rosu", "Timisoara", 250);
                    Comanda comanda2 = new Comanda("Suport casti", 650, "Negru", "Bucuresti", 650);
                    admin.AdaugaComanda(comanda1);
                    admin.AdaugaComanda(comanda2);
                    admin.VizualizeazaToateComenzile();
                    
                    break;
                case "8":admin.ProcesareComenzi();
                    break;
                default:
                    Console.WriteLine("Optiune invalidă.");
                    break;
            }
        }

        static void MeniuUtilizator()
        {
            Console.WriteLine("Meniu Utilizator:");
            Console.WriteLine("1. Vizualizarea Costurilor");
            Console.WriteLine("2. Vizualizează comenzile");
            

            var optiune = Console.ReadLine();

            switch (optiune)
            {
                case "1":
                    Console.WriteLine("Costurile pentru imprimanta cu rasina sunt de 1 RON/G");
                    Console.WriteLine("Costurile pentru imprimanta cu filament sunt de 5 RON/CM");
                    break;
                case "2":
                    Console.WriteLine("Introduceti de la tastatura gramajul obiectului comandat/pe care il doriti sa il comandati:");
                    int x, y,d;
                    x = int.Parse(Console.ReadLine());
                    y = x;
                    d = 5 * x;
                    Console.WriteLine($"Pretul pentru imprimanta cu rasina este de {x} lei pe {y} grame");
                    Console.WriteLine($"Pretul pentru imprimanta cu filament este de {x} lei pe {d} cm");

                    break;

                    
                    
               /* case "3":
                    Comanda comanda3 = new Comanda("Suport creioane", 330, "Verde", "Cluj-Napoca,", 330);
                    Comanda comanda4 = new Comanda("Suport card", 150, "Albastru", "Iasi", 150);
                    break;*/
                default:
                    Console.WriteLine("Opțiune invalidă.");
                    break;
            }
        }
    }
}
