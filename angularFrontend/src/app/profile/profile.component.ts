import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../user.service';
import { UserDetails } from '../User';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent {
  userDetails: UserDetails = {
    name: '',
    email: '',
    sessionToken: '',
    role: false
  };
  oldPassword: string = '';
  newPassword: string = '';
  confirmNewPassword: string = '';
  errorMessage: string = '';

  constructor (private router: Router, private _userService: UserService) { }

  ngOnInit(){
    this._userService.getIsLoggedIn() ? '' :this.router.navigate(['/login']);
    this.userDetails = this._userService.getUserDetails();
  }
  
  ngDoUpdate(){
    this.userDetails = this._userService.getUserDetails();
    
  }

  onSubmit(): void {
    if (!this.oldPassword && !this.newPassword && !this.confirmNewPassword){
      this.errorMessage = "please fill all the fields";
    }

    if (this.newPassword != this.confirmNewPassword){
      this.errorMessage = "new passwords don't match";
    }

    this._userService.changePassword(this.oldPassword, this.newPassword)
      .subscribe(
        (response) => {
          this.newPassword = '';
          this.oldPassword = '';
          this.confirmNewPassword = '';
          this.errorMessage = response;
        },
        (error) => {
          this.errorMessage = error.error;
        }
      )
  }

}
