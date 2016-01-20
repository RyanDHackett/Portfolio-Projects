package com.example.ryan.quizgameathome;

import android.app.Activity;
import android.content.Intent;
import android.os.*;
import android.view.*;
import android.widget.*;
import android.view.View.OnClickListener;

public class MainActivity extends Activity {

    private TextView txtQuizTitle;
    private Button btnStartQuiz;
    private EditText txtName;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        btnStartQuiz = (Button)findViewById(R.id.btnStartQuiz);
        txtQuizTitle = (TextView)findViewById(R.id.txtQuizTitle);
        txtName = (EditText)findViewById(R.id.txtName);
        btnStartQuiz.setOnClickListener(btnQuizStartClickListener);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
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

    private OnClickListener btnQuizStartClickListener = new OnClickListener() {
        @Override
        public void onClick(View view) {
            Intent toQuiz = new Intent(MainActivity.this,QuizActivity.class);
            Bundle extras = new Bundle();
            extras.putString("playerName",txtName.getText().toString());
            toQuiz.putExtras(extras);
            startActivity(toQuiz);
        }
    };
}
