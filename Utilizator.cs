namespace Proiectt
{

    public class Utilizator
    {
        public string NumeUtilizator { get; set; }
        public string Parola { get; set; }

        public Utilizator(string numeUtilizator, string parola)
        {
            NumeUtilizator = numeUtilizator;
            Parola = parola;
        }

        public bool VerificareUtilizator(string numeUtilizator, string parola)
        {
            return NumeUtilizator == numeUtilizator && Parola == parola;
        }

        public Comanda PlaseazaComanda()
        {
            // Colectăm informațiile necesare pentru comandă
            Console.WriteLine("Introduceți numele obiectului:");
            string numeObiect = Console.ReadLine();

            Console.WriteLine("Introduceți greutatea obiectului (în grame):");
            double greutateObiect = double.Parse(Console.ReadLine());

            Console.WriteLine("Introduceți culoarea obiectului:");
            string culoareObiect = Console.ReadLine();

            Console.WriteLine("Introduceți adresa de livrare:");
            string adresaLivrare = Console.ReadLine();

            // Calculăm costul comenzii, de exemplu, 5 RON per gram.
            double costFinal = greutateObiect * 5; // Exemplu de calcul al costului

            // Creăm și returnăm comanda
            Comanda comanda = new Comanda(numeObiect, greutateObiect, culoareObiect, adresaLivrare, costFinal);
            Console.WriteLine("Comanda a fost plasată cu succes!");

            return comanda;
        }

    }

    public class Admin : Utilizator
    {
        public Admin(string numeUtilizator, string parola) : base(numeUtilizator, parola)
        {
        }
        // Administratorul poate avea metode suplimentare pentru gestionarea imprimantelor, comenzilor etc.
    }
}
