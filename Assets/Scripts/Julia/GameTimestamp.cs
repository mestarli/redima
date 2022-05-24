using System;

[Serializable]

public class GameTimestamp
{
    // Variables
    public int year;
    
    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter
    }

    public Season season;

    public enum DayOfTheWeek
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }
    
    public int day;
    public int hour;
    public int minute;

    public GameTimestamp(int year, Season season, int day, int hour, int minute)
    {
        this.year = year;
        this.season = season;
        this.day = day;
        this.hour = hour;
        this.minute = minute;
    }

    public void UpdateClock()
    {
        minute++;

        // 60 minutos en 1 hora
        if (minute >= 60)
        {
            // Reseteamos los minutos y aumentamos las horas
            minute = 0;
            hour++;
        }
        
        // 24 horas en 1 dia
        if (hour >= 24)
        {
            // Reseteamos las horas y aumentamos los dias
            hour = 0;
            day++;
        }

        if (day > 30)
        {
            // Reseteamos los dias y aumentamos los "meses"
            day = 1;
            season++;

            // Si estamos en la estacion final, se resetea y se cambia a spring
            if (season == Season.Winter)
            {
                season = Season.Spring;
                
                // Empieza un nuevo año
                year++;
            }
            else
            {
                season++;
            }
        }
    }

    // Aumenta el tiempo en 1 minuto
    public DayOfTheWeek GetDayOfTheWeek()
    {
        // Conversion total del tiempo transcurrido a dias
        int daysPased = YearsToDays(year) + SeasonsToDays(season) + day;
        
        // Resta despues de dividir los dias ya transcurridos
        int dayIndex = daysPased % 7;

        return (DayOfTheWeek) dayIndex;
    }
    
    // Conversión de horas a minutos
    public static int HoursToMinutes(int hour)
    {
        // 60 minutos = 1 hora
        return hour * 60;
    }
    
    // Conversión de dias a horas
    public static int DaysToHours(int days)
    {
        // 24 horas = 1 dia
        return days * 24;
    }
    
    // Conversión de Seasons a dias
    public static int SeasonsToDays(Season season)
    {
        int seasonIndex = (int) season;
        return seasonIndex * 30;
    }
    
    // Conversión de años a dias
    public static int YearsToDays(int years)
    {
        return years * 4 * 30;
    }
}
