
using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Gof
{
    class K
    {

        public static void Main() // point start
        {
            List<object> list = new List<object>() { 34, 46, 34.4, 4.4, "34df", "sfg4", "4.4f" }; // list with different values

            TwoSize(list); // call method TwoSize
        }

        private static void TwoSize<T>(List<T> git) // realization sort elements on to size 'valueType' and 'referenceType'
        {
            List<T> valType = new List<T>(); // list for value types
            List<T> refType = new List<T>(); // list for reference types

            foreach(var item in git) // loop operator for selected every element
            {
                Type ty = item.GetType(); // emphasis on the current type of the element
                
                if(ty.IsValueType) // validate element on 'value type'
                {
                    valType.Add(item); // add current element to the list -> 'valType'
                    Console.WriteLine(ty + " value"); // output current element
                }
                else if(!ty.IsByRef) // validate element on 'reference type'
                {
                    refType.Add(item); // add current element to the list -> 'refType'
                    Console.WriteLine(ty + " reference"); // output current element
                }
            }

            Console.WriteLine(valType.Count); // output count list-> valType
            Console.WriteLine(refType.Count); // output count list -> refType
            
        }
    }
}