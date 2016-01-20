package com.example.ryan.quizgameathome;

import android.app.Activity;
import android.graphics.Color;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.*;
import android.view.View.OnClickListener;
import android.content.Intent;


public class ResultsActivity extends Activity {
    TextView txtScoreNumeric;
    TextView txtScorePercent;
    Button btnStartOver;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_results);
        txtScoreNumeric = (TextView)findViewById(R.id.txtScoreNumeric);
        txtScorePercent = (TextView)findViewById(R.id.txtScorePercent);
        btnStartOver = (Button)findViewById(R.id.btnStartOver);
        btnStartOver.setOnClickListener(btnStartOverClickListener);
        Bundle extras = getIntent().getExtras();
        if(extras!=null)
        {
            double playerScore = Integer.parseInt(extras.getString("playerScore"));
            double totalQuestions = Integer.parseInt(extras.getString("numberOfQuestions"));
            double scorePercent = playerScore/totalQuestions*100;
            txtScoreNumeric.setText("You Scored: " + String.valueOf((int) playerScore) + "/" + String.valueOf((int) totalQuestions));
            if(scorePercent>=80)
            {
                txtScorePercent.setTextColor(Color.GREEN);
            }
            else if(scorePercent<80&&scorePercent>=60)
            {
                txtScorePercent.setTextColor(Color.YELLOW);
            }
            else
            {
                txtScorePercent.setTextColor(Color.RED);
            }
            txtScorePercent.setText(String.valueOf((int)scorePercent)+"%");
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_results, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    private OnClickListener btnStartOverClickListener = new OnClickListener() {
        @Override
        public void onClick(View view) {
            Intent toStart = new Intent(ResultsActivity.this,MainActivity.class);
            startActivity(toStart);
        }
    };
}
