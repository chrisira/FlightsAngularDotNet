import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FlightsService } from '../api/services/flights.service';
import { FlightsRm } from '../api/models'



@Component({
  selector: 'app-book-flight',
  templateUrl: './book-flight.component.html',
  styleUrls: ['./book-flight.component.css']
})
export class BookFlightComponent implements OnInit {
  flightId = "not loaded";
  flight: FlightsRm = {}


  constructor(private route: ActivatedRoute,private flightservice:FlightsService) {

  }
  

  ngOnInit(): void {
    this.route.paramMap.subscribe(p => this.findFlight(p.get("flightId")));

  }

  private findFlight = (flightId: string | null) => {
    this.flightId = flightId ?? "not passed";

    this.flightservice.findFlights$Json({ id: this.flightId }).subscribe(flight => this.flight = flight);
  };

}
