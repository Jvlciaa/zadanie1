using System;
using System.IO;

class Program
{
    static void Main()
    {
        string sciezkaKlucz = "klucz.txt";
        string sciezkaOdpowiedzi = "odpowiedzi.txt";
        string sciezkaWyniki = "wyniki.txt";

        if(File.Exists(sciezkaKlucz) && File.Exists(sciezkaOdpowiedzi))
        {
            string zawartosc = File.ReadAllText(sciezkaKlucz);
            string[] klucz = zawartosc.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            
            Console.WriteLine("Poprawnie wczytano klucz!");

            Console.WriteLine("\nSprawdzanie odpowiedzi uczestników...");
            string[] linieOdpowiedzi = File.ReadAllLines(sciezkaOdpowiedzi);

            using (StreamWriter writer = new StreamWriter(sciezkaWyniki))
            {
                foreach (string linia in linieOdpowiedzi)
                {
                    if (string.IsNullOrWhiteSpace(linia)) continue;

                    string[] dane = linia.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    
                    if (dane.Length < 2) continue;

                    string imie = dane[0];
                    string nazwisko = dane[1];
                    int poprawnePunkty = 0;

                    for (int i = 0; i < klucz.Length; i++)
                    {
                        if (i + 2 < dane.Length && dane[i + 2] == klucz[i])
                        {
                            poprawnePunkty++;
                        }
                    }

                    double procent = (double)poprawnePunkty / klucz.Length * 100;
                    int koncowyProcent = (int)Math.Round(procent);

                    writer.WriteLine($"{imie} {nazwisko} {koncowyProcent}%");
                }
            }
            Console.WriteLine($"Gotowe! Wyniki zapisano w pliku {sciezkaWyniki}.");
        }
        else
        {
            Console.WriteLine("Nie znaleziono pliku klucz.txt lub odpowiedzi.txt w folderze z programem.");
        }
    }
}