using System;
using System.Collections.Generic;
using System.Text;

namespace GraObiektowo
{
    public class Ruch
    {
        public DateTime Czas { get; }
        public int Propozycja { get; }
        public ModelGry.Odpowiedz Wynik { get; }
        public ModelGry.Status StatusGry { get; }

        public Ruch(int propozycja, ModelGry.Odpowiedz wynik, ModelGry.Status statusGry)
        {
            Propozycja = propozycja;
            Wynik = wynik;
            StatusGry = statusGry;
            Czas = DateTime.Now;
        }

        public override string ToString() => $"({Propozycja}, {Wynik}, {Czas}, {StatusGry})";
    }
}