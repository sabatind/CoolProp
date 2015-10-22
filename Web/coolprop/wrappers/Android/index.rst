.. _Android:

************
Android Wrapper
************

.. contents:: :depth: 2


User-Compiled Binaries
======================

Common Requirements
-------------------
Compilation of the Android wrapper requires a few :ref:`common wrapper pre-requisites <wrapper_common_prereqs>`

Linux & OSX
-----------

* Install NDK from here: https://developer.android.com/ndk/downloads/index.html

* Build the library:

    # Check out the sources for CoolProp
    git clone https://github.com/CoolProp/CoolProp --recursive
    # Move into the folder you just created
    mkdir -p CoolProp/build && cd CoolProp/build
    # Construct the makefile using CMake
    cmake .. -DCOOLPROP_ANDROID_MODULE=ON -DNDK_PATH=~/Downloads/android-ndk-r10e
    # Now actually do the build
    cmake --build
    
    
Example Code
======================

mainactivity.java::,

    package com.example.coolprop.androidexample;

    import android.support.v7.app.ActionBarActivity;
    import android.os.Bundle;
    import android.view.Menu;
    import android.view.MenuItem;
    import android.widget.TextView;


    public class MainActivity extends ActionBarActivity {
    
        static {
            System.loadLibrary("CoolProp");
        }
    
        @Override
        protected void onCreate(Bundle savedInstanceState) {
            super.onCreate(savedInstanceState);
            setContentView(R.layout.activity_main);
    
            TextView CoolPropsDisplay = (TextView) findViewById(R.id.CoolPropsDisplay);
            CoolPropsDisplay.setText(Double.toString(CoolProp.PropsSI("T", "P", 101300, "Q", 0, "Water")));
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
    }

activity_main.xml::,

    <RelativeLayout xmlns:android="http://schemas.android.com/apk/res/android"
        xmlns:tools="http://schemas.android.com/tools" android:layout_width="match_parent"
        android:layout_height="match_parent" android:paddingLeft="@dimen/activity_horizontal_margin"
        android:paddingRight="@dimen/activity_horizontal_margin"
        android:paddingTop="@dimen/activity_vertical_margin"
        android:paddingBottom="@dimen/activity_vertical_margin" tools:context=".MainActivity">
    
        <LinearLayout
            android:layout_width="fill_parent"
            android:layout_height="fill_parent"
            android:orientation="vertical">
    
            <TextView
                android:layout_width="fill_parent"
                android:layout_height="fill_parent"
                android:textSize="40dp"
                android:gravity="center"
                android:id="@+id/CoolPropsDisplay"
                android:layout_weight="1"
                android:textAlignment="gravity" />
    
        </LinearLayout>
    
    </RelativeLayout>
