
namespace Backend;

internal class Stack<T>
{
    private T[] _stack;
    public int _top;

    public Stack(int n)
    {
        N = n;
        _stack = new T[N];
        _top = 0;
        Count = 0;
    }

    public Stack() : this(100)
    {
    }

    public int N { get; }

    public bool IsEmpty => _top == 0;

    public bool IsFull => _top == N;

    public int Count { get; private set; }

    public T GetItemInTop() => _stack[_top - 1]; // Devuelve lo que hay en el tope

    public void Push(T item)
    {
        if (IsFull)
        {
            throw new Exception("Stack full.");
        }

        _stack[_top++] = item;
        Count++;
    }

    public T Pop()
    {
        if (IsEmpty)
        {
            throw new Exception("Stack empty.");
        }

        _top--;
        var item = _stack[_top];
        Count--;

        return item;
    }

    public T Peek()
    {
        if (IsEmpty)
        {
            throw new Exception("Stack empty.");
        }

        return _stack[_top - 1];
    }
}

