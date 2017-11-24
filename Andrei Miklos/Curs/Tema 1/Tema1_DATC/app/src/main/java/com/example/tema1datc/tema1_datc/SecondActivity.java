package com.example.tema1datc.tema1_datc;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.TextView;

public class SecondActivity extends AppCompatActivity {


    public static TextView data1;

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_second);

        data1 = (TextView) findViewById(R.id.fetched_data1);
        fetchData process1 = new fetchData();
        process1.execute();
    }
}
