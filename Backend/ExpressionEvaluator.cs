namespace Backend;

public static class ExpressionEvaluator
{
    public static double Evalute(string infix) => EvalutePostfix(ToPostfix(infix));

    private static string ToPostfix(string infix) 
    {
        var postfix = string.Empty;
        var stack = new Stack<char>();
        var number = string.Empty;

        foreach (var item in infix)
        {
            if ((item >= '0' && item <= '9') || item == '.')  
            {
                number += item;
            }
            else
            {
                if (number.Length > 0)
                {
                    postfix += number + " "; 
                    number = string.Empty; 
                }

                if (item == ' ')
                {
                    continue;
                }

                if (IsOperator(item))
                {
                    if (item == ')')
                    {
                        while (stack.Count > 0 && stack.Peek() != '(')
                        {
                            postfix += stack.Pop() + " ";
                        }

                        if (stack.Count > 0)
                        {
                            stack.Pop();
                        }
                        else
                        {
                            throw new Exception("Invalid expression.");
                        }
                    }
                    else
                    {
                        if (stack.Count == 0 || item == '(')
                        {
                            stack.Push(item);
                        }
                        else
                        {
                            while (stack.Count > 0 &&
                                   stack.Peek() != '(' &&
                                   PriorityInfix(item) <= PriorityStack(stack.Peek()))
                            {
                                postfix += stack.Pop() + " ";
                            }

                            stack.Push(item);
                        }
                    }
                }
                else
                {
                    throw new Exception("Invalid expression.");
                }
            }
        }

        if (number.Length > 0)
        {
            postfix += number + " ";
        }

        while (stack.Count != 0)
        {
            if (stack.Peek() == '(')
            {
                throw new Exception("Invalid expression.");
            }

            postfix += stack.Pop() + " ";
        }

        return postfix;
    }

    private static int PriorityStack(char op) => op switch
    {
        '^' => 3,
        '*' => 2,
        '/' => 2,
        '+' => 1,
        '-' => 1,
        '(' => 0,
        _ => throw new Exception("Invalid expression."),
    };

    private static int PriorityInfix(char op) => op switch
    {
        '^' => 4,
        '*' => 2,
        '/' => 2,
        '+' => 1,
        '-' => 1,
        '(' => 5,
        _ => throw new Exception("Invalid expression."), 
    };

    private static bool IsOperator(char item) =>
        item == '^' || item == '*' || item == '/' ||
        item == '+' || item == '-' || item == '(' || item == ')';

    private static double EvalutePostfix(string postfix)
    {
        var stack = new Stack<double>();
        var number = string.Empty;

        foreach (var item in postfix)
        {
            if (char.IsDigit(item) || item == '.')
            {
                number += item;
            }
            else
            {
                if (number.Length > 0)
                {
                    stack.Push(double.Parse(
                        number,
                        System.Globalization.CultureInfo.InvariantCulture));

                    number = string.Empty;
                }

                if (item == ' ')
                {
                    continue;
                }

                if (IsOperator(item))
                {
                    var ope2 = stack.Pop();
                    var ope1 = stack.Pop();
                    stack.Push(Calculate(ope1, ope2, item));
                }
            }
        }

        if (number.Length > 0)
        {
            stack.Push(double.Parse(
                number,
                System.Globalization.CultureInfo.InvariantCulture));
        }

        return stack.Pop();
    }

    private static double Calculate(double ope1, double ope2, char item) => item switch
    {
        '*' => ope1 * ope2,
        '/' => ope1 / ope2,
        '+' => ope1 + ope2,
        '-' => ope1 - ope2,
        '^' => Math.Pow(ope1, ope2),
        _ => throw new Exception("Invalid expression."),
    };
}
