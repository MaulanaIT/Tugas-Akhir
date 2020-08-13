using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockController : MonoBehaviour {

    public static ClockController 
        clockController;

    public Text 
        textDate, 
        textDay, 
        textHours, 
        textMinutes, 
        //textPeriods, 
        textMonth;

    public float 
        timeCooldown;

    public string 
        month, 
        day;

    public int 
        date, 
        hour, 
        minute;

    private void Awake() {
        if (clockController == null) {
            clockController = this;
        } else if (clockController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        day = textDay.text;
        month = textMonth.text;
        GameController.gameController.clock.day = textDay.text;
        GameController.gameController.clock.month = textMonth.text;
    }

    void Update() {
        timeCooldown -= Time.deltaTime;

        if (timeCooldown <= 0) {
            minute += 10;
            timeCooldown = 1f;
        }

        if (minute >= 60) {
            hour++;
            minute = 0;
        }

        if (hour >= 23) {
            switch (day) {
                case "SEN":
                    day = "SEL";
                    break;
                case "SEL":
                    day = "RAB";
                    break;
                case "RAB":
                    day = "KAM";
                    break;
                case "KAM":
                    day = "JUM";
                    break;
                case "JUM":
                    day = "SAB";
                    break;
                case "SAB":
                    day = "MIN";
                    break;
                case "MIN":
                    day = "SEN";
                    break;
            }

            textDay.text = day;

            if (minute >= 0) {
                hour = 0;
            }
        }

        if (date > 30) {
            switch (month) {
                case "JAN":
                    month = "FEB";
                    break;
                case "FEB":
                    month = "MAR";
                    break;
                case "MAR":
                    month = "APR";
                    break;
                case "APR":
                    month = "MEI";
                    break;
                case "MEI":
                    month = "JUN";
                    break;
                case "JUN":
                    month = "JUL";
                    break;
                case "JUL":
                    month = "AGU";
                    break;
                case "AGU":
                    month = "SEP";
                    break;
                case "SEP":
                    month = "OKT";
                    break;
                case "OKT":
                    month = "NOV";
                    break;
                case "NOV":
                    month = "DES";
                    break;
                case "DES":
                    month = "JAN";
                    break;
            }

            date = 1;
            textMonth.text = month;
        }

        if (date < 10) {
            textDate.text = "0" + date;
        } else {
            textDate.text = "" + date;
        }

        if (hour < 10) {
            textHours.text = "0" + hour;
        } else {
            textHours.text = "" + hour;
        }

        if (minute == 0) {
            textMinutes.text = "0" + minute;
        } else {
            textMinutes.text = "" + minute;
        }

        GameController.gameController.clock.day = day;
        GameController.gameController.clock.month = month;
        GameController.gameController.clock.hours = hour;
        GameController.gameController.clock.minutes = minute;
        GameController.gameController.clock.date = date;
    }
}
