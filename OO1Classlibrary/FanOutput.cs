using System;

namespace OO1Classlibrary
{
    public class FanOutput
    {
        private string _name;
        private double _temp;
        private double _humidity;
        public int ID { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                if (value.Length < 2) throw new ArgumentOutOfRangeException("Value", "Value needs to be 2 or more characters");
                _name = value;
            }
        }

        public double Temp
        {
            get => _temp;
            set
            {
                if(value < 15 || value > 25) throw new ArgumentOutOfRangeException("Value", "Value needs to be between 15 and 25");
                _temp = value;
            }
        }

        public double Humidity
        {
            get => _humidity;
            set
            {
                if(value < 30 || value > 80) throw new ArgumentOutOfRangeException("Value", "Value needs to be between 30 and 80");
                _humidity = value;
            }
        }

        public FanOutput()
        {
        }

        public FanOutput(int id, string name, double temp, double humidity)
        {
            ID = id;
            Name = name;
            Temp = temp;
            Humidity = humidity;
        }

        public override string ToString()
        {
            return $"{nameof(ID)}: {ID}, {nameof(Name)}: {Name}, {nameof(Temp)}: {Temp}, {nameof(Humidity)}: {Humidity}";
        }
    }
}
