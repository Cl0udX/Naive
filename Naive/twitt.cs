using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naive
{
    class twitt
    {
        private String[] worldsTwitt;
        private List<String> pureTwitt;
        private List<int> worldsCount;
       

        public twitt(String[] worldsTwitt, List<String> pureTwitt, List<int> worldsCount)
        {
           this.worldsTwitt = worldsTwitt;
            this.pureTwitt = pureTwitt;
            this.worldsCount = worldsCount;
            
        }
        

        public string[] WorldsTwitt { get => worldsTwitt; set => worldsTwitt = value; }
        public List<string> PureTwitt { get => pureTwitt; set => pureTwitt = value; }
        public List<int> WorldsCount { get => worldsCount; set => worldsCount = value; }
    }
}
