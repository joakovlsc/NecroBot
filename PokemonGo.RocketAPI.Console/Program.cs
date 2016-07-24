﻿#region using directives

using System;
using System.Threading;
using System.Threading.Tasks;
using PokemonGo.RocketAPI.Exceptions;
using PokemonGo.RocketAPI.Logic.State;

#endregion

namespace PokemonGo.RocketAPI.Console
{
    internal class Program
    {
        private static void Main()
        {
            Logger.SetLogger(new ConsoleLogger(LogLevel.Info));

            StateMachine machine = new StateMachine();
            ConsoleEventListener listener = new ConsoleEventListener();

            machine.EventListener += listener.Listen;
            machine.SetFailureState(new LoginState());
            machine.AsyncStart(new VersionCheckState(), new Context(new Settings()));

            /*Task.Run(() =>
            {
                try
                {
                    new Logic.Logic(new Settings()).Execute().Wait();
                }
                catch (PtcOfflineException)
                {
                    Logger.Write("PTC Servers are probably down OR your credentials are wrong. Try google",
                        LogLevel.Error);
                    Logger.Write("Trying again in 20 seconds...");
                    Thread.Sleep(20000);
                    new Logic.Logic(new Settings()).Execute().Wait();
                }
                catch (AccountNotVerifiedException)
                {
                    Logger.Write("Account not verified. - Exiting");
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    Logger.Write($"Unhandled exception: {ex}", LogLevel.Error);
                    new Logic.Logic(new Settings()).Execute().Wait();
                }
            });*/
            System.Console.ReadLine();
        }
    }
}