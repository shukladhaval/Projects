using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acquarium
{
    public class FishManagment
    {
        List<Fish> _listOfFish;

        public FishManagment()
        {
            _listOfFish = new List<Fish>();
            getFishFromXML();
        }

        public List<Fish> GetListOfFish()
        {
            return _listOfFish;
        }

        public double GetTotalFoodFeedRequire() 
        {
            return _listOfFish.Sum(x => (double) x.FoodRequire);
        }

        public double GetFoodFeedRequireByFishType(Fish fish)
        {
            return _listOfFish.Where(x => x.Type == fish.Type).Sum(x => x.FoodRequire);                
        }

        private void addFish(Fish fish)
        {
            _listOfFish.Add(fish);
        }

        private void getFishFromXML()
        {
            //List<Fish> fishList = new List<Fish>(); 

            Fish fishA = new Fish();
            fishA.Name = "Fish A";
            fishA.Type = "A";
            fishA.FoodRequire = 0.1;
            _listOfFish.Add(fishA);

            Fish fishB = new Fish();
            fishB.Name = "Fish B";
            fishB.Type = "B";
            fishB.FoodRequire = 0.2;
            _listOfFish.Add(fishB);
            _listOfFish.Add(fishB);

            Fish fishC = new Fish();
            fishC.Name = "Fish C";
            fishC.Type = "C";
            fishC.FoodRequire = 0.3;
            _listOfFish.Add(fishC);
           
        }

    }
}
