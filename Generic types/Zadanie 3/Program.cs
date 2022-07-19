using System;
using System.Collections;
using System.Collections.Generic;

namespace Zadanie_3
{
   //Zadanie 3.1

   interface Shape
   {
       public int GetArea();
   }
   class Rectangle : Shape
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int GetArea()
        {
            return Width * Height;
        }
    }
    class Square : Shape
    {
        public  int Size { get; set; }
        public int GetArea()
        {
            return Size * Size;
        }
    }
    //Zadanie 3.2
    class Queue : ArrayList
    {
        public void Enqueue(Object value)
        {
            this.Add(value);
        }

        public Object Dequeue()
        {
            Object _temp = this[0];
            this.RemoveAt(0);
            return _temp;
        }
    }

    class Queue2 
    {
        private ArrayList _list;
        
        public void Enqueue(Object value)
        {
            _list.Add(value);
        }

        public Object Dequeue()
        {
            Object _temp = _list[0];
            _list.RemoveAt(0);
            return _temp;
        }
    }
    //Zadanie 3.3
    class Queue3 
    {
        private int first, last, capacity;
        private object []_list;
        
        public Queue3(int c)
        {
            first = last = 0;
            capacity = c;
            _list = new object [capacity];
        }
        
        public void Enqueue(int data)
        {
            if (capacity == last)
            {
                
            }
            else
            {
                _list[last] = data;
                last++;
            }
        }
 
        public void Dequeue()
        {
            if (first == last)
            {
                
            }
            else
            {
                for (int i = 0; i < last - 1; i++)
                {
                    _list[i] = _list[i + 1];
                }
                if (last < capacity)
                    _list[last] = 0;
                last--;
            }
        }
    }

    //Zadanie 3.4

    interface IComplex<T>
    {
        
    }
    class Complex <T> : IComplex<T> where T: struct
    {
        public T r { get; set; }
        public T i { get; set; }
        
        //Zadanie 3.6
        public static Complex<T> operator *(Complex<T> a, Complex<T> b)
        {
            Complex<T> c = new Complex<T>();
            c.r = (dynamic)a.r*b.r - (dynamic)a.i*b.i;
            c.i = (dynamic)a.r*b.i + (dynamic)a.i*b.r;
            return c;
        }
        
        public static Complex<T> operator +(Complex<T> a, Complex<T> b)
        {
            Complex<T> c = new Complex<T>();
            c.r = (dynamic)a.r + b.r;
            c.i = (dynamic)a.i + b.i;
            return c;
        }
    }

    //Zadanie 3.5
    class Matrix <T> where T : struct, IComplex<T>
    {
        public T [,] _matrix;
        public int N => _matrix.GetUpperBound(0) + 1;
        public int M => _matrix.GetUpperBound(1) + 1;

        public Matrix()
        {
            
        }

        public Matrix(T[,] matrix)
        {
            _matrix = matrix;
        }
        
        public Matrix(int n, int m)
        {
            _matrix = new T[n, m];
        }

        
        public void Mult(Matrix<T> b)
        {
            if (this.M != b.N)
            {
                return;
            }
            Matrix<T> c = new Matrix<T>(this.N, b.M);
            for (int i = 0; i < c.N; i++)
            {
                for (int j = 0; j < c.M; j++)
                {
                    dynamic s = 0;
                    for (int m = 0; m < this.M; m++)
                    {
                        s += (dynamic)_matrix[i, m] * b._matrix[m, j];
                    }
                    c._matrix[i, j] = s;
                }
            }
            _matrix = c._matrix;
        }
        
        public void Add(Matrix<T> b)
        {
            if (this.M != b.M || this.N != b.N)
            {
                return;
            }
            Matrix<T> c = new Matrix<T>(this.N, b.M);

            for (int i = 0; i < c.N; i++)
            {
                for (int j = 0; j < c.M; j++)
                {
                    c._matrix[i, j] = (dynamic)_matrix[i, j] + b._matrix[i, j];
                }
            }
            _matrix = c._matrix;
        }
    }

    class SquareMatrix<T> : Matrix<T> where T:struct ,IComplex<T>
    {
        private T [,] _matrix;
        public int N => _matrix.GetUpperBound(0) + 1;
        
        public SquareMatrix(T[,] matrix)
        {
            _matrix = matrix;
        }
        
        public SquareMatrix(int n)
        {
            _matrix = new T[n, n];
        }
        public void Mult(Matrix<T> b)
        {
            if (this.M != b.N)
            {
                return;
            }
            Matrix<T> c = new Matrix<T>(this.N, b.M);
            for (int i = 0; i < c.N; i++)
            {
                for (int j = 0; j < c.M; j++)
                {
                    dynamic s = 0;
                    for (int m = 0; m < this.M; m++)
                    {
                        s += (dynamic)_matrix[i, m] * b._matrix[m, j];
                    }
                    c._matrix[i, j] = s;
                }
            }
            _matrix = c._matrix;
        }
        
        public void Add(Matrix<T> b)
        {
            if (this.N != b.M || this.N != b.N)
            {
                return;
            }
            Matrix<T> c = new Matrix<T>(this.N, b.M);

            for (int i = 0; i < c.N; i++)
            {
                for (int j = 0; j < c.M; j++)
                {
                    c._matrix[i, j] = (dynamic)_matrix[i, j] + b._matrix[i, j];
                }
            }
            _matrix = c._matrix;
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}