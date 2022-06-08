import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { HttpErrorResponse, HttpClient } from '@angular/common/http';
import { TextModel } from './text.model';

@Injectable({
  providedIn: 'root'
})
export class TextService {

  constructor(private _http: HttpClient) { }

  public upload = (txtModel: TextModel): Observable<any> => {
    const uploadUrl = 'localhost:1000/api/upload';
    return this._http.post<TextModel>(uploadUrl, txtModel);
  };
}