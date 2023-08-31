import { Component } from '@angular/core';
//importing the flight services from the api
import { FlightsService } from './../api/services/flights.service'

// importing the Flights Read Model
import { FlightsRm } from '../api/models';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent {

  SearchResult: FlightsRm[] = []

  // adding a constructor

  constructor(private flightsService: FlightsService, private fb: FormBuilder) {

  }
  searchForm = this.fb.group({
    source: [''],
    destination: [''],
    fromDate: [''],
    toDate: [''],
    numberOfPassengers:[1]

  })

  search() {
    this.flightsService.searchFlights$Json({}).subscribe(response => this.SearchResult = response, this.HandleError);
    
  }
  private HandleError(err: any) {
    console.log("error status code " + err.status);
    console.log("error status Message " + err.statusText);
    console.log(err)
  }

}
