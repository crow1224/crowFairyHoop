using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace QMG
{

    public class GameContext
    {

        /// <summary>
        /// callback after confirmed choose context
        /// </summary>
        public static System.Action chooseAsync_Callback = null;

        [DllImport("__Internal")]
        public static extern void context_chooseAsync(string jsonStr);

        [DllImport("__Internal")]
        public static extern string context_getType();

        [DllImport("__Internal")]
        public static extern string context_getID();

        public void chooseAsync(Dictionary<string, object> p, System.Action cb)
        {
            chooseAsync_Callback = cb;
            context_chooseAsync(SimpleJson.SimpleJson.SerializeObject(p));
        }

        public string getType()
        {
            return context_getType();
        }

        public string getID()
        {
            return context_getID();
        }


    }

}
