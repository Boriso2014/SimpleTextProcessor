import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { HttpErrorResponse, HttpClient, HttpHeaders } from '@angular/common/http';
import { TextModel } from './text.model';
import { FileModel } from './file.model';

@Injectable({
  providedIn: 'root'
})
export class TextService {

  constructor(private _http: HttpClient) { }

  public upload = (txtModel: TextModel): Observable<any> => {
    const uploadUrl = 'http://localhost:5036/api/texts/upload';
    return this._http.post<TextModel>(uploadUrl, txtModel);
  };

  public getFiles = (): Observable<FileModel> => {
    const getFilesUrl = 'http://localhost:5036/api/texts/files';
    return this._http.get<FileModel>(getFilesUrl);
  }

  public getFileContent = (name: string): Observable<string> => {
    const queryString = `name=${name}`;
    const getFileContentUrl = `http://localhost:5036/api/texts/file?${queryString}`;
    return this._http.get<string>(getFileContentUrl);
  }

  public deleteFile = (name: string): Observable<string> => {
    const queryString = `name=${name}`;
    const deleteFileUrl = `http://localhost:5036/api/texts/file?${queryString}`;
    return this._http.delete<string>(deleteFileUrl);
  }

  public test = (): Observable<any> => {
    const testUrl = 'http://localhost:5036/api/texts/test';
    return this._http.get(testUrl);
  }
}