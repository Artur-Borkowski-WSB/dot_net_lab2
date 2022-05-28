using Pesel;

namespace PeselTests
{
    [TestClass]
    public class PESELUnitTest
    {
        [TestMethod]
        public void SprawdzanieNumerow()
        {
            String[] zlePesele = { "12345678910", "qwertyuiopx", "00000000000", "1234567890", "098765432101" };
            String[] dobrePesele = { "22222222222", "00523168533", "88822977850", "21681600439", "47070331355" };

            foreach (String pesel in zlePesele)
            {
                Assert.ThrowsException<ArgumentException>(() => new PESELWalidator(pesel), pesel);
            }

            foreach (String pesel in dobrePesele)
            {
                try
                {
                    new PESELWalidator(pesel);
                }
                catch (Exception)
                {
                    Assert.Fail(pesel);
                }
            }
        }

        [TestMethod]
        public void SprawdzanieSum()
        {
            PESELWalidator pesel1 = new("22222222222"),
                pesel2 = new("88822977850"),
                pesel3 = new("65011197178");

            int sum1 = pesel1.SumaKontrolna(),
                sum2 = pesel2.SumaKontrolna(),
                sum3 = pesel3.SumaKontrolna();

            Assert.AreEqual(sum1, 2);
            Assert.AreEqual(sum2, 0);
            Assert.AreEqual(sum3, 8);
        }

        [TestMethod]
        public void SprawdzanieDat()
        {
            PESELWalidator pesel1 = new("00222937258"),
                pesel2 = new("78132012640"),
                pesel3 = new("00422944261"),
                pesel4 = new("00272513576");

            String data1 = pesel1.DataUrodzenia(),
                data2 = pesel2.DataUrodzenia(),
                data3 = pesel3.DataUrodzenia(),
                data4 = pesel4.DataUrodzenia();

            Assert.AreEqual(data1, "2000-02-29");
            Assert.AreEqual(data2, "Niepoprawna data urodzenia");
            Assert.AreEqual(data3, "Niepoprawna data urodzenia");
            Assert.AreEqual(data4, "2000-07-25");
        }

        [TestMethod]
        public void SprawdzaniePlci()
        {
            String[] kobiety = {
                "53072358883",
                "98010695644",
                "57121838889",
                "73052355961",
                "61102793568",
                "94071579741",
                "98072497448",
                "59111939767",
                "85111234984",
                "60020214762",
            };

            String[] mezczyzni = {
                "68052873133",
                "87110487134",
                "05220312412",
                "91110613895",
                "86031854971",
                "68103064392",
                "77110569251",
                "85013096275",
                "63072891912",
                "02251587314",
            };

            foreach (String pesel in kobiety)
            {
                PESELWalidator k = new(pesel);
                Assert.AreEqual("Kobieta", k.Plec(), pesel);
            }

            foreach (String pesel in mezczyzni)
            {
                PESELWalidator m = new(pesel);
                Assert.AreEqual("Mê¿czyzna", m.Plec(), pesel);
            }
        }

        [TestMethod]
        public void SprawdzaniePeseli()
        {
            String[] zlePesele = { "65011197170", "22222222223", "01222872474", "99113117392", "78132012640" };
            String[] dobrePesele = { "22222222222", "00222937258", "88822977850", "21681600439", "47070331355" };

            foreach (String pesel in zlePesele)
            {
                PESELWalidator zle = new(pesel);
                Assert.IsFalse(zle.SprawdzPesel(), pesel);
            }

            foreach (String pesel in dobrePesele)
            {
                PESELWalidator dobre = new(pesel);
                Assert.IsTrue(dobre.SprawdzPesel(), pesel);
            }
        }
    }
}