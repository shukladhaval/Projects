
namespace Acquarium
{
    public class Fish : IFish
    {
        string _name;
        string _type;
        double _foodRequire;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public double FoodRequire
        {
            get
            {
                return _foodRequire;
            }
            set
            {
                _foodRequire = value;
            }
        }
    }
}
