namespace AliquotSequenceCalculator;
public class AliquotCalculator {
    public void CalculateToEnd() {
        while (true) {
            Console.Clear();
            var input = getInput();
            if (int.TryParse(input, out int num)) {
                int prev = num;
                while (true) {
                    var next = sumFactors(prev);
                    Console.Write($"{next}, ");
                    if (next == prev) break;
                    prev = next;
                }
            } else {
                Console.WriteLine("Not a number!");
            }
            promptToShowWork(num);
            if (promptToQuit()) break;
        }
    }

    private void promptToShowWork(int num) {
        Console.WriteLine("Show my work? (y/n) ");
        if (Console.ReadLine() == "y") {
            var prev = num;
            while (true) {
                (string text, int sum)= factorSumString(prev);
                Console.Write($"{text}{Environment.NewLine}");
                if (sum == prev) break;
                prev = sum;
            }
        }
    }

    private int sumFactors(int num) {
        var count = (int)Math.Sqrt(num);
        var range = Enumerable.Range(1, count).ToList();

        return range.Aggregate(0, (acc, next) =>
            num % next == 0
                ? next == 1
                    ? acc + 1
                    : num / next != next
                        ? acc + next + (num / next)
                        : acc + next
                : acc);
    }

    private bool promptToQuit() {
        Console.WriteLine("Quit? (y/n)");
        return (Console.ReadLine() == "y");
    }

    private string? getInput() {
        Console.Write("What number? ");
        return Console.ReadLine();
    }

    private (string, int) factorSumString(int num) {
        var factors = getFactors(num);
        var sum = factors.Sum();
        return ($"Sum of proper factors of {num} is {factorExpression(factors)} = {factors.Sum()}",sum);
    }

    private IEnumerable<int> getFactors(int num) {
        var max = (int)Math.Sqrt(num);
        return [..Enumerable.Range(2, (int)Math.Sqrt(num)-1)
                    .SelectMany(x => {
                        List<int> retVal = [];
                        if (num % x == 0) {
                            retVal.Add(x);
                            if (num / x != x) {
                                retVal.Add(num / x);
                            }
                        }
                        return retVal;
                    }),1];
    }

    private string factorExpression(IEnumerable<int> factors) {
        return string.Join(" + ", factors.Select(x => x.ToString()));
    }
}
