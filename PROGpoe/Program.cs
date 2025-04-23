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
        static void Main()
        {
            //play audio greeting
            PlayGreetingAudio("Greeting.wav");

            Console.Title = "Cybersecurity Awareness Chatbot";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('═', 60));
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

            Console.WriteLine(new string('═', 60));

            //Initial Chatbot greeting
            Console.WriteLine("Hello! Welcome to your Cybersecurity Awareness Assistant!");
            Console.Write("What's your name? ");
            Console.ForegroundColor = ConsoleColor.White;

            //Get user input name 
            string userName = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Hello {userName}, I'm here to help you stay safe online.");

            //Display options 
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("You can ask about: ");
            Console.WriteLine(" - passwords.");
            Console.WriteLine(" - phishing.");
            Console.WriteLine(" - safe browsing.");
            Console.WriteLine(" - Or type 'exit' to quit.");
            Console.WriteLine(new string('-', 50));

            //Chatbot conversation loop 
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{userName}: ");
                string userInput = Console.ReadLine()?.ToLower().Trim();

                //Handle empty input
                if (string.IsNullOrEmpty(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Chatbot: Please enter a valid question.");
                    continue;
                }
                //Exit
                if (userInput == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Chatbot: Stay safe, Goodbye!");
                    break;
                }
                //Handle question input
                HandleUserQuery(userInput, userName);
            }
        }
             //Method to play audio
            static void PlayGreetingAudio(string filePath)
            {
               try
            {
                // Get the full path to the audio file
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

                if (File.Exists(fullPath))
                {
                    SoundPlayer player = new SoundPlayer(fullPath);
                    player.PlaySync(); //play audio synchronously
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

        // Method to handle user queries and match them to predefined responses
        static void HandleUserQuery(string input, string userName)
        {
            // Dictionary mapping keywords to chatbot responses
            Dictionary<string, string> responses = new Dictionary<string, string>
            {
                { "passwords", "Create strong passwords by using a mix of letters, numbers, and symbols. Avoid using personal info!"},
                { "phishing", "Be cautious of emails that ask for personal info urgently. Always verify the sender!"},
                { "safe browsing", "Use secure websites (https), keep your browser updated, and avoid clicking on pop-ups." },
                { "how are you", "I'm just code, but I'm here and ready to help you stay safe online!" },
                { "what's your purpose", "I'm your Cybersecurity Awareness Assistant. I'm here to help you stay safe on the internet." },
                { "what can i ask", "You can ask me about password safety, recognizing phishing, data protection, or safe browsing tips." },
                { "help", "You can ask about passwords, phishing, or protecting your personal data."},

            };

            bool foundMatch = false;

            // Loop through the keywords and check if user input contains one
            foreach (var keyword in responses.Keys)
            {
                if (input.Contains(keyword))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Chatbot: {responses[keyword]}");
                    foundMatch = true;
                    break;
                }
            }
            // If no match found, ask if the user wants help
            if (!foundMatch)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Chatbot: Sorry, I didn't understand. Would you like to see what you can ask? (yes/no)");
                string reply = Console.ReadLine()?.ToLower().Trim();

                if (reply == "yes")
                {
                    ShareOptions();  // Show available question topics
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Chatbot: No worries! Let me know if you need any assistance.");
                }
            }

        }

        // Display all supported question topics and example queries
        static void ShareOptions()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Chatbot: You can ask about passwords, phishing, or protecting your personal data.");
            Console.WriteLine("password - Create strong passwords by using a mix of letters, numbers, and symbols. Avoid using personal info!");
            Console.WriteLine("phishing - Be cautious of emails that ask for personal info urgently. Always verify the sender!");
            Console.WriteLine("safe browsing - Use secure websites (https), keep your browser updated, and avoid clicking on pop-ups.");
            Console.WriteLine(" - Or just ask how I'm doing or what I can help with!");

        }
      }
    }

// https://www.w3schools.com/cpp/cpp_while_loop.asp 