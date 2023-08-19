using System;
using System.Collections.Generic;

namespace MovieApi.Services
{
    public class Validations
    {
        // string type check
        public bool InputValidate(String s)
        {
            if (string.IsNullOrEmpty(s) || int.TryParse(s, out _))
            {
                return false;
            }
            return true;
        }

        // type casting string to int
        public int InputTypeCheck(string id)
        {
            int newid;
            if (int.TryParse(id, out _))
            {
                newid = int.Parse(id);
                if (newid <= 0)
                    return -1;
                else
                    return newid;
            }
            else
                return -1;
        }

       public bool yearCheck(int year)
        {
            string newyear = year.ToString();
            if (newyear.Length != 4)
                return false;
            return true;
        }

        public bool IdsCheck(List<int> ids)
        {
            if(ids.Count == 0) return false;
            foreach(var id in ids)
            {
                if (id <= 0)
                    return false;
            }
            return true;
        }
    }
}
