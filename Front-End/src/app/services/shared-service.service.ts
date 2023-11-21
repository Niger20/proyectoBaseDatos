import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedServiceService {

  private loggedIn = new BehaviorSubject<boolean>(false)

  private userRole = new BehaviorSubject<string>('');

  setUserRole(role: string) {
    this.userRole.next(role);
  }

  getUserRole() {
    return this.userRole.asObservable();
  }


  get isLoggedIn() {
    return this.loggedIn.asObservable();
  }

  login() {
    this.loggedIn.next(true);
  }

  logout() {
    this.loggedIn.next(false);
  }

}

