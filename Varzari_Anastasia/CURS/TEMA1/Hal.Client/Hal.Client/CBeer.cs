using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hal.Client
{
    public class CBeers
    {
        [JsonProperty("TotalResults")]
        public int totalResults { get; set; }

        [JsonProperty("TotalPages")]
        public int totalPages { get; set; }

        [JsonProperty("Page")]
        public int page { get; set; }

        [JsonProperty("_links")]
        public CBeersLinks beersLinks { get; set; }

        [JsonProperty("_embedded")]
        public CBeersEmbedded beersEmbedded { get; set; }
    }

    public class CBeersLinks
    {
        [JsonProperty("self")]
        public CBeersSelfLinks beersSelf { get; set; }

        [JsonProperty("page")]
        public List<CBeersPage> beersPage { get; set; }

        [JsonProperty("beer")]
        public List<CBeersList> beersList { get; set; }
    }

    public class CBeersList
    {
        [JsonProperty("href")]
        public string beerHref { get; set; }
    }

    public class CBeersPage
    {
        [JsonProperty("href")]
        public string beersHref { get; set; }

        [JsonProperty("templated")]
        public bool templated { get; set; }
    }

    public class CBeersSelfLinks
    {
        [JsonProperty("href")]
        public string beersSelfHref { get; set; }

    }

    public class CBeersEmbedded
    {
        [JsonProperty("beer")]
        public List<CBeer> beer { get; set; }

    }

    public class CBeer
    {
        [JsonProperty("Id")]
        public int beerId { get; set; }

        [JsonProperty("Name")]
        public string beerName { get; set; }

        [JsonProperty("BreweryId")]
        public int breweryId { get; set; }

        [JsonProperty("BreweryName")]
        public string breweryName { get; set; }

        [JsonProperty("StyleId")]
        public int styleId { get; set; }

        [JsonProperty("StyleName")]
        public string styleName { get; set; }

        [JsonProperty("_links")]
        public CBeerLinks beerLinks { get; set; }

    }

    public class CBeerLinks
    {
        [JsonProperty("self")]
        public CBeerSelfLinks beerSelf { get; set; }

        [JsonProperty("style")]
        public CBeerStyleLinks beerStyle { get; set; }

        [JsonProperty("brewery")]
        public CBeerBreweryLinks beerBreweryLinks { get; set; }

    }

    public class CBeerBreweryLinks
    {
        [JsonProperty("href")]
        public string beerBrwHref { get; set; }
    }

    public class CBeerSelfLinks
    {
        [JsonProperty("href")]
        public string beerSelfHref { get; set; }
    }

    public class CBeerStyleLinks
    {
        [JsonProperty("href")]
        public string beerStyleHref { get; set; }
    }

    public class CAllBeers
    {
        [JsonProperty("_embedded")]
        public CAllBeerEmbedded embedded { get; set; }

        [JsonProperty("_links")]
        public CALLBeerLinks links { get; set; }

        [JsonProperty("Page")]
        public int page { get; set; }

        [JsonProperty("TotalPages")]
        public int totalPages { get; set; }

        [JsonProperty("TotalResults")]
        public int totalResults { get; set; }
    }

    public class CAllBeerEmbedded
    {
        [JsonProperty("beer")]
        public List<CAllBeer> beer { get; set; }
    }

    public class CAllBeer
    {
        [JsonProperty("Id")]
        public int allBeerId { get; set; }

        [JsonProperty("Name")]
        public string allBeerName { get; set; }

        [JsonProperty("BreweryId")]
        public CAllBeerLink allBeerlink { get; set; }
    }

    public class CAllBeerLink
    {
        [JsonProperty("self")]
        public CALLBeerSelf CAllbeerSelf { get; set; }
    }

    public class CALLBeerSelf
    {
        [JsonProperty("href")]
        public string allbeerSelfHref { get; set; }
    }

    public class CALLBeerLinks
    {
        [JsonProperty("beer")]
        public List<CAllBeers> allBeers { get; set; }

        [JsonProperty("next")]
        public CAllBeerNext allBeerNext { get; set; }

        [JsonProperty("page")]
        public List<CAllBeerPage> allBeerPage { get; set; }

        [JsonProperty("self")]
        public CAllBeersSelf allBeersSelf { get; set; }


    }

    public class CAllBeersSelf
    {
        [JsonProperty("href")]
        public string allBeerSelfHref { get; set; }

    }

    public class CAllBeerPage
    {
        [JsonProperty("href")]
        public string allBeerPageHref { get; set; }

        [JsonProperty("templated")]
        public bool allBeerPageTemplated { get; set; }
    }

    public class CAllBeerNext
    {
        [JsonProperty("href")]
        public string allBeerNext { get; set; }
    }
}
