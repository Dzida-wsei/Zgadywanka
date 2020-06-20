using System;
using System.Collections.Generic;

namespace GraObiektowo
{
    /// <summary>
    /// Odpowiedzialna za logikę gry "Za duzo za mało".
    /// </summary>
    /// <remarks>
    /// Gra może być w jednym z 3 mozliwych stanów
    /// WTrakcie - gracz jeszcze nie odgadł liczby, może pdawac propozycje
    /// Poddana - gracz przerwał rozgrywkę, może poznać liczbę, którą odgadywał
    /// Zakonczona - gracz odgadł liczbę
    /// </remarks>
    public class ModelGry
    {
        readonly private int liczbaDoOdgadniecia;
        public int LiczbaOdgadywana
        {
            get
            {
                if (StanGry == Status.WTrakcie)
                    throw new NotSupportedException("nie wolno odczytać, bo gra trwa");

                return liczbaDoOdgadniecia;
            }
        }

        public int MinimalnaLiczbaDoOdgadniecia { get; private set; }
        public int MaksymalnaLiczbaDoOdgadniecia { get; private set; }

        public Status StanGry { get; private set; }
        // historia gry
        private readonly List<Ruch> historia;

        public IReadOnlyList<Ruch> HistoriaGry => historia.AsReadOnly();


        //public ModelGry(int min = 1, int max = 100)
        public ModelGry(int min, int max)
        {
            if (min > max)
                throw new ArgumentException("zły przedział losowania");

            MinimalnaLiczbaDoOdgadniecia = min;
            MaksymalnaLiczbaDoOdgadniecia = max;

            liczbaDoOdgadniecia = (new Random()).Next(min, max + 1);
            StanGry = Status.WTrakcie;

            // zainicjowanie historii gry
            historia = new List<Ruch>();
        }

        public ModelGry() : this(1, 100) { }


        /// <summary>
        /// Zwraca odpowiedź na podstawie podanej propozycji
        /// </summary>
        /// <param name="propozycja">dowolna liczba całkowita</param>
        /// <returns>jeśli <c>propozycja</c> jest mniejsza liczby odgadywanej .... </returns>
        /// <example>
        /// </example>
        public Odpowiedz Ocena(int propozycja)
        {
            Random rand = new Random();
            int test = rand.Next(0, 2);
            Odpowiedz odp;
            if (propozycja == liczbaDoOdgadniecia)
            {
                StanGry = Status.Zakonczona;
                odp = Odpowiedz.Trafiono;
            }
            else if (propozycja + 2 == liczbaDoOdgadniecia && test == 0)

                odp = Odpowiedz.ZaMalo;
            else if (propozycja + 2 == liczbaDoOdgadniecia && test == 1)

                odp = Odpowiedz.ZaDuzo;

            else if (propozycja < liczbaDoOdgadniecia)
                odp = Odpowiedz.ZaMalo;
            else
                odp = Odpowiedz.ZaDuzo;

            var ruch = new Ruch(propozycja, odp, StanGry);
            historia.Add(ruch);
            return odp;
        }


        public int Przerwij()
        {
            StanGry = Status.Poddana;
            return liczbaDoOdgadniecia;
        }


        public override string ToString()
        {
            return $"Losowanie z przedziału: {MinimalnaLiczbaDoOdgadniecia} .. {MaksymalnaLiczbaDoOdgadniecia}";
        }

        // =======================
        public enum Odpowiedz
        {
            ZaMalo = -1,
            Trafiono = 0,
            ZaDuzo = 1
        }

        public enum Status { WTrakcie, Zakonczona, Poddana }
    }
}