using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            Array<object> myArray = new Array<object>(1, "2", 3.0, true);
            myArray.ItemValueChanged += MyArray_ItemValueChanged;
            myArray[2] = false;
            myArray[3] = 6.0;

            Console.ReadKey();
        }

        private static void MyArray_ItemValueChanged(object sender, ArrayItemValueChangeArgs<object> e)
        {
            Console.WriteLine("Item[{0}] = {1} change to {2}", e.Index, e.OldValue.ToString(), e.NewValue.ToString());
        }
    }

    public class Array<T>
    {
        public T[] Items { get; }

        public T this[int index]
        {
            get
            {
                if (Items == null || index < 0 || index >= Items.Length)
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    return Items[index];
                }
            }
            set
            {
                if (Items == null || index < 0 || index >= Items.Length)
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    T oldValue = Items[index];
                    Items[index] = value;
                    ItemValueChanged?.Invoke(this, new ArrayItemValueChangeArgs<T>(oldValue, value, index));
                }
            }
        }

        public event EventHandler<ArrayItemValueChangeArgs<T>> ItemValueChanged;

        public Array(params T[] items)
        {
            Items = items;
        }

    }
    public class ArrayItemValueChangeArgs<T> : EventArgs
    {
        public T OldValue { get; }
        public T NewValue { get; }
        public int Index { get; }
        public bool CancelRequested { get; set; }
        public ArrayItemValueChangeArgs(T oldValue, T newValue, int index)
        {
            OldValue = oldValue;
            NewValue = newValue;
            Index = index;
        }
    }
}