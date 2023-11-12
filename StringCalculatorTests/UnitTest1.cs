using StringCalculatorKATA;

namespace StringCalculatorTests
{
    public class UnitTes1
    {
        [Fact]
        public void Calculator_Returns_Zero_For_Empty_String()
        {
            var calc = new StringCalculator();
            var result = calc.Add("");
            Assert.Equal(0, result);
        }
        [Fact]
        public void Calculator_Returns_Input_When_Single_Input_Is_Entered()
        {
            var calc = new StringCalculator();
            var result = calc.Add("1");
            Assert.Equal(1, result);
        }
        [Fact]
        public void Calculator_Adds_Successfully()
        {
            var calc = new StringCalculator();
            var result = calc.Add("1,2");
            Assert.Equal(3, result);
        }
        [Fact]
        public void Calculator_Handles_Unknown_Args()
        {
            var calc = new StringCalculator();
            var result = calc.Add("1,2,1,1");
            Assert.Equal(5, result);
        }
        [Fact]
        public void Calculator_Handles_Newline_Separators()
        {
            var calc = new StringCalculator();
            var result = calc.Add("1,2\n3");
            Assert.Equal(6, result);
        }
        [Fact]
        public void Calculator_Does_Not_Allow_Separator_At_The_End()
        {
            var calc = new StringCalculator();

            Assert.Throws<ArgumentException>(() => calc.Add("1,2,"));
        }
        [Fact]
        public void Calculator_Handles_Different_Delimiters()
        {
            var calc = new StringCalculator();
            var result = calc.Add("//;\n1;3");
            Assert.Equal(4, result);
        }
        [Fact]
        public void Calculator_Handles_Negative_Numbers()
        {
            var calc = new StringCalculator();
            Assert.Throws<ArgumentException>(() => calc.Add("1,-2"));
        }
        [Fact]
        public void Calculator_Ignores_Numbers_Greater_Than_1000()
        {
            var calc = new StringCalculator();
            var result = calc.Add("1001,2");
            Assert.Equal(2, result);
        }
    }
}