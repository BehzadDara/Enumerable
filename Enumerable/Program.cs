using System.Collections;
using System.Xml.Linq;

// use custom IEnumerable
MyEnumerator<MyClass> myList = new()
{
    // Add method for adding values to list
    new MyClass {MyProperty= 3},
    new MyClass {MyProperty= 9},
    new MyClass {MyProperty= 10},
    new MyClass {MyProperty= 12},
    new MyClass {MyProperty= 13},
};

// [int index] method for get and set by indexing
myList[5] = new MyClass { MyProperty = 7 };

// add method
myList.Add(new MyClass { MyProperty = 5 });
myList.Add(new MyClass { MyProperty = 7 });

// remove method
myList.Remove(1);

// remove method
myList.Remove(myList.Where(x => x.MyProperty % 2 == 0));

// remove method
myList.Remove(myList.FirstOrDefault(x => x.MyProperty == 13));

Console.WriteLine($"Size: {myList.Size}");

// order .Net7 ( IComparable )
myList = myList.Order().ToList();

// IEnumerable interface and GetEnumerator method for using foreach on the list
myList.Print();

class MyEnumerator<T> : IEnumerable<T>
{
    readonly List<T> MyList = new();

    public MyEnumerator()
    {
    }

    public MyEnumerator(List<T> value)
    {
        MyList = value;
    }

    public int Size => MyList.Count;

    public int Key { get; private set; }

    public static implicit operator MyEnumerator<T>(List<T> value) 
    { 
        return new MyEnumerator<T>(value); 
    }

    public T this[int index]
    {
        get { return MyList[index]; }
        set { MyList.Insert(index, value); }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return MyList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public void Add(T t)
    {
        MyList.Add(t);
    }

    public void Remove(T? t)
    {
        if (t is not null)
            MyList.Remove(t);
    }

    public void Remove(IEnumerable<T> listT)
    {
        foreach(var t in listT.ToList())
            MyList.Remove(t);
    }
    
    public void Remove(int index)
    {
        if(index < Size)
            Remove(MyList[index]);
    }

    public void Print()
    {
        foreach (var item in MyList)
        {
            Console.WriteLine(item);
        }
    }
}

class MyClass: IFormattable, IComparable<object>
{
    public int MyProperty { get; set; }

    public int CompareTo(object? other)
    {
        if (other is not MyClass myClass) throw new Exception();

        if (MyProperty > myClass.MyProperty) return 1;
        if (MyProperty == myClass.MyProperty) return 0;
        return -1;
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return $"MyProperty is {MyProperty}";
    }

}