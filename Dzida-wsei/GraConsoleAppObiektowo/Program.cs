﻿using System;
using GraObiektowo;

namespace GraConsoleAppObiektowo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Witaj, zagrajmy w zgadywankę");
            Console.WriteLine("Pamiętaj, komputer może Cię raz oszukać");
            Console.Write("Podaj min i maks oddzielone spacją: ");
            string[] dane = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int[] zakres = Array.ConvertAll(dane, int.Parse);

            ModelGry gra = new ModelGry(zakres[0], zakres[1]);

            int menu;
            do
            {
                Console.WriteLine("-------------------");
                Console.WriteLine("1. Podaję liczbę");
                Console.WriteLine("2. ");
                Console.WriteLine("0. Poddaję się");
                Console.WriteLine("-------------------");
                menu = int.Parse(Console.ReadLine());
                switch (menu)
                {
                    case 0:
                        foreach (var ruch in gra.HistoriaGry)
                        {
                            Console.WriteLine(ruch);
                        }

                        Console.WriteLine($"gra przerwana, poszukiwana = {gra.Przerwij()}");
                        Console.WriteLine(gra.MinimalnaLiczbaDoOdgadniecia);
                        Console.WriteLine(gra.MaksymalnaLiczbaDoOdgadniecia);
                        Console.WriteLine(gra.StanGry);
                        break;
                    case 1:
                        Console.Write("Wylosowałem liczbę, podaj propozycję:");
                        var prop = int.Parse(Console.ReadLine());
                        var odp = gra.Ocena(prop);
                        foreach (var ruch in gra.HistoriaGry)
                        {
                            Console.WriteLine(ruch);
                        }
                        
                        break;
                    case 2:
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Podaj wlasciwy numer!");
                        break;
                }
            } while (menu != 0);

        }
    }
}