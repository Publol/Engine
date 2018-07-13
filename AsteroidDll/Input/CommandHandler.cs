using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidDll.Input
{
    public static class CommandHandler 
    {
        private static Dictionary<string, Tuple<Command, bool>> _keyCommands = new Dictionary<string, Tuple<Command, bool>>();

        private static Dictionary<int, Tuple<Command, int>> _timerCommands = new Dictionary<int, Tuple<Command, int>>();
        
        public static void BindKeyToCommand(string key, Command command, bool keyUp = true)
        {
            _keyCommands.Add(key, new Tuple<Command, bool>(command, keyUp));
        }
        public static void UnbindKeyCommand(string key)
        {
            if (_keyCommands.ContainsKey(key))
                _keyCommands.Remove(key);
        }
        public static void BindTimerToCommand(int key, Command command, int delay)
        {
            _timerCommands.Add(key, new Tuple<Command, int>(command, delay));
        }
        public static void UnbindTimerCommand(int key)
        {
            if (_timerCommands.ContainsKey(key))
                _timerCommands.Remove(key);
        }
        public static void UnbindAll()
        {
            _keyCommands.Clear();
            _timerCommands.Clear();
        }
       

        public static Dictionary<string, Tuple<Command, bool>> GetKeyCommands()
        {
            return _keyCommands;
        }
        public static Dictionary<int, Tuple<Command, int>> GetTimerCommands()
        {
            return _timerCommands;
        }
    }
}
