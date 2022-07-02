import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ErrorModel } from '../error-handlers/error.model';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.scss']
})
export class ErrorComponent implements OnInit {

  public title: string = '';
  public message: string = '';
  public code?: number;
  public stack: string = '';

  private _err!: ErrorModel;

  constructor(private _router: Router) {
    this._err = this._router.getCurrentNavigation()?.extras.state as ErrorModel;
  }

  ngOnInit(): void {
      this.title = this._err ? this._err.notification : 'No exception data.';
      this.message = this._err ? this._err.message : 'Unavailable';
      this.code = this._err ? this._err.code: 0;
      this.stack = this._err ? this._err.stack : 'Unavailable';
  }

  public goToHome = () => {
    const url: string = '';
    this._router.navigateByUrl(url);
  };
}