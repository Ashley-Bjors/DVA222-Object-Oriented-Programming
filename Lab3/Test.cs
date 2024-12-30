using System;
using System.Collections;
using System.Collections.Generic;  

class Program {
    static public void Main () {
        Test(new Dictionary<char, double>()); //to be replaced with your class
    }
    static public void Test(IDictionary<char, double> d) {
        d.Add('?', 3.9);
        d.Add('2', 0.2);
        d.Add('r', 1.2);
        var p = new KeyValuePair<char,double>('*', 5.2);
        d.Add(p);
        d['?'] -= d['?'];
        try { d.Add('2', 6.0); } catch (Exception e) { Console.WriteLine(e.GetType().ToString()+" : "+e.Message); } 
        // System.ArgumentException : An item with the same key has already been added. Key: 2
        foreach(var elem in d.Keys)
            Console.Write("'{0}' ", elem); 
        Console.WriteLine();
        // '?' '2' 'r' '*'
        foreach(var elem in d.Values)
            Console.Write("{0} ", elem);
        Console.WriteLine();
        // 0 0.2 1.2 5.2 
        int count = 0;
        foreach(var elem in d)
            { Console.Write("{0},'{1}' ", elem.Key, elem.Value); ++count; } 
        Console.WriteLine("("+d.Count+"="+count+")");
        // ?,'0' 2,'0.2' r,'1.2' *,'5.2' (4=4)
        Console.WriteLine("{0} {1}", d.ContainsKey('2'), d.ContainsKey('>')); 
        // True False
        Console.WriteLine("{0} {1}", d.Contains(new KeyValuePair<char,double>('r', 7.9)), d.Contains(p)); 
        // False True
        KeyValuePair<char,double>[] a = new KeyValuePair<char,double>[d.Count];
        try {  d.CopyTo(a,1); } catch (Exception e) { Console.WriteLine(e.GetType().ToString()+" : "+e.Message); } 
        // System.ArgumentException : Destination array is not long enough to copy all the items in the collection. Check array index and length.
        d.CopyTo(a,0);
        foreach(var elem in a)
            Console.Write("'{0}'={1} ", elem.Key, d.ContainsKey(elem.Key)); 
        Console.WriteLine();
        //'?'=True '2'=True 'r'=True '*'=True
        Console.WriteLine("{0} {1}", d.Remove('?'), d.Remove('#')); 
        // True False
        Console.WriteLine(d.Count); 
        // 3
        Console.WriteLine("{0} {1}", d.Remove(new KeyValuePair<char,double>('r', 7.9)), d.Remove(p)); 
        // False True
        Console.WriteLine(d.Count); 
        // 2
        double v1 = -1, v2 = -1;
        d.TryGetValue('r', out v1);
        d.TryGetValue('R', out v2);
        Console.WriteLine(v1+" "+v2); 
        // 1.2 0
        d.Clear();
        count = 0;
        foreach(var elem in d) ++count;
        Console.WriteLine(d.Count+"="+count); 
        // 0=0
    }
}
