using System;
class Temperature{

    public string city;
    public double[] temperatures = new double[7];
    
    public Temperature(){
        city = "Białystok";
    }
    
    public Temperature(string city){
        this.city = city;
    }
    public Temperature(string city, double[] temperatures){
        this.city = city;
        this.temperatures = temperatures;
    }
    public void SetTemperature(int day, double temperature){
        temperatures[day] = temperature;
    }
}
class Program {
    static void Main() {
        Temperature t = new Temperature ();
        int[] tint = new int[10];
        string[] tstring = new string[10];
        
    }

}