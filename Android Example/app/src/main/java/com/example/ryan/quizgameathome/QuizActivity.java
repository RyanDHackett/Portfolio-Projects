package com.example.ryan.quizgameathome;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Color;
import android.nfc.Tag;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.*;
import android.view.View.OnClickListener;

import java.io.BufferedReader;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.util.Collections;

public class QuizActivity extends Activity {
    private ArrayList<String> Questions;
    private ArrayList<String> Answers;
    private Map<String,String> qaKey;
    private int currentQuestion = 0;
    private int playerScore = 0;
    TextView txtQuestionNum;
    TextView txtCurrentScore;
    TextView txtQuestion;
    TextView txtQuizName;
    Button[] buttons;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_quiz);
        txtQuestionNum = (TextView)findViewById(R.id.txtQuestionNum);
        txtCurrentScore = (TextView)findViewById(R.id.txtCurrentScore);
        txtQuestion = (TextView)findViewById(R.id.txtQuestion);
        txtQuizName = (TextView)findViewById(R.id.txtQuizName);
        buttons = new Button[4];
        buttons[0] = (Button)findViewById(R.id.btnAnsOne);
        buttons[1] = (Button)findViewById(R.id.btnAnsTwo);
        buttons[2] = (Button)findViewById(R.id.btnAnsThree);
        buttons[3] = (Button)findViewById(R.id.btnAnsFour);
        for(final Button button:buttons)
        {
            button.setOnClickListener
                    (
                            new OnClickListener()
                            {
                                @Override
                                public void onClick(View v) {
                                    answerButtonClick(button);
                                };
                            }
                    );
        }
        Questions = new ArrayList<String>();
        Answers = new ArrayList<String>();
        qaKey = new HashMap<String,String>();
        loadQuizData();
        if(Questions == null)
        {
            Log.w("QuizActivity","No Questions in Quiz File");
            //toStartScreen("Quiz has no questions!");
        }
        if (Questions.size() < 4)
        {
            Log.w("QuizActivity","Quiz File contained less than 4 questions");
            // toStartScreen("Quiz must have at least four questions!");
        }
        //Data is ready, start the game!
        Bundle extras = getIntent().getExtras();
        if(extras!=null)
        {
            txtQuizName.setText("Test for: " + extras.getString("playerName"));
        }
        Collections.shuffle(Questions);
        Collections.shuffle(Answers);
        txtCurrentScore.setText("Score: " + String.valueOf(playerScore) + "/" + String.valueOf(Questions.size()));
        txtQuestionNum.setText("Question # " + String.valueOf(currentQuestion + 1));
        displayNextQuestion();
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_quiz, menu);
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
    public void loadQuizData()
    {
        String currentLine;
        try
        {
            InputStream is = this.getResources().openRawResource(R.raw.quiz);
            BufferedReader br = new BufferedReader(new InputStreamReader(is));
            while((currentLine = br.readLine())!=null)
            {
                String[] parts = currentLine.split(":");
                if (parts.length == 2)
                {
                    this.Questions.add(parts[0]);
                    this.Answers.add(parts[1]);
                    this.qaKey.put(parts[0], parts[1]);
                    this.qaKey.put(parts[1], parts[0]);
                }
            }
        }
        catch (IOException e)
        {
           // toStartScreen("Quiz File could not be read");
            Log.e("QuizActivity","Quiz File could not be read");
        }
    }

    int randomZeroToNum(int num)
    {
        return (int)(Math.random()*(num+1));
    }

//    int[] randomThreeUniqueFromRangeNotIncluding(int range,int dontInclude)
//    {
//        int[] nums = new int[3];
//        for(int x:nums)
//        {
//            nums[x] = -1;
//        }
//        int i = 0;//index for nums
//        while ( i !=4 )
//        {
//            int matchCount = 0;
//            nums[i] = (int)(Math.random()*range+1);//question number
//            for(int x:nums)
//            {
//                if(nums[i] ==  x)
//                {
//                    matchCount++;
//                }
//            }
//            if(matchCount == 1)
//            {
//                if(nums[i]!=dontInclude)
//                {
//                    i++;
//                }
//            }
//        }
//        return nums;
//    }

    void toStartScreen(String message)
    {
        //Toast.makeText(getBaseContext(),message, Toast.LENGTH_SHORT).show();
        Intent toStartScreen = new Intent(QuizActivity.this,MainActivity.class);
        startActivity(toStartScreen);
        //finish();
    }

//    int getNextQuestion()
//    {
//        boolean done = false;
//        int questionNum = -1;//if -1 is returned, there's a problem
//        while(!done)
//        {
//            questionNum = (int)(Math.random()*(this.Questions.size()-1));//roll question number
//            int matchCount = 0;
//            for(int i: previousQuestions)//check if that question number has been generated before
//            {
//                if(i == questionNum)
//                {
//                    matchCount++;
//                }
//            }
//            if(matchCount == 0)
//            {
//                done = true;
//            }
//        }
//        return questionNum;
//    }

    void displayNextQuestion()
    {
//        int[] wrongAnswers = randomThreeUniqueFromRangeNotIncluding(Questions.size(),currentQuestion);
        txtQuestion.setText(Questions.get(currentQuestion));
        String currentQuestionText = Questions.get(currentQuestion);//
        String theAnswer = qaKey.get(currentQuestionText);
        int putAnswerHere = randomZeroToNum(3);//get a number from 0-4
        int i = 0;//index for for each loop
        for(Button b:buttons)
        {
            String currentAnswer = Answers.get(i);//answer for this button
            try {
                if(currentAnswer.equals(theAnswer))
                {
                    i++;//don't print the answer!
                    currentAnswer = Answers.get(i);
                }
                b.setText(currentAnswer);//print the next answer
                i++;
            }
            catch(IndexOutOfBoundsException e){
                Log.e("QuizActivity", "Tried to access an answer that was out of bounds of the 'Answers' ArrayList");
            }
        }
        buttons[putAnswerHere].setText(theAnswer);
        currentQuestion++;
    }

    void answerButtonClick(Button button)
    {
        String currentQuestionText = (String)txtQuestion.getText();
        String clickedAnswer = (String)button.getText();
        String correctAnswer = qaKey.get(currentQuestionText);
        int test = 0;
        if (clickedAnswer.equals(correctAnswer)) {
            playerScore++;
            txtCurrentScore.setText("Score: " + String.valueOf(playerScore) + "/" + String.valueOf(Questions.size()));
            txtCurrentScore.setTextColor(Color.GREEN);
        }
        else {
            txtCurrentScore.setTextColor(Color.RED);
        }

        if (currentQuestion == Questions.size()) {
            Intent toResults = new Intent(QuizActivity.this, ResultsActivity.class);
            Bundle extras = new Bundle();
            extras.putString("playerScore", String.valueOf(playerScore));
            extras.putString("numberOfQuestions", String.valueOf(Questions.size()));
            toResults.putExtras(extras);
            startActivity(toResults);
        } else {
            Collections.shuffle(Answers);
            displayNextQuestion();
            txtQuestionNum.setText("Question # " + String.valueOf(currentQuestion));
        }
    }
}//end class QuizActivity
