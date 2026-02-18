using System;
using static System.Console;

namespace Dinosauri
{
    enum TipoDinosauro
    {
        Brontosauro,
        Triceratopo,
        Pterodattilo,
        TRex,
        Stegosauro
    }

    enum Taglia
    {
        Piccolo,
        Medio,
        Grande
    }

    struct Dinosauro
    {
        public int codice;
        public TipoDinosauro tipo;
        public Taglia taglia;
        public int eta;
        public int forza;
        public string proprietario;

        public Dinosauro(int codice, TipoDinosauro tipo, Taglia taglia, int eta, int forza, string proprietario)                //costruttore per inizializzare i campi del dinosauro
        {
            this.codice = codice;
            this.tipo = tipo;
            this.taglia = taglia;
            this.eta = eta;
            this.forza = forza;
            this.proprietario = proprietario;
        }

        public string Stampa()
        {
            return $"Codice: {codice} | Tipo: {tipo} | Taglia: {taglia} | Età: {eta} | Forza: {forza} | Proprietario: {proprietario}";
        }
    }

    class Program
    {
        static Dinosauro[] recinto = new Dinosauro[100];            //vettore per contenere i dinosauri
        static int conteggio = 0;

        static void Main()
        {
            int scelta;
            bool x;

            do
            {
                WriteLine("\nGestione dinosauri");
                WriteLine("1 - Riempi recinto");
                WriteLine("2 - Sostituisci dinosauro");
                WriteLine("3 - Scambia proprietari");
                WriteLine("4 - Visualizza tutti");
                WriteLine("5 - Filtra per tipo");
                WriteLine("6 - Filtra per taglia");
                WriteLine("7 - Esci");
                Write("Scelta: ");

                do
                {
                    x = int.TryParse(ReadLine(), out scelta);
                    if (!x || scelta < 1 || scelta > 7)
                    {
                        WriteLine("Inserisci un numero valido (da 1 a 7)");
                    }
                } while (!x || scelta < 1 || scelta > 7);

                switch (scelta)
                {
                    case 1:
                        RiempiRecinto();
                        break;

                    case 2:
                        SostituisciDinosauro();
                        break;

                    case 3:
                        ScambiaProprietari();
                        break;

                    case 4:
                        VisualizzaTutti();
                        break;

                    case 5:
                        FiltraPerTipo();
                        break;

                    case 6:
                        FiltraPerTaglia();
                        break;

                    case 7:
                        WriteLine("Programma terminato");
                        break;

                    default:
                        WriteLine("Scelta non valida");
                        break;
                }
            } while (scelta != 7);
        }

        static void RiempiRecinto()
        {
            int quanti;      
            bool x;

            conteggio = 0;

            do
            {
                Write("Quanti dinosauri inserire (1-100)? ");
                x = int.TryParse(ReadLine(), out quanti);

                if (!x)
                {
                    WriteLine("Devi inserire un numero");
                }
                else if (quanti < 1 || quanti > 100)
                {
                    WriteLine("Valore fuori range (1-100)");
                }
            } while (!x || quanti < 1 || quanti > 100);

            for (int i = 0; i < quanti; i++)            //ciclo per inserire i dinosauri uno alla volta
            {
                WriteLine($"Inserimento {i + 1} / {quanti}");

                int codice = i + 1;
                TipoDinosauro tipo = LeggiTipo();
                Taglia taglia = LeggiTaglia();
                int eta = LeggiEta();
                int forza = LeggiForza();
                string proprietario = LeggiProprietario();

                recinto[i] = new Dinosauro(codice, tipo, taglia, eta, forza, proprietario);     //creazione del dinosauro e inserimento nel vettore
                conteggio++;
            }

            WriteLine($"Inseriti {conteggio} dinosauri.");
        }

        static void SostituisciDinosauro()          //sostituisce il dinosauro con uno nuovo
        {
            int codice;
            int posizione;
            if (conteggio == 0)
            {
                WriteLine("Nessun dinosauro presente");
                return;
            }
            do
            {
                WriteLine("Inserisci il codice del dinosauro da sostituire");
                codice = LeggiCodice();
                posizione = TrovaPerCodice(codice);

                if (posizione == -1)
                {
                    WriteLine("Codice non trovato");
                }
            } while (posizione == -1);
            

            WriteLine(recinto[posizione].Stampa());     //stampa i dati del dinosauro da sostituire

            WriteLine("Inserisci i nuovi dati:");
            TipoDinosauro tipo = LeggiTipo();
            Taglia taglia = LeggiTaglia();
            int eta = LeggiEta();
            int forza = LeggiForza();
            string proprietario = LeggiProprietario();

            recinto[posizione] = new Dinosauro(codice, tipo, taglia, eta, forza, proprietario); //creazione del nuovo dinosauro e sostituzione di quello vecchio
            WriteLine("Dinosauro sostituito");
        }

