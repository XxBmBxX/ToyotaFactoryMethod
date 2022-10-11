namespace ToyotaFactoryMethod
{
    public enum ToyotaColor
    {
        Gray,
        Green,
        Red,
        Blue,
        Yellow,
        Black,
    }

    public enum ToyotaModel
    {
        Corolla,
        Prius
    }

    public enum ToyotaEngineType
    {
        Diesel,
        Gas,
        Petrol,
        Electric
    }

    public class ToyotaGarage
    {
        private List<Toyota> _toyotas = new() { };

        public List<Toyota> Toyotas { get => _toyotas; }

        public Toyota AddToyota(ToyotaFactory toyotaFactory, ToyotaColor toyotaColor)
        {
            Toyota newToyota = toyotaFactory.CreateToyota(toyotaColor);
            _toyotas.Add(newToyota);
            return newToyota;
        }

        public Toyota AddToyota(ToyotaFactory toyotaFactory)
        {
            Toyota newToyota = toyotaFactory.CreateToyota();
            _toyotas.Add(newToyota);
            return newToyota;
        }
    }

    public abstract class ToyotaFactory
    {
        public abstract Toyota CreateToyota();
        public abstract Toyota CreateToyota(ToyotaColor toyotaColor);
    }

    public class EuropeanFactory : ToyotaFactory
    {
        public override Toyota CreateToyota()
        {
            return new ToyotaCorolla();
        }
      
        public override Toyota CreateToyota(ToyotaColor toyotaColor)
        {
            return new ToyotaCorolla(toyotaColor);
        }
    }

    public class JapanFactory : ToyotaFactory
    {
        public override Toyota CreateToyota()
        {
            return new ToyotaPrius();
        }

        public override Toyota CreateToyota(ToyotaColor toyotaColor)
        {
            return new ToyotaPrius(toyotaColor);
        }
    }

    public class ToyotaPrius : Toyota
    {
        public ToyotaPrius() 
        {
            Model = ToyotaModel.Prius;
        }
        public ToyotaPrius(ToyotaColor toyotaColor) : base(toyotaColor, ToyotaModel.Prius) { }

        public override void CreateToyotaWithColor(ToyotaColor toyotaColor)
        {
            Color = toyotaColor;
        }
    }

    public class ToyotaCorolla : Toyota
    {
        public ToyotaCorolla() { }

        public ToyotaCorolla(ToyotaColor toyotaColor) : base(toyotaColor, ToyotaModel.Corolla) { }

        public override void CreateToyotaWithColor(ToyotaColor toyotaColor)
        {
            Color = toyotaColor;
        }
    }

    public abstract class Toyota
    {
        private ToyotaModel _model;
        private ToyotaEngineType _engineType;
        private ToyotaColor _color;

        public ToyotaModel Model { get => _model; set => _model = value; }
        public ToyotaEngineType EngineType { get => _engineType; set => _engineType = value; }
        public ToyotaColor Color { get => _color; set => _color = value; }

        public Toyota() { }
        public Toyota(ToyotaColor toyotaColor, ToyotaModel toyotaModel)
        {
            Model = toyotaModel;
            CreateToyotaWithColor(toyotaColor);
        }

        public abstract void CreateToyotaWithColor(ToyotaColor toyotaColor);

        public override string ToString()
        {
            return $"This is Toyota {Model} with {EngineType.ToString().ToLower()} engine in {Color.ToString().ToLower()} color.";
        }
    }

    public static class MainApp
    {
        public static void Main()
        {
            ToyotaGarage toyotaGarage = new();
            EuropeanFactory europeanFactory = new();
            JapanFactory japanFactory = new();
            Console.WriteLine(toyotaGarage.AddToyota(europeanFactory));
            Console.WriteLine(toyotaGarage.AddToyota(europeanFactory, ToyotaColor.Red));
            Console.WriteLine(toyotaGarage.AddToyota(japanFactory));
            Console.WriteLine(toyotaGarage.AddToyota(japanFactory, ToyotaColor.Blue));
        }
    }
}
