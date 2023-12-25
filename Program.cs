namespace PracticeCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TemperatureSensor temperatureSensor = new TemperatureSensor();
            TemperatureMonitor temperatureMonitor = new TemperatureMonitor();
            temperatureSensor.TemperatureChanged += temperatureMonitor.OnTemperatureChanged;
            temperatureSensor.GenerateTemperature();
        }
    }

    internal class TemperatureSensor
    {

        private static Random random = new Random();

        public int Temperature { get; private set; }
        public int MaxTemperature { get; private set; }
        public int MinTemperature { get; private set; }

        public event EventHandler? TemperatureChanged;

        protected virtual void OnTemperatureChanged()
        {
            TemperatureChanged?.Invoke(this, EventArgs.Empty);
        }

        // generate temperature and publish an event "OnTemperatureChanged"
        public void GenerateTemperature()
        {
            while (true)
            {
                Temperature = random.Next(minValue: 0, maxValue: 101);
                if (Temperature > MaxTemperature)
                {
                    MaxTemperature = Temperature;
                }

                if (Temperature < MinTemperature)
                {
                    MinTemperature = Temperature;
                }

                OnTemperatureChanged();
                Thread.Sleep(3000);
            }
        }
    }

    internal class TemperatureMonitor
    {
        public void OnTemperatureChanged(object? source, EventArgs args)
        {
            if(source is TemperatureSensor temperatureSensor)
            {
                Console.WriteLine($"Temperature changed: {temperatureSensor.Temperature} °C, Max: {temperatureSensor.MaxTemperature} °C, Min: {temperatureSensor.MinTemperature} °C");
            }
        }
    }
    

}