        static void ScambiaProprietari()
        {
            if (conteggio < 2)
            {
                WriteLine("Devono esserci almeno 2 dinosauri");
                return;
            }

            int codice1;
            int codice2;
            int pos1;
            int pos2;

            do
            {
                WriteLine("Primo dinosauro:");
                codice1 = LeggiCodice();
                pos1 = TrovaPerCodice(codice1);

                if (pos1 == -1)
                {
                    WriteLine("Codice non trovato, riprova.");
                }
            } while (pos1 == -1);

            do
            {
                WriteLine("Secondo dinosauro:");
                codice2 = LeggiCodice();
                pos2 = TrovaPerCodice(codice2);

                if (pos2 == -1)
                {
                    WriteLine("Codice non trovato, riprova.");
                }
                else if (pos1 == pos2)
                {
                    WriteLine("Hai inserito lo stesso dinosauro! Inserisci un codice diverso.");
                    pos2 = -1;          //far ripetere il ciclo
                }
                } while (pos2 == -1);

                // scambio
                string tempProprietario = recinto[pos1].proprietario;        //salvo il proprietario del primo dinosauro in una variabile temporanea
                recinto[pos1].proprietario = recinto[pos2].proprietario;
                recinto[pos2].proprietario = tempProprietario;              //assegno al secondo dinosauro il proprietario del primo dinosauro

                WriteLine("Scambio completato con successo!");
                WriteLine(recinto[pos1].Stampa());
                WriteLine(recinto[pos2].Stampa());
    }

        static void VisualizzaTutti()
        {
            if (conteggio == 0)
            {
                WriteLine("Nessun dinosauro nel recinto");
                return;
            }

            for (int i = 0; i < conteggio; i++)
            {
                WriteLine(recinto[i].Stampa());     //stampa i dati del dinosauro nell'array
            }
        }

        static void FiltraPerTipo()
        {
            if (conteggio == 0)
            {
                WriteLine("Nessun dinosauro presente");
                return;
            }

            Write("Tipo da visualizzare (0-4): ");
            TipoDinosauro tipoCercato = LeggiTipo();

            int trovati = 0;

            for (int i = 0; i < conteggio; i++)
            {
                if (recinto[i].tipo == tipoCercato)         //se il tipo del dinosauro è uguale a quello cercato, allora stampo i suoi dati
                {
                    WriteLine(recinto[i].Stampa());
                    trovati++;
                }
            }

            if (trovati == 0)
            {
                WriteLine("Nessun dinosauro di questo tipo");
            }
            else
            {
                WriteLine($"Trovati {trovati} dinosauri");
            }
        }

        static void FiltraPerTaglia()
        {
            if (conteggio == 0)
            {
                WriteLine("Nessun dinosauro presente");
                return;
            }

            Write("Taglia da visualizzare (0-2): ");
            Taglia tagliaCercata = LeggiTaglia();

            int trovati = 0;

            for (int i = 0; i < conteggio; i++)
            {
                if (recinto[i].taglia == tagliaCercata)
                {
                    WriteLine(recinto[i].Stampa());
                    trovati++;
                }
            }

            if (trovati == 0)
            {
                WriteLine("Nessun dinosauro di questa taglia");
            }
            else
            {
                WriteLine($"Trovati {trovati} dinosauri");
            }
        }


    //funzioni per leggere i dati dei dinosauri
        static TipoDinosauro LeggiTipo()
        {
            int valore;
            bool x;

            do
            {
                WriteLine("0 Brontosauro   1 Triceratopo");
                WriteLine("2 Pterodattilo  3 TRex       4 Stegosauro");
                Write("Scelta: ");
                x = int.TryParse(ReadLine(), out valore);

                if (!x)
                {
                    WriteLine("Inserisci un numero");
                }
                else if (valore < 0 || valore > 4)
                {
                    WriteLine("Valore non valido (0-4)");
                }
            } while (!x || valore < 0 || valore > 4);

            return (TipoDinosauro)valore;       //converte il numero inserito in un valore dell'enum TipoDinosauro
        }

        static Taglia LeggiTaglia()
        {
            int valore;
            bool x;

            do
            {
                WriteLine("0 Piccolo  1 Medio  2 Grande");
                Write("Scelta: ");
                x = int.TryParse(ReadLine(), out valore);

                if (!x)
                {
                    WriteLine("Inserisci un numero");
                }
                else if (valore < 0 || valore > 2)
                {
                    WriteLine("Valore non valido (0-2)");
                }
            } while (!x || valore < 0 || valore > 2);

            return (Taglia)valore;
        }

        static int LeggiEta()
        {
            int eta;
            bool x;

            do
            {
                Write("Età (>= 0): ");
                x = int.TryParse(ReadLine(), out eta);

                if (!x)
                {
                    WriteLine("Inserisci un numero");
                }
                else if (eta < 0)
                {
                    WriteLine("Età non può essere negativa");
                }
            } while (!x || eta < 0);

            return eta;
        }

        static int LeggiForza()
        {
            int forza;
            bool x;

            do
            {
                Write("Forza (1-100): ");
                x = int.TryParse(ReadLine(), out forza);

                if (!x)
                {
                    WriteLine("Inserisci un numero");
                }
                else if (forza < 1 || forza > 100)
                {
                    WriteLine("Valore fuori range (1-100)");
                }
            } while (!x || forza < 1 || forza > 100);

            return forza;
        }

        static string LeggiProprietario()
        {
            Write("Nome proprietario: ");
            
            string nome = ReadLine();
            
            if (nome == "")      
            {
                nome = "Nessuno";
            }
            
            return nome;
        }

    //ricerche
        static int LeggiCodice()
        {
            int codice;
            bool x;

            do
            {
                Write("Inserisci codice del dinosauro: ");
                x = int.TryParse(ReadLine(), out codice);

                if (!x)
                {
                    WriteLine("Inserisci un numero");
                }
                else if (codice <= 0)
                {
                    WriteLine("Il codice deve essere positivo");
                }
            } while (!x || codice <= 0);

            return codice;
        }

        static int TrovaPerCodice(int codice)
        {
            for (int i = 0; i < conteggio; i++)
            {
                if (recinto[i].codice == codice)
                {
                    return i;           //indice del dinosauro trovato
                }
            }
            return -1;
        }
    }
}