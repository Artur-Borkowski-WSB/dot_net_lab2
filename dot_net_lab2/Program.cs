namespace Pesel
{
    public class PESELWalidator
    {
        protected int[] wagi = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
        protected int[] pesel;

        public PESELWalidator(String pesel)
        {
            WczytajPesel(pesel);
        }

        public void WczytajPesel(String pesel)
        {
            String pattern = @"^\d{11}$";
            if (System.Text.RegularExpressions.Regex.IsMatch(pesel, pattern) == false
                || pesel == "00000000000"
                || pesel == "12345678910")
            {
                throw new ArgumentException("Wprowadzono niepoprawy pesel");
            }
            else
            {
                char[] chars = pesel.ToCharArray();
                this.pesel = Array.ConvertAll(chars, ch => (int)Char.GetNumericValue(ch));
            }
        }

        public int SumaKontrolna()
        {
            int suma = 0;
            for (int i = 0; i < 10; i++)
            {
                suma += this.wagi[i] * this.pesel[i];
            }
            suma = 10 - suma % 10;
            suma = suma == 10 ? 0 : suma;
            return suma;
        }

        public String DataUrodzenia()
        {
            String data;
            int stulecie, msc, rok, miesiac, dzien;
            bool przestepny = false;
            if (this.pesel[2] == 8 || this.pesel[2] == 9)
            {
                stulecie = 1800;
                msc = 80;
            }
            else if (this.pesel[2] == 1 || this.pesel[2] == 0)
            {
                stulecie = 1900;
                msc = 0;
            }
            else if (this.pesel[2] == 2 || this.pesel[2] == 3)
            {
                stulecie = 2000;
                msc = 20;
            }
            else if (this.pesel[2] == 4 || this.pesel[2] == 5)
            {
                stulecie = 2100;
                msc = 40;
            }
            else if (this.pesel[2] == 6 || this.pesel[2] == 7)
            {
                stulecie = 2200;
                msc = 60;
            }
            else
            {
                return "Niepoprawna data urodzenia";
            }

            rok = stulecie + int.Parse((this.pesel[0].ToString() + this.pesel[1].ToString()));
            if ((rok % 4 == 0 && rok % 100 != 0) || rok % 400 == 0)
            {
                przestepny = true;
            }
            miesiac = int.Parse(this.pesel[2].ToString() + this.pesel[3].ToString()) - msc;
            dzien = int.Parse(this.pesel[4].ToString() + this.pesel[5].ToString());
            if (miesiac > 12 || miesiac <= 0 || dzien <= 0)
            {
                return "Niepoprawna data urodzenia";
            }
            else if (miesiac == 2)
            {
                if (dzien > (przestepny == true ? 29 : 28))
                {
                    return "Niepoprawna data urodzenia";
                }
            }
            else if (miesiac == 4 || miesiac == 6 || miesiac == 9 || miesiac == 11)
            {
                if (dzien > 30)
                {
                    return "Niepoprawna data urodzenia";
                }
            }
            else
            {
                if (dzien > 31)
                {
                    return "Niepoprawna data urodzenia";
                }
            }
            data = rok + (miesiac < 10 ? "-0" : "-") + miesiac + (dzien < 10 ? "-0" : "-") + dzien;
            return data;
        }

        public String Plec()
        {
            return this.pesel[9] % 2 == 0 ? "Kobieta" : "Mężczyzna";
        }

        public Boolean SprawdzPesel()
        {
            String peselStr = "";
            foreach (int ch in this.pesel)
            {
                peselStr += ch.ToString();
            }
            Console.WriteLine(peselStr);
            if (SumaKontrolna() != this.pesel[10])
            {
                Console.WriteLine("Niepoprawna suma kontrolna");
                return false;
            }
            String data = DataUrodzenia();
            Console.WriteLine(data);
            if (data == "Niepoprawna data urodzenia")
            {
                return false;
            }
            Console.WriteLine(Plec());
            Console.WriteLine("Pesel jest poprawny");
            return true;
        }

        static void Main()
        {
            PESELWalidator pesel = new("00272513576");
            pesel.SprawdzPesel();
        }
    }
}
