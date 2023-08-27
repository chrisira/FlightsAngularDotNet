import { Component, OnInit } from '@angular/core';
import { BookingRm } from '../api/models';
import { BookingService } from '../api/services';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-my-bookings',
  templateUrl: './my-bookings.component.html',
  styleUrls: ['./my-bookings.component.css']
})
export class MyBookingsComponent implements OnInit {

  bookings!: BookingRm[]
  constructor(private bookingService:BookingService,private authService : AuthService) {

  }


  ngOnInit(): void{
    this.bookingService.listBooking$Json({
      email: this.authService.
      currentUser?.email ?? ''
    }).subscribe(r => this.bookings = r)

  }

}
