using DeepEqual.Syntax;
using MugginsDominoes;
using MugginsDominoes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using XUnitTests.Models;

namespace XUnitTests
{
    public class HelperUnitTest
    {
        // in order to distinguish 0 from 00 we are using 100 for double-zero
        private const int doubleZero = 100;

        // Important: see images for each case in "ImagesForDifferentCases" folder
        /* 
         * CASES:
            1/1. OneOpenEnd_OneDoubleInput
          
            1/3. TwoOpenEnds_NoDoublesInputs
            2/3. TwoOpenEnds_OneDoubleInput
            3/3. TwoOpenEnds_TwoDoubleInputs

            1/4. ThreeOpenEndsAndOnePotentialEnd_NoDoublesInputs
            2/4. ThreeOpenEndsAndOnePotentialEnd_OneDoubleInput
            3/4. ThreeOpenEndsAndOnePotentialEnd_TwoDoubleInputs
            4/4. ThreeOpenEndsAndOnePotentialEnd_ThreeDoubleInputs

            1/5. FourOpenEnds_NoDoublesInputs
            2/5. FourOpenEnds_OneDoubleInput
            3/5. FourOpenEnds_TwoDoubleInputs
            4/5. FourOpenEnds_ThreeDoubleInputs
            5/5. FourOpenEnds_FourDoubleInputs
            
            1/7. TwoOpenEndsAndOnePotentialEnd
            2/7. TwoOpenEndsAndTwoPotentialEnds
            3/7. TwoOpenEndsAndThreePotentialEnds
            4/7. TwoOpenEndsAndFourPotentialEnds
            5/7. TwoOpenEndsAndFivePotentialEnds
            6/7. TwoOpenEndsAndSixPotentialEnds
            7/7. TwoOpenEndsAndSevenPotentialEnds
        */

        #region Test one open end

