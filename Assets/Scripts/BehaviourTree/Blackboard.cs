using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Blackboard
    {
        private Dictionary<string, object> _data = new Dictionary<string, object>();

        public void SetData(string name, object data)
        {
            if (data == null)
                return;

            if(_data.ContainsKey(name))
                _data[name] = data;
            else
                _data.Add(name, data);
        }

        public object GetData(string name)
        {
            if(!_data.ContainsKey(name))
                return null;

            return _data[name];
        }
    }
}

