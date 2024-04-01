import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../user.service';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.scss'
})
export class SignupComponent {
  name: string = '';
  email: string = '';
  password: string = '';
  error: string = '';

  constructor(private router: Router, private _userService: UserService) { }

  onSubmit(): void {
    // Add your signup logic here (e.g., create a new user with name, email, and password)
    console.log('Name:', this.name);
    console.log('Email:', this.email);
    console.log('Password:', this.password);
    // Redirect or perform further actions after signup

    this._userService.userSignUp(this.name, this.email, this.password)
      .subscribe(
        (response) => {
            console.log(response, "SIGUP RESPONSE");
            this.error = 'User Successfully Created!';
        },
        (error) => {
          this.error = error.error;
          console.log(error, "ERROR");
        }
      )
  }
}
