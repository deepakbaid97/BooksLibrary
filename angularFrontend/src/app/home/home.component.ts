import { Component } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { UserService } from '../user.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterOutlet, RouterLink, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  showCreateBookPage: boolean = false;
  error: string = '';
  constructor(private _userService: UserService, private router: Router) { }

  ngOnInit() {
    if (this._userService.getUserDetails().role) {
      this.showCreateBookPage = true
    }
  }

  logout() {
    this._userService.setIsLoggedIn(false);
    this.router.navigate(['/login']);
  }
}
