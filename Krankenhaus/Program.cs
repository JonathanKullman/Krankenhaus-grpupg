﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HospitalLibrary;

namespace Krankenhaus
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"How many patients?");
            int nrOfPatients = ReadInt();
            var simulationNr1 = new Simulation(nrOfPatients);


            simulationNr1.EveryTick(null);
            //Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        simulationNr1.ToScreen();
            //        simulationNr1.EveryTick(null);
            //        Thread.Sleep(1000);
            //    }

            //});

            
            Console.ReadKey();






        }
    /// <summary>
    /// Runs a TryParse-loop for integers. Prompts user to retry while it's not a number.
    /// </summary>
    /// <returns></returns>
    static int ReadInt()
    {
        int integer;
        while (!int.TryParse(Console.ReadLine(), out integer))
        {
            Console.WriteLine("Invalid input. You have to use a number.");
        }
        return integer;
    }
}
}
