
namespace Acquarium
{
    public class Tank : ITank
    {
        private string _name;
        private string _capacity;

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

        public string Capacity
        {
            get
            {
                return _capacity;
            }
            set
            {
                _capacity = value;
            }
        }

        public double Feed()
        {           
            FishManagment fishManagment = new FishManagment();
            return fishManagment.GetTotalFoodFeedRequire();            
        }
    }
}
