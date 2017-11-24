using BeersClient.Models;
using BeersClient.Services;
using BeersClient.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BeersClient
{
    class Program
    {
        private static HALClientService _service;
        private static Level _level;
        private static Level _previousLevel;
        private static Level _nextLevel;
        private static UiModel _uiModel;
        private static string _userOption;
        static void Main(string[] args)
        {
            _uiModel = new UiModel();
            _userOption = string.Empty;
            _level = Level.Breweries;
            _service = new HALClientService();
            ExecuteClient().GetAwaiter().GetResult();
        }

        private static async Task ExecuteClient()
        {
            while (_level != Level.None)
            {
                try
                {
                    switch (_level)
                    {
                        case Level.None:
                            break;
                        case Level.Breweries:
                            _uiModel.Breweries = await _service.GetBreweries();
                            Menu.Display(_uiModel.Breweries);
                            _previousLevel = Level.None;
                            _nextLevel = Level.Brewery;
                            break;
                        case Level.Brewery:
                            _uiModel.Brewery = _uiModel.Breweries.FirstOrDefault(x => x.Id.ToString() == _userOption);
                            Menu.Display(await _service.GetBrewery(_uiModel.Brewery.Links.Self.Href));
                            _previousLevel = Level.Breweries;
                            _nextLevel = Level.Beers;
                            break;
                        case Level.Beers:
                            var beerLink = _uiModel.Brewery.Links.Beers.Href;
                            _uiModel.Beers = (await _service.GetBeers(beerLink)).Embedded.Beer.ToList();
                            Menu.Display(_uiModel.Beers);
                            _previousLevel = Level.Brewery;
                            break;
                        case Level.Beer:
                            //var beerString = _uiModel.Beers.FirstOrDefault().;
                            //var beer = _service.GetBeer();
                            break;
                        case Level.Add:
                            string beerName = Console.ReadLine();
                            var result = _service.AddBeer(beerName);
                            Console.WriteLine("Added");
                            break;
                        default:
                            break;
                    }
                    _userOption = Console.ReadLine();
                    _level = _userOption == "b" ? _previousLevel : _nextLevel;
                    if (_userOption == "q")
                        _level = Level.None;
                    if (_userOption == "beers")
                        _level = Level.Beers;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    _level = Level.Breweries;
                }
            }
        }
    }
}
