package com.mkyong;

import java.io.BufferedReader;

import java.io.InputStreamReader;
import java.io.StringReader;
import java.net.URL;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.Scanner;
import java.util.Set;

import org.apache.http.Header;
import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import com.google.gson.Gson;
import com.google.gson.JsonObject;

@SuppressWarnings("deprecation")

public class Main {

	private final String USER_AGENT = "application/hal+json";

	public static void main(String[] args) throws Exception {
		

		int option = 0, status = 1, breweryChoose = 0, beerChoose = 0, linkOption = 0, styleLinkOption = 0, breweryLinkOption = 0;
		String newBeer = "";
		Scanner input = new Scanner(System.in);

		Main http = new Main();

		System.out.println("***************************Welcome***************************");

		while (status == 1) {

			System.out.println("\nChoose an option from the menu\n");
			System.out.println("	1. Navigation");
			System.out.println("	2. Add a beer");
			System.out.println("	3. Exit the application");
			System.out.print("\nYour choice: ");
			option = input.nextInt();

			switch (option) {

			case 1:
				System.out.println("\nChoose which brewery you want to explore");
				System.out.println("\n  The list of brewerys is:\n");
				String url = "http://datc-rest.azurewebsites.net/breweries";
				String json = http.sendGet(url);
				JSONParser jsonParser = new JSONParser();
				JSONObject jsonObject = (JSONObject) jsonParser.parse(json);
				JSONObject structure = (JSONObject) jsonObject.get("_embedded");
				Gson gson = new Gson();
				Page page = gson.fromJson(structure.toString(), Page.class);
				for (Brewery resource : page.brewery)
					System.out.println("    " + resource.Id + "." + " " + resource.Name);
				System.out.print("\nYour choice: ");
				breweryChoose = input.nextInt();
				String breweryUrl = "http://datc-rest.azurewebsites.net/breweries/" + breweryChoose + "/beers/";
				String beersJSON = http.sendGet(breweryUrl);
				JSONParser beersJSONParser = new JSONParser();
				JSONObject beersJSONObject = (JSONObject) beersJSONParser.parse(beersJSON);
				JSONObject beersStructure = (JSONObject) beersJSONObject.get("_embedded");
				BreweryBeers beers = gson.fromJson(beersStructure.toString(), BreweryBeers.class);
				System.out.println("\nAt this brewery we have these beers:\n");
				for (Beer listedBeer : beers.beer)
					System.out.println("    " + listedBeer.Id + "." + " " + listedBeer.Name);

				System.out.println(
						"\nChoose which beer you want to explore \nPlease enter the ID of the beer to explore it");
				System.out.print("\nYour choice: ");
				beerChoose = input.nextInt();
				String beerUrl = "http://datc-rest.azurewebsites.net/beers/" + beerChoose;
				String beerJSON = http.sendGet(beerUrl);
				MatchedBeer listedBeer = gson.fromJson(beerJSON, MatchedBeer.class);
				System.out.println("\nDetails about the beer:");
				System.out.println("\n	Beer name:" + "    " + listedBeer.Name);
				System.out.println("	Brewery name:" + " " + listedBeer.BreweryName);
				System.out.println("	Brewery ID:" + "   " + listedBeer.Id);
				System.out.println("	Style:" + "        " + listedBeer.StyleName);
				System.out.println("	Style ID:" + "     " + listedBeer.StyleId);
				JSONParser beersJSONParser1 = new JSONParser();
				JSONObject beersJSONObject1 = (JSONObject) beersJSONParser1.parse(beerJSON);
				JSONObject listedBeerLinks = (JSONObject) beersJSONObject1.get("_links");
				JSONObject style = (JSONObject) listedBeerLinks.get("style");
				StyleLink listedBeerLinksbeer = gson.fromJson(style.toString(), StyleLink.class);
				System.out.println("	Style link:"+"   " + listedBeerLinksbeer.href);
				System.out.println("\nDo you want to open the Style link?\nChoose: 1 - Yes 0 - No");
				System.out.print("\nYour choice: ");
				linkOption = input.nextInt();
				if(linkOption == 1){
					String styleLink = "http://datc-rest.azurewebsites.net" + listedBeerLinksbeer.href;	
					String styleJSONLink = http.sendGet(styleLink);
					Style styleJSON = gson.fromJson(styleJSONLink, Style.class);
					System.out.println("\nThis is the style link opened:\n");
					System.out.println("	Style ID:" + "     " + styleJSON.Id);
					System.out.println("	Style:" + "        " + styleJSON.Name);
					JSONObject styleBeerLink = (JSONObject) beersJSONParser1.parse(styleJSONLink);
					JSONObject styleLinkLinks = (JSONObject) styleBeerLink.get("_links");
					JSONObject styleBeersLink = (JSONObject) styleLinkLinks.get("beers");
					Beers beersLink = gson.fromJson(styleBeersLink.toString(), Beers.class);
					System.out.println("	Beers link:"+"   " + beersLink.href);
					System.out.println("\nDo you want to open the beers link inside this style?\nChoose: 1 - Yes 0 - No");
					System.out.print("\nYour choice: ");
					styleLinkOption = input.nextInt();
					if(styleLinkOption == 1){
						String beersStyleLink = "http://datc-rest.azurewebsites.net" + beersLink.href;
						String beersStyleLinkJSON = http.sendGet(beersStyleLink);
						JSONObject beersStyleJSON = (JSONObject) beersJSONParser.parse(beersStyleLinkJSON);
						JSONObject beersLinkStructure = (JSONObject) beersStyleJSON.get("_embedded");
						BreweryBeers styleBeersLinkJSON = gson.fromJson(beersLinkStructure.toString(), BreweryBeers.class);
						System.out.println("\nIn this style we have this beers:\n");
						for (Beer styleListedBeer : styleBeersLinkJSON.beer){
							System.out.println("    " + "Beer name:"+ "    " + styleListedBeer.Id + "." + " " + styleListedBeer.Name + 
									"\n" + "    Brewery Id:" +  "   " + styleListedBeer.BreweryId + 
									"\n" + "    Brewery Name:" +  " " + styleListedBeer.BreweryName +
									"\n" + "    Style Id:" +  "     " + styleListedBeer.StyleId + 
									"\n" + "    Style Name:" +  "   " + styleListedBeer.StyleName + "\n");
						}
						
						JSONArray beer = (JSONArray) beersLinkStructure.get("beer");
						System.out.println("This are the links from the breweries that these beers come from:\n");
						JSONObject beersLinkStructure1 = (JSONObject) beersStyleJSON.get("_embedded");
						 JSONArray beerBeer = (JSONArray) beersLinkStructure1.get("beer");
			                Iterator j = beerBeer.iterator();
							Iterator i = beer.iterator();
						while (i.hasNext() && j.hasNext()) {
							                JSONObject innerObj = (JSONObject) i.next();
							                JSONObject linksInnerObj = (JSONObject) innerObj.get("_links");
							                JSONObject breweryInnerObj = (JSONObject) linksInnerObj.get("brewery");
							                JSONObject syleIdInerObj = (JSONObject) j.next();
							                Beer styleListedBrewery = gson.fromJson(syleIdInerObj.toString(), Beer.class);
							                BreweryLink brewery = gson.fromJson(breweryInnerObj.toString(), BreweryLink.class);
											System.out.println("    Link:"+"   "+ styleListedBrewery.BreweryId +". "+ brewery.href);
						} 
						System.out.println("\nPlease choose a brewery to explore.");
						System.out.println("Please enter the ID from the link so you can explore the brewery.");
						System.out.print("\nYour choice: ");
						breweryLinkOption = input.nextInt();
						String breweryLink = "http://datc-rest.azurewebsites.net/breweries/" + breweryLinkOption;
						String breweryJson = http.sendGet(breweryLink);
						Brewery brewery = gson.fromJson(breweryJson.toString(), Brewery.class);
						System.out.println("\nThis is the ID and the name of the brewery you wanted to explore:\n");
						System.out.println("    " + brewery.Id + "." + " " + brewery.Name);
						System.out.println("\nThank you for navigation through this application!!!");
					}
				}
				break;
			case 2:
				String newBeerURl = "http://datc-rest.azurewebsites.net/beers";
				System.out.println("\nEnter the name of the beer you want to add");
				System.out.print("\n    New beer: ");
				newBeer = input.next();
				String newBeerLocation = http.sendPost(newBeerURl, newBeer);
				System.out.println("\nYour new beer has been added successfully!\n");
				System.out.print("   The link of your new beer is: ");
				System.out.print(newBeerLocation + "\n");
				break;
			default:
				System.out.println("\n     GOODBYE");
				System.exit(option);
				break;
			}
			System.out.println("\nDo you want to use another option?");
			System.out.println("Choose: 1 - Yes 0 - No\n");
			System.out.print("Your choice: ");
			status = input.nextInt();
			if (status == 1) {

			} else {
				System.out.println("\n     GOODBYE");
			}

		}
	}

