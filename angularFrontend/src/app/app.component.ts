import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { UserService } from './user.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule, HttpClientModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  constructor (private router: Router, private _userService: UserService) {}

  ngOnInit(){
    this._userService.getIsLoggedIn() ? '' :this.router.navigate(['/login']);
  }
  title = 'angularFrontend';
}
