import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SuccessfulUserSignupData, UserDetails } from './User';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  private _url = 'http://localhost:5207';
  private isLoggedIn: boolean = false;
  private userDetails: UserDetails = {
    name: '',
    email: '',
    sessionToken: '',
    role: false
  };

  login(Email: string, Password: string) : Observable<any> {
    const loginData = {Email, Password};
    return this.http.post<UserDetails>(this._url + '/user/login', loginData);
  }

  userSignUp(Name: string, Email: string, Password: string): Observable<any> {
    const signupdata = {Name, Email, Password};
    console.log(signupdata, "signup data");
    return this.http.post<SuccessfulUserSignupData>(this._url + '/user/create', signupdata);
  }

  setIsLoggedIn (val: boolean){
    this.isLoggedIn = val;
    if (val == false) {
      localStorage.removeItem('userDetails');
    }
  }

  changePassword (Oldpassword: string, NewPassword: string) : Observable<any>{
    const Email = this.userDetails.email;
    const data = {Oldpassword, NewPassword, Email};

    const headers = new HttpHeaders({
      'Authorization': this.userDetails.sessionToken
    })
    return this.http.put(this._url + '/user/updatePassword', data, {headers});
  }

  getIsLoggedIn(): boolean {
    if (this.isLoggedIn) return true;

    let userDetails = localStorage.getItem('userDetails');
    
    if (userDetails){
      userDetails = JSON.parse(userDetails);
      this.http.post<UserDetails>(this._url + '/user/auth', userDetails)
        .subscribe(
          (response) => {
            localStorage.setItem('userDetails', JSON.stringify(response));
            this.setIsLoggedIn(true);
            this.setUserDetails(response);
            return true;
          },
          (error) => {
            localStorage.removeItem('userDetails');
            this.setIsLoggedIn(false);
            return false;
          }
          );
    } 
    return false;

  }

  setUserDetails(details: UserDetails) {
    this.userDetails = details;
  }

  getUserDetails(): UserDetails {
    return this.userDetails;
  }
}
