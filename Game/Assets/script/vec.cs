using System.Collections;
using System.Collections.Generic;

public class vec<T>
{
    public vec()
    {
        size = 0;
        cap = 8;
        _elements = new T[cap];
    }
    public vec(int sze)
    {
        size = sze;
        cap = size+8;
        _elements = new T[cap];
    }

    public static vec<T> operator + (vec<T> a, vec<T> b)
    {
        vec<T>  vec = new vec<T>();

        return vec;
    }

    public int size;
    private int cap;
    private T[] _elements;
}
