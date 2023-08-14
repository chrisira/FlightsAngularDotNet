import { Component, OnInit } from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { FlightsService } from '../api/services/flights.service';
import { BookDto, FlightsRm } from '../api/models'
import { AuthService } from '../auth/auth.service';
import { FormBuilder,Validators } from '@angular/forms';



@Component({
  selector: 'app-book-flight',
  templateUrl: './book-flight.component.html',
  styleUrls: ['./book-flight.component.css']
})
export class BookFlightComponent implements OnInit {
  flightId = "not loaded";
  flight: FlightsRm = {}


  constructor(private route: ActivatedRoute, private flightservice: FlightsService, private router: Router
    , private authService: AuthService, private fb: FormBuilder) {

  }

  form = this.fb.group({
    number: [1, Validators.compose([Validators.required, Validators.min(1), Validators.max(254)])]
  })
  

  ngOnInit(): void {
    if (!this.authService.currentUser) {
      this.router.navigate(['/register-passenger']);
    }
    this.route.paramMap.subscribe(p => this.findFlight(p.get("flightId")));

  }

  private findFlight = (flightId: string | null) => {
    this.flightId = flightId ?? "not passed";

    this.flightservice.findFlights$Json({ id: this.flightId }).subscribe(flight => this.flight = flight, this.HandleError);
  };

  private HandleError = (err: any) => {
    if (err.status == 404 || err.status==400) {
      alert("The flight is not found!");
      this.router.navigate(['/search-flight'])
    }
    console.log("error status code " + err.status);
    console.log("error status Message " + err.statusText);
    console.log(err)
  }
  book() {
    if (this.form.invalid) {
      return;
    }
    console.log(`booking ${this.form.get('number')?.value} passengers for the flight ${this.flightId}`)


    const booking: BookDto = {
      flightId: this.flight.id,
      passengerEmail: this.authService.currentUser?.email,
      numberOfSeats: this.form.get('number')?.value!
    }
    this.flightservice.bookFlights({ body: booking }).subscribe(_ => this.router.navigate(["/my-booking"]), this.HandleError)
    
  }

  // creating a getter for getting the number

  get number() {
    return this.form.controls.number;
  }

}
