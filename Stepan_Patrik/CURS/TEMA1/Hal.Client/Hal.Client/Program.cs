using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{

    class Program
    {
        static void Main(string[] args)
        {
            UIStateType gState = UIStateType.MAIN_MENU;
            UIStateType gNextState = UIStateType.ERROR;

            while(true)
            {
                Run(gState, out gNextState);
                gState = gNextState;
            }
        }

        static private void Run(UIStateType state, out UIStateType nextState)
        {
            nextState = UIStateType.ERROR;
            switch(state)
            {
                case UIStateType.MAIN_MENU:
                    nextState = UIMainMenuState.Execute();
                    break;
                case UIStateType.LIST_ALL_BREWERIES:
                    nextState = UIBreweriesListState.Execute();
                    break;
                case UIStateType.LIST_ALL_BEERS:
                    nextState = UIBeerList.Execute();
                    break;
                case UIStateType.LIST_BEERS_PER_BREWERY:
                    nextState = UIBeersPerBrewery.Execute();
                    break;
                case UIStateType.SEARCH_BEER_BY_NAME:
                    break;
                case UIStateType.BEER_DETAILS:
                    nextState = UIBeerDetails.Execute();
                    break;
                case UIStateType.ADD_BEER:
                    nextState = UIAddBeer.Execute();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("ERROR");
                    break;
            }
        }
    }
}