        [Fact]
        public void OneOpenEnd_OneDoubleInput_1()
        {
            // actual
            var input = new List<int> { doubleZero }; // 0:0
            var actual = Helper.GetResults(input, null);

            // expected
            var expected = new List<Result>() {
                new Result() { TargetEnd = 0, Match = 5, Sum = 5 },
            };

            //  compare
            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void OneOpenEnd_OneDoubleInput_2()
        {
            // actual
            var input = new List<int> { 11 }; // 1:1
            var actual = Helper.GetResults(input, null);

            // expected
            var expected = new List<Result>() {
                new Result() { TargetEnd = 1, Match = 3, Sum = 5 },
            };

            //  compare
            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void OneOpenEnd_OneDoubleInput_3()
        {
            var inputs = new GetResultsActualModel() {
                OpenEnds = new List<int> { 22 }, // 2:2
                PotentialEnds = null
            };

            var expected = new List<Result>() {
                new Result() { TargetEnd = 2, Match = 1, Sum = 5 },
                new Result() { TargetEnd = 2, Match = 6, Sum = 10 },
            };

            Test_GetResults(inputs, expected);
        }

        [Fact]
        public void OneOpenEnd_OneDoubleInput_4()
        {
            // actual
            var input = new List<int> { 33 }; // 3:3
            var actual = Helper.GetResults(input, null);

            // expected
            var expected = new List<Result>() {
                new Result() { TargetEnd = 3, Match = 4, Sum = 10 },
            };

            //  compare
            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void OneOpenEnd_OneDoubleInput_5()
        {
            // actual
            var input = new List<int> { 44 }; // 4:4
            var actual = Helper.GetResults(input, null);

            // expected
            var expected = new List<Result>() {
                new Result() { TargetEnd = 4, Match = 2, Sum = 10 },
            };

            //  compare
            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void OneOpenEnd_OneDoubleInput_6()
        {
            // actual
            var input = new List<int> { 55 }; // 5:5
            var actual = Helper.GetResults(input, null);

            // expected
            var expected = new List<Result>() {
                new Result() { TargetEnd = 5, Match = 0, Sum = 10 },
            };

            //  compare
            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void OneOpenEnd_OneDoubleInput_7()
        {
            // actual
            var input = new List<int> { 66 }; // 6:6
            var actual = Helper.GetResults(input, null);

            // expected
            var expected = new List<Result>() {
                new Result() { TargetEnd = 6, Match = 3, Sum = 15 },
            };

            //  compare
            actual.ShouldDeepEqual(expected);
        }


        #endregion Test one open end

        #region Test two open ends


        #region Test two open ends with no double inputs - TwoOpenEnds_NoDoublesInputs

        [Fact]
        public void TwoOpenEnds_NoDoublesInputs_NoDoublesOutputs_1()
        {
            var inputs = new GetResultsActualModel()
            {
                OpenEnds = new List<int> { 6, 1 },
                PotentialEnds = null
            };

            var expected = new List<Result>() {
                new Result() { TargetEnd = 6, Match = 4, Sum = 5 },
                new Result() { TargetEnd = 1, Match = 4, Sum = 10 },
            };

            Test_GetResults(inputs, expected);
        }

        [Fact]
        public void TwoOpenEnds_NoDoublesInputs_NoDoublesOutputs_2()
        {
            // actual
            var input = new List<int> { 2, 3 };
            var actual = Helper.GetResults(input, null).Count;

            // expected
            var expected = 0;

            //  compare
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TwoOpenEnds_NoDoublesInputs_NoDoublesOutputs_3()
        {
            var inputs = new GetResultsActualModel()
            {
                OpenEnds = new List<int> { 5, 2 },
                PotentialEnds = null
            };

            var expected = new List<Result>() {
                new Result() { TargetEnd = 5, Match = 3, Sum = 5 },
                new Result() { TargetEnd = 2, Match = 0, Sum = 5 },
                new Result() { TargetEnd = 2, Match = 5, Sum = 10 },
            };

            Test_GetResults(inputs, expected);
        }

        [Fact]
        public void TwoOpenEnds_NoDoublesInputs_NoDoublesOutputs_4()
        {
            var inputs = new GetResultsActualModel()
            {
                OpenEnds = new List<int> { 4, 0 },
                PotentialEnds = null
            };

            var expected = new List<Result>() {
                // new Result() { TargetEnd = 4, Match = 0, Sum = 0 },
                new Result() { TargetEnd = 4, Match = 5, Sum = 5 },
                new Result() { TargetEnd = 0, Match = 1, Sum = 5 },
                new Result() { TargetEnd = 0, Match = 6, Sum = 10 },
            };

            Test_GetResults(inputs, expected);
        }

        [Fact]
        public void TwoOpenEnds_NoDoublesInputs_OneDoubleOutput_1()
        {
            var inputs = new GetResultsActualModel()
            {
                OpenEnds = new List<int> { 3, 4 },
                PotentialEnds = null
            };

            var expected = new List<Result>() {
                new Result() { TargetEnd = 3, Match = 1, Sum = 5 },
                new Result() { TargetEnd = 3, Match = 6, Sum = 10 },
                new Result() { TargetEnd = 3, Match = 3, Sum = 10 }, // double-three
                new Result() { TargetEnd = 4, Match = 2, Sum = 5 },
            };

            Test_GetResults(inputs, expected);
        }

        [Fact]
        public void TwoOpenEnds_NoDoublesInputs_OneDoubleOutput_2()
        {
            var inputs = new GetResultsActualModel()
            {
                OpenEnds = new List<int> { 3, 6 },
                PotentialEnds = null
            };

            var expected = new List<Result>() {
                new Result() { TargetEnd = 3, Match = 4, Sum = 10 },
                new Result() { TargetEnd = 6, Match = 2, Sum = 5 },
                new Result() { TargetEnd = 6, Match = 6, Sum = 15 }, // double-six
            };

            Test_GetResults(inputs, expected);
        }

        [Fact]
        public void TwoOpenEnds_NoDoublesInputs_OneDoubleOutput_3()
        {
            var inputs = new GetResultsActualModel()
            {
                OpenEnds = new List<int> { 5, 5 },
                PotentialEnds = null
            };

            var expected = new List<Result>() {
                new Result() { TargetEnd = 5, Match = 0, Sum = 5 },
                new Result() { TargetEnd = 5, Match = 5, Sum = 15 }, // double-five
            };

            Test_GetResults(inputs, expected);
        }

        [Fact]
        public void TwoOpenEnds_NoDoublesInputs_TwoDoubleOutputs_1()
        {
            var inputs = new GetResultsActualModel()
            {
                OpenEnds = new List<int> { 5, 0 },
                PotentialEnds = null
            };

            var expected = new List<Result>() {
                new Result() { TargetEnd = 5, Match = 5, Sum = 10 }, // double-five
                new Result() { TargetEnd = 0, Match = 0, Sum = 5 }, // double-zero
                new Result() { TargetEnd = 0, Match = 5, Sum = 10 },
            };

            Test_GetResults(inputs, expected);
        }

        #endregion Test two open ends with no double inputs - TwoOpenEnds_NoDoublesInputs


        #region Test two open ends with one double input and one single input - TwoOpenEnds_OneDoubleInput

        [Fact]
        public void TwoOpenEnds_OneDoubleInput_NoDoublesOutputs_1()
        {
            var inputs = new GetResultsActualModel()
            {
                OpenEnds = new List<int> { 33, 4 },
                PotentialEnds = null
            };

            var expected = new List<Result>() {
                new Result() { TargetEnd = 3, Match = 1, Sum = 5 },
                new Result() { TargetEnd = 3, Match = 6, Sum = 10 },
            };

            Test_GetResults(inputs, expected);
        }

        [Fact]
        public void TwoOpenEnds_OneDoubleInput_NoDoublesOutputs_2()
        {
            var inputs = new GetResultsActualModel()
            {
                OpenEnds = new List<int> { 55, 2 },
                PotentialEnds = null
            };

            var expected = new List<Result>() {
                new Result() { TargetEnd = 2, Match = 0, Sum = 10 },
                new Result() { TargetEnd = 2, Match = 5, Sum = 15 },
                new Result() { TargetEnd = 5, Match = 3, Sum = 5 },
            };

            Test_GetResults(inputs, expected);
        }

        [Fact]
        public void TwoOpenEnds_OneDoubleInput_OneDoubleOutput_1()
        {
            var inputs = new GetResultsActualModel()
            {
                OpenEnds = new List<int> { 33, 2 },
                PotentialEnds = null
            };

            var expected = new List<Result>() {
                new Result() { TargetEnd = 2, Match = 4, Sum = 10 },
                new Result() { TargetEnd = 2, Match = 2, Sum = 10 }, // double-two
            };

            Test_GetResults(inputs, expected);
        }

        [Fact]
        public void TwoOpenEnds_OneDoubleInput_OneDoubleOutput_2()
        {
            var inputs = new GetResultsActualModel()
            {
                OpenEnds = new List<int> { 55, 0 },
                PotentialEnds = null
            };

            var expected = new List<Result>() {
                new Result() { TargetEnd = 0, Match = 0, Sum = 10 }, // double-zero
                new Result() { TargetEnd = 0, Match = 5, Sum = 15 },
            };

            Test_GetResults(inputs, expected);
        }

        #endregion Test two open ends with one double input and on single input - TwoOpenEnds_OneDoubleInput


        #region Test two open ends with two double inputs - TwoOpenEnds_TwoDoubleInputs

        [Fact]
        public void TwoOpenEnds_TwoDoubleInputs_1()
        {
            // actual
            var input = new List<int> { 11, 33 };
            var actual = Helper.GetResults(input, null);

            // expected
            var expected = new List<Result>() {
                new Result() { TargetEnd = 1, Match = 4, Sum = 10 },
            };

            //  compare
            actual.ShouldDeepEqual(expected);
        }

        [Fact]
        public void TwoOpenEnds_TwoDoubleInputs_2()
        {
            // actual
            var input = new List<int> { 55, doubleZero }; 
            var actual = Helper.GetResults(input, null);

            // expected
            var expected = new List<Result>() {
                new Result() { TargetEnd = 0, Match = 5, Sum = 15 },
            };

            //  compare
            actual.ShouldDeepEqual(expected);
        }

        #endregion Test two open ends with two double inputs - TwoOpenEnds_TwoDoubleInputs


        #endregion Test two open ends

        #region Test three open ends and one potential ends

        [Fact]
        public void ThreeOpenEndsAndOnePotentialEnd_NoDoublesInputs_NoDoublesOutputs_1()
        {
            var inputs = new GetResultsActualModel()
            {
                OpenEnds = new List<int> { 1, 3, 4 },
                PotentialEnds = new List<int> { 1 }
            };

            var expected = new List<Result>() {
                new Result() { TargetEnd = 1, Match = 3, Sum = 10 },
                new Result() { TargetEnd = 3, Match = 0, Sum = 5 },
                new Result() { TargetEnd = 3, Match = 5, Sum = 10 },
                new Result() { TargetEnd = 4, Match = 1, Sum = 5 },
                new Result() { TargetEnd = 4, Match = 6, Sum = 10 },
                new Result() { TargetEnd = 1, Match = 2, Sum = 10, IsPotentialEnd = true },
            };

            Test_GetResults(inputs, expected);
        }

        [Fact]
        public void ThreeOpenEndsAndOnePotentialEnd_NoDoublesInputs_OneDoubleOutput_1()
        {
            var inputs = new GetResultsActualModel()
            {
                OpenEnds = new List<int> { 5, 5, 5 },
                PotentialEnds = new List<int> { 5 }
            };

            var expected = new List<Result>() {
                new Result() { TargetEnd = 5, Match = 0, Sum = 10 },
                new Result() { TargetEnd = 5, Match = 5, Sum = 20 }, // double-five
                new Result() { TargetEnd = 5, Match = 0, Sum = 15, IsPotentialEnd = true },
            };

            Test_GetResults(inputs, expected);
        }

        [Fact]
        public void ThreeOpenEndsAndOnePotentialEnd_NoDoublesInputs_OneDoubleOutput_2()
        {
            var inputs = new GetResultsActualModel()
            {
                OpenEnds = new List<int> { 5, 5, 5 },
                PotentialEnds = new List<int> { 4 }
            };

            var expected = new List<Result>() {
                new Result() { TargetEnd = 5, Match = 0, Sum = 10 },
                new Result() { TargetEnd = 5, Match = 5, Sum = 20 }, //  double-five
                new Result() { TargetEnd = 4, Match = 0, Sum = 15, IsPotentialEnd = true },
                new Result() { TargetEnd = 4, Match = 5, Sum = 20, IsPotentialEnd = true },
            };

            Test_GetResults(inputs, expected);
        }

        [Fact]
        public void ThreeOpenEndsAndOnePotentialEnd_NoDoublesInputs_OneDoubleOutput_3()
        {
            var inputs = new GetResultsActualModel()
            {
                OpenEnds = new List<int> { 5, 2, 3 },
                PotentialEnds = null
            };

            var expected = new List<Result>() {
                new Result() { TargetEnd = 5, Match = 0, Sum = 5 },
                new Result() { TargetEnd = 5, Match = 5, Sum = 15 }, // double-five
            };

            Test_GetResults(inputs, expected);
        }

        [Fact]
        public void ThreeOpenEndsAndOnePotentialEnd_NoDoublesInputs_OneDoubleOutput_4()
        {
            var inputs = new GetResultsActualModel()
            {
                OpenEnds = new List<int> { 2, 5, 6 },
                PotentialEnds = new List<int> { 6 }
            };

            var expected = new List<Result>() {
                new Result() { TargetEnd = 2, Match = 4, Sum = 15 },
                new Result() { TargetEnd = 2, Match = 2, Sum = 15 }, // double-two
                new Result() { TargetEnd = 5, Match = 2, Sum = 10 },
                new Result() { TargetEnd = 6, Match = 3, Sum = 10 },
                new Result() { TargetEnd = 6, Match = 2, Sum = 15, IsPotentialEnd = true },
            };

            Test_GetResults(inputs, expected);
        }

        [Fact]
        public void ThreeOpenEndsAndOnePotentialEnd_NoDoublesInputs_TwoDoublesOutputs_1()
        {
            var inputs = new GetResultsActualModel()
            {
                OpenEnds = new List<int> { 1, 2, 6 },
                PotentialEnds = null
            };

            var expected = new List<Result>() {
                new Result() { TargetEnd = 1, Match = 1, Sum = 10 }, // double-one
                new Result() { TargetEnd = 1, Match = 2, Sum = 10 },
                new Result() { TargetEnd = 2, Match = 3, Sum = 10 },
                new Result() { TargetEnd = 6, Match = 2, Sum = 5 },
                new Result() { TargetEnd = 6, Match = 6, Sum = 15 }, // double-six
            };

            Test_GetResults(inputs, expected);
        }

        [Fact]
        public void ThreeOpenEndsAndOnePotentialEnd_TwoDoublesInputs_1()
        {
            var inputs = new GetResultsActualModel()
            {
                OpenEnds = new List<int> { 22, 55, 5},
                PotentialEnds = new List<int> { 0 },
            };

            var expected = new List<Result>() {
                new Result() { TargetEnd = 2, Match = 0, Sum = 15 },
                new Result() { TargetEnd = 2, Match = 5, Sum = 20 },
                new Result() { TargetEnd = 5, Match = 1, Sum = 10 },
                new Result() { TargetEnd = 5, Match = 1, Sum = 15 },
                new Result() { TargetEnd = 5, Match = 6, Sum = 20 },
                new Result() { TargetEnd = 0, Match = 1, Sum = 20, IsPotentialEnd = true }
            };

            Test_GetResults(inputs, expected);
        }

        #endregion Test three open ends and one potential end

        #region Test two open ends and 1-7 potential ends

        //1/7. TwoOpenEndsAndOnePotentialEnd
        //   2/7. TwoOpenEndsAndTwoPotentialEnds
        //   3/7. TwoOpenEndsAndThreePotentialEnds
        //   4/7. TwoOpenEndsAndFourPotentialEnds
        //   5/7. TwoOpenEndsAndFivePotentialEnds
        //   6/7. TwoOpenEndsAndSixPotentialEnds
        //   7/7. TwoOpenEndsAndSevenPotentialEnds
        [Fact]
        public void TwoOpenEndsAndOnePotentialEnd_1()
        {
            var inputs = new GetResultsActualModel()
            {
                OpenEnds = new List<int> { 6, 1 },
                PotentialEnds = new List<int> { 66 }
            };

            var expected = new List<Result>() {
                new Result() { TargetEnd = 6, Match = 4, Sum = 5 },
                new Result() { TargetEnd = 1, Match = 4, Sum = 10 },
                new Result() { TargetEnd = 6, Match = 3, Sum = 10, IsPotentialEnd = true },
            };

            Test_GetResults(inputs, expected);
        }

        [Fact]
        public void TwoOpenEndsAndOnePotentialEnd_2()
        {
            var inputs = new GetResultsActualModel()
            {
                OpenEnds = new List<int> { 66, 4 },
                PotentialEnds = new List<int> { 00 }
            };

            var expected = new List<Result>() {
                new Result() { TargetEnd = 6, Match = 1, Sum = 5 },
                new Result() { TargetEnd = 4, Match = 3, Sum = 15 },
                new Result() { TargetEnd = 4, Match = 4, Sum = 20 },
                new Result() { TargetEnd = 0, Match = 4, Sum = 20, IsPotentialEnd = true },
            };

            Test_GetResults(inputs, expected);
        }

        #endregion

        #region Private methods

        private List<Result> SortResults(List<Result> results) {
            return results
                .OrderBy(o => o.TargetEnd)
                .ThenBy(o => o.Match)
                .ThenBy(o => o.Sum)
                .ThenBy(o => o.IsPotentialEnd)
                .ToList();
        }

        private void Test_GetResults(GetResultsActualModel inputs, List<Result> expected)
        {
            var actual = Helper.GetResults(inputs.OpenEnds, inputs.PotentialEnds);

            // sort
            actual = SortResults(actual);
            expected = SortResults(expected);

            //  compare
            actual.ShouldDeepEqual(expected);
        }

        #endregion Private methods

    }
}
