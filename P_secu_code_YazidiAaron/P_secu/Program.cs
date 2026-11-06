using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace P_secu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Boucle pour recommencer
            bool recommencer;

            do
            {
                // Affichage choix de l'utilisateur
                Console.Clear();

                Console.WriteLine("=== Couteau Suisse - Utilitaires ===");
                Console.WriteLine("1. Convertir du texte en code Morse");
                Console.WriteLine("2. Convertir des nombres entre différentes bases (Décimal <> Binaire <> Octal)");
                Console.WriteLine("3. Stéganographie");
                Console.Write("Veuillez entrer votre choix : ");

                string input = Console.ReadLine();

                // Converteur Morse
                if (input == "1")
                {
                    Console.Clear();

                    Console.WriteLine("=== Convertisseur Morse ===");
                    Console.Write("Entrez votre texte : ");

                    string texte = Console.ReadLine();

                    // Message d'erreur si aucune saisi
                    if (string.IsNullOrWhiteSpace(texte))
                    {
                        Console.WriteLine("Vous n'avez rien saisi.");
                    }
                    else
                    {
                        string morse = MorseCodeConverter.ConvertToMorse(texte);

                        Console.WriteLine("\nMessage en Morse :");
                        Console.WriteLine(morse);
                    }

                }

                // Converteur de bases
                else if (input == "2")
                {
                    ConvertisseurBases();
                }

                // Stéganographie
                else if (input == "3")
                {
                    Steganographie();
                }

                // Erreur si le choix est invalide
                else
                {
                    Console.WriteLine("Choix invalide.");
                }

                // Demander à l'utilisateur si il veur recommencer oui = relance une boucle, non = arrête le programme 
                Console.Write("\nVoulez-vous recommencer ? (O/N) : ");
                string choix = Console.ReadLine().ToUpper();


                recommencer = (choix == "O");

            } while (recommencer);
        }

        /// <summary>
        /// Choix de quel bases convertir
        /// </summary>
        static void ConvertisseurBases()
        {
            Console.Clear();

            // Affichage du choix de quel bases convertir 
            Console.WriteLine("=== Convertisseur de bases ===");
            Console.WriteLine("1. Décimal > Binaire");
            Console.WriteLine("2. Binaire > Décimal");
            Console.WriteLine("3. Binaire > Octal");
            Console.WriteLine("4. Octal > Binaire");
            Console.Write("Veuillez entrer votre choix : ");

            string choix = Console.ReadLine();

            Console.WriteLine();

            switch (choix)
            {
                // Conversion Décimal > Binaire
                case "1":
                    Console.Write("Entrez un nombre décimal : ");

                    if (int.TryParse(Console.ReadLine(), out int decimalNumber))
                    {
                        string resultat = "";

                        if (decimalNumber == 0)
                        {
                            resultat = "0";
                        }
                        else
                        {
                            while (decimalNumber > 0)
                            {
                                int reste = decimalNumber % 2;
                                resultat = reste + resultat;
                                decimalNumber = decimalNumber / 2;
                            }
                        }

                        Console.WriteLine("Résultat : " + resultat);
                    }

                    // Erreur si nombre décimal invalide
                    else
                    {
                        Console.WriteLine("Nombre décimal invalide");
                    }

                    break;

                // Conversion Binaire > Décimal
                case "2":
                    Console.Write("Entrez un nombre binaire : ");

                    string binaire = Console.ReadLine();

                    bool binaireValide = true;

                    foreach (char chiffre in binaire)
                    {
                        if (chiffre != '0' && chiffre != '1')
                        {
                            binaireValide = false;
                        }
                    }

                    if (binaireValide && !string.IsNullOrEmpty(binaire))
                    {
                        int resultatDecimal = 0;

                        foreach (char chiffre in binaire)
                        {
                            int valeur = chiffre - '0';
                            resultatDecimal = resultatDecimal * 2 + valeur;
                        }

                        Console.WriteLine("Résultat : " + resultatDecimal);
                    }

                    // Erreur si nombre binaire invalide
                    else
                    {
                        Console.WriteLine("Nombre binaire invalide");
                    }

                    break;

                // Conversion Binaire > Octal
                case "3":
                    Console.Write("Entrez un nombre binaire : ");

                    binaire = Console.ReadLine();

                    binaireValide = true;

                    foreach (char chiffre in binaire)
                    {
                        if (chiffre != '0' && chiffre != '1')
                        {
                            binaireValide = false;
                        }
                    }

                    if (binaireValide && !string.IsNullOrEmpty(binaire))
                    {
                        int decimalValue = 0;

                        foreach (char chiffre in binaire)
                        {
                            int valeur = chiffre - '0';
                            decimalValue = decimalValue * 2 + valeur;
                        }

                        string resultatOctal = "";

                        if (decimalValue == 0)
                        {
                            resultatOctal = "0";
                        }
                        else
                        {
                            while (decimalValue > 0)
                            {
                                int reste = decimalValue % 8;
                                resultatOctal = reste + resultatOctal;
                                decimalValue = decimalValue / 8;
                            }
                        }

                        Console.WriteLine("Résultat : " + resultatOctal);
                    }

                    // Erreur si nombre binaire invalide 
                    else
                    {
                        Console.WriteLine("Nombre binaire invalide");
                    }

                    break;

                // Conversion Octal > Binaire 
                case "4":
                    Console.Write("Entrez un nombre octal : ");

                    string octal = Console.ReadLine();

                    bool octalValide = true;

                    foreach (char chiffre in octal)
                    {
                        if (chiffre < '0' || chiffre > '7')
                        {
                            octalValide = false;
                        }
                    }

                    if (octalValide && !string.IsNullOrEmpty(octal))
                    {
                        int decimalValue = 0;

                        foreach (char chiffre in octal)
                        {
                            int valeur = chiffre - '0';
                            decimalValue = decimalValue * 8 + valeur;
                        }

                        string resultatBinaire = "";

                        if (decimalValue == 0)
                        {
                            resultatBinaire = "0";
                        }
                        else
                        {
                            while (decimalValue > 0)
                            {
                                int reste = decimalValue % 2;
                                resultatBinaire = reste + resultatBinaire;
                                decimalValue = decimalValue / 2;
                            }
                        }

                        Console.WriteLine("Résultat : " + resultatBinaire);
                    }

                    // Erreur si nombre octal invalide
                    else
                    {
                        Console.WriteLine("Nombre octal invalide");
                    }

                    break;

                // Erreur si choix de la base à convertir invalide
                default:
                    Console.WriteLine("Choix invalide");
                    break;
            }
        }

        /// <summary>
        /// Choix de la stéganographie
        /// </summary>
        static void Steganographie()
        {
            Console.Clear();

            Console.WriteLine("=== Stéganographie ===");
            Console.WriteLine("1. Encoder (Cacher un message)");
            Console.WriteLine("2. Décoder (Extraire un message caché)");
            Console.Write("Veuillez entrer votre choix : ");

            string choix = Console.ReadLine();

            Console.WriteLine();

            switch (choix)
            {
                // Encoder un message
                case "1":
                    Console.WriteLine("Texte porteur (ce que l'on verra) :");
                    string textePorteur = Console.ReadLine();
                    Console.WriteLine("Message secret à cacher (A-Z et espace) :");

                    string messageSecret = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(textePorteur))
                    {
                        Console.WriteLine("Le texte porteur ne peut pas être vide.");
                    }
                    else if (string.IsNullOrWhiteSpace(messageSecret))
                    {
                        Console.WriteLine("Le message secret ne peut pas être vide.");
                    }
                    else
                    {
                        messageSecret = messageSecret.ToUpper();

                        bool messageValide = true;

                        foreach (char caractere in messageSecret)
                        {
                            if ((caractere < 'A' || caractere > 'Z') && caractere != ' ')
                            {
                                messageValide = false;
                            }
                        }

                        if (!messageValide)
                        {
                            Console.WriteLine("Le message secret contient des caractères invalides.");
                        }
                        else
                        {
                            string resultat = SteganographieEncoder.Encode(
                                textePorteur,
                                messageSecret
                            );

                            {
                                File.WriteAllText("stegano.txt", resultat, Encoding.UTF8);                               
                                Console.WriteLine("Le texte avec stéganographie a été sauvegardé dans le fichier stegano.txt");
                                Console.WriteLine(Path.GetFullPath("stegano.txt"));
                            }
                        }
                    }
                    break;

                // Décoder un message
                case "2":
                    string texteStegano = File.ReadAllText("stegano.txt", Encoding.UTF8);

                    if (string.IsNullOrEmpty(texteStegano))
                    {
                        Console.WriteLine("Vous n'avez rien saisi.");
                    }
                    else
                    {
                        string message = SteganographieEncoder.Decode(texteStegano);

                        if (message == "")
                        {
                            Console.WriteLine("Aucun message caché trouvé.");
                        }
                        else
                        {
                            Console.WriteLine("Voici le message caché dans le fichier stegano.txt\n: " + message);
                        }
                    }

                    break;

                // Erreur si le choix est invalide
                default:
                    Console.WriteLine("Choix invalide");
                    break;
            }
        }
    }

    /// <summary>
    /// Dictionaire Morse 
    /// </summary>
    class MorseCodeConverter
    {
        private static Dictionary<char, string> _textToMorse =
            new Dictionary<char, string>()
        {
            {' ', "/"},
            {'A', ".-"},
            {'B', "-..."},
            {'C', "-.-."},
            {'D', "-.."},
            {'E', "."},
            {'F', "..-."},
            {'G', "--."},
            {'H', "...."},
            {'I', ".."},
            {'J', ".---"},
            {'K', "-.-"},
            {'L', ".-.."},
            {'M', "--"},
            {'N', "-."},
            {'O', "---"},
            {'P', ".--."},
            {'Q', "--.-"},
            {'R', ".-."},
            {'S', "..."},
            {'T', "-"},
            {'U', "..-"},
            {'V', "...-"},
            {'W', ".--"},
            {'X', "-..-"},
            {'Y', "-.--"},
            {'Z', "--.."},
            {'1', ".----"},
            {'2', "..---"},
            {'3', "...--"},
            {'4', "....-"},
            {'5', "....."},
            {'6', "-...."},
            {'7', "--..."},
            {'8', "---.."},
            {'9', "----."},
            {'0', "-----"}
        };

        /// <summary>
        /// Méthode pour convertir en Morse
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ConvertToMorse(string text)
        {
            StringBuilder result = new StringBuilder();
            text = text.ToUpper();

            foreach (char character in text)
            {
                if (_textToMorse.ContainsKey(character))
                {
                    result.Append(_textToMorse[character]);
                    result.Append(" ");
                }
                else
                {
                    Console.WriteLine(
                        $"Le caractère '{character}' n'est pas reconnu."
                    );
                }
            }

            return result.ToString().Trim();
        }
    }

    /// <summary>
    /// Création de caractère invisible pour cacher le message secret
    /// </summary>
    class SteganographieEncoder
    {
        public static string Encode(string textePorteur, string messageSecret)
        {
            string resultat = textePorteur;

            // Marque le début du message caché
            resultat += '\u2060';

            // Parcours du message secret
            foreach (char caractere in messageSecret)
            {
                int valeur = caractere;

                // Conversion du caractère en 8 bits
                for (int i = 7; i >= 0; i--)
                {
                    int bit = (valeur >> i) & 1;

                    if (bit == 0)
                    {
                        resultat += '\u200B';
                    }
                    else
                    {
                        resultat += '\u200C';
                    }
                }

                resultat += '\u200D';
            }

            // Marque la fin du message caché
            resultat += '\u2060';

            return resultat;
        }

        public static string Decode(string texteStegano)
        {
            string message = "";
            string bits = "";
            bool debutMessage = false;

            // Parcours du texte stéganographié
            foreach (char caractere in texteStegano)
            {
                // Cherche le début ou la fin du message
                if (caractere == '\u2060')
                {
                    if (!debutMessage)
                    {
                        debutMessage = true;
                    }
                    else
                    {
                        break;
                    }
                }

                // Lit les caractères invisibles
                else if (debutMessage)
                {
                    // Le caractère invisible représente un 0
                    if (caractere == '\u200B')
                    {
                        bits += "0";
                    }

                    // Le caractère invisible représente un 1
                    else if (caractere == '\u200C')
                    {
                        bits += "1";
                    }

                    // Fin des 8 bits du caractère
                    else if (caractere == '\u200D')
                    {
                        if (bits.Length == 8)
                        {
                            int valeur = 0;

                            // Reconvertit les 8 bits en nombre
                            foreach (char bit in bits)
                            {
                                valeur = valeur * 2 + (bit - '0');
                            }

                            // Convertit le nombre en caractère
                            message += (char)valeur;
                        }
                        bits = "";
                    }
                }
            }
            return message;
        }
    }
}


