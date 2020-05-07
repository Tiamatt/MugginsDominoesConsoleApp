using MugginsDominoes.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MugginsDominoes
{
    public static class Helper
    {
        #region public methods

        public static List<int> ConvertStringIntoListForOpenEnds(string openEndsString, out string retryMessage)
        {
            // validate when string of open ends is empty
            if (String.IsNullOrWhiteSpace(openEndsString))
            {
                retryMessage = $"You didn't enter open ends";
                return null;
            }

            // split string of ends by comma
            var splittedInputs = openEndsString.Split(',');

            // validate length of entered ends
            if (splittedInputs.Length > 4)
            {
                retryMessage = $"You can't have more than four open ends";
                return null;
            }

            // convert string of open ends into numbers of open ends
            List<int> openEnds = SplitInputsIntoEnds(splittedInputs, out retryMessage);
            if (String.IsNullOrEmpty(retryMessage) == false)
                return null;

            if (ValidateOpenEnds(openEnds, out retryMessage) == false)
                return null;

            retryMessage = String.Empty;
            return openEnds;
        }

        public static List<int> ConvertStringIntoListForPotentialEnds(string potentialEndsString, List<int> openEnds, out string retryMessage)
        {
            // when string of potential ends is empty then return
            if (String.IsNullOrWhiteSpace(potentialEndsString))
            {
                retryMessage = (openEnds.Count == 3) ? $"You didn't enter potential end" : String.Empty;
                return null;
            }

            // split string of ends by comma
            var splittedInputs = potentialEndsString.Split(',');

            // convert string of open ends into numbers of open ends
            List<int> potentialEnds = SplitInputsIntoEnds(splittedInputs, out retryMessage);
            if (String.IsNullOrEmpty(retryMessage) == false)
                return null;

            if (ValidatePotentialEnds(potentialEnds, openEnds, out retryMessage) == false)
                return null;

            retryMessage = String.Empty;
            return potentialEnds;
        }

        public static List<Result> GetResults(List<int> openEnds, List<int> potentialEnds)
        {
            // first get results for open ends
            var results = GetResultsForOpenEnds(openEnds);
            // case #1: if no potential ends then return results for open ends only
            if (potentialEnds?.Count > 0)
            {
                var resultForPotentialEnds = GetResultsForPotentialEnds(openEnds, potentialEnds);

                // case #2: if there are both results - for open and potential ends - then merge them and return 
                if (resultForPotentialEnds?.Count > 0 && results?.Count > 0)
                    results.AddRange(resultForPotentialEnds);

                // case #3: if there are no results for open ends then return results for potential ends (note, can be empty)
                else if (results == null || results.Count == 0)
                    results = resultForPotentialEnds;
            }

            return results;
        }

        public static List<Result> GetResultsForOpenEnds(List<int> openEnds)
        {
            var results = new List<Result>();

            // sum of all open ends
            var totalSumOfOpenEnds = SumEnds(openEnds);
            HashSet<int> proceededOpenEnds = new HashSet<int>();
            foreach (var openEnd in openEnds)
            {
                // if there are two or more open ends with the same value, we  want to EXCLUDE duplicated results (e.g. for {5,5} results are {5:0, 5:5, 5:0, 5:5} - but we want remove duplications)
                if (proceededOpenEnds.Contains(openEnd))
                    continue;
                proceededOpenEnds.Add(openEnd);

                // check if targetOpenEnd is a double: 11, 22, 33, 44, 55, 66, 00 (which is 100)
                var isTargetOpenEndDouble = IsDouble(openEnd);
                // adjust targetOpenEnd if targetOpenEnd is double
                var targetOpenEnd = (isTargetOpenEndDouble) ? (openEnd == 100 ? 0 : openEnd / 11) : openEnd;
                // adjust total sum of nonTargetOpenEnds based on a) single targetOpenEnd, b) double targetOpenEnd and ends more than 1 c) double targetOpenEnd and single end
                var sumOfAllNonTargetOpenEnds = totalSumOfOpenEnds - targetOpenEnd;
                if (isTargetOpenEndDouble) {
                    sumOfAllNonTargetOpenEnds = (openEnds.Count > 1) ? totalSumOfOpenEnds - 2 * targetOpenEnd : totalSumOfOpenEnds;
                }

                var scores = FindAllPossibleCombinationsOfScore(targetOpenEnd, sumOfAllNonTargetOpenEnds);
                foreach (var score in scores)
                {
                    var match = score - sumOfAllNonTargetOpenEnds;
                    
                    // e.g.: openEnds: 6,3,3 and 4. For 4 open end there are two possible scores: 
                    // a) (6+3+3) + _3_ = 15. Thus 4:3 is one of our results 
                    // b) (6+3+3) + _8_ = 20. Thus 8 can be transformed into 4:4 and used as another of our results 
                    if (isTargetOpenEndDouble == false && match % 2 == 0 && match / 2 == targetOpenEnd) // e.g. inputs (3,4)
                    {
                        var resultDouble = new Result() { TargetEnd = targetOpenEnd, Match = targetOpenEnd, Sum = score };
                        results.Add(resultDouble);
                    }

                    // e.g.: openEnds: 1 and 44. For 44 open end there is only one score: 
                    // (1) + _4_ = 5. Thus 4:4 is a result. BUT, as 44 is already an open end, we can't use it again
                    if (match != targetOpenEnd && match <= 6) // e.g. inputs (2,3), (3,6)
                    {
                        var result = new Result() { TargetEnd = targetOpenEnd, Match = match, Sum = score };
                        results.Add(result);
                    }
                }
            }

            return results;
        }

        public static List<Result> GetResultsForPotentialEnds(List<int> openEnds, List<int> potentialEnds)
        {
            var results = new List<Result>();

            var totalSumOfOpenEnds = SumEnds(openEnds);
            foreach (var potentialEnd in potentialEnds)
            {
                var isPotentialEndDouble = IsDouble(potentialEnd);
                var targetPotentialEnd = IsDouble(potentialEnd) 
                    ? (potentialEnd == 100 ? 0 : potentialEnd / 11) 
                    : potentialEnd;
                var scores = FindAllPossibleCombinationsOfScore(targetPotentialEnd, totalSumOfOpenEnds);
                foreach(var score in scores)
                {
                    var match = score - totalSumOfOpenEnds;
                    // TODO: re-visit
                    if (match != targetPotentialEnd && match <= 6)
                    {
                        var result = new Result() { TargetEnd = targetPotentialEnd, Match = match, Sum = score, IsPotentialEnd = true };
                        results.Add(result);
                    }
                }
            }

            return results;
        }
        
        #endregion public methods

        #region private methods

        private static List<int> SplitInputsIntoEnds(string[] splittedInputs, out string retryMessage)
        {
            // convert inputs into int
            List<int> ends = new List<int>();
            foreach (string input in splittedInputs)
            {
                try
                {
                    // first try to conver string input into number input
                    int end = int.Parse(input);

                    // for "0:0" use "100"
                    if (input == "00")
                        end = 100;

                    // check is number can be a dominoes' end
                    if (ValidateEnd(end) == false)
                        throw new Exception($"Input '{end}' is invalid");

                    ends.Add(end);
                }
                catch (Exception ex)
                {
                    retryMessage = ex.Message;
                    return null;
                }
            }

            retryMessage = String.Empty;
            return ends;
        }

        private static bool ValidateEnd(int number)
        {
            /* allowed numbers are: 
                *   0,  1,  2,  3,  4,  5,  6
                * 100, 11, 22, 33, 44, 55, 66
            */
            if (number >= 0 && number <= 6)
                return true;

            if (number >= 11 && number <= 66 && number % 11 == 0)
                return true;

            if (number == 100)
                return true;

            return false;
        }

        private static bool IsDouble(int input)
        {
            // if 00 which is 100
            // if 11, 22, 33, 44, 55, 66
            // then  return true, else return false
            return (input == 100 || (input != 0 && input % 11 == 0));
        }

        private static bool ValidateOpenEnds(List<int> openEnds, out string retryMessage)
        {
            // 0. No open ends - not possible
            if (openEnds == null || openEnds.Count == 0)
            {
                retryMessage = $"List of open ends can't be empty";
                return false;
            }

            // 1. One open end - doubles only
            /*
              Allowed:
               {double}
           */
            if (openEnds.Count == 1 && IsDouble(openEnds[0]) == false)
            {
                retryMessage = $"Your only open end should be a DOUBLE";
                return false;
            }


            // 2. Two open ends - any open end is allowed 
            /*
               Allowed:
                {single, single}
                {single, double}
                {double, double}
            */

            // 3. Three open ends - any open end is allowed 
            /*
               Allowed:
                {single, single, single}
                {single, single, double}
                {single, double, single}
                {double, single, single}
                {single, double, double}
                {double, single, double}
                {double, double, single}
                {double, double, double}
            */

            // 4. Fours open ends - any open end is allowed 
            /*
               Allowed:
                {single, single, single, single}
                ............ so on .............
                {double, double, double, double}
            */

            // 5. More than 4 open ends - not possible
            if (openEnds.Count > 4)
            {
                retryMessage = $"Number of open ends can't be exceed FOUR";
                return false;
            }

            retryMessage = String.Empty;
            return true;
        }

        private static bool ValidatePotentialEnds(List<int> potentialEnds, List<int> openEnds, out string retryMessage) 
        {
            // have already privented in GetInputsAndReturnOutputsRecursively() - just in case
            // Note: if there is only one open end, which has to be double, no potential ends can exist
            //       if there are all four open ends, then there are only "dead" potential ends left, thus they should be ignored
            if ((openEnds.Count == 1 || openEnds.Count == 4) && potentialEnds.Count > 0)
            {
                retryMessage = $"Potential ends should not exist when there is ONE or FOUR open ends";
                return false;
            }

            if (openEnds.Count == 3 && potentialEnds.Count != 1)
            {
                retryMessage = $"For 3 open ends there should be only ONE potential end";
                return false;
            }

            // ==================== otherwise ====================

            // 0. No potential end - OK

            // 1. One potential end - any potential end is allowed 
            /*
               Allowed:
                {single}
                {double}
            */

            // 2. Two-Seven potential ends - doubles only
            /*
              Allowed:
               {double}
           */
            if (potentialEnds.Count >= 2 && potentialEnds.Count <=7 && potentialEnds.Any(end => IsDouble(end) == false))
            {
                retryMessage = $"All your potential ends should be DOUBLES";
                return false;
            }

            if (potentialEnds.Count > 7)
            {
                retryMessage = $"Number of potential ends can't exceed SEVEN";
                return false;
            }

            retryMessage = String.Empty;
            return true;
        }

        private static int SumEnds(List<int> ends)
        {
            var sum = 0;
            foreach (var end in ends) {
                // if 00 (which is 100) then skip
                if (end == 100)
                    continue;

                // if any real double, then add as total sum of double (11 => 2; 22 =>4, ..., 66=>12)
                sum += (end % 11 == 0) ? 2 * (end / 11) : end;
            }
            return sum;
        }

        private static List<int> FindAllPossibleCombinationsOfScore(int targetEnd, int sumOfAllNonTargerEnds)
        {
            // Find  all possible combinations of your score, 
            // i.e. sum all ends except target end and then round it to the nearest possible multiples of 5 
            // Note, there can be more than one score

            // results
            var scoresDivisibleBy5 = new List<int>();

            // if nonTargerEnds is zero then return results immediately  
            // 0=>5 (if end is 5 then return 10 as well because user can put 5:5 for 5 open end)
            if (sumOfAllNonTargerEnds == 0)
            {
                scoresDivisibleBy5.Add(5);
                if(targetEnd == 5)
                    scoresDivisibleBy5.Add(10);
                return scoresDivisibleBy5;
            }

            // otherwise

            // part 1:  1=>5; 2=>5, 3=>5, 4=>5,PART2; 5=>5,PART2; 6=>10; 7=>10; 8=>10; 9=>10; 10=>10,PART2; 11=>15; 12=>5; 13=>15, 14=>15,PART2;  15=>15,PART2;, 16=>20, etc
            var multiplicator = (int)Math.Ceiling((decimal)sumOfAllNonTargerEnds / 5);
            var clothestDcoresDivisibleBy5 = 5 * multiplicator;
            scoresDivisibleBy5.Add(clothestDcoresDivisibleBy5);

            // part 2: 4=>PART1,10; 5=>PART1,10; 14=>PART1,20; 15=>PART1,20; 24=>PART1,30; 25=>PART1,30; etc.
            if (sumOfAllNonTargerEnds % 10 == 4 || sumOfAllNonTargerEnds % 5 == 0)
                scoresDivisibleBy5.Add(clothestDcoresDivisibleBy5 + 5);

            // part 3: for example, WHEN target = 4 && nonTargerEnds = 12 THEN 4:4 + 12 = 20
            var doubleTargetEndAndOtherEnds = sumOfAllNonTargerEnds + 2 * targetEnd;
            if (scoresDivisibleBy5.Contains(doubleTargetEndAndOtherEnds) == false && (sumOfAllNonTargerEnds + 2*targetEnd) % 5 == 0)
                scoresDivisibleBy5.Add(doubleTargetEndAndOtherEnds);

            return scoresDivisibleBy5;
        }

        #endregion private methods
    }
}