	// HTTP POST request
	private String sendPost(String newBeerURl, String newBeer) throws Exception {

		HttpClient client = new DefaultHttpClient();
		HttpPost post = new HttpPost(newBeerURl);

		// add header
		post.setHeader("Accept", USER_AGENT);

		List<NameValuePair> urlParameters = new ArrayList<NameValuePair>();
		urlParameters.add(new BasicNameValuePair("Name", newBeer));

		post.setEntity(new UrlEncodedFormEntity(urlParameters));
		HttpResponse response = client.execute(post);
		// response.getAllHeaders();
		return response.getFirstHeader("Location").toString();
	}

	// HTTP GET request
	private String sendGet(String url) throws Exception {

		HttpClient client = new DefaultHttpClient();
		HttpGet request = new HttpGet(url);

		// add request header
		request.addHeader("Accept", USER_AGENT);
		HttpResponse response = client.execute(request);
		BufferedReader rd = new BufferedReader(new InputStreamReader(response.getEntity().getContent()));
		StringBuffer result = new StringBuffer();
		String line = "";
		while ((line = rd.readLine()) != null) {
			result.append(line);
		}
		return result.toString();
	}

	// JSON types
	// Brewery type
	static class Brewery {
		String Id;
		String Name;
		List<Links> links;
	}
	static class Links {
		List<Self> self;
		List<Beers> beers;
	}
	static class Page {
		List<Brewery> brewery;
	}
	static class Self {
		String href;
	}
	static class Beers {
		String href;
	}

	// Type for the beers on the brewery
	static class BreweryBeers {
		List<Beer> beer;
	}
	static class Beer {
		String Id;
		String Name;
		String BreweryId;
		String BreweryName;
		String StyleId;
		String StyleName;
		List<BeerLinks> BeerLinks;
	}
	static class BeerLinks {
		List<BreweryLink> brewery;
		List<Self> self;
		List<StyleLink> style;
		
	}
	static class StyleLink {
		String href;
	}
	static class BreweryLink {
		String href;
	}

	// Type for the matched beer details
	static class MatchedBeer {
		String Id;
		String Name;
		String BreweryId;
		String BreweryName;
		String StyleId;
		String StyleName;
	}
	static class MatchedBeerLinks {
		List<Review> review;
		List <BreweryLink> breweryLink;
		List<Self> self;
		List <StyleLink> style;
	}
	static class Review {
		String templated;
	}
	static class Style {
		String Id;
		String Name;
		List<Links> links;
	}
}