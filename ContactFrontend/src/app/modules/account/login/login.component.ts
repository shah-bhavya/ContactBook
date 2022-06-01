import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/core/services/account/account.service';
import { AuthService } from 'src/app/core/services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  submitted = false;
  isLoggedIn = false;
  constructor(private fb: FormBuilder,
    private accountService: AccountService,
    private route: Router,
    private authService: AuthService) { }

  ngOnInit(): void {

    if(this.authService.isLoggedIn())
      this.isLoggedIn = true;

    this.loginForm = this.fb.group({
      password: ['', [Validators.required, Validators.minLength(6)]],
      email: ['', [Validators.email]],
    });
  }

  get f() {
    return this.loginForm.controls;
  }

  submitForm() {
    this.accountService.Login(this.loginForm.value).subscribe(data => {
      localStorage.setItem('token', data.token);
      this.route.navigate(['']);
    });
  }

}
