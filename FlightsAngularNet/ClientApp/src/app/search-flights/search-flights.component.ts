import { Component } from '@angular/core';

@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent {

  SearchResult: FlightRm[] = [
    {
      airline: 'American Airlines',
      NumberOfRemainingSeats: 500,
      departure: { time: Date.now().toString(), place: 'Los Angeles' },
      arrival: { time: Date.now().toString(), place: 'Istanbul' },
      price: "350"
    },
    {
      airline: 'Rwandair',
      NumberOfRemainingSeats: 300,
      departure: { time: Date.now().toString(), place: 'Kigali' },
      arrival: { time: Date.now().toString(), place: 'Kenya' },
      price: "1200"
    },
    {
      airline: 'Kenya Airways',
      NumberOfRemainingSeats: 230,
      departure: { time: Date.now().toString(), place: 'Nairobi' },
      arrival: { time: Date.now().toString(), place: 'Kigali' },
      price: "1250"
    }
    
  ]

}

export interface FlightRm {
  airline: string;
  arrival: TimeplaceRm;
  departure: TimeplaceRm;
  price: string;
  NumberOfRemainingSeats: number;


}
export interface TimeplaceRm {
  place: string;
  time: string;
}
