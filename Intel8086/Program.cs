namespace Intel8086
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<string> rejestry = new List<string>();
            List<string> nazwyRejestrow = new List<string> { "AL", "BH", "BL", "CH", "CL", "DH", "DL" };

            messageScanRegistry("AH");
            string ahRegistry = Console.ReadLine();
            rejestry.Add(ahRegistry);

            nazwyRejestrow.ForEach(delegate(string nazwaRejestru)
            {
                messageScanRegistry(nazwaRejestru);
                rejestry.Add(Console.ReadLine());
            });

            messageCommand();
            messageMenu();
            var command = (Rozkaz)Convert.ToInt16(Console.ReadLine());


            while (!command.Equals(Rozkaz.exit))
            {

                switch (command)
                {
                    case Rozkaz.mov:
                        MovCommand(rejestry);
                        break;
                    case Rozkaz.xchg:
                        XchgCommand(rejestry);
                        break;
                    case Rozkaz.not:
                        NotCommand(rejestry);
                        break;
                    case Rozkaz.inc:
                        IncCommand(rejestry);
                        break;
                    case Rozkaz.dec:
                        DecCommand(rejestry);
                        break;
                    case Rozkaz.and:
                        AndCommand(rejestry);
                        break;
                    case Rozkaz.or:
                        OrCommand(rejestry);
                        break;
                    case Rozkaz.xor:
                        XorCommand(rejestry);
                        break;
                    case Rozkaz.add:
                        AddCommand(rejestry);
                        break;
                    case Rozkaz.sub:
                        SubCommand(rejestry);
                        break;

                }

                messageCommand();
                messageMenu();
                command = (Rozkaz)Convert.ToInt16(Console.ReadLine());

            }
        }

        private static void messageScanRegistry(string registry)
        {
            Console.WriteLine("Podaj wartość rejestru " + registry);
        }

        private static void messageFirstRegistry()
        {
            Console.Write("Podaj pierwszy rejestr");
            Console.Write(" 0.AH 1.AL 2.BH 3.BL 4.CH 5.CL 6.DH 7.DL\n");
        }

        private static void messageSecondRegistry()
        {
            Console.Write("Podaj drugi rejestr");
            Console.Write(" 0.AH 1.AL 2.BH 3.BL 4.CH 5.CL 6.DH 7.DL\n");
        }

        private static void messageMenu()
        {
            Console.Write("0.mov 1.xchg 2.not 3.inc 4.dec 5.and 6.or 7.xor 8.add 9.sub 10. exit\n");
        }

        private static void messageCommand()
        {
            Console.Write("Podaj rozkaz do wykonania\n");
        }

        private static void MovCommand(List<string> rejestry) // Rozkaz Mov
        {
            messageFirstRegistry();
            int pRej = Convert.ToInt16(Console.ReadLine());
            messageSecondRegistry();
            int dRej = Convert.ToInt16(Console.ReadLine());
            rejestry[pRej] = rejestry[dRej];
            Console.WriteLine($"Pierwszy rejestr wynosi {rejestry[pRej]}, drugi rejestr wynosi {rejestry[dRej]}");
        }

        private static void XchgCommand(List<string> rejestry) //Rozkaz Xchg
        {
            messageFirstRegistry();
            int pRej = Convert.ToInt16(Console.ReadLine());
            messageSecondRegistry();
            int dRej = Convert.ToInt16(Console.ReadLine());
            var tymczasowa = rejestry[pRej];
            rejestry[pRej] = rejestry[dRej];
            rejestry[dRej] = tymczasowa;
            Console.WriteLine($"Pierwszy rejestr wynosi {rejestry[pRej]}, drugi rejestr wynosi {rejestry[dRej]}");
        }

        private static void NotCommand(List<string> rejestry) //rozkaz NOT
        {
            messageFirstRegistry();
            int pRej = Convert.ToInt16(Console.ReadLine());
            messageSecondRegistry();
            int dRej = Convert.ToInt16(Console.ReadLine());


            string pbin = HexToBinary(rejestry[pRej]);
            string dbin = HexToBinary(rejestry[dRej]);
            string pbinNOT = "";
            for (int i = 0; i < pbin.Length; i++) //Operacja logiczna NOT na liczbie binarnej pierwszego rejestru
            {
                if (pbin[i] == '0')
                {
                    pbinNOT += "1";
                }
                else if (pbin[i] == '1')
                {
                    pbinNOT += "0";
                }
            }

            string dbinNOT = "";
            for (int i = 0; i < dbin.Length; i++) //Operacja logiczna NOT na liczbie binarnej drugiego rejestru
            {
                if (dbin[i] == '0')
                {
                    dbinNOT += "1";
                }
                else if (dbin[i] == '1')
                {
                    dbinNOT += "0";
                }
            }

            rejestry[pRej] = Convert.ToString(BinaryToHex(pbinNOT));
            rejestry[dRej] = Convert.ToString(BinaryToHex(dbinNOT));
            Console.WriteLine($"Pierwszy rejestr wynosi {rejestry[pRej]}, drugi rejestr wynosi {rejestry[dRej]}");
        }

        private static void IncCommand(List<string> rejestry) //Rozkaz INC
        {
            messageFirstRegistry();
            int pRej = Convert.ToInt16(Console.ReadLine());
            messageSecondRegistry();
            int dRej = Convert.ToInt16(Console.ReadLine());
            string pbin = HexToBinary(rejestry[pRej]);
            string dbin = HexToBinary(rejestry[dRej]);
            rejestry[pRej] = BinaryToHex(IncBinary(pbin));
            rejestry[dRej] = BinaryToHex(IncBinary(dbin));
            Console.WriteLine($"Pierwszy rejestr wynosi {rejestry[pRej]}, drugi rejestr wynosi {rejestry[dRej]}");
        }

        private static string IncBinary(string binary) //zwieksza binarna wartosc o 1
        {
            var digits = binary.Select(c => c - '0').ToList();
            for (int i = digits.Count - 1; i >= 0; i--)
            {
                if (digits[i] == 1)
                {
                    digits[i] = 0;
                }
                else
                {
                    digits[i] = 1;
                    break;
                }
            }

            if (digits[0] == 0)
            {
                digits.Insert(0, 1);
            }

            return string.Join("", digits);
        }

        private static string DecBinary(string binaryString) //zmniejsza binarna wartosc o 1
        {
            try
            {
                long binaryNumber = Convert.ToInt64(binaryString, 2);
                binaryNumber--;
                string stated = Convert.ToString(binaryNumber, 2);
                return stated;
            }
            catch
            {
                return "0";
            }
        }

        private static void DecCommand(List<string> rejestry) //instrukcja DEC
        {
            messageFirstRegistry();
            int pRej = Convert.ToInt16(Console.ReadLine());
            messageSecondRegistry();
            int dRej = Convert.ToInt16(Console.ReadLine());
            string pbin = HexToBinary(rejestry[pRej]);
            string dbin = HexToBinary(rejestry[dRej]);
            rejestry[pRej] = BinaryToHex(DecBinary(pbin));
            rejestry[dRej] = BinaryToHex(DecBinary(dbin));
            Console.WriteLine($"Pierwszy rejestr wynosi {rejestry[pRej]}, drugi rejestr wynosi {rejestry[dRej]}");
        }

        private static void AndCommand(List<string> rejestry) //instrukcja AND
        {
            messageFirstRegistry();
            int pRej = Convert.ToInt16(Console.ReadLine());
            messageSecondRegistry();
            int dRej = Convert.ToInt16(Console.ReadLine());
            string pbin = HexToBinary(rejestry[pRej]);
            string dbin = HexToBinary(rejestry[dRej]);
            int tymczasowaX = Convert.ToInt32(pbin, 2);
            int tymczasowaY = Convert.ToInt32(dbin, 2);
            int result = tymczasowaX & tymczasowaY;
            string wynik = Convert.ToString(result, 2);
            string WynikAnd = BinaryToHex(wynik);
            Console.WriteLine($"Wynik oprecji AND na rejestrach {rejestry[pRej]} i {rejestry[dRej]} wynosi {WynikAnd}");
        }

        private static void OrCommand(List<string> rejestry) //instrukcja OR
        {
            messageFirstRegistry();
            int pRej = Convert.ToInt16(Console.ReadLine());
            messageSecondRegistry();
            int dRej = Convert.ToInt16(Console.ReadLine());
            string pbin = HexToBinary(rejestry[pRej]);
            string dbin = HexToBinary(rejestry[dRej]);
            int tymczasowaX = Convert.ToInt32(pbin, 2);
            int tymczasowaY = Convert.ToInt32(dbin, 2);
            int result = tymczasowaX | tymczasowaY;
            string wynik = Convert.ToString(result, 2);
            string WynikOr = BinaryToHex(wynik);
            Console.WriteLine($"Wynik oprecji OR na rejestrach {rejestry[pRej]} i {rejestry[dRej]} wynosi {WynikOr}");
        }

        private static void XorCommand(List<string> rejestry) //instrukcja XOR
        {
            messageFirstRegistry();
            int pRej = Convert.ToInt16(Console.ReadLine());
            messageSecondRegistry();
            int dRej = Convert.ToInt16(Console.ReadLine());
            string pbin = HexToBinary(rejestry[pRej]);
            string dbin = HexToBinary(rejestry[dRej]);
            int tymczasowaX = Convert.ToInt32(pbin, 2);
            int tymczasowaY = Convert.ToInt32(dbin, 2);
            int result = tymczasowaX ^ tymczasowaY;
            string wynik = Convert.ToString(result, 2);
            string WynikXor = BinaryToHex(wynik);
            Console.WriteLine($"Wynik oprecji XOR na rejestrach {rejestry[pRej]} i {rejestry[dRej]} wynosi {WynikXor}");
        }

        private static void AddCommand(List<string> rejestry) //instrukcja ADD
        {
            messageFirstRegistry();
            int pRej = Convert.ToInt16(Console.ReadLine());
            messageSecondRegistry();
            int dRej = Convert.ToInt16(Console.ReadLine());
            string pbin = HexToBinary(rejestry[pRej]);
            string dbin = HexToBinary(rejestry[dRej]);
            int length = Math.Max(pbin.Length, dbin.Length);
            pbin = pbin.PadLeft(length, '0');
            dbin = dbin.PadLeft(length, '0');
            string result = "";
            int carry = 0;
            for (int i = length - 1; i >= 0; i--)
            {
                int sum = (pbin[i] - '0') + (dbin[i] - '0') + carry;
                carry = sum / 2;
                result = (sum % 2) + result;
            }

            if (carry > 0)
            {
                result = carry + result;
            }

            string wynikAdd = BinaryToHex(result);
            Console.WriteLine($"Wynik oprecji ADD na rejestrach {rejestry[pRej]} i {rejestry[dRej]} wynosi {wynikAdd}");
        }

        private static void SubCommand(List<string> rejestry) //instrukcja SUB
        {
            messageFirstRegistry();
            int pRej = Convert.ToInt16(Console.ReadLine());
            messageSecondRegistry();
            int dRej = Convert.ToInt16(Console.ReadLine());
            string pbin = HexToBinary(rejestry[pRej]);
            string dbin = HexToBinary(rejestry[dRej]);
            int tymczasowaX = Convert.ToInt32(pbin, 2);
            int tymczasowaY = Convert.ToInt32(dbin, 2);
            int wyliczone = tymczasowaX - tymczasowaY;
            string decwyn = Convert.ToString(wyliczone);
            string wynikSub;
            if (decwyn[0] == '-')
            {
                decwyn = decwyn.Substring(1);
                string dectohex = Convert.ToString(Convert.ToInt32(decwyn, 10), 16);
                wynikSub = "-" + dectohex.ToUpper();
            }
            else
            {
                string dectohex = Convert.ToString(Convert.ToInt32(decwyn, 10), 16);
                wynikSub = dectohex.ToUpper();
            }

            Console.WriteLine($"Wynik oprecji SUB na rejestrach {rejestry[pRej]} i {rejestry[dRej]} wynosi {wynikSub}");
        }

        static string HexToBinary(string Hex) // zamiana zawartości na binarny
        {
            return Convert.ToString(Convert.ToInt32(Hex, 16), 2);
        }

        static string BinaryToHex(string binary) //binarny na szesnastkowy + anty bufor
        {
            if (binary == "1111111111111111111111111111111111111111111111111111111111111111")
            {
                return "0";
            }
            else
            {
                string hex = Convert.ToInt32(binary, 2).ToString("X");
                return hex;
            }
        }

        enum Rozkaz
        {

            mov,
            xchg,
            not,
            inc,
            dec,
            and,
            or,
            xor,
            add,
            sub,
            exit
        }
    }
}