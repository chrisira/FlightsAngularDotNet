import { Component } from '@angular/core';

@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent {

  SearchResult: any = [
    'British Airlines',
    'Kenya Airways',
    'Rwandair',
    'Ethiopia Airways'
  ]

}
