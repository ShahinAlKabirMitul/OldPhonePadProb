
using System.Text;

namespace OldPhonePadProblem;

public abstract class OldPhoneKeypad
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine(OldPhonePad("33#")); // Output: E
            Console.WriteLine(OldPhonePad("227*#")); // Output: B
            Console.WriteLine(OldPhonePad("4433555 555666#")); // Output: HELLO
            Console.WriteLine(OldPhonePad("8 88777444666*664#")); // Output: TUNING
            Console.WriteLine(OldPhonePad(null));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private  static string OldPhonePad(string input)
    {
        try
        {
            var finalOutput = new StringBuilder();
            char? lastKey = null;
            var count = 0;

            if (string.IsNullOrEmpty(input))
            {
                return "Invalid input";
            }
            
            var keyPadLogic = new Dictionary<char, string>
            {
                { '1', "" },
                { '2', "ABC" },
                { '3', "DEF" },
                { '4', "GHI" },
                { '5', "JKL" },
                { '6', "MNO" },
                { '7', "PQRS" },
                { '8', "TUV" },
                { '9', "WXYZ" },
                { '0', " " }
            };

            foreach (var c in input)
            {
                if (c == '#')
                {
                    AppendCharacter(finalOutput, keyPadLogic, lastKey, count);
                    break;
                }
                switch (c)
                {
                    case '*':
                    {
                        AppendCharacter(finalOutput, keyPadLogic, lastKey, count);
                        if (finalOutput.Length > 0) finalOutput.Length--;
                        lastKey = null;
                        count = 0;
                        break;
                    }
                    case ' ':
                        AppendCharacter(finalOutput, keyPadLogic, lastKey, count);
                        lastKey = null;
                        count = 0;
                        break;
                    default:
                    {
                        if (keyPadLogic.ContainsKey(c))
                        {
                            if (lastKey == c)
                            {
                                count++;
                            }
                            else
                            {
                                AppendCharacter(finalOutput, keyPadLogic, lastKey, count);
                                lastKey = c;
                                count = 1;
                            }
                        }

                        break;
                    }
                }
            }

            return finalOutput.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while processing the input: {ex.Message}");
            return string.Empty;
        }
    }

    private static void AppendCharacter(StringBuilder finalOutput, Dictionary<char, string> keyPadLogic, char? lastKey,
        int count)
    {
        if (lastKey == null || count <= 0 || keyPadLogic[lastKey.Value].Length <= 0)
        {
            return;
        }

        var finalizedChar = keyPadLogic[lastKey.Value][(count - 1) % keyPadLogic[lastKey.Value].Length];
        finalOutput.Append(finalizedChar);
    }
}