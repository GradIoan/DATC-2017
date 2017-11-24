package com.example.tema1datc.tema1_datc;

import android.os.AsyncTask;
import android.util.Log;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

/**
 * Created by Andrei on 10/8/2017.
 */

public class fetchData extends AsyncTask<Void,Void,Void> {
    String data = "";
    String dataParsed = "";
    String test;
    String singleParsed = "";
    String singleParsed1 = "";
    String dataParsed1 = "";
    String singleParsed2 = "";
    String dataParsed2 = "";
    @Override
    protected Void doInBackground(Void... voids) {
        try {
            URL url = new URL("https://datc-rest.azurewebsites.net/breweries");
            HttpURLConnection httpURLConnection = (HttpURLConnection) url.openConnection();
            httpURLConnection.setRequestProperty("accept","application/hal+json");
            httpURLConnection.setRequestProperty("content-type","application/json");
            InputStream inputStream = httpURLConnection.getInputStream();
            BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
            String line = "";
            while(line != null) {
                line = bufferedReader.readLine();
                data = data + line;
            }

            JSONObject parentObject = new JSONObject(data); // obiectul intreg

            JSONObject links = parentObject.getJSONObject("_links");//ramura links din obiectul mare

            // pt obiectul brewery din links
            JSONArray breweryArray = links.getJSONArray("brewery");
            for(int i =0;i<breweryArray.length();i++){
                JSONObject breweryFinal = breweryArray.getJSONObject(i);
                singleParsed1 = "href:" + breweryFinal.getString("href")+"\n";
                dataParsed1 = dataParsed1+singleParsed1+"\n";

            }

            JSONObject embedded = parentObject.getJSONObject("_embedded");//ramura _embedded din obiectul mare

            // pt obiectul brewery din _embedded
            JSONArray embeddedArray = embedded.getJSONArray("brewery");
            for(int j =0;j<embeddedArray.length();j++){
                JSONObject embeddedFinal = embeddedArray.getJSONObject(j);

                singleParsed = "ID:" + embeddedFinal.getString("Id")+"\n"+
                        "Nume:"+embeddedFinal.getString("Name")+"\n";
                dataParsed = dataParsed+singleParsed+"\n";
            }

           // test = brewery1.toString();

        } catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } catch (JSONException e) {
            e.printStackTrace();
        }

        return null;
    }

    @Override
    protected void onPostExecute(Void aVoid) {
        super.onPostExecute(aVoid);
        //SecondActivity.data1.setText(this.test);
        SecondActivity.data1.setText(this.dataParsed);
    }
}
