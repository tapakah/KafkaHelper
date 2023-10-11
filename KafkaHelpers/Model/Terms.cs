using System;

namespace KafkaHelpers
{
    public class Terms
    {
        private static DateTime? _Start;
        private static DateTime? _End;
        private static string _Key;
        private static string _Message;
        private static int _Counter;
        private static int _Top;
        private static bool _IsStatistic;
        private dynamic _KeyType = string.Empty;
        private dynamic _MessageType = string.Empty;

        public DateTime? Start
        { set { _Start = value; } get { return _Start; } }

        public DateTime? End
        { set { _End = value; } get { return _End; } }

        public string Key
        { set { _Key = value; } get { return _Key; } }

        public string Message
        { set { _Message = value; } get { return _Message; } }

        public int Counter
        { set { _Counter = value; } get { return _Counter; } }

        public int MaxRows
        { set { _Top = value; } get { return _Top; } }

        public bool IsStatistic
        { set { _IsStatistic = value; } get { return _IsStatistic; } }

        public dynamic KeyType
        { set { _KeyType = value; } get { return _KeyType; } }

        public dynamic MessageType
        { set { _MessageType = value; } get { return _MessageType; } }

        public Terms()
        {
        }
    }
}