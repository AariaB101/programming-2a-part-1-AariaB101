using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media; // This allows you to use SoundPlayer
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PROGpoe
{
    class Program
    {


        // Global memory to store user preferences
        static string userInterest = "";
        static string userName = "";
        static void Main()
        {
            //play audio greeting
            PlayGreetingAudio("Greeting.wav");

            Console.Title = "Cybersecurity Awareness Chatbot";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"

                  __________    ___      ___   _____________         ________
                 |          |   \  \    /  /  |             |       /        \
                 |    ______|    \  \__/  /   |    _________|      /  ______  \
                 |   |            \      /    |   |               /  /      \  \
                 |   |             \    /     |   |_________     /  /        \  \
                 |   |              |  |      |________    |    /  /__________\  \
                 |   |______        |  |              |    |   /   ____________   \
                 |          |       |  |      ________|    |  /___/            \___\
                 |          |       |__|     |             |           BOT         
                 |__________|                |_____________|             



");  // ASCII ART


            // Initial greeting
            Console.WriteLine("Hello! Welcome to your Cybersecurity Awareness Assistant!");
            Console.Write("What's your name? ");
            Console.ForegroundColor = ConsoleColor.White;
            userName = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Hello {userName}, I'm here to help you stay safe online.");

            Console.WriteLine(new string('-', 50));
            ShareOptions();
            Console.WriteLine(new string('-', 50));

            // Chat loop
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{userName}: ");
                string userInput = Console.ReadLine()?.ToLower().Trim();

                if (string.IsNullOrEmpty(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Chatbot: Please enter a valid question.");
                    continue;
                }

                if (userInput == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Chatbot: Stay safe, goodbye!");
                    break;
                }

                HandleUserQuery(userInput);
            }
        }

        // Play greeting sound
        static void PlayGreetingAudio(string filePath)
        {
            try
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                if (File.Exists(fullPath))
                {
                    new SoundPlayer(fullPath).PlaySync();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: The file '{filePath}' was not found.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error playing audio: {ex.Message}");
            }
        }

        // Handle user input with keyword, memory, sentiment and random response support
        static void HandleUserQuery(string input)
        {
            // Keyword-based static responses
            Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>
            {
                { "password", new List<string> {
                    "Use long, complex passwords that include letters, numbers, and symbols.",
                    "Avoid using the same password across multiple sites.",
                    "Consider using a password manager to keep your credentials safe."
                }},
                { "phishing", new List<string> {
                    "Watch out for emails with urgent requests or unfamiliar links.",
                    "Don't click on suspicious attachments or download links.",
                    "Check sender email addresses closely — scammers spoof trusted contacts."
                }},
                { "privacy", new List<string> {
                    "Review the privacy settings on your social media accounts regularly.",
                    "Avoid sharing sensitive information on public platforms.",
                    "Use encrypted messaging apps when discussing personal matters."
                }},
                { "scam", new List<string> {
                    "Scams often promise rewards or threaten penalties. Stay skeptical.",
                    "Verify unexpected messages through a second communication channel.",
                    "Never send personal data or money without confirming the recipient."
                }},
                { "safe browsing", new List<string> {
                    "Use HTTPS websites and avoid suspicious pop-ups.",
                    "Install browser security extensions and keep them updated.",
                    "Clear your cookies and cache regularly to maintain privacy."
                }},
                { "help", new List<string> {
                    "You can ask about passwords, phishing, scams, safe browsing, or privacy."
                }}
            };

            // Sentiment detection and adjustment
            if (input.Contains("worried"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Chatbot: It's okay to feel that way. Cybersecurity can be overwhelming, but I'm here to help you step by step.");
                return;
            }
            if (input.Contains("frustrated"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Chatbot: I understand. Let's tackle your questions one at a time.");
                return;
            }
            if (input.Contains("curious"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Chatbot: Curiosity is great! Let's explore how you can be safer online.");
                return;
            }

            // Memory recognition
            if (input.Contains("interested in"))
            {
                int index = input.IndexOf("interested in") + "interested in".Length;
                userInterest = input.Substring(index).Trim();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Chatbot: Great! I'll remember that you're interested in {userInterest}. It's an important part of online safety.");
                return;
            }

            if (!string.IsNullOrEmpty(userInterest) && input.Contains("what else"))
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"Chatbot: As someone interested in {userInterest}, you might want to explore advanced settings or educational articles on that topic.");
                return;
            }

            bool matched = false;

            foreach (var keyword in keywordResponses.Keys)
            {
                if (input.Contains(keyword))
                {
                    var responses = keywordResponses[keyword];
                    Random rand = new Random();
                    string randomResponse = responses[rand.Next(responses.Count)];
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Chatbot: {randomResponse}");
                    matched = true;
                    break;
                }
            }

            if (!matched)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Chatbot: I'm not sure I understand. Would you like to see what you can ask? (yes/no)");
                string reply = Console.ReadLine()?.ToLower().Trim();
                if (reply == "yes") ShareOptions();
                else Console.WriteLine("Chatbot: No worries! Just ask whenever you're ready.");
            }
        }

        // Display available question topics
        static void ShareOptions()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("You can ask about:");
            Console.WriteLine("- password safety");
            Console.WriteLine("- phishing or scams");
            Console.WriteLine("- privacy protection");
            Console.WriteLine("- safe browsing habits");
            Console.WriteLine("- You can also say things like 'I'm interested in privacy' or 'I'm worried about phishing'");
        }
    }
}




// https://www.w3schools.com/cpp/cpp_while_loop.asp 