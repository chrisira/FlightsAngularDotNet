import { Component } from '@angular/core';
//importing the flight services from the api
import { FlightsService } from './../api/services/flights.service'

// importing the Flights Read Model
import { FlightsRm } from '../api/models';

@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent {

  SearchResult: FlightsRm[] = []

  // adding a constructor

  constructor(private flightsService:FlightsService) {

  }

  search() {
    this.flightsService.searchFlights$Json({}).subscribe(response => this.SearchResult = response, this.HandleError);
    
  }
  private HandleError(err: any) {
    console.log(err)
  }

}
