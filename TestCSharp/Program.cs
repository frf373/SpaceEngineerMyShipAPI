using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCSharp
{
    class Program
    {
        static void Main(string[] args)
        {

            MyArrayCollection<MyState> collections = 
                new MyArrayCollection<MyState>(new MyState[] { new MyState(), new MyState(), new MyState() });
            foreach(var item in collections)
            {
                Console.WriteLine(item.a);
                
            }
            Console.ReadLine();
        }
    }

    public class MyArrayCollection<T> : IEnumerable<T> where T : class, ICloneable
    {
        public T[] myArrayList;
        public MyArrayCollection(T[] myArrayList)
        {
            this.myArrayList = new T[myArrayList.Length];
            int count = 0;
            foreach(var array in myArrayList)
            {
                this.myArrayList[count++] = array.Clone() as T;
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new MyArrayIEnumerable<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        { 
            throw new NotImplementedException();
        }
    }
    public class MyState:ICloneable
    {
        public int a = DateTime.Now.Millisecond;
        public int b = DateTime.Now.Millisecond;

        public MyState()
        {

        }
        public MyState(MyState state)
        {
            this.a = state.a;
            this.b = state.b;
        }
        public object Clone()
        {
            return new MyState(this);
        }
    }
    public class MyArrayIEnumerable<T> : IEnumerator<T> where T : class , ICloneable
    {
        int pos = -1;

        int length;

        T[] myArrayList;

        public MyArrayIEnumerable(MyArrayCollection<T> myArrayCollection)
        {
            this.length=myArrayCollection.myArrayList.Length;
            this.myArrayList = myArrayCollection.myArrayList;
        }
        public T Current => myArrayList[pos];

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            return pos++ <length-1?true:false;
        }

        public void Reset()
        {
            pos = -1;
        }
    }
    public class ShipCollection : IEnumerable<Ship>,IEnumerable<int>
    {
        public Ship ship1=new Ship();
        public Ship ship2=new Ship();

        public IEnumerator<Ship> GetEnumerator()
        {
            return new ShipEnumrator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ShipEnumrator(this);
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            return new ShipEnumrator(this);
        }
    }
    public class Ship 
    {
        public int number;
        public Ship()
        {
            number = DateTime.Now.Millisecond;
        }

    }
    public class ShipEnumrator : IEnumerator<Ship>, IEnumerator<int>
    {
        int position = -1;

        Hashtable hashtable=new Hashtable();

        public ShipEnumrator(ShipCollection ships)
        {
            int count = 0;
            hashtable.Add(count++, ships.ship1);
            hashtable.Add(count++, ships.ship2);
        }
        public Ship Current => hashtable[position] as Ship;

        object IEnumerator.Current => Current;

        int IEnumerator<int>.Current => Current.number;

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            return position++ < 1 ? true : false;
        }

        public void Reset()
        {
            position = -1;
        }
    }
    public class Test2 : IEnumerable
    {
        public int a = 666;

        public int b = 888;
        public IEnumerator GetEnumerator()
        {
            return new TestEnumrator(this);
        }
    }
    public class TestEnumrator : IEnumerator
    {
        private int a;

        private int b;

        private int[] objectsInClass=new int[2] { 0,1};

        private int objectNum=-1;
        public TestEnumrator(Test2 test)
        {
            a = test.a;
            b = test.b;
            hashtable.Add(objectsInClass[0],a);
            hashtable.Add(objectsInClass[1], b);
        }
        private Hashtable hashtable=new Hashtable(); 
        public object Current => hashtable[objectsInClass[objectNum]];

        public bool MoveNext()
        {
            if (objectNum < 1)
            {
                objectNum++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            objectNum = -1;
        }
    }
    abstract public class Father
    {
        abstract public void Test();

        public Father()
        {
            Test();
        }
    }
    public class Son:Father
    {
        public Son()
        {

        }

        public override void Test()
        {
            Console.WriteLine("Son");
        }
    }
    public class GrandSon:Son
    {
        public GrandSon()
        {

        }

        public override void Test()
        {
            Console.WriteLine("GrandSon");
        }
    }
}
