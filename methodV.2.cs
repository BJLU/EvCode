private static string GetEl(int IdEl)
        {
            var _elem = List.FirstOrDefault(e => IdEl == e.Id); // get element with current id -> 'IdEl'
            if(_elem is null) // if get root, then
            {
                strPath += "Root"; // show that it's Root
                goto end; // and return full path
            }

            strPath += _elem.Name + " -> "; // save current  step-path
            GetEl(_elem.ParentId); // call current method
            
            end:
            return strPath; // return full path to the Root
        }