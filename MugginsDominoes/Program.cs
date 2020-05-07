using MugginsDominoes.Models;
using System;
using System.Collections.Generic;

namespace MugginsDominoes
{
    class Program
    {

        static void Main(string[] args)
        {
            GetInputsAndReturnOutputsRecursively();
        }

        public static void GetInputsAndReturnOutputsRecursively()
        {
            string retryMessage = String.Empty;

            // Step 1. Keep asking for open ends till user enters valid data
            Console.WriteLine("Enter your OPEN ends separated by comma");
            List<int> openEnds = null;
            while (openEnds == null)
            {
                string openEndsAsString = Console.ReadLine();
                openEnds = Helper.ConvertStringIntoListForOpenEnds(openEndsAsString, out retryMessage);
                if (String.IsNullOrEmpty(retryMessage) == false)
                    Console.WriteLine($"{retryMessage}, please re-enter OPEN ends");
            }

            // Step 2. Keep asking for potential ends till user enters valid data
            List<int> potentialEnds = null;
            // Note: if there is only one open end, which has to be a double then no potential ends can exist
            //       if there are all four open ends, then there are only "dead" potential ends left, thus they should be ignored
            if (openEnds.Count == 2 || openEnds.Count == 3)
            {
                Console.WriteLine("Enter your POTENTIAL ends separated by comma");
                do
                {
                    string potentialEndsAsString = Console.ReadLine();
                    potentialEnds = Helper.ConvertStringIntoListForPotentialEnds(potentialEndsAsString, openEnds, out retryMessage);
                    if (String.IsNullOrEmpty(retryMessage) == false)
                        Console.WriteLine($"{retryMessage}, please re-enter POTENTIAL ends");
                } while (String.IsNullOrEmpty(retryMessage) == false);
            }

            // Step 3. Analyze and get results
            List<Result> results = Helper.GetResults(openEnds, potentialEnds);

            // Step 4. Present results in readable format
            Console.WriteLine(ConvertResultToReadableMessage(results));

            // Step 5. Call project again
            Console.WriteLine();
            GetInputsAndReturnOutputsRecursively();
        }

        public static string ConvertResultToReadableMessage(List<Result> results)
        {
            if (results == null || results.Count == 0)
                return "----- NO RESULTS FOUND -----";

            var message = "----- RESULTS -----" + Environment.NewLine;
            foreach (var result in results)
            {
                message += $"{result.TargetEnd} { (result.IsPotentialEnd ? "(potential)" : "") } => {result.TargetEnd}:{result.Match} for {result.Sum} " + Environment.NewLine;
            }
            return message;
        }

    }
}
