package com.example.tema1datc.tema1_datc;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

public class MainActivity extends AppCompatActivity {


    Button click,posteaza;

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        click = (Button) findViewById(R.id.button_c);
        posteaza = (Button)  findViewById(R.id.button_posteaza);
        posteaza.setVisibility(View.INVISIBLE);


        posteaza.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

            }
        });

        click.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent second = new Intent(MainActivity.this,SecondActivity.class);
                startActivity(second);

            }
        });
    }
}
