private static string GetElementId(int IdElement) // method for performed full path
        {
            var _element = List.FirstOrDefault(el => IdElement == el.Id); // get current object with certain parameter - 'IdElement'
            if(_element is null) // if variable is null then
            {
                Console.Write("Root"); // output current string
                Stop(); // and call mathod for close program
            }
            Console.Write($"{ _element.Name} - > "); // output 'Name' for current element

            string strPath = GetElementId(_element.ParentId); // call method with Id about Parent current element
            //string strPath = _element.Name;

            return strPath;
        }