import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../user.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  error: string = '';

  constructor(private router: Router, private _userService: UserService) {}

  onSubmit(): void {
    this._userService.login(this.email, this.password)
      .subscribe(
        async (response) => {
          localStorage.removeItem('userDetails');
          localStorage.setItem('userDetails', JSON.stringify(response));
          this._userService.setIsLoggedIn(true);
          this._userService.setUserDetails(response);
          this.router.navigate(['/']);
        },
        (error) => {
          this.error = error.error;
          localStorage.removeItem('userDetails');
        }
      )
    // Redirect or perform further actions based on authentication result
  }

  goToSignup(): void {
    this.router.navigate(['/signup']); // Redirect to signup page
  }

  ngDoCheck () {
    if (this._userService.getIsLoggedIn()){
      this.router.navigate(['../']);
    }
  }
}
