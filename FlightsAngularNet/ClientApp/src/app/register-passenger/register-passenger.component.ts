import { Component, OnInit } from '@angular/core';
import { PassengerService } from '../api/services/passenger.service';
import { FormBuilder,Validators } from '@angular/forms'
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-passenger',
  templateUrl: './register-passenger.component.html',
  styleUrls: ['./register-passenger.component.css']
})
export class RegisterPassengerComponent implements OnInit {

  constructor(private passengerService: PassengerService, private fb: FormBuilder
  ,private authService:AuthService,private router:Router) {

  }
  form = this.fb.group({
    email: ['', Validators.compose([Validators.required, Validators.email, Validators.min(3), Validators.max(254)])],
    firstName: ['', Validators.compose([Validators.required, Validators.min(3), Validators.max(254)])],
    lastName: ['', Validators.compose([Validators.required, Validators.min(3), Validators.max(254)])],
    isFemale: [true, Validators.required]
  })
  ngOnInit(): void { }

  checkPassenger(): void {
    const params = { email: this.form.get('email')?.value! }
    this.passengerService.findPassenger$Json(params).subscribe(this.login, e => {
      if (e.status != 404) {
        console.error(e)
      }
    });
  }
        
    
  register() {
    if (this.form.invalid) {
      return;
    }
    
    //console.log("Form values :",this.form.value)
    this.passengerService.registerPassenger({ body: this.form.value })
      .subscribe(this.login, console.error);
  }

  login = () => {
    this.authService.loginUser({ email: this.form.get('email')?.value! })
    this.router.navigate(['/search-flight'])

  }

  get formEmail() {
    return this.form.controls.email;
  }
  get formFirstName() {
    return this.form.controls.firstName;
  }
  get formLastName() {
    return this.form.controls.lastName;
  }
  get formGender() {
    return this.form.controls.isFemale;
  }



}
