import { Injectable } from '@angular/core';
import { Observable, throwError, lastValueFrom, firstValueFrom } from 'rxjs';
import { HttpErrorResponse, HttpClient, HttpHeaders } from '@angular/common/http';
import { TransferTextModel } from './transfer-text.model';
import { FileModel } from './file.model';

@Injectable({
  providedIn: 'root'
})
export class TextService {

  constructor(private _http: HttpClient) { }

  public upload (txtModel: TransferTextModel): Observable<TransferTextModel> {
    const uploadUrl = 'http://localhost:5036/api/texts/upload';
    return this._http.post<TransferTextModel>(uploadUrl, txtModel);
  }

  public getFiles = (): Observable<FileModel> => {
    const getFilesUrl = 'http://localhost:5036/api/texts/files';
    return this._http.get<FileModel>(getFilesUrl);
  }

  // public getFileContent = (name: string): Observable<string> => {
  //   const queryString = `name=${name}`;
  //   const getFileContentUrl = `http://localhost:5036/api/texts/file?${queryString}`;
  //   return this._http.get<string>(getFileContentUrl);
  // }

  public download(name: string, start: number, size: number): Observable<TransferTextModel> {
    const downloadUrl = `http://localhost:5036/api/texts/download/${name}/start/${start}/chunk-size/${size}`;
    return this._http.get<TransferTextModel>(downloadUrl);
  }

  public deleteFile = (name: string): Observable<string> => {
    const queryString = `name=${name}`;
    const deleteFileUrl = `http://localhost:5036/api/texts/file?${queryString}`;
    return this._http.delete<string>(deleteFileUrl);
  }
}