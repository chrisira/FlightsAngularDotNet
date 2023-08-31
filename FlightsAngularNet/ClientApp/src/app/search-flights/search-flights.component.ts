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
    from : [''],
    destination: [''],
    fromDate: [''],
    toDate: [''],
    numberOfPassengers:[1]

  })
  ngOnInit(): void {
    this.search()

  }


  search() {
    const searchFormValue = this.searchForm.value;

    const searchParams = {
      FromDate: searchFormValue.fromDate || '', // Provide a default empty string if it's null or undefined
      ToDate: searchFormValue.toDate || '',
      From: searchFormValue.from || '',
      Destination: searchFormValue.destination || '',
      NumberOfPassengers: searchFormValue.numberOfPassengers || 1 // Provide a default value (e.g., 0) if it's null or undefined
    };

    this.flightsService.searchFlights$Json(searchParams)
      .subscribe(response => this.SearchResult = response, this.HandleError);
  }
  private HandleError(err: any) {
    console.log("error status code " + err.status);
    console.log("error status Message " + err.statusText);
    console.log(err)
  }

}
