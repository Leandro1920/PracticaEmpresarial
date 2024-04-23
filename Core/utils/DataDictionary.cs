using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.utils
{
    public class DataDictionary
    {
        private Dictionary<string, string> _values;

        public DataDictionary()
        {
            _values = new Dictionary<string, string>();
        }

        public DataDictionary(Dictionary<string, string> data)
        {
            _values = data;
        }

        public int Count => _values.Count;

        public Boolean ContainsKey(string p_key)
        {
            return _values.ContainsKey(p_key);
        }

        public void Add(string key, string val)
        {
            _values.Add(key, val);
        }

        public string ItemString(string p_key)
        {
            string tmp = "";
            if (_values.ContainsKey(p_key))
            {
                return _values[p_key];
            }
            else
            {
                return tmp;
            }
        }

        public string Item(string p_key)
        {
            string tmp = "";
            if (_values.ContainsKey(p_key))
            {
                return _values[p_key];
            }
            else
            {
                return tmp;
            }
        }

        public DateTime? ItemDateTime(string p_key)
        {
            DateTime? dateval = null;
            string tmp = "";
            if (_values.ContainsKey(p_key))
            {
                tmp = _values[p_key];
                try
                {
                    dateval = DateTime.ParseExact(tmp, "yyyy-MM-dd HH:mm:ss", null);
                }
                catch (Exception ex)
                {
                }

                return dateval;
            }
            else
            {
                return dateval;
            }
        }

        public int ItemInteger(string p_key)
        {
            string tmp = "";
            if (_values.ContainsKey(p_key))
            {
                try
                {
                    return int.Parse(_values[p_key]);
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public static implicit operator DataDictionary(Dictionary<string, string> v)
        {
            throw new NotImplementedException();
        }
    }
}